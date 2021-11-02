Namespace BL
    Public Class Cost

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Cost.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "CO" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Cost.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Cost, ByVal clsDataDetail() As VO.CostDet) As String
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID)
                    If DL.Cost.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                        'ElseIf Format(clsData.CostDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate Then
                        '    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    If DL.Cost.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                        'ElseIf DL.Cost.IsPostedGL(clsData.ID) Then
                        '    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If
                    DL.Cost.DeleteDataDetail(clsData.ID)
                End If

                DL.Cost.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.CostDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.Cost.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.CostID = clsData.ID
                    DL.Cost.SaveDataDetail(clsDetail)
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

        Public Shared Function GetDetail(ByVal strID As String) As VO.Cost
            BL.Server.ServerDefault()
            Return DL.Cost.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.Cost)
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.Cost.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                    'ElseIf DL.Cost.IsPostedGL(clsData.ID) Then
                    '    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.Cost.DeleteData(clsData.ID)

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

        'Public Shared Sub PostData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
        '    Dim dtData As DataTable = DL.Cost.ListDataOutstandingPostGL(dtmDateFrom, dtmDateTo)
        '    For Each dr As DataRow In dtData.Rows
        '        Dim dtItem As DataTable = DL.Cost.ListDataDetail(dr.Item("ID"))

        '        '# Generate Journal
        '        Dim clsJournal As VO.Journal = New VO.Journal
        '        clsJournal.CompanyID = dr.Item("CompanyID")
        '        clsJournal.ID = dr.Item("JournalID")
        '        clsJournal.ReferencesID = dr.Item("ID")
        '        clsJournal.JournalDate = dr.Item("CostDate")
        '        clsJournal.TotalAmount = dr.Item("TotalAmount")
        '        clsJournal.IsAutoGenerate = True
        '        clsJournal.IDStatus = VO.Status.Values.Draft
        '        clsJournal.Remarks = dr.Item("Remarks")
        '        clsJournal.LogBy = UI.usUserApp.UserID

        '        Dim clsJournalDet As VO.JournalDet
        '        Dim clsJournalDetAll(dtItem.Rows.Count) As VO.JournalDet
        '        '# Expense
        '        For i As Integer = 0 To dtItem.Rows.Count - 1
        '            clsJournalDet = New VO.JournalDet
        '            clsJournalDet.JournalID = dr.Item("JournalID")
        '            clsJournalDet.CoAID = dtItem.Rows(i).Item("CoAID")
        '            clsJournalDet.CoAName = dtItem.Rows(i).Item("CoAName")
        '            clsJournalDet.DebitAmount = dtItem.Rows(i).Item("Amount")
        '            clsJournalDet.CreditAmount = 0
        '            clsJournalDetAll(i) = clsJournalDet
        '        Next

        '        '# Cash / Bank
        '        clsJournalDet = New VO.JournalDet
        '        clsJournalDet.JournalID = dr.Item("JournalID")
        '        clsJournalDet.CoAID = dr.Item("CoAID")
        '        clsJournalDet.CoAName = dr.Item("CoAName")
        '        clsJournalDet.DebitAmount = 0
        '        clsJournalDet.CreditAmount = dr.Item("TotalAmount")
        '        clsJournalDetAll(dtItem.Rows.Count) = clsJournalDet

        '        Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
        '        '# End Of Generate Journal

        '        '#Update Journal ID
        '        If strJournalID.Trim <> "" Then DL.Cost.UpdateJournalID(dr.Item("ID"), strJournalID)

        '        DL.Cost.PostGL(dr.Item("ID"), UI.usUserApp.UserID)
        '    Next
        'End Sub

        'Public Shared Sub UnpostData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
        '    Dim dtData As DataTable = DL.Cost.ListData(dtmDateFrom, dtmDateTo, VO.Status.Values.All)
        '    For Each dr As DataRow In dtData.Rows
        '        DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
        '        DL.Journal.DeleteDataPure(dr.Item("JournalID"))

        '        '#Update Journal ID
        '        DL.Cost.UpdateJournalID(dr.Item("ID"), "")
        '        DL.Cost.UnpostGL(dr.Item("ID"))
        '    Next
        'End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strCostID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Cost.ListDataDetail(strCostID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Cost.ListDataStatus(strSalesReturnID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strCostID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.CostStatus
            clsData.ID = strCostID & "-" & Format(DL.Cost.GetMaxIDStatus(strCostID), "000")
            clsData.CostID = strCostID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.Cost.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class 

End Namespace

