Namespace BL
    Public Class DownPayment

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        ByVal intIDStatus As Integer, ByVal intDPType As VO.DownPayment.Type) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.DownPayment.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus, intDPType)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal intDPType As VO.DownPayment.Type)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "DP" & "-" & IIf(intDPType = VO.DownPayment.Type.Sales, "SO", "RV") & "-" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.DownPayment.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.DownPayment) As String
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.DPType)
                    If DL.DownPayment.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.DPDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    If DL.DownPayment.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf DL.DownPayment.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If

                    '# Checking Already Usage
                End If

                DL.DownPayment.SaveData(bolNew, clsData)

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

        Public Shared Function GetDetail(ByVal strID As String) As VO.DownPayment
            BL.Server.ServerDefault()
            Return DL.DownPayment.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.DownPayment)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.DownPayment.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.DownPayment.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.DownPayment.DeleteData(clsData.ID)

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
            'dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            'Dim dtData As DataTable = DL.AccountPayable.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("APDate")
            '    clsJournal.TotalAmount = dr.Item("TotalAmount")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll(1) As VO.JournalDet

            '    '# Account Payable
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
            '    clsJournalDet.DebitAmount = dr.Item("TotalAmount")
            '    clsJournalDet.CreditAmount = 0
            '    clsJournalDetAll(0) = clsJournalDet

            '    '# Cash / Bank
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = dr.Item("CoAIDOfOutgoingPayment")
            '    clsJournalDet.CoAName = ""
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = dr.Item("TotalAmount")
            '    clsJournalDetAll(1) = clsJournalDet

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '# Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.AccountPayable.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.AccountPayable.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            'Dim dtData As DataTable = DL.AccountPayable.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            'For Each dr As DataRow In dtData.Rows
            '    '# Delete Journal
            '    DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
            '    DL.Journal.DeleteDataPure(dr.Item("JournalID"))

            '    '# Update Journal ID
            '    DL.AccountPayable.UpdateJournalID(dr.Item("ID"), "")
            '    DL.AccountPayable.UnpostGL(dr.Item("ID"))

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strDPID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.DownPayment.ListDataStatus(strDPID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strDPID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                          ByVal strRemarks As String)
            Dim clsData As New VO.DownPaymentStatus
            clsData.ID = strDPID & "-" & Format(DL.AccountPayable.GetMaxIDStatus(strDPID), "000")
            clsData.DPID = strDPID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.DownPayment.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace

