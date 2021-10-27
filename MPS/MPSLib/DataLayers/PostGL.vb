Namespace DL
    Public Class PostGL

        Public Shared Function ListData() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.DateFrom, A.DateTo, A.PostedBy, A.PostedDate  " & vbNewLine & _
                   "FROM sysPostGL A " & vbNewLine & _
                   "ORDER BY " & vbNewLine & _
                   "    A.ID DESC " & vbNewLine
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataForUnpost() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.DateFrom, A.DateTo, A.PostedBy, A.PostedDate " & vbNewLine & _
                   "FROM sysPostGL A " & vbNewLine & _
                   "ORDER BY " & vbNewLine & _
                   "    A.ID DESC " & vbNewLine
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal clsData As VO.PostGL)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO sysPostGL " & vbNewLine & _
                   "    (ID, DateFrom, DateTo, PostedBy, PostedDate)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @DateFrom, @DateTo, @PostedBy, GETDATE())  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = clsData.DateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = clsData.DateTo
                .Parameters.Add("@PostedBy", SqlDbType.VarChar, 20).Value = clsData.PostedBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.PostGL
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim voReturn As New VO.PostGL
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.DateFrom, A.DateTo, A.PostedBy, A.PostedDate  " & vbNewLine & _
                       "FROM sysPostGL A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.DateFrom = .Item("DateFrom")
                        voReturn.DateTo = .Item("DateTo")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return voReturn
        End Function

        Public Shared Function GetDetailLast() As VO.PostGL
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim voReturn As New VO.PostGL
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.DateFrom, A.DateTo, A.PostedBy, A.PostedDate  " & vbNewLine & _
                       "FROM sysPostGL A " & vbNewLine & _
                       "ORDER BY " & vbNewLine & _
                       "    A.DateTo DESC " & vbNewLine

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.DateFrom = .Item("DateFrom")
                        voReturn.DateTo = .Item("DateTo")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
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
                    "DELETE FROM sysPostGL " & vbNewLine & _
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
                        "FROM sysPostGL " & vbNewLine & _
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

        Public Shared Function LastPostedDate() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 20000101
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.DateTo " & vbNewLine & _
                       "FROM sysPostGL A " & vbNewLine & _
                       "ORDER BY " & vbNewLine & _
                       "    A.PostedDate DESC " & vbNewLine

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = Format(.Item("DateTo"), "yyyyMMdd")
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

