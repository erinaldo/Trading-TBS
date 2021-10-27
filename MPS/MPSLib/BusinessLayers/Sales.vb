Namespace BL

    Public Class Sales

#Region "Main"

        Public Shared Function ListData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Sales.ListData(dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Public Shared Function ListDataBonFaktur(ByVal strID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataBonFaktur(strID)
        End Function

        Public Shared Function ListDataOutstanding() As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataOutstanding()
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SO" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-"
            strReturn = strReturn & Format(DL.Sales.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Sales) As String
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID)
                    If DL.Sales.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                        'ElseIf Format(clsData.SalesDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate Then
                        '    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    'Dim strReturnID As String = DL.SalesReturn.AlreadyCreatedReturn(clsData.ID)
                    'Dim strInvoiceID As String = DL.AccountReceivable.AlreadyCreatedInvoice(clsData.ID)

                    If DL.Sales.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                        'ElseIf strReturnID.Trim <> "" Then
                        '    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                        'ElseIf strInvoiceID.Trim <> "" Then
                        '    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                        'ElseIf DL.Sales.IsPostedGL(clsData.ID) Then
                        '    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If
                End If

                DL.Sales.SaveData(bolNew, clsData)

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

        Public Shared Function GetDetail(ByVal strID As String) As VO.Sales
            BL.Server.ServerDefault()
            Return DL.Sales.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.Sales)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                'Dim strReturnID As String = DL.SalesReturn.AlreadyCreatedReturn(clsData.ID)
                'Dim strInvoiceID As String = DL.AccountReceivable.AlreadyCreatedInvoice(clsData.ID)

                If DL.Sales.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                    'ElseIf strReturnID.Trim <> "" Then
                    '    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                    'ElseIf strInvoiceID.Trim <> "" Then
                    '    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                    'ElseIf DL.Sales.IsPostedGL(clsData.ID) Then
                    '    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
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

        Public Shared Sub PrintBonFaktur(ByVal clsData As VO.Sales, ByVal doColor As List(Of String))
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.Sales.PrintBonFaktur(clsData.ID)

                '# Save Data Status
                For i As Integer = 0 To doColor.Count - 1
                    SaveDataStatus(clsData.ID, "PRINT " & doColor(i).Trim, clsData.LogBy, clsData.Remarks)
                Next

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try

        End Sub

        'Public Shared Function ListDataDeliveryOrder(ByVal strID As String) As DataTable
        '    BL.Server.ServerDefault()
        '    Return DL.Sales.ListDataDeliveryOrder(strID)
        'End Function

        'Public Shared Sub PrintDeliveryOrder(ByVal clsData As VO.Sales, ByVal doColor As List(Of String))
        '    BL.Server.ServerDefault()
        '    Try
        '        DL.SQL.OpenConnection()
        '        DL.SQL.BeginTransaction()

        '        DL.Sales.PrintDeliveryOrder(clsData.ID)

        '        '# Save Data Status
        '        For i As Integer = 0 To doColor.Count - 1
        '            SaveDataStatus(clsData.ID, "PRINT " & doColor(i).Trim, clsData.LogBy, clsData.Remarks)
        '        Next

        '        DL.SQL.CommitTransaction()
        '    Catch ex As Exception
        '        DL.SQL.RollBackTransaction()
        '        Throw ex
        '    Finally
        '        DL.SQL.CloseConnection()
        '    End Try

        'End Sub

        'Public Shared Sub PostData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
        '    Dim dtData As DataTable = DL.Sales.ListDataOutstandingPostGL(dtmDateFrom, dtmDateTo)
        '    For Each dr As DataRow In dtData.Rows
        '        Dim dtItem As DataTable = DL.Sales.ListDataDetail(dr.Item("ID"))
        '        Dim decCOGS As Decimal = BL.COGS.CalculateCOGS(dtItem, dr.Item("ID"), dr.Item("SalesDate"))

        '        '# Generate Journal
        '        Dim clsJournal As VO.Journal = New VO.Journal
        '        clsJournal.CompanyID = dr.Item("CompanyID")
        '        clsJournal.ID = dr.Item("JournalID")
        '        clsJournal.ReferencesID = dr.Item("ID")
        '        clsJournal.JournalDate = dr.Item("SalesDate")
        '        clsJournal.TotalAmount = dr.Item("SubTotal")
        '        clsJournal.IsAutoGenerate = True
        '        clsJournal.IDStatus = VO.Status.Values.Draft
        '        clsJournal.Remarks = dr.Item("Remarks")
        '        clsJournal.LogBy = UI.usUserApp.UserID

        '        Dim clsJournalDet As VO.JournalDet
        '        Dim clsJournalDetAll(4) As VO.JournalDet

        '        '# Revenue 
        '        clsJournalDet = New VO.JournalDet
        '        clsJournalDet.JournalID = dr.Item("JournalID")
        '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
        '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofRevenue
        '        clsJournalDet.DebitAmount = 0
        '        clsJournalDet.CreditAmount = dr.Item("SubTotal")
        '        clsJournalDetAll(0) = clsJournalDet

        '        '# Account Receivable
        '        clsJournalDet = New VO.JournalDet
        '        clsJournalDet.JournalID = dr.Item("JournalID")
        '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
        '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountReceivable
        '        clsJournalDet.DebitAmount = dr.Item("GrandTotal")
        '        clsJournalDet.CreditAmount = 0
        '        clsJournalDetAll(1) = clsJournalDet

        '        '# Sales Discount
        '        clsJournalDet = New VO.JournalDet
        '        clsJournalDet.JournalID = dr.Item("JournalID")
        '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofSalesDisc
        '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofSalesDisc
        '        clsJournalDet.DebitAmount = dr.Item("TotalDiscount")
        '        clsJournalDet.CreditAmount = 0
        '        clsJournalDetAll(2) = clsJournalDet

        '        '# COGS
        '        clsJournalDet = New VO.JournalDet
        '        clsJournalDet.JournalID = dr.Item("JournalID")
        '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofCOGS
        '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofCOGS
        '        clsJournalDet.DebitAmount = decCOGS
        '        clsJournalDet.CreditAmount = 0
        '        clsJournalDetAll(3) = clsJournalDet

        '        '# Stock
        '        clsJournalDet = New VO.JournalDet
        '        clsJournalDet.JournalID = dr.Item("JournalID")
        '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
        '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
        '        clsJournalDet.DebitAmount = 0
        '        clsJournalDet.CreditAmount = decCOGS
        '        clsJournalDetAll(4) = clsJournalDet

        '        Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
        '        '# End Of Generate Journal

        '        '#Update Journal ID
        '        If strJournalID.Trim <> "" Then DL.Sales.UpdateJournalID(dr.Item("ID"), strJournalID)

        '        DL.Sales.PostGL(dr.Item("ID"), UI.usUserApp.UserID)
        '    Next
        'End Sub

        'Public Shared Sub UnpostData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
        '    Dim dtData As DataTable = DL.Sales.ListData(dtmDateFrom, dtmDateTo, VO.Status.Values.All)
        '    For Each dr As DataRow In dtData.Rows
        '        BL.COGS.UnpostCOGS(dr.Item("ID"))
        '        DL.StockOut.DeleteDataByReferencesID(dr.Item("ID"))

        '        DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
        '        DL.Journal.DeleteDataPure(dr.Item("JournalID"))

        '        '#Update Journal ID
        '        DL.Sales.UpdateJournalID(dr.Item("ID"), "")
        '        DL.Sales.UnpostGL(dr.Item("ID"))
        '    Next
        'End Sub

#End Region

        '#Region "Detail"

        '        Public Shared Function ListDataDetail(ByVal strSalesID As String) As DataTable
        '            BL.Server.ServerDefault()
        '            Return DL.Sales.ListDataDetail(strSalesID)
        '        End Function

        '        Public Shared Function ListDataOutstandingUsage(ByVal strSalesID As String) As DataTable
        '            BL.Server.ServerDefault()
        '            Return DL.Sales.ListDataOutstandingUsage(strSalesID)
        '        End Function

        '        Public Shared Function ListDataOutstandingReturn(ByVal intBPID As Integer) As DataTable
        '            BL.Server.ServerDefault()
        '            Return DL.Sales.ListDataOutstandingReturn(intBPID)
        '        End Function

        '#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataStatus(strSalesID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strSalesID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
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