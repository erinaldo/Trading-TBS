Namespace DL
    Public Class AccountReceivable

#Region "Main"

        Public Shared Function ListData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.ARDate, A.PaymentReferencesID, D.Name AS PaymentReferencesName, A.ReferencesNote, A.TotalAmount,   " & vbNewLine & _
                   "     A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo,   " & vbNewLine & _
                   "     A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traAccountReceivable A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstPaymentReferences D ON " & vbNewLine & _
                   "    A.PaymentReferencesID=D.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ARDate>=@DateFrom AND A.ARDate<=@DateTo " & vbNewLine

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
                   "     A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.ARDate, A.PaymentReferencesID, D.Name AS PaymentReferencesName, A.ReferencesNote, A.TotalAmount,   " & vbNewLine & _
                   "     A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo,   " & vbNewLine & _
                   "     A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID, A.CoAIDOfIncomePayment  " & vbNewLine & _
                   "FROM traAccountReceivable A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstPaymentReferences D ON " & vbNewLine & _
                   "    A.PaymentReferencesID=D.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ARDate>=@DateFrom AND A.ARDate<=@DateTo " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.AccountReceivable)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traAccountReceivable " & vbNewLine & _
                       "    (CompanyID, ID, BPID, ARDate, PaymentReferencesID, ReferencesNote, TotalAmount,   " & vbNewLine & _
                       "     Remarks, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate, CoAIDOfIncomePayment)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ID, @BPID, @ARDate, @PaymentReferencesID, @ReferencesNote, @TotalAmount,   " & vbNewLine & _
                       "     @Remarks, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE(), @CoAIDOfIncomePayment)  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE traAccountReceivable SET " & vbNewLine & _
                    "    CompanyID=@CompanyID, " & vbNewLine & _
                    "    BPID=@BPID, " & vbNewLine & _
                    "    ARDate=@ARDate, " & vbNewLine & _
                    "    PaymentReferencesID=@PaymentReferencesID, " & vbNewLine & _
                    "    ReferencesNote=@ReferencesNote, " & vbNewLine & _
                    "    TotalAmount=@TotalAmount, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE(), " & vbNewLine & _
                    "    CoAIDOfIncomePayment=@CoAIDOfIncomePayment " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@ARDate", SqlDbType.DateTime).Value = clsData.ARDate
                .Parameters.Add("@PaymentReferencesID", SqlDbType.Int).Value = clsData.PaymentReferencesID
                .Parameters.Add("@ReferencesNote", SqlDbType.VarChar, 150).Value = clsData.ReferencesNote
                .Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = clsData.TotalAmount
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@CoAIDOfIncomePayment", SqlDbType.Int).Value = clsData.CoAIDOfIncomePayment
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.AccountReceivable
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim voReturn As New VO.AccountReceivable
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.ARDate, A.PaymentReferencesID, D.Name AS PaymentReferencesName, A.ReferencesNote, A.TotalAmount,   " & vbNewLine & _
                       "    A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus,   " & vbNewLine & _
                       "    A.LogBy, A.LogDate, A.LogInc, A.JournalID, A.CoAIDOfIncomePayment, COA.Code AS CoACodeOfIncomePayment, COA.Name AS CoANameOfIncomePayment " & vbNewLine & _
                       "FROM traAccountReceivable A " & vbNewLine & _
                       "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                       "    A.BPID=C.ID " & vbNewLine & _
                       "INNER JOIN mstPaymentReferences D ON " & vbNewLine & _
                       "    A.PaymentReferencesID=D.ID " & vbNewLine & _
                       "INNER JOIN mstChartOfAccount COA ON " & vbNewLine & _
                       "    A.CoAIDOfIncomePayment=COA.ID " & vbNewLine & _
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
                        voReturn.ARDate = .Item("ARDate")
                        voReturn.PaymentReferencesID = .Item("PaymentReferencesID")
                        voReturn.PaymentReferencesName = .Item("PaymentReferencesName")
                        voReturn.ReferencesNote = .Item("ReferencesNote")
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
                        voReturn.CoAIDOfIncomePayment = .Item("CoAIDOfIncomePayment")
                        voReturn.CoACodeOfIncomePayment = .Item("CoACodeOfIncomePayment")
                        voReturn.CoANameOfIncomePayment = .Item("CoANameOfIncomePayment")
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
                    "UPDATE traAccountReceivable " & vbNewLine & _
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
                        "FROM traAccountReceivable " & vbNewLine & _
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
                        "FROM traAccountReceivable " & vbNewLine & _
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
                        "FROM traAccountReceivable " & vbNewLine & _
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
                        "FROM traAccountReceivable " & vbNewLine & _
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
                    "UPDATE traAccountReceivable " & vbNewLine & _
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
                    "UPDATE traAccountReceivable SET" & vbNewLine & _
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
                    "UPDATE traAccountReceivable SET" & vbNewLine & _
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

        Public Shared Function ListDataDetailWithOutstanding(ByVal strARID As String, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(1 AS BIT) AS Pick, A.ID, A.ARID, A.SalesID, A.Amount, TS.GrandTotal-TS.TotalReturn-TS.TotalPayment+A.Amount AS MaxAmount " & vbNewLine & _
                   "FROM traAccountReceivableDet A " & vbNewLine & _
                   "INNER JOIN traSales TS ON " & vbNewLine & _
                   "    A.SalesID=TS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ARID=@ARID" & vbNewLine & _
                   "    AND TS.IsDeleted=0 " & vbNewLine

                .CommandText += _
                    "UNION ALL" & vbNewLine & _
                    "SELECT " & vbNewLine & _
                    "     CAST(0 AS BIT) AS Pick, CAST(NEWID() AS VARCHAR(40)) AS ID, CAST('' AS VARCHAR(20)) AS ARID, TS.ID AS SalesID, CAST(0 AS DECIMAL(18,2)) Amount, TS.GrandTotal-TS.TotalReturn-TS.TotalPayment AS MaxAmount " & vbNewLine & _
                    "FROM traSales TS " & vbNewLine & _
                    "WHERE  " & vbNewLine & _
                    "    TS.BPID=@BPID" & vbNewLine & _
                    "    AND TS.IsDeleted=0 " & vbNewLine & _
                    "    AND TS.ID NOT IN " & vbNewLine & _
                    "        (" & vbNewLine & _
                    "            SELECT ARD.SalesID " & vbNewLine & _
                    "            FROM traAccountReceivableDet ARD " & vbNewLine & _
                    "            INNER JOIN traAccountReceivable AR ON " & vbNewLine & _
                    "                ARD.ARID=AR.ID " & vbNewLine & _
                    "            WHERE " & vbNewLine & _
                    "                AR.IsDeleted=0 " & vbNewLine & _
                    "                AND AR.BPID=@BPID " & vbNewLine & _
                    "        )" & vbNewLine

                .Parameters.Add("@ARID", SqlDbType.VarChar, 20).Value = strARID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataDetail(ByVal strARID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(1 AS BIT) AS Pick, A.ID, A.ARID, A.SalesID, A.Amount, TS.GrandTotal-TS.TotalReturn-TS.TotalPayment+A.Amount AS MaxAmount " & vbNewLine & _
                   "FROM traAccountReceivableDet A " & vbNewLine & _
                   "INNER JOIN traSales TS ON " & vbNewLine & _
                   "    A.SalesID=TS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ARID=@ARID" & vbNewLine

                .Parameters.Add("@ARID", SqlDbType.VarChar, 20).Value = strARID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataDetailOutstanding(ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(0 AS BIT) AS Pick, CAST(NEWID() AS VARCHAR(40)) AS ID, CAST('' AS VARCHAR(20)) AS ARID, TS.ID AS SalesID, CAST(0 AS DECIMAL(18,2)) Amount, TS.GrandTotal-TS.TotalReturn-TS.TotalPayment AS MaxAmount " & vbNewLine & _
                   "FROM traSales TS " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    TS.BPID=@BPID" & vbNewLine & _
                   "    AND TS.IsDeleted=0 " & vbNewLine & _
                   "    AND TS.GrandTotal-TS.TotalReturn-TS.TotalPayment>0 " & vbNewLine

                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataDetail(ByVal clsData As VO.AccountReceivableDet)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traAccountReceivableDet " & vbNewLine & _
                   "    (ID, ARID, SalesID, Amount)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @ARID, @SalesID, @Amount)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@ARID", SqlDbType.VarChar, 20).Value = clsData.ARID
                .Parameters.Add("@SalesID", SqlDbType.VarChar, 20).Value = clsData.SalesID
                .Parameters.Add("@Amount", SqlDbType.Decimal).Value = clsData.Amount
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataDetail(ByVal strARID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traAccountReceivableDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ARID=@ARID " & vbNewLine

                .Parameters.Add("@ARID", SqlDbType.VarChar, 20).Value = strARID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDDetail(ByVal strARID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traAccountReceivableDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(ARID,17)=@ARID " & vbNewLine

                    .Parameters.Add("@ARID", SqlDbType.VarChar, 17).Value = strARID
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

        Public Shared Function AlreadyCreatedInvoice(ByVal strSalesID As String) As String
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim strReturn As String = ""
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1	" & vbNewLine & _
                        "	A.ID 	" & vbNewLine & _
                        "FROM traAccountReceivable A	" & vbNewLine & _
                        "INNER JOIN traAccountReceivableDet B ON 	" & vbNewLine & _
                        "	A.ID=B.ARID 	" & vbNewLine & _
                        "WHERE 	" & vbNewLine & _
                        "	A.IsDeleted=0 	" & vbNewLine & _
                        "	AND B.SalesID=@SalesID	" & vbNewLine

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

        Public Shared Function ListDataStatus(ByVal strARID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.ARID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traAccountReceivableStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ARID=@ARID " & vbNewLine

                .Parameters.Add("@ARID", SqlDbType.VarChar, 20).Value = strARID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.AccountReceivableStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traAccountReceivableStatus " & vbNewLine & _
                   "    (ID, ARID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @ARID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@ARID", SqlDbType.VarChar, 20).Value = clsData.ARID
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

        Public Shared Function GetMaxIDStatus(ByVal strARID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traAccountReceivableStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(ARID,17)=@ARID " & vbNewLine

                    .Parameters.Add("@ARID", SqlDbType.VarChar, 17).Value = strARID

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

