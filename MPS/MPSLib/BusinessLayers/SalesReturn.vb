Namespace BL

    Public Class SalesReturn

#Region "Main"

        Public Shared Function ListData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.SalesReturn.ListData(dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SR" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-"
            strReturn = strReturn & Format(DL.SalesReturn.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.SalesReturn, ByVal clsDataDetail() As VO.SalesReturnDet) As String
            BL.Server.ServerDefault()
            Dim dtPreviousItem As New DataTable, dtListSalesID As New DataTable
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID)
                    If DL.SalesReturn.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.SalesReturnDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    If DL.SalesReturn.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf DL.SalesReturn.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If

                    dtPreviousItem = DL.SalesReturn.ListDataDetail(clsData.ID)
                    dtListSalesID = DL.SalesReturn.ListDataSalesID(clsData.ID)
                    DL.SalesReturn.DeleteDataDetail(clsData.ID)

                    '# Revert Return Qty
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Sales.CalculateReturnQty(dr.Item("SalesDetID"))
                    Next

                    '# Revert Total Return
                    For Each dr As DataRow In dtListSalesID.Rows
                        DL.Sales.UpdateTotalReturn(dr.Item("SalesID"))
                    Next
                End If

                DL.SalesReturn.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.SalesReturnDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.SalesReturn.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.SalesReturnID = clsData.ID
                    DL.SalesReturn.SaveDataDetail(clsDetail)

                    '# Calculate
                    DL.Sales.CalculateReturnQty(clsDetail.SalesDetID)
                Next

                '# Calculate Total Return
                dtListSalesID = DL.SalesReturn.ListDataSalesID(clsData.ID)
                For Each dr As DataRow In dtListSalesID.Rows
                    DL.Sales.UpdateTotalReturn(dr.Item("SalesID"))
                Next

                '# Save Data Status
                SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.SalesReturn
            BL.Server.ServerDefault()
            Return DL.SalesReturn.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.SalesReturn)
            Dim dtPreviousItem As New DataTable, dtListSalesID As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.SalesReturn.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.SalesReturn.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    dtListSalesID = DL.SalesReturn.ListDataSalesID(clsData.ID)

                    DL.SalesReturn.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Revert Return Qty
                    dtPreviousItem = DL.SalesReturn.ListDataDetail(clsData.ID)
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Sales.CalculateReturnQty(dr.Item("SalesDetID"))
                    Next

                    '# Revert Total Return
                    For Each dr As DataRow In dtListSalesID.Rows
                        DL.Sales.UpdateTotalReturn(dr.Item("SalesID"))
                    Next

                    '# Delete Journal
                    Dim clsJournal As VO.Journal = New VO.Journal
                    clsJournal.CompanyID = clsData.CompanyID
                    clsJournal.ID = clsData.JournalID
                    clsJournal.ReferencesID = clsData.ID
                    clsJournal.JournalDate = clsData.SalesReturnDate
                    clsJournal.TotalAmount = clsData.SubTotal
                    clsJournal.IsAutoGenerate = True
                    clsJournal.IDStatus = clsData.IDStatus
                    clsJournal.Remarks = clsData.Remarks
                    clsJournal.LogBy = clsData.LogBy

                    BL.Journal.DeleteData(clsJournal)
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub PostData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.SalesReturn.ListDataOutstandingPostGL(dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtData.Rows
                Dim dtItem As DataTable = DL.SalesReturn.ListDataDetail(dr.Item("ID"))
                Dim decCOGS As Decimal = BL.COGS.RevertCalculateCOGS(dtItem, dr.Item("ID"), dr.Item("SalesReturnDate"))

                '# Generate Journal
                Dim clsJournal As VO.Journal = New VO.Journal
                clsJournal.CompanyID = dr.Item("CompanyID")
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("SalesReturnDate")
                clsJournal.TotalAmount = dr.Item("SubTotal")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = dr.Item("Remarks")
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(4) As VO.JournalDet

                '# Revenue 
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofRevenue
                clsJournalDet.DebitAmount = dr.Item("SubTotal")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(0) = clsJournalDet

                '# Account Receivable
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountReceivable
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("GrandTotal")
                clsJournalDetAll(1) = clsJournalDet

                '# Sales Discount
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofSalesDisc
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofSalesDisc
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("TotalDiscount")
                clsJournalDetAll(2) = clsJournalDet

                '# Stock
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
                clsJournalDet.DebitAmount = decCOGS
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(3) = clsJournalDet

                '# COGS
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofCOGS
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofCOGS
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = decCOGS
                clsJournalDetAll(4) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
                '# End Of Generate Journal

                '#Update Journal ID
                If strJournalID.Trim <> "" Then DL.SalesReturn.UpdateJournalID(dr.Item("ID"), strJournalID)

                DL.SalesReturn.PostGL(dr.Item("ID"), UI.usUserApp.UserID)
            Next
        End Sub

        Public Shared Sub UnpostData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.SalesReturn.ListData(dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                DL.StockIN.DeleteDataByReferencesID(dr.Item("ID"))

                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '#Update Journal ID
                DL.SalesReturn.UpdateJournalID(dr.Item("ID"), "")
                DL.SalesReturn.UnpostGL(dr.Item("ID"))
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strSalesReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.SalesReturn.ListDataDetail(strSalesReturnID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.SalesReturn.ListDataStatus(strSalesReturnID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strSalesReturnID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.SalesReturnStatus
            clsData.ID = strSalesReturnID & "-" & Format(DL.SalesReturn.GetMaxIDStatus(strSalesReturnID), "000")
            clsData.SalesReturnID = strSalesReturnID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.SalesReturn.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace