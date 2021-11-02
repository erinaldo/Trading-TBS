Namespace BL
    Public Class Journal

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Journal.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal bolIsAutoGenerated As Boolean, ByVal intProgramID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = IIf(bolIsAutoGenerated, "JA", "JT") & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Journal.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveDataDefault(ByVal bolNew As Boolean, ByVal clsData As VO.Journal, ByVal clsDataDetail() As VO.JournalDet) As String
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                SaveData(bolNew, clsData, clsDataDetail)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Journal, ByVal clsDataDetail() As VO.JournalDet) As String
            If bolNew Then
                clsData.ID = GetNewID(clsData.CompanyID, clsData.IsAutoGenerate, clsData.ProgramID)
                If DL.Journal.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID sudah ada sebelumnya")
                    'ElseIf Format(clsData.JournalDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate Then
                    '    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                End If
            Else
                If DL.Journal.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                ElseIf DL.Journal.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                End If

                DL.Journal.DeleteDataDetail(clsData.ID)

            End If

            DL.Journal.SaveData(bolNew, clsData)

            '# Save Data Detail
            For Each clsDetail As VO.JournalDet In clsDataDetail
                clsDetail.ID = clsData.ID & "-" & Format(DL.Journal.GetMaxIDDetail(clsData.ID), "000")
                clsDetail.JournalID = clsData.ID
                DL.Journal.SaveDataDetail(clsDetail)
            Next

            '# Save Data Status
            SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.Journal
            BL.Server.ServerDefault()
            Return DL.Journal.GetDetail(strID)
        End Function

        Public Shared Sub DeleteDataDefault(ByVal clsData As VO.Journal)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DeleteData(clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub DeleteData(ByVal clsData As VO.Journal)
            If DL.Journal.IsDeleted(clsData.ID) Then
                Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
            ElseIf DL.Journal.IsPostedGL(clsData.ID) Then
                Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
            Else
                DL.Journal.DeleteData(clsData.ID)

                '# Save Data Status
                SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)
            End If
        End Sub

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.Journal.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtData.Rows
                DL.Journal.PostGL(dr.Item("ID"), UI.usUserApp.UserID)
            Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.Journal.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                DL.Journal.UnpostGL(dr.Item("ID"))
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strJournalID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Journal.ListDataDetail(strJournalID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strJournalID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Journal.ListDataStatus(strJournalID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strJournalID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.JournalStatus
            clsData.ID = strJournalID & "-" & Format(DL.Journal.GetMaxIDStatus(strJournalID), "000")
            clsData.JournalID = strJournalID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.Journal.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace

