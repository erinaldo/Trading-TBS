Namespace BL
    Public Class AccountPayable

#Region "Main"

        Public Shared Function ListData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListData(dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "AP" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-"
            strReturn = strReturn & Format(DL.AccountPayable.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.AccountPayable, ByVal clsDataDetail() As VO.AccountPayableDet) As String
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID)
                    If DL.AccountPayable.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.APDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    If DL.AccountPayable.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf DL.AccountPayable.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If
                    dtPreviousItem = DL.AccountPayable.ListDataDetail(clsData.ID)
                    DL.AccountPayable.DeleteDataDetail(clsData.ID)

                    '# Revert Total Payment
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Receive.CalculateTotalPayment(dr.Item("ReceiveID"))
                    Next
                End If

                DL.AccountPayable.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.AccountPayableDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.AccountPayable.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.APID = clsData.ID
                    DL.AccountPayable.SaveDataDetail(clsDetail)

                    '# Calculate
                    DL.Receive.CalculateTotalPayment(clsDetail.ReceiveID)
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

        Public Shared Function GetDetail(ByVal strID As String) As VO.AccountPayable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.AccountPayable)
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.AccountPayable.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.AccountPayable.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.AccountPayable.DeleteData(clsData.ID)

                    dtPreviousItem = DL.AccountPayable.ListDataDetail(clsData.ID)
                    '# Revert Total Payment
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Receive.CalculateTotalPayment(dr.Item("ReceiveID"))
                    Next

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Delete Journal
                    Dim clsJournal As VO.Journal = New VO.Journal
                    clsJournal.CompanyID = clsData.CompanyID
                    clsJournal.ID = clsData.JournalID
                    clsJournal.ReferencesID = clsData.ID
                    clsJournal.JournalDate = clsData.APDate
                    clsJournal.TotalAmount = clsData.TotalAmount
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
            Dim dtData As DataTable = DL.AccountPayable.ListDataOutstandingPostGL(dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtData.Rows
                '# Generate Journal
                Dim clsJournal As VO.Journal = New VO.Journal
                clsJournal.CompanyID = dr.Item("CompanyID")
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("APDate")
                clsJournal.TotalAmount = dr.Item("TotalAmount")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = dr.Item("Remarks")
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(1) As VO.JournalDet

                '# Account Receivable
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
                clsJournalDet.DebitAmount = dr.Item("TotalAmount")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(0) = clsJournalDet

                '# Cash / Bank
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = dr.Item("CoAIDOfOutgoingPayment")
                clsJournalDet.CoAName = ""
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                clsJournalDetAll(1) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
                '# End Of Generate Journal

                '#Update Journal ID
                If strJournalID.Trim <> "" Then DL.AccountPayable.UpdateJournalID(dr.Item("ID"), strJournalID)

                DL.AccountPayable.PostGL(dr.Item("ID"), UI.usUserApp.UserID)
            Next
        End Sub

        Public Shared Sub UnpostData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.AccountPayable.ListData(dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '#Update Journal ID
                DL.AccountPayable.UpdateJournalID(dr.Item("ID"), "")
                DL.AccountPayable.UnpostGL(dr.Item("ID"))
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetailWithOutstanding(ByVal strAPID As String, ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataDetailWithOutstanding(strAPID, intBPID)
        End Function

        Public Shared Function ListDataDetail(ByVal strAPID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataDetail(strAPID)
        End Function

        Public Shared Function ListDataDetailOutstanding(ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataDetailOutstanding(intBPID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataStatus(strSalesReturnID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strAPID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.AccountPayableStatus
            clsData.ID = strAPID & "-" & Format(DL.AccountPayable.GetMaxIDStatus(strAPID), "000")
            clsData.APID = strAPID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.AccountPayable.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class
End Namespace