Namespace DL
    Public Class Program

        Public Shared Function ListData() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(0 AS BIT) AS Pick, A.ID AS ProgramID, A.Name AS ProgramName, A.IsDeleted, A.CreatedBy, A.CreatedDate, A.LogBy,   " & vbNewLine & _
                   "     A.LogDate  " & vbNewLine & _
                   "FROM mstProgram A " & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Program)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO mstProgram " & vbNewLine & _
                       "    (ID, Name, CreatedBy, CreatedDate, LogBy,   " & vbNewLine & _
                       "      LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @Name, @LogBy, GETDATE(), @LogBy,   " & vbNewLine & _
                       "      GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE mstProgram SET " & vbNewLine & _
                    "    Name=@Name, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = clsData.Name
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.Program
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.Program
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.Name, A.IsDeleted, A.LogBy, A.LogDate  " & vbNewLine & _
                       "FROM mstProgram A " & vbNewLine & _
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
                        voReturn.Name = .Item("Name")
                        voReturn.IsDeleted = .Item("IsDeleted")
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
                    "UPDATE mstProgram " & vbNewLine & _
                    "SET IsDeleted=1 " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = intID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxID() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM mstProgram " & vbNewLine

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
                        "FROM mstProgram " & vbNewLine & _
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

        Public Shared Function IsDeleted(ByVal intID As Integer) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolIsDeleted As Integer = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   IsDeleted " & vbNewLine & _
                        "FROM mstProgram " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolIsDeleted = .Item("IsDeleted")
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return bolIsDeleted
        End Function

    End Class

End Namespace

