Namespace DL
    Public Class Cost

#Region "Main"

        Public Shared Function ListData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, A.ID, A.CostDate, A.CoAID, C.Code AS CoACode, C.Name AS CoAName, A.TotalAmount,   " & vbNewLine & _
                   "     A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo,   " & vbNewLine & _
                   "     A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traCost A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstChartOfAccount C ON " & vbNewLine & _
                   "    A.CoAID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CostDate>=@DateFrom AND A.CostDate<=@DateTo " & vbNewLine

                If intIDStatus <> VO.Status.Values.All Then
                    .CommandText += "    AND A.IDStatus=@IDStatus" & vbNewLine
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = intIDStatus
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataOutstandingPostGL(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, A.ID, A.CostDate, A.CoAID, C.Code AS CoACode, C.Name AS CoAName, A.TotalAmount,   " & vbNewLine & _
                   "     A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo,   " & vbNewLine & _
                   "     A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traCost A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstChartOfAccount C ON " & vbNewLine & _
                   "    A.CoAID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CostDate>=@DateFrom AND A.CostDate<=@DateTo " & vbNewLine & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Cost)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traCost " & vbNewLine & _
                       "    (CompanyID, ID, CostDate, CoAID, TotalAmount,   " & vbNewLine & _
                       "     Remarks, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ID, @CostDate, @CoAID, @TotalAmount,   " & vbNewLine & _
                       "     @Remarks, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE traCost SET " & vbNewLine & _
                    "    CompanyID=@CompanyID, " & vbNewLine & _
                    "    CostDate=@CostDate, " & vbNewLine & _
                    "    CoAID=@CoAID, " & vbNewLine & _
                    "    TotalAmount=@TotalAmount, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@CostDate", SqlDbType.DateTime).Value = clsData.CostDate
                .Parameters.Add("@CoAID", SqlDbType.Int).Value = clsData.CoAID
                .Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = clsData.TotalAmount
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

        Public Shared Function GetDetail(ByVal strID As String) As VO.Cost
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim voReturn As New VO.Cost
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.CompanyID, A.ID, A.CostDate, A.CoAID, COA.Code AS CoACode, COA.Name AS CoAName, A.TotalAmount,   " & vbNewLine & _
                       "    A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus,   " & vbNewLine & _
                       "    A.LogBy, A.LogDate, A.LogInc, A.JournalID " & vbNewLine & _
                       "FROM traCost A " & vbNewLine & _
                       "INNER JOIN mstChartOfAccount COA ON " & vbNewLine & _
                       "    A.CoAID=COA.ID " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    A.ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.CompanyID = .Item("CompanyID")
                        voReturn.ID = .Item("ID")
                        voReturn.CostDate = .Item("CostDate")
                        voReturn.CoAID = .Item("CoAID")
                        voReturn.CoACode = .Item("CoACode")
                        voReturn.CoAName = .Item("CoAName")
                        voReturn.TotalAmount = .Item("TotalAmount")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.Remarks = .Item("Remarks")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.LogInc = .Item("LogInc")
                        voReturn.JournalID = .Item("JournalID")
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return voReturn
        End Function

        Public Shared Sub DeleteData(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traCost " & vbNewLine & _
                    "SET IsDeleted=1, IDStatus=@IDStatus " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Deleted
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxID(ByVal strID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traCost " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(ID,13)=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 13).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("ID") + 1
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return intReturn
        End Function

        Public Shared Function DataExists(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traCost " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolExists = True
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return bolExists
        End Function

        Public Shared Function IsDeleted(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traCost " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine & _
                        "   AND IsDeleted=1 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolExists = True
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return bolExists
        End Function

        Public Shared Function IsPostedGL(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traCost " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine & _
                        "   AND IsPostedGL=1 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolExists = True
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return bolExists
        End Function

        Public Shared Sub UpdateJournalID(ByVal strID As String, ByVal strJournalID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traCost " & vbNewLine & _
                    "SET JournalID=@JournalID " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
                .Parameters.Add("@JournalID", SqlDbType.VarChar, 20).Value = strJournalID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub PostGL(ByVal strID As String, ByVal strLogBy As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traCost SET" & vbNewLine & _
                    "   IsPostedGL=1, " & vbNewLine & _
                    "   PostedBy=@LogBy, " & vbNewLine & _
                    "   PostedDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
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
                    "UPDATE traCost SET" & vbNewLine & _
                    "   IsPostedGL=0, " & vbNewLine & _
                    "   PostedBy='' " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strCostID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.CostID, A.CoAID, C.Code AS CoACode, C.Name AS CoAName, A.Remarks, A.Amount " & vbNewLine & _
                   "FROM traCostDet A " & vbNewLine & _
                   "INNER JOIN mstChartOfAccount C ON " & vbNewLine & _
                   "    A.CoAID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CostID=@CostID" & vbNewLine

                .Parameters.Add("@CostID", SqlDbType.VarChar, 20).Value = strCostID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataDetail(ByVal clsData As VO.CostDet)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traCostDet " & vbNewLine & _
                   "    (ID, CostID, CoAID, Remarks, Amount)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @CostID, @CoAID, @Remarks, @Amount)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@CostID", SqlDbType.VarChar, 20).Value = clsData.CostID
                .Parameters.Add("@CoAID", SqlDbType.Int).Value = clsData.CoAID
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@Amount", SqlDbType.Decimal).Value = clsData.Amount
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataDetail(ByVal strCostID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traCostDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CostID=@CostID " & vbNewLine

                .Parameters.Add("@CostID", SqlDbType.VarChar, 20).Value = strCostID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDDetail(ByVal strCostID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traCostDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(CostID,17)=@CostID " & vbNewLine

                    .Parameters.Add("@CostID", SqlDbType.VarChar, 17).Value = strCostID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("ID") + 1
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return intReturn
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strCostID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.CostID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traCostStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CostID=@CostID " & vbNewLine

                .Parameters.Add("@CostID", SqlDbType.VarChar, 20).Value = strCostID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.CostStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traCostStatus " & vbNewLine & _
                   "    (ID, CostID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @CostID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@CostID", SqlDbType.VarChar, 20).Value = clsData.CostID
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

        Public Shared Function GetMaxIDStatus(ByVal strCostID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traCostStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(CostID,17)=@CostID " & vbNewLine

                    .Parameters.Add("@CostID", SqlDbType.VarChar, 17).Value = strCostID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("ID") + 1
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return intReturn
        End Function

#End Region

    End Class

End Namespace

