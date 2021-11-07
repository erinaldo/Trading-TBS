Namespace BL
    Public Class AccountReceivable

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "AR" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.AccountReceivable.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.AccountReceivable, ByVal clsDataDetail() As VO.AccountReceivableDet) As String
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID)
                    If DL.AccountReceivable.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.ARDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    If DL.AccountReceivable.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf DL.AccountReceivable.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If
                    dtPreviousItem = DL.AccountReceivable.ListDataDetail(clsData.ID)
                    DL.AccountReceivable.DeleteDataDetail(clsData.ID)

                    '# Revert Total Payment
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Sales.CalculateTotalPayment(dr.Item("SalesID"))
                    Next
                End If

                DL.AccountReceivable.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.AccountReceivableDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.AccountReceivable.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.ARID = clsData.ID
                    DL.AccountReceivable.SaveDataDetail(clsDetail)

                    '# Calculate
                    DL.Sales.CalculateTotalPayment(clsDetail.SalesID)
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

        Public Shared Function GetDetail(ByVal strID As String) As VO.AccountReceivable
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.AccountReceivable)
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.AccountReceivable.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.AccountReceivable.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.AccountReceivable.DeleteData(clsData.ID)

                    '# Revert Total Payment
                    dtPreviousItem = DL.AccountReceivable.ListDataDetail(clsData.ID)
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Sales.CalculateTotalPayment(dr.Item("SalesID"))
                    Next

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

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            Dim dtData As DataTable = DL.AccountReceivable.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtData.Rows
                '# Generate Journal
                Dim clsJournal As VO.Journal = New VO.Journal
                clsJournal.CompanyID = intCompanyID
                clsJournal.ProgramID = intProgramID
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("ARDate")
                clsJournal.TotalAmount = dr.Item("TotalAmount")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = dr.Item("Remarks")
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(1) As VO.JournalDet

                '# Cash / Bank
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = dr.Item("CoAIDOfIncomePayment")
                clsJournalDet.CoAName = ""
                clsJournalDet.DebitAmount = dr.Item("TotalAmount")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(0) = clsJournalDet

                '# Account Receivable
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofRevenue
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                clsJournalDetAll(1) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
                '# End Of Generate Journal

                '# Update Journal ID
                If strJournalID.Trim <> "" Then DL.AccountReceivable.UpdateJournalID(dr.Item("ID"), strJournalID)

                DL.AccountReceivable.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            Dim dtData As DataTable = DL.AccountReceivable.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.AccountReceivable.UpdateJournalID(dr.Item("ID"), "")
                DL.AccountReceivable.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetailWithOutstanding(ByVal strARID As String, ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListDataDetailWithOutstanding(strARID, intBPID)
        End Function

        Public Shared Function ListDataDetail(ByVal strARID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListDataDetail(strARID)
        End Function

        Public Shared Function ListDataDetailOutstanding(ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListDataDetailOutstanding(intBPID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strARID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListDataStatus(strARID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strARID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                          ByVal strRemarks As String)
            Dim clsData As New VO.AccountReceivableStatus
            clsData.ID = strARID & "-" & Format(DL.AccountReceivable.GetMaxIDStatus(strARID), "000")
            clsData.ARID = strARID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.AccountReceivable.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace

