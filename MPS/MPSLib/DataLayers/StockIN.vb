Namespace DL
    Public Class StockIN
        Public Shared Function ListData() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.ItemID, A.ReferencesIDDetail, A.ReferencesID, A.ReferencesDate, A.Qty, A.Price,   " & vbNewLine & _
                   "     A.NetPrice, A.QtyOut, A.LogDate  " & vbNewLine & _
                   "FROM sysStockIN A " & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataByReferencesID(ByVal strReferencesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.ItemID, A.ReferencesIDDetail, A.ReferencesID, A.ReferencesDate, A.Qty, A.Price,   " & vbNewLine & _
                   "     A.NetPrice, A.QtyOut, A.LogDate  " & vbNewLine & _
                   "FROM sysStockIN A " & vbNewLine & _
                   "WHERE " & vbNewLine & _
                   "    A.ReferencesID=@ReferencesID " & vbNewLine

                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 20).Value = strReferencesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataOutstanding(ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.ItemID, A.ReferencesIDDetail, A.ReferencesID, A.ReferencesDate, A.Qty, A.Price,   " & vbNewLine & _
                   "     A.NetPrice, A.QtyOut, A.LogDate  " & vbNewLine & _
                   "FROM sysStockIN A " & vbNewLine & _
                   "WHERE " & vbNewLine & _
                   "    A.ItemID=@ItemID " & vbNewLine & _
                   "    AND A.Qty-QtyOut>0  " & vbNewLine & _
                   "ORDER BY A.ID ASC " & vbNewLine

                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataByReferencesIDDetail(ByVal strReferencesIDDetail As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	SI.ID, SO.Qty, SI.Price, SI.NetPrice 	" & vbNewLine & _
                    "FROM sysStockOut SO 	" & vbNewLine & _
                    "INNER JOIN sysStockIN SI ON 	" & vbNewLine & _
                    "	SO.IDStockIN=SI.ID 	" & vbNewLine & _
                    "WHERE SO.ReferencesIDDetail=@ReferencesIDDetail	" & vbNewLine & _
                    "ORDER BY SO.Qty DESC " & vbNewLine

                .Parameters.Add("@ReferencesIDDetail", SqlDbType.VarChar, 20).Value = strReferencesIDDetail
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.StockIN)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO sysStockIN " & vbNewLine & _
                       "    (ID, ItemID, ReferencesIDDetail, ReferencesID, ReferencesDate, Qty, Price,   " & vbNewLine & _
                       "      NetPrice, QtyOut, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @ItemID, @ReferencesIDDetail, @ReferencesID, @ReferencesDate, @Qty, @Price,   " & vbNewLine & _
                       "      @NetPrice, @QtyOut, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE sysStockIN SET " & vbNewLine & _
                    "    ItemID=@ItemID, " & vbNewLine & _
                    "    ReferencesIDDetail=@ReferencesIDDetail, " & vbNewLine & _
                    "    ReferencesID=@ReferencesID, " & vbNewLine & _
                    "    ReferencesDate=@ReferencesDate, " & vbNewLine & _
                    "    Qty=@Qty, " & vbNewLine & _
                    "    Price=@Price, " & vbNewLine & _
                    "    NetPrice=@NetPrice, " & vbNewLine & _
                    "    QtyOut=@QtyOut, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.BigInt).Value = clsData.ID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ReferencesIDDetail", SqlDbType.VarChar, 20).Value = clsData.ReferencesIDDetail
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 20).Value = clsData.ReferencesID
                .Parameters.Add("@ReferencesDate", SqlDbType.DateTime).Value = clsData.ReferencesDate
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = clsData.Qty
                .Parameters.Add("@Price", SqlDbType.Decimal).Value = clsData.Price
                .Parameters.Add("@NetPrice", SqlDbType.Decimal).Value = clsData.NetPrice
                .Parameters.Add("@QtyOut", SqlDbType.Decimal).Value = clsData.QtyOut
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub UpdateQtyStockOut(ByVal intID As Integer, ByVal decQtyOut As Decimal)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                "UPDATE sysStockIN SET " & vbNewLine & _
                "    QtyOut=@QtyOut, " & vbNewLine & _
                "    LogDate=GETDATE() " & vbNewLine & _
                "WHERE " & vbNewLine & _
                "    ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.BigInt).Value = intID
                .Parameters.Add("@QtyOut", SqlDbType.Decimal).Value = decQtyOut
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DecreaseQtyStockOut(ByVal intID As Integer, ByVal decQty As Decimal)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                "UPDATE sysStockIN SET " & vbNewLine & _
                "    QtyOut=QtyOut-@Qty, " & vbNewLine & _
                "    LogDate=GETDATE() " & vbNewLine & _
                "WHERE " & vbNewLine & _
                "    ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.BigInt).Value = intID
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = decQty
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
                    "DELETE FROM sysStockIN " & vbNewLine & _
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
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM sysStockIN " & vbNewLine

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

        Public Shared Function GetLastPriceByItemID() As Decimal
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim decReturn As Decimal = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   NetPrice " & vbNewLine & _
                        "FROM sysStockIN " & vbNewLine & _
                        "ORDER BY LogDate DESC " & vbNewLine

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        decReturn = .Item("NetPrice")
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return decReturn
        End Function

    End Class

End Namespace

