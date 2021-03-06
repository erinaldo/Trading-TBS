Namespace BL

    Public Class Sales

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Sales.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SO" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Sales.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Sales, ByVal clsReceive As VO.Receive) As String
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                '# Sales
                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID)
                    If DL.Sales.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.SalesDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    Dim strReturnID As String = DL.SalesReturn.GetReturnID(clsData.ID)
                    Dim strInvoiceID As String = DL.AccountReceivable.GetInvoiceID(clsData.ID)

                    If DL.Sales.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf strReturnID.Trim <> "" Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                    ElseIf strInvoiceID.Trim <> "" Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                    ElseIf DL.Sales.IsSplitReceive(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses split data pembelian")
                    ElseIf DL.Sales.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If
                End If

                DL.Sales.SaveData(bolNew, clsData)

                '# Save Data Status Sales
                SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

                '# Receive
                If bolNew Then clsReceive.ID = BL.Receive.GetNewID(clsReceive.CompanyID, clsReceive.ProgramID)

                clsReceive.ReferencesID = clsData.ID
                DL.Receive.SaveData(bolNew, clsReceive)

                '# Save Data Status Receive
                BL.Receive.SaveDataStatus(clsReceive.ID, IIf(bolNew, "BARU", "EDIT"), clsReceive.LogBy, clsReceive.Remarks)

                '# Calculate Arrival Usage
                DL.Sales.CalculateArrivalUsage(clsData.ID)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function SplitDataReceive(ByVal clsData As VO.Sales, ByVal clsReceive() As VO.Receive) As Boolean
            Dim bolReturn As Boolean = False
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                '# Checking Already Split Receive
                If DL.Sales.IsSplitReceive(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dilakukan split pembelian. Dikarenakan data telah dilakukan split pembelian sebelumnya.")
                End If

                '# Save Data Sales Supplier
                For Each clsItem As VO.Receive In clsReceive
                    BL.Receive.SaveData(True, clsItem)
                Next

                DL.Sales.SetIsSplitReceive(clsData.ID, True)
                DL.Sales.CalculateArrivalUsage(clsData.ID)

                '# Save Data Status
                SaveDataStatus(clsData.ID, "SPLIT PEMBELIAN", clsData.LogBy, clsData.Remarks)

                DL.SQL.CommitTransaction()
                bolReturn = True
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return bolReturn
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.Sales
            BL.Server.ServerDefault()
            Return DL.Sales.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.Sales)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                Dim strReturnID As String = DL.SalesReturn.GetReturnID(clsData.ID)
                Dim strInvoiceID As String = DL.AccountReceivable.GetInvoiceID(clsData.ID)

                If DL.Sales.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf strReturnID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                ElseIf strInvoiceID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                ElseIf DL.Sales.IsSplitReceive(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses split data pembelian")
                ElseIf DL.Sales.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.Sales.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Function ListDataSplitReceive(ByVal strSalesID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataSplitReceive(strSalesID)
        End Function

        Public Shared Sub PrintFakturPenjualan(ByVal clsData As VO.Sales)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.Sales.PrintFakturPenjualan(clsData.ID)

                '# Save Data Status
                SaveDataStatus(clsData.ID, "PRINT FAKTUR PENJUALAN", clsData.LogBy, clsData.Remarks)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try

        End Sub

        Public Shared Function ListDataFakturPenjualan(ByVal strID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataFakturPenjualan(strID)
        End Function

        Public Shared Function ListDataOutstandingReturn(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataOutstandingReturn(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intBPID)
        End Function

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            Dim dtData As DataTable = DL.Sales.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtData.Rows
                Dim decCOGS As Decimal = BL.COGS.CalculateCOGS(intCompanyID, intProgramID, _
                                                               dr.Item("ItemID"), dr.Item("ArrivalNettoAfter"), _
                                                               dr.Item("ID"), dr.Item("SalesDate"))
                '# Generate Journal
                Dim clsJournal As VO.Journal = New VO.Journal
                clsJournal.CompanyID = intCompanyID
                clsJournal.ProgramID = intProgramID
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("SalesDate")
                clsJournal.TotalAmount = dr.Item("TotalPrice")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = dr.Item("Remarks")
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(3) As VO.JournalDet

                '# Revenue 
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofRevenue
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("TotalPrice")
                clsJournalDetAll(0) = clsJournalDet

                '# Account Receivable
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountReceivable
                clsJournalDet.DebitAmount = dr.Item("TotalPrice")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(1) = clsJournalDet

                '# COGS
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofCOGS
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofCOGS
                clsJournalDet.DebitAmount = decCOGS
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(2) = clsJournalDet

                '# Stock
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = decCOGS
                clsJournalDetAll(3) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
                '# End Of Generate Journal

                '# Update Journal ID
                If strJournalID.Trim <> "" Then DL.Sales.UpdateJournalID(dr.Item("ID"), strJournalID)

                DL.Sales.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            Dim dtData As DataTable = DL.Sales.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                BL.COGS.UnpostCOGS(intCompanyID, intProgramID, dr.Item("ID"))
                DL.StockOut.DeleteDataByReferencesID(intCompanyID, intProgramID, dr.Item("ID"))

                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.Sales.UpdateJournalID(dr.Item("ID"), "")
                DL.Sales.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Supplier"

        Public Shared Function ListDataSupplier(ByVal strSalesID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataSupplier(strSalesID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataStatus(strSalesID)
        End Function

        Public Shared Sub SaveDataStatus(ByVal strSalesID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.SalesStatus
            clsData.ID = strSalesID & "-" & Format(DL.Sales.GetMaxIDStatus(strSalesID), "000")
            clsData.SalesID = strSalesID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.Sales.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace