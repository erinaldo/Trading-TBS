Namespace DL
    Public Class StockOut
        Public Shared Function ListData() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.IDStockIN, A.ItemID, A.ReferencesIDDetail, A.ReferencesID, A.ReferencesDate, A.Qty,   " & vbNewLine & _
                   "     A.LogDate  " & vbNewLine & _
                   "FROM sysStockOut A " & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataByReferencesID(ByVal strReferencesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.IDStockIN, A.ItemID, A.ReferencesIDDetail, A.ReferencesID, A.ReferencesDate, A.Qty,   " & vbNewLine & _
                   "     A.LogDate  " & vbNewLine & _
                   "FROM sysStockOut A " & vbNewLine & _
                   "WHERE " & vbNewLine & _
                   "    A.ReferencesID=@ReferencesID " & vbNewLine

                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 20).Value = strReferencesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal clsData As VO.StockOut)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO sysStockOut " & vbNewLine & _
                   "    (ID, IDStockIN, ItemID, ReferencesIDDetail, ReferencesID, ReferencesDate, Qty,   " & vbNewLine & _
                   "      LogDate)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @IDStockIN, @ItemID, @ReferencesIDDetail, @ReferencesID, @ReferencesDate, @Qty,   " & vbNewLine & _
                   "      GETDATE())  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.BigInt).Value = clsData.ID
                .Parameters.Add("@IDStockIN", SqlDbType.Int).Value = clsData.IDStockIN
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ReferencesIDDetail", SqlDbType.VarChar, 20).Value = clsData.ReferencesIDDetail
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@ReferencesDate", SqlDbType.DateTime).Value = clsData.ReferencesDate
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = clsData.Qty
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteData(ByVal intID As Integer)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM sysStockOut " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.BigInt).Value = intID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataByReferencesID(ByVal strReferencesID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM sysStockOut " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ReferencesID=@ReferencesID " & vbNewLine

                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 20).Value = strReferencesID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxID() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM sysStockOut " & vbNewLine 

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

    End Class

End Namespace

