Namespace DL
    Public Class Journal

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.JournalDate, A.ReferencesID, A.TotalAmount, A.IsAutoGenerate, A.IsPostedGL, A.PostedBy,   " & vbNewLine & _
                   "     A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate,   " & vbNewLine & _
                   "     A.LogInc, A.LogBy, A.LogDate, B.Name AS StatusInfo " & vbNewLine & _
                   "FROM traJournal A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.JournalDate>=@DateFrom AND A.JournalDate<=@DateTo " & vbNewLine & _
                   "    AND A.IsAutoGenerate=0 " & vbNewLine

                If intIDStatus <> VO.Status.Values.All Then
                    .CommandText += "    AND A.IDStatus=@IDStatus" & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = intIDStatus
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Journal)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traJournal " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, JournalDate, ReferencesID, TotalAmount, IsAutoGenerate, Remarks, IDStatus, CreatedBy, CreatedDate,   " & vbNewLine & _
                       "     LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @JournalDate, @ReferencesID, @TotalAmount, @IsAutoGenerate, @Remarks, @IDStatus, @LogBy, GETDATE(),   " & vbNewLine & _
                       "     @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE traJournal SET " & vbNewLine & _
                    "    CompanyID=@CompanyID, " & vbNewLine & _
                    "    ProgramID=@ProgramID, " & vbNewLine & _
                    "    JournalDate=@JournalDate, " & vbNewLine & _
                    "    ReferencesID=@ReferencesID, " & vbNewLine & _
                    "    TotalAmount=@TotalAmount, " & vbNewLine & _
                    "    IsAutoGenerate=@IsAutoGenerate, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@JournalDate", SqlDbType.DateTime).Value = clsData.JournalDate
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = clsData.TotalAmount
                .Parameters.Add("@IsAutoGenerate", SqlDbType.Bit).Value = clsData.IsAutoGenerate
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.Journal
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.Journal
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.JournalDate, A.ReferencesID, A.TotalAmount, A.IsAutoGenerate, A.IsPostedGL, A.PostedBy,   " & vbNewLine & _
                        "    A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.LogBy, A.LogDate  " & vbNewLine & _
                        "FROM traJournal A " & vbNewLine & _
                        "INNER JOIN mstCompany MC ON " & vbNewLine & _
                        "   A.CompanyID=MC.ID " & vbNewLine & _
                        "INNER JOIN mstProgram MP ON " & vbNewLine & _
                        "   A.ProgramID=MP.ID " & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "    A.ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.CompanyID = .Item("CompanyID")
                        voReturn.CompanyName = .Item("CompanyName")
                        voReturn.ProgramID = .Item("ProgramID")
                        voReturn.ProgramName = .Item("ProgramName")
                        voReturn.ID = .Item("ID")
                        voReturn.JournalDate = .Item("JournalDate")
                        voReturn.ReferencesID = .Item("ReferencesID")
                        voReturn.TotalAmount = .Item("TotalAmount")
                        voReturn.IsAutoGenerate = .Item("IsAutoGenerate")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.Remarks = .Item("Remarks")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return voReturn
        End Function

        Public Shared Sub DeleteData(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traJournal " & vbNewLine & _
                    "SET IsDeleted=1, IDStatus=@IDStatus " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Deleted
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataPure(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE traJournal " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Deleted
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxID(ByVal strID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traJournal " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(ID,16)=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 16).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("ID") + 1
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return intReturn
        End Function

        Public Shared Function DataExists(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traJournal " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolExists = True
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return bolExists
        End Function

        Public Shared Function IsDeleted(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traJournal " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine & _
                        "   AND IsDeleted=1 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolExists = True
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return bolExists
        End Function

        Public Shared Function IsPostedGL(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traJournal " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine & _
                        "   AND IsPostedGL=1 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolExists = True
                    End If 
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return bolExists
        End Function

        Public Shared Sub PostGL(ByVal strID As String, ByVal strLogBy As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traJournal SET" & vbNewLine & _
                    "   IsPostedGL=1, " & vbNewLine & _
                    "   PostedBy=@LogBy, " & vbNewLine & _
                    "   PostedDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = strLogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub UnpostGL(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traJournal SET" & vbNewLine & _
                    "   IsPostedGL=0, " & vbNewLine & _
                    "   PostedBy='' " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function ListDataOutstandingPostGL(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                         ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, A.ID, A.JournalDate, A.ReferencesID, A.TotalAmount, A.IsAutoGenerate, A.IsPostedGL, A.PostedBy,   " & vbNewLine & _
                   "     A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate,   " & vbNewLine & _
                   "     A.LogInc, A.LogBy, A.LogDate  " & vbNewLine & _
                   "FROM traJournal A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.JournalDate>=@DateFrom AND A.JournalDate<=@DateTo " & vbNewLine & _
                   "    AND A.IsAutoGenerate=0 " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strJournalID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.ID, A.JournalID, A.CoAID, B.Code AS CoACode, B.Name AS CoAName, A.DebitAmount, A.CreditAmount " & vbNewLine & _
                   "FROM traJournalDet A " & vbNewLine & _
                   "INNER JOIN mstChartOfAccount B ON " & vbNewLine & _
                   "    A.CoAID=B.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.JournalID=@JournalID" & vbNewLine

                .Parameters.Add("@JournalID", SqlDbType.VarChar, 30).Value = strJournalID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataDetail(ByVal clsData As VO.JournalDet)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "INSERT INTO traJournalDet " & vbNewLine & _
                    "    (ID, JournalID, CoAID, DebitAmount, CreditAmount)   " & vbNewLine & _
                    "VALUES " & vbNewLine & _
                    "    (@ID, @JournalID, @CoAID, @DebitAmount, @CreditAmount)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@JournalID", SqlDbType.VarChar, 30).Value = clsData.JournalID
                .Parameters.Add("@CoAID", SqlDbType.Int).Value = clsData.CoAID
                .Parameters.Add("@DebitAmount", SqlDbType.Decimal).Value = clsData.DebitAmount
                .Parameters.Add("@CreditAmount", SqlDbType.Decimal).Value = clsData.CreditAmount
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataDetail(ByVal strJournalID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traJournalDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   JournalID=@JournalID " & vbNewLine

                .Parameters.Add("@JournalID", SqlDbType.VarChar, 30).Value = strJournalID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDDetail(ByVal strJournalID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traJournalDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(JournalID,21)=@JournalID " & vbNewLine

                    .Parameters.Add("@JournalID", SqlDbType.VarChar, 21).Value = strJournalID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("ID") + 1
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return intReturn
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strJournalID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.JournalID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traJournalStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.JournalID=@JournalID " & vbNewLine

                .Parameters.Add("@JournalID", SqlDbType.VarChar, 30).Value = strJournalID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.JournalStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traJournalStatus " & vbNewLine & _
                   "    (ID, JournalID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @JournalID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@JournalID", SqlDbType.VarChar, 30).Value = clsData.JournalID
                .Parameters.Add("@Status", SqlDbType.VarChar, 100).Value = clsData.Status
                .Parameters.Add("@StatusBy", SqlDbType.VarChar, 20).Value = clsData.StatusBy
                .Parameters.Add("@StatusDate", SqlDbType.DateTime).Value = clsData.StatusDate
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDStatus(ByVal strJournalID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traJournalStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(JournalID,21)=@JournalID " & vbNewLine

                    .Parameters.Add("@JournalID", SqlDbType.VarChar, 21).Value = strJournalID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("ID") + 1
                    End If 
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return intReturn
        End Function

#End Region

    End Class

End Namespace