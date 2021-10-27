Namespace BL
    Public Class ReceiveReturn

#Region "Main"

        Public Shared Function ListData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.ReceiveReturn.ListData(dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "RR" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-"
            strReturn = strReturn & Format(DL.ReceiveReturn.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.ReceiveReturn, ByVal clsDataDetail() As VO.ReceiveReturnDet) As String
            BL.Server.ServerDefault()
            Dim dtPreviousItem As New DataTable, dtListReceiveID As New DataTable
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID)
                    If DL.ReceiveReturn.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.ReceiveReturnDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    If DL.ReceiveReturn.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf DL.ReceiveReturn.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If

                    dtPreviousItem = DL.ReceiveReturn.ListDataDetail(clsData.ID)
                    dtListReceiveID = DL.ReceiveReturn.ListDataReceiveID(clsData.ID)
                    DL.ReceiveReturn.DeleteDataDetail(clsData.ID)

                    '# Revert Return Qty
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Receive.CalculateReturnQty(dr.Item("ReceiveDetID"))
                    Next

                    '# Revert Total Return
                    For Each dr As DataRow In dtListReceiveID.Rows
                        DL.Receive.UpdateTotalReturn(dr.Item("ReceiveID"))
                    Next
                End If

                DL.ReceiveReturn.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.ReceiveReturnDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.ReceiveReturn.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.ReceiveReturnID = clsData.ID
                    DL.ReceiveReturn.SaveDataDetail(clsDetail)

                    '# Calculate
                    DL.Receive.CalculateReturnQty(clsDetail.ReceiveDetID)
                Next

                '# Calculate Total Return
                dtListReceiveID = DL.ReceiveReturn.ListDataReceiveID(clsData.ID)
                For Each dr As DataRow In dtListReceiveID.Rows
                    DL.Receive.UpdateTotalReturn(dr.Item("ReceiveID"))
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

        Public Shared Function GetDetail(ByVal strID As String) As VO.ReceiveReturn
            BL.Server.ServerDefault()
            Return DL.ReceiveReturn.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.ReceiveReturn)
            Dim dtPreviousItem As New DataTable, dtListReceiveID As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.ReceiveReturn.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.ReceiveReturn.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    dtListReceiveID = DL.ReceiveReturn.ListDataReceiveID(clsData.ID)

                    DL.ReceiveReturn.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Revert Return Qty
                    dtPreviousItem = DL.ReceiveReturn.ListDataDetail(clsData.ID)
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Receive.CalculateReturnQty(dr.Item("ReceiveDetID"))
                    Next

                    '# Revert Total Return
                    For Each dr As DataRow In dtListReceiveID.Rows
                        DL.Receive.UpdateTotalReturn(dr.Item("ReceiveID"))
                    Next

                    '# Delete Journal
                    Dim clsJournal As VO.Journal = New VO.Journal
                    clsJournal.CompanyID = clsData.CompanyID
                    clsJournal.ID = clsData.JournalID
                    clsJournal.ReferencesID = clsData.ID
                    clsJournal.JournalDate = clsData.ReceiveReturnDate
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
            Dim dtData As DataTable = DL.ReceiveReturn.ListDataOutstandingPostGL(dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtData.Rows
                Dim dtItem As DataTable = DL.ReceiveReturn.ListDataDetail(dr.Item("ID"))
                Dim decCOGS As Decimal = BL.COGS.CalculateCOGS(dtItem, dr.Item("ID"), dr.Item("ReceiveReturnDate"))

                '# Generate Journal
                Dim clsJournal As VO.Journal = New VO.Journal
                clsJournal.CompanyID = dr.Item("CompanyID")
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("ReceiveReturnDate")
                clsJournal.TotalAmount = dr.Item("SubTotal")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = dr.Item("Remarks")
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(2) As VO.JournalDet

                '# Account Payable
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
                clsJournalDet.DebitAmount = dr.Item("GrandTotal")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(0) = clsJournalDet

                '# Purchase Discount
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofPurchaseDisc
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofPurchaseDisc
                clsJournalDet.DebitAmount = dr.Item("TotalDiscount")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(1) = clsJournalDet

                '# Stock
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("SubTotal")
                clsJournalDetAll(2) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
                '# End Of Generate Journal

                '#Update Journal ID
                If strJournalID.Trim <> "" Then DL.ReceiveReturn.UpdateJournalID(dr.Item("ID"), strJournalID)

                DL.ReceiveReturn.PostGL(dr.Item("ID"), UI.usUserApp.UserID)
            Next
        End Sub

        Public Shared Sub UnpostData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.ReceiveReturn.ListData(dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                BL.COGS.UnpostCOGS(dr.Item("ID"))
                DL.StockOut.DeleteDataByReferencesID(dr.Item("ID"))

                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '#Update Journal ID
                DL.ReceiveReturn.UpdateJournalID(dr.Item("ID"), "")
                DL.ReceiveReturn.UnpostGL(dr.Item("ID"))
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strReceiveReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.ReceiveReturn.ListDataDetail(strReceiveReturnID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strReceiveReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.ReceiveReturn.ListDataStatus(strReceiveReturnID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strReceiveReturnID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.ReceiveReturnStatus
            clsData.ID = strReceiveReturnID & "-" & Format(DL.ReceiveReturn.GetMaxIDStatus(strReceiveReturnID), "000")
            clsData.ReceiveReturnID = strReceiveReturnID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.ReceiveReturn.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace