Namespace DL
    Public Class SalesReturn

#Region "Main"

        Public Shared Function ListData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.SalesReturnDate, A.PaymentTerm, A.DueDate, A.SubTotal,   " & vbNewLine & _
                   "    A.TotalDiscount, A.TotalTax, A.GrandTotal, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traSalesReturn A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.SalesReturnDate>=@DateFrom AND A.SalesReturnDate<=@DateTo " & vbNewLine

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
                   "    A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.SalesReturnDate, A.PaymentTerm, A.DueDate, A.SubTotal,   " & vbNewLine & _
                   "    A.TotalDiscount, A.TotalTax, A.GrandTotal, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traSalesReturn A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.SalesReturnDate>=@DateFrom AND A.SalesReturnDate<=@DateTo " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.SalesReturn)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traSalesReturn " & vbNewLine & _
                       "    (CompanyID, ID, BPID, SalesReturnDate, PaymentTerm, DueDate, SubTotal,   " & vbNewLine & _
                       "      TotalDiscount, TotalTax, GrandTotal, Remarks, IDStatus, CreatedBy,   " & vbNewLine & _
                       "      CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ID, @BPID, @SalesReturnDate, @PaymentTerm, @DueDate, @SubTotal,   " & vbNewLine & _
                       "      @TotalDiscount, @TotalTax, @GrandTotal, @Remarks, @IDStatus, @LogBy,   " & vbNewLine & _
                       "      GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                        "UPDATE traSalesReturn SET " & vbNewLine & _
                        "    CompanyID=@CompanyID, " & vbNewLine & _
                        "    BPID=@BPID, " & vbNewLine & _
                        "    SalesReturnDate=@SalesReturnDate, " & vbNewLine & _
                        "    PaymentTerm=@PaymentTerm, " & vbNewLine & _
                        "    DueDate=@DueDate, " & vbNewLine & _
                        "    SubTotal=@SubTotal, " & vbNewLine & _
                        "    TotalDiscount=@TotalDiscount, " & vbNewLine & _
                        "    TotalTax=@TotalTax, " & vbNewLine & _
                        "    GrandTotal=@GrandTotal, " & vbNewLine & _
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
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@SalesReturnDate", SqlDbType.DateTime).Value = clsData.SalesReturnDate
                .Parameters.Add("@PaymentTerm", SqlDbType.Int).Value = clsData.PaymentTerm
                .Parameters.Add("@DueDate", SqlDbType.DateTime).Value = clsData.DueDate
                .Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = clsData.SubTotal
                .Parameters.Add("@TotalDiscount", SqlDbType.Decimal).Value = clsData.TotalDiscount
                .Parameters.Add("@TotalTax", SqlDbType.Decimal).Value = clsData.TotalTax
                .Parameters.Add("@GrandTotal", SqlDbType.Decimal).Value = clsData.GrandTotal
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

        Public Shared Function GetDetail(ByVal strID As String) As VO.SalesReturn
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim voReturn As New VO.SalesReturn
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.SalesReturnDate, A.PaymentTerm, A.DueDate, A.SubTotal,   " & vbNewLine & _
                       "    A.TotalDiscount, A.TotalTax, A.GrandTotal, A.IsPostedGL,   " & vbNewLine & _
                       "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.LogBy,   " & vbNewLine & _
                       "    A.LogDate, A.LogInc, A.JournalID  " & vbNewLine & _
                       "FROM traSalesReturn A " & vbNewLine & _
                       "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                       "    A.BPID=C.ID " & vbNewLine & _
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
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPName = .Item("BPName")
                        voReturn.SalesReturnDate = .Item("SalesReturnDate")
                        voReturn.PaymentTerm = .Item("PaymentTerm")
                        voReturn.DueDate = .Item("DueDate")
                        voReturn.SubTotal = .Item("SubTotal")
                        voReturn.TotalDiscount = .Item("TotalDiscount")
                        voReturn.TotalTax = .Item("TotalTax")
                        voReturn.GrandTotal = .Item("GrandTotal")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.Remarks = .Item("Remarks")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogInc = .Item("LogInc")
                        voReturn.LogDate = .Item("LogDate")
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
                    "UPDATE traSalesReturn " & vbNewLine & _
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
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesReturn " & vbNewLine & _
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
                        "FROM traSalesReturn " & vbNewLine & _
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
                        "FROM traSalesReturn " & vbNewLine & _
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
                        "FROM traSalesReturn " & vbNewLine & _
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
                    "UPDATE traSalesReturn " & vbNewLine & _
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

        Public Shared Function ListDataHistoryBussinessPartners(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    SH.CompanyID, 'RETUR PENJUALAN' AS Trans, SH.ID, SH.SalesReturnDate AS TransactionDate, SH.BPID, BP.Name AS BPName, A.Qty, A.UomID, C.Code AS UomCode, A.Price, A.Disc,   " & vbNewLine & _
                   "    A.Tax, A.TotalPrice, A.Remarks, SH.IDStatus, MS.Name AS StatusInfo, SH.CreatedBy, SH.CreatedDate, SH.LogInc, SH.LogBy, SH.LogDate, SH.JournalID  " & vbNewLine & _
                   "FROM traSalesReturnDet A " & vbNewLine & _
                   "INNER JOIN traSalesReturn SH ON " & vbNewLine & _
                   "    A.SalesReturnID=SH.ID " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    A.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    A.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    SH.IDStatus=MS.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner BP ON " & vbNewLine & _
                   "    SH.BPID=BP.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    SH.SalesReturnDate>=@DateFrom AND SH.SalesReturnDate<=@DateTo " & vbNewLine & _
                   "    AND SH.IsDeleted=0 " & vbNewLine & _
                   "    AND A.ItemID=@ItemID " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataHistoryItem(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    SH.CompanyID, 'RETUR PENJUALAN' AS Trans, SH.ID, SH.SalesReturnDate AS TransactionDate, A.ItemID, B.Code AS ItemCode, B.Name AS ItemName, A.Qty, A.UomID, C.Code AS UomCode, A.Price, A.Disc,   " & vbNewLine & _
                   "    A.Tax, A.TotalPrice, A.Remarks, SH.IDStatus, MS.Name AS StatusInfo, SH.CreatedBy, SH.CreatedDate, SH.LogInc, SH.LogBy, SH.LogDate, SH.JournalID  " & vbNewLine & _
                   "FROM traSalesReturnDet A " & vbNewLine & _
                   "INNER JOIN traSalesReturn SH ON " & vbNewLine & _
                   "    A.SalesReturnID=SH.ID " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    A.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    A.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    SH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    SH.SalesReturnDate>=@DateFrom AND SH.SalesReturnDate<=@DateTo " & vbNewLine & _
                   "    AND SH.IsDeleted=0 " & vbNewLine & _
                   "    AND SH.BPID=@BPID " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub PostGL(ByVal strID As String, ByVal strLogBy As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSalesReturn SET" & vbNewLine & _
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
                    "UPDATE traSalesReturn SET" & vbNewLine & _
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

        Public Shared Function ListDataDetail(ByVal strSalesReturnID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.ID, A.SalesReturnID, A.SalesDetID, AA.SalesID, A.ItemID, B.Code AS ItemCode, B.Name AS ItemName, A.Qty, AA.Qty-AA.ReturnQty+A.Qty AS MaxQty, A.UomID, C.Code AS UomCode, A.Price, A.Disc,   " & vbNewLine & _
                   "    A.Tax, A.TotalPrice, A.Remarks  " & vbNewLine & _
                   "FROM traSalesReturnDet A " & vbNewLine & _
                   "INNER JOIN traSalesDet AA ON " & vbNewLine & _
                   "    A.SalesDetID=AA.ID " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    A.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    A.UomID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.SalesReturnID=@SalesReturnID" & vbNewLine

                .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 20).Value = strSalesReturnID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataSalesID(ByVal strSalesReturnID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	SD.SalesID " & vbNewLine & _
                    "FROM traSalesReturnDet SRD	" & vbNewLine & _
                    "INNER JOIN traSalesReturn SR ON 	" & vbNewLine & _
                    "	SRD.SalesReturnID=SR.ID 	" & vbNewLine & _
                    "INNER JOIN traSalesDet SD ON 	" & vbNewLine & _
                    "	SRD.SalesDetID=SD.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	SR.IsDeleted=0 	" & vbNewLine & _
                    "	AND SR.ID=@ID 	" & vbNewLine & _
                    "GROUP BY SD.SalesID	" & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strSalesReturnID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataDetail(ByVal clsData As VO.SalesReturnDet)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "INSERT INTO traSalesReturnDet " & vbNewLine & _
                    "    (ID, SalesReturnID, SalesDetID, ItemID, UomID, Qty, Price, Disc,   " & vbNewLine & _
                    "     Tax, TotalPrice, Remarks)   " & vbNewLine & _
                    "VALUES " & vbNewLine & _
                    "    (@ID, @SalesReturnID, @SalesDetID, @ItemID, @UomID, @Qty, @Price, @Disc,   " & vbNewLine & _
                    "     @Tax, @TotalPrice, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 20).Value = clsData.SalesReturnID
                .Parameters.Add("@SalesDetID", SqlDbType.VarChar, 20).Value = clsData.SalesDetID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@UomID", SqlDbType.Int).Value = clsData.UomID
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = clsData.Qty
                .Parameters.Add("@Price", SqlDbType.Decimal).Value = clsData.Price
                .Parameters.Add("@Disc", SqlDbType.Decimal).Value = clsData.Disc
                .Parameters.Add("@Tax", SqlDbType.Decimal).Value = clsData.Tax
                .Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = clsData.TotalPrice
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataDetail(ByVal strSalesReturnID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traSalesReturnDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   SalesReturnID=@SalesReturnID " & vbNewLine

                .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 20).Value = strSalesReturnID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDDetail(ByVal strSalesReturnID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesReturnDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(SalesReturnID,17)=@SalesReturnID " & vbNewLine

                    .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 20).Value = strSalesReturnID
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

        Public Shared Function AlreadyCreatedReturn(ByVal strSalesID As String) As String
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim strReturn As String = ""
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "	SR.ID 	" & vbNewLine & _
                        "FROM traSalesReturn SR 	" & vbNewLine & _
                        "INNER JOIN traSalesReturnDet SRD ON 	" & vbNewLine & _
                        "	SR.ID=SRD.SalesReturnID 	" & vbNewLine & _
                        "INNER JOIN traSalesDet SD ON 	" & vbNewLine & _
                        "	SRD.SalesDetID=SD.ID 	" & vbNewLine & _
                        "WHERE 	" & vbNewLine & _
                        "	SD.SalesID=@SalesID 	" & vbNewLine & _
                        "	AND SR.IsDeleted=0	" & vbNewLine

                    .Parameters.Add("@SalesID", SqlDbType.VarChar, 20).Value = strSalesID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        strReturn = .Item("ID")
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return strReturn
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesReturnID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.SalesReturnID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traSalesReturnStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.SalesReturnID=@SalesReturnID " & vbNewLine

                .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 20).Value = strSalesReturnID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.SalesReturnStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traSalesReturnStatus " & vbNewLine & _
                   "    (ID, SalesReturnID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @SalesReturnID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 20).Value = clsData.SalesReturnID
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

        Public Shared Function GetMaxIDStatus(ByVal strSalesReturnID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesReturnStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(SalesReturnID,17)=@SalesReturnID " & vbNewLine

                    .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 17).Value = strSalesReturnID

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