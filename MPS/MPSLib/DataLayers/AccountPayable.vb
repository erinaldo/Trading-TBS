Namespace DL
    Public Class AccountPayable

#Region "Main"

        Public Shared Function ListData(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.APDate, A.PaymentReferencesID, D.Name AS PaymentReferencesName, A.ReferencesNote, A.TotalAmount,   " & vbNewLine & _
                   "     A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo,   " & vbNewLine & _
                   "     A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traAccountPayable A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstPaymentReferences D ON " & vbNewLine & _
                   "    A.PaymentReferencesID=D.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.APDate>=@DateFrom AND A.APDate<=@DateTo " & vbNewLine

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
                   "     A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.APDate, A.PaymentReferencesID, D.Name AS PaymentReferencesName, A.ReferencesNote, A.TotalAmount,   " & vbNewLine & _
                   "     A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo,   " & vbNewLine & _
                   "     A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID, A.CoAIDOfOutgoingPayment  " & vbNewLine & _
                   "FROM traAccountPayable A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstPaymentReferences D ON " & vbNewLine & _
                   "    A.PaymentReferencesID=D.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.APDate>=@DateFrom AND A.APDate<=@DateTo " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.AccountPayable)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traAccountPayable " & vbNewLine & _
                       "    (CompanyID, ID, BPID, APDate, PaymentReferencesID, ReferencesNote, TotalAmount,   " & vbNewLine & _
                       "     Remarks, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate, CoAIDOfOutgoingPayment)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ID, @BPID, @APDate, @PaymentReferencesID, @ReferencesNote, @TotalAmount,   " & vbNewLine & _
                       "     @Remarks, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE(), @CoAIDOfOutgoingPayment)  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE traAccountPayable SET " & vbNewLine & _
                    "    CompanyID=@CompanyID, " & vbNewLine & _
                    "    BPID=@BPID, " & vbNewLine & _
                    "    APDate=@APDate, " & vbNewLine & _
                    "    PaymentReferencesID=@PaymentReferencesID, " & vbNewLine & _
                    "    ReferencesNote=@ReferencesNote, " & vbNewLine & _
                    "    TotalAmount=@TotalAmount, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE(), " & vbNewLine & _
                    "    CoAIDOfOutgoingPayment=@CoAIDOfOutgoingPayment " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@APDate", SqlDbType.DateTime).Value = clsData.APDate
                .Parameters.Add("@PaymentReferencesID", SqlDbType.Int).Value = clsData.PaymentReferencesID
                .Parameters.Add("@ReferencesNote", SqlDbType.VarChar, 150).Value = clsData.ReferencesNote
                .Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = clsData.TotalAmount
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@CoAIDOfOutgoingPayment", SqlDbType.Int).Value = clsData.CoAIDOfOutgoingPayment
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.AccountPayable
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim voReturn As New VO.AccountPayable
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.APDate, A.PaymentReferencesID, D.Name AS PaymentReferencesName, A.ReferencesNote, A.TotalAmount,   " & vbNewLine & _
                       "    A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus,   " & vbNewLine & _
                       "    A.LogBy, A.LogDate, A.LogInc, A.JournalID, A.CoAIDOfOutgoingPayment, COA.Code AS CoACodeOfOutgoingPayment, COA.Name AS CoANameOfOutgoingPayment " & vbNewLine & _
                       "FROM traAccountPayable A " & vbNewLine & _
                       "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                       "    A.BPID=C.ID " & vbNewLine & _
                       "INNER JOIN mstPaymentReferences D ON " & vbNewLine & _
                       "    A.PaymentReferencesID=D.ID " & vbNewLine & _
                       "INNER JOIN mstChartOfAccount COA ON " & vbNewLine & _
                       "    A.CoAIDOfOutgoingPayment=COA.ID " & vbNewLine & _
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
                        voReturn.APDate = .Item("APDate")
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
                        voReturn.CoAIDOfOutgoingPayment = .Item("CoAIDOfOutgoingPayment")
                        voReturn.CoACodeOfOutgoingPayment = .Item("CoACodeOfOutgoingPayment")
                        voReturn.CoANameOfOutgoingPayment = .Item("CoANameOfOutgoingPayment")
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
                    "UPDATE traAccountPayable " & vbNewLine & _
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
                        "FROM traAccountPayable " & vbNewLine & _
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
                        "FROM traAccountPayable " & vbNewLine & _
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
                        "FROM traAccountPayable " & vbNewLine & _
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
                        "FROM traAccountPayable " & vbNewLine & _
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
                    "UPDATE traAccountPayable " & vbNewLine & _
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
                    "UPDATE traAccountPayable SET" & vbNewLine & _
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
                    "UPDATE traAccountPayable SET" & vbNewLine & _
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

        Public Shared Function ListDataDetailWithOutstanding(ByVal strAPID As String, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(1 AS BIT) AS Pick, A.ID, A.APID, A.ReceiveID, A.Amount, TS.GrandTotal-TS.TotalReturn-TS.TotalPayment+A.Amount AS MaxAmount " & vbNewLine & _
                   "FROM traAccountPayableDet A " & vbNewLine & _
                   "INNER JOIN traReceive TS ON " & vbNewLine & _
                   "    A.ReceiveID=TS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.APID=@APID" & vbNewLine & _
                   "    AND TS.IsDeleted=0 " & vbNewLine

                .CommandText += _
                    "UNION ALL" & vbNewLine & _
                    "SELECT " & vbNewLine & _
                    "     CAST(0 AS BIT) AS Pick, CAST(NEWID() AS VARCHAR(40)) AS ID, CAST('' AS VARCHAR(20)) AS APID, TS.ID AS ReceiveID, CAST(0 AS DECIMAL(18,2)) Amount, TS.GrandTotal-TS.TotalReturn-TS.TotalPayment AS MaxAmount " & vbNewLine & _
                    "FROM traReceive TS " & vbNewLine & _
                    "WHERE  " & vbNewLine & _
                    "    TS.BPID=@BPID" & vbNewLine & _
                    "    AND TS.IsDeleted=0 " & vbNewLine & _
                    "    AND TS.ID NOT IN " & vbNewLine & _
                    "        (" & vbNewLine & _
                    "            SELECT ARD.ReceiveID " & vbNewLine & _
                    "            FROM traAccountPayableDet ARD " & vbNewLine & _
                    "            INNER JOIN traAccountPayable AR ON " & vbNewLine & _
                    "                ARD.APID=AR.ID " & vbNewLine & _
                    "            WHERE " & vbNewLine & _
                    "                AR.IsDeleted=0 " & vbNewLine & _
                    "                AND AR.BPID=@BPID " & vbNewLine & _
                    "        )" & vbNewLine

                .Parameters.Add("@APID", SqlDbType.VarChar, 20).Value = strAPID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataDetail(ByVal strAPID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(1 AS BIT) AS Pick, A.ID, A.APID, A.ReceiveID, A.Amount, TS.GrandTotal-TS.TotalReturn-TS.TotalPayment AS MaxAmount " & vbNewLine & _
                   "FROM traAccountPayableDet A " & vbNewLine & _
                   "INNER JOIN traReceive TS ON " & vbNewLine & _
                   "    A.ReceiveID=TS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.APID=@APID" & vbNewLine

                .Parameters.Add("@APID", SqlDbType.VarChar, 20).Value = strAPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataDetailOutstanding(ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(0 AS BIT) AS Pick, CAST(NEWID() AS VARCHAR(40)) AS ID, CAST('' AS VARCHAR(20)) AS APID, TS.ID AS ReceiveID, CAST(0 AS DECIMAL(18,2)) Amount, TS.GrandTotal-TS.TotalReturn-TS.TotalPayment AS MaxAmount " & vbNewLine & _
                   "FROM traReceive TS " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    TS.BPID=@BPID" & vbNewLine & _
                   "    AND TS.IsDeleted=0 " & vbNewLine & _
                   "    AND TS.GrandTotal-TS.TotalReturn-TS.TotalPayment>0 " & vbNewLine

                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataDetail(ByVal clsData As VO.AccountPayableDet)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traAccountPayableDet " & vbNewLine & _
                   "    (ID, APID, ReceiveID, Amount)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @APID, @ReceiveID, @Amount)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@APID", SqlDbType.VarChar, 20).Value = clsData.APID
                .Parameters.Add("@ReceiveID", SqlDbType.VarChar, 20).Value = clsData.ReceiveID
                .Parameters.Add("@Amount", SqlDbType.Decimal).Value = clsData.Amount
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataDetail(ByVal strAPID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traAccountPayableDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   APID=@APID " & vbNewLine

                .Parameters.Add("@APID", SqlDbType.VarChar, 20).Value = strAPID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDDetail(ByVal strAPID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traAccountPayableDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(APID,17)=@APID " & vbNewLine

                    .Parameters.Add("@APID", SqlDbType.VarChar, 17).Value = strAPID
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

        Public Shared Function AlreadyCreatedInvoice(ByVal strReceiveID As String) As String
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim strReturn As String = ""
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1	" & vbNewLine & _
                        "	A.ID 	" & vbNewLine & _
                        "FROM traAccountPayable A	" & vbNewLine & _
                        "INNER JOIN traAccountPayableDet B ON 	" & vbNewLine & _
                        "	A.ID=B.APID 	" & vbNewLine & _
                        "WHERE 	" & vbNewLine & _
                        "	A.IsDeleted=0 	" & vbNewLine & _
                        "	AND B.ReceiveID=@ReceiveID	" & vbNewLine

                    .Parameters.Add("@ReceiveID", SqlDbType.VarChar, 20).Value = strReceiveID
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

        Public Shared Function ListDataStatus(ByVal strAPID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.APID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traAccountPayableStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.APID=@APID " & vbNewLine

                .Parameters.Add("@APID", SqlDbType.VarChar, 20).Value = strAPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.AccountPayableStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traAccountPayableStatus " & vbNewLine & _
                   "    (ID, APID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @APID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@APID", SqlDbType.VarChar, 20).Value = clsData.APID
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

        Public Shared Function GetMaxIDStatus(ByVal strAPID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traAccountPayableStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(APID,17)=@APID " & vbNewLine

                    .Parameters.Add("@APID", SqlDbType.VarChar, 17).Value = strAPID

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