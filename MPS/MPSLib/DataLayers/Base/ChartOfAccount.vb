Namespace DL
    Public Class ChartOfAccount
        Public Shared Function ListData(ByVal enumFilterGroup As VO.ChartOfAccount.FilterGroup) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	COA.ID, COA.AccountGroupID, COAG.Name AS AccountGroupName, COAG.AliasName AS GroupAccount, COA.Code, COA.Name, 	" & vbNewLine & _
                    "	Balance=ISNULL(	" & vbNewLine & _
                    "	COA.FirstBalance+	" & vbNewLine & _
                    "	CASE WHEN 	" & vbNewLine & _
                    "		COA.AccountGroupID=1 OR COA.AccountGroupID=2 OR COA.AccountGroupID=3 OR COA.AccountGroupID=4 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=5 OR COA.AccountGroupID=6 OR COA.AccountGroupID=7 OR COA.AccountGroupID=17 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=18 OR COA.AccountGroupID=13 OR COA.AccountGroupID=14 OR COA.AccountGroupID=15 OR COA.AccountGroupID=16 	" & vbNewLine & _
                    "	THEN ISNULL(JH.TotalDebit,0)-ISNULL(JH.TotalCredit,0) " & vbNewLine & _
                    "	ELSE ISNULL(JH.TotalCredit,0)-ISNULL(JH.TotalDebit,0) " & vbNewLine & _
                    "	END,0),	" & vbNewLine & _
                    "	COA.IDStatus, MS.Name AS StatusInfo, COA.CreatedBy, COA.CreatedDate, COA.LogBy, COA.LogDate, COA.LogInc 	" & vbNewLine & _
                    "FROM mstChartOfAccount COA 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccountGroup COAG ON  	" & vbNewLine & _
                    "    COA.AccountGroupID=COAG.ID 	" & vbNewLine & _
                    "INNER JOIN mstStatus MS ON  	" & vbNewLine & _
                    "    COA.IDStatus=MS.ID 	" & vbNewLine & _
                    "LEFT JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		JD.CoAID, SUM(JD.DebitAmount) AS TotalDebit, SUM(JD.CreditAmount) AS TotalCredit	" & vbNewLine & _
                    "	FROM traJournalDet JD 	" & vbNewLine & _
                    "	INNER JOIN traJournal JH ON 	" & vbNewLine & _
                    "		JD.JournalID=JH.ID 	" & vbNewLine & _
                    "	WHERE 	" & vbNewLine & _
                    "		JH.IsDeleted=0 	" & vbNewLine & _
                    "	GROUP BY JD.CoAID 	" & vbNewLine & _
                    ") JH ON COA.ID=JH.CoAID	" & vbNewLine

                If enumFilterGroup = VO.ChartOfAccount.FilterGroup.CashOrBank Then
                    .CommandText += "WHERE COAG.ID=1"
                ElseIf enumFilterGroup = VO.ChartOfAccount.FilterGroup.Expense Then
                    .CommandText += "WHERE COAG.ID IN (14,15) "
                End If

                .CommandText += _
                    "ORDER BY " & vbNewLine & _
                    "    COA.Code ASC " & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.ChartOfAccount)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO mstChartOfAccount " & vbNewLine & _
                       "    (ID, AccountGroupID, Code, Name, FirstBalance, FirstBalanceDate, IDStatus,   " & vbNewLine & _
                       "      CreatedBy, CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @AccountGroupID, @Code, @Name, @FirstBalance, @FirstBalanceDate, @IDStatus,   " & vbNewLine & _
                       "      @LogBy, GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE mstChartOfAccount SET " & vbNewLine & _
                    "    AccountGroupID=@AccountGroupID, " & vbNewLine & _
                    "    Code=@Code, " & vbNewLine & _
                    "    Name=@Name, " & vbNewLine & _
                    "    FirstBalance=@FirstBalance, " & vbNewLine & _
                    "    FirstBalanceDate=@FirstBalanceDate, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@AccountGroupID", SqlDbType.Int).Value = clsData.AccountGroupID
                .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = clsData.Code
                .Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = clsData.Name
                .Parameters.Add("@FirstBalance", SqlDbType.Decimal).Value = clsData.FirstBalance
                .Parameters.Add("@FirstBalanceDate", SqlDbType.DateTime).Value = clsData.FirstBalanceDate
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.ChartOfAccount
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.ChartOfAccount
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.AccountGroupID, A.Code, A.Name, A.FirstBalance, A.FirstBalanceDate, A.IDStatus,   " & vbNewLine & _
                       "    A.LogBy, A.LogDate  " & vbNewLine & _
                       "FROM mstChartOfAccount A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.AccountGroupID = .Item("AccountGroupID")
                        voReturn.Code = .Item("Code")
                        voReturn.Name = .Item("Name")
                        voReturn.FirstBalance = .Item("FirstBalance")
                        voReturn.FirstBalanceDate = .Item("FirstBalanceDate")
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

        Public Shared Sub DeleteData(ByVal intID As Integer)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE mstChartOfAccount " & vbNewLine & _
                    "SET IDStatus=@IDStatus " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = intID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.InActive
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxID() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM mstChartOfAccount " & vbNewLine

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

        Public Shared Function DataExists(ByVal intID As Integer) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM mstChartOfAccount " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID

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

        Public Shared Function CodeExists(ByVal strCode As String, ByVal intID As Integer) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM mstChartOfAccount " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   Code=@Code " & vbNewLine & _
                        "   AND ID<>@ID " & vbNewLine

                    .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = strCode
                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID
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

        Public Shared Function GetIDStatus(ByVal intID As Integer) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = VO.Status.Values.Active
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   IDStatus " & vbNewLine & _
                        "FROM mstChartOfAccount " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("IDStatus")
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

    End Class

End Namespace

