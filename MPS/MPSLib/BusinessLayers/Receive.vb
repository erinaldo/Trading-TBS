Namespace BL

    Public Class Receive

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Receive.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Public Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "RV" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Receive.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveDataDefault(ByVal bolNew As Boolean, ByVal clsData As VO.Receive) As String
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                SaveData(bolNew, clsData)

                DL.Sales.CalculateArrivalUsage(clsData.ReferencesID)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Receive) As String
            If bolNew Then
                clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID)
                If DL.Receive.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID sudah ada sebelumnya")
                ElseIf Format(clsData.ReceiveDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                End If
            Else
                Dim strReturnID As String = DL.ReceiveReturn.GetReturnID(clsData.ID)
                Dim strInvoiceID As String = DL.AccountPayable.GetInvoiceID(clsData.ID)

                If DL.Receive.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                ElseIf strReturnID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                ElseIf strInvoiceID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses pembayaran dengan nomor " & strInvoiceID)
                ElseIf DL.Receive.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                End If
            End If

            DL.Receive.SaveData(bolNew, clsData)

            '# Save Data Status
            SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.Receive
            BL.Server.ServerDefault()
            Return DL.Receive.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.Receive)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                Dim strReturnID As String = DL.ReceiveReturn.GetReturnID(clsData.ID)
                Dim strInvoiceID As String = DL.AccountPayable.GetInvoiceID(clsData.ID)

                If DL.Receive.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf strReturnID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                ElseIf strInvoiceID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses pembayaran dengan nomor " & strInvoiceID)
                ElseIf DL.Receive.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.Receive.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# CaLculate Sales Arrival Usage
                    DL.Sales.CalculateArrivalUsage(clsData.ReferencesID)

                    '# Checking Outstanding Receive
                    If Not DL.Sales.OutstandingReceive(clsData.ReferencesID) Then
                        DL.Sales.SetIsSplitReceive(clsData.ReferencesID, False)

                        '# Save Data Sales
                        BL.Sales.SaveDataStatus(clsData.ReferencesID, "BATAL SPLIT PEMBELIAN", MPSLib.UI.usUserApp.UserID, clsData.Remarks)
                    End If
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub PrintSlipTimbang(ByVal clsData As VO.Sales)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.Receive.PrintSlipTimbang(clsData.ID)

                '# Save Data Status
                SaveDataStatus(clsData.ID, "PRINT SLIP TIMBANGAN", clsData.LogBy, clsData.Remarks)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Function ListDataSlipTimbang(ByVal strID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Receive.ListDataSlipTimbang(strID)
        End Function

        Public Shared Function ListDataOutstandingReturn(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                         ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Receive.ListDataOutstandingReturn(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intBPID)
        End Function
        
        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            Dim clsStockIn As New VO.StockIN
            Dim dtData As DataTable = DL.Receive.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)

            For Each dr As DataRow In dtData.Rows
                clsStockIn = New VO.StockIN
                clsStockIn.CompanyID = intCompanyID
                clsStockIn.ProgramID = intProgramID
                clsStockIn.ID = DL.StockIN.GetMaxID
                clsStockIn.ItemID = dr.Item("ItemID")
                clsStockIn.ReferencesID = dr.Item("ID")
                clsStockIn.ReferencesDate = dr.Item("ReceiveDate")
                clsStockIn.Qty = dr.Item("ArrivalNettoAfter")
                clsStockIn.Price = dr.Item("Price1")
                clsStockIn.NetPrice = dr.Item("Price1")
                clsStockIn.QtyOut = 0
                DL.StockIN.SaveData(True, clsStockIn)

                '# Generate Journal
                Dim clsJournal As VO.Journal = New VO.Journal
                clsJournal.CompanyID = intCompanyID
                clsJournal.ProgramID = intProgramID
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("ReceiveDate")
                clsJournal.TotalAmount = dr.Item("TotalPrice1")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = dr.Item("Remarks")
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(1) As VO.JournalDet

                '# Stock
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
                clsJournalDet.DebitAmount = dr.Item("TotalPrice1")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(0) = clsJournalDet

                '# Account Payable
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("TotalPrice1")
                clsJournalDetAll(1) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
                '# End Of Generate Journal

                '# Update Journal ID
                If strJournalID.Trim <> "" Then DL.Receive.UpdateJournalID(dr.Item("ID"), strJournalID)

                DL.Receive.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            Dim dtData As DataTable = DL.Receive.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                '# Delete Stock In
                DL.StockIN.DeleteDataByReferencesID(intCompanyID, intProgramID, dr.Item("ID"))

                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '#Update Journal ID
                DL.Receive.UpdateJournalID(dr.Item("ID"), "")
                DL.Receive.UnpostGL(dr.Item("ID"))

                '#Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strReceiveID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Receive.ListDataStatus(strReceiveID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strReceiveID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.ReceiveStatus
            clsData.ID = strReceiveID & "-" & Format(DL.Receive.GetMaxIDStatus(strReceiveID), "000")
            clsData.ReceiveID = strReceiveID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.Receive.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace