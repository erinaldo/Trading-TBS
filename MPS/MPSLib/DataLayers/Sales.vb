Namespace DL
    Public Class Sales

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                   "    A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.ArrivalReturn, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstItem MI ON " & vbNewLine & _
                   "    A.ItemID=MI.ID " & vbNewLine & _
                   "INNER JOIN mstUOM MU ON " & vbNewLine & _
                   "    MI.UomID=MU.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.SalesDate>=@DateFrom AND A.SalesDate<=@DateTo " & vbNewLine

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

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Sales)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traSales " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, BPID, SalesDate, PaymentTerm, DueDate, DriverName, PlatNumber, " & vbNewLine & _
                       "     PPN, PPH, ItemID, ArrivalBrutto, ArrivalTarra, ArrivalNettoBefore, ArrivalDeduction, ArrivalNettoAfter, Price, TotalPrice, Remarks, IDStatus, CreatedBy,   " & vbNewLine & _
                       "     CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @BPID, @SalesDate, @PaymentTerm, @DueDate, @DriverName, @PlatNumber, " & vbNewLine & _
                       "     @PPN, @PPH, @ItemID, @ArrivalBrutto, @ArrivalTarra, @ArrivalNettoBefore, @ArrivalDeduction, @ArrivalNettoAfter, @Price, @TotalPrice, @Remarks, @IDStatus, @LogBy,   " & vbNewLine & _
                       "     GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                        "UPDATE traSales SET " & vbNewLine & _
                        "    CompanyID=@CompanyID, " & vbNewLine & _
                        "    ProgramID=@ProgramID, " & vbNewLine & _
                        "    BPID=@BPID, " & vbNewLine & _
                        "    SalesDate=@SalesDate, " & vbNewLine & _
                        "    PaymentTerm=@PaymentTerm, " & vbNewLine & _
                        "    PlatNumber=@PlatNumber, " & vbNewLine & _
                        "    DriverName=@DriverName, " & vbNewLine & _
                        "    DueDate=@DueDate, " & vbNewLine & _
                        "    PPN=@PPN, " & vbNewLine & _
                        "    PPH=@PPH, " & vbNewLine & _
                        "    ItemID=@ItemID, " & vbNewLine & _
                        "    ArrivalBrutto=@ArrivalBrutto, " & vbNewLine & _
                        "    ArrivalTarra=@ArrivalTarra, " & vbNewLine & _
                        "    ArrivalNettoBefore=@ArrivalNettoBefore, " & vbNewLine & _
                        "    ArrivalDeduction=@ArrivalDeduction, " & vbNewLine & _
                        "    ArrivalNettoAfter=@ArrivalNettoAfter, " & vbNewLine & _
                        "    Price=@Price, " & vbNewLine & _
                        "    TotalPrice=@TotalPrice, " & vbNewLine & _
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
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@SalesDate", SqlDbType.DateTime).Value = clsData.SalesDate
                .Parameters.Add("@PaymentTerm", SqlDbType.Int).Value = clsData.PaymentTerm
                .Parameters.Add("@DueDate", SqlDbType.DateTime).Value = clsData.DueDate
                .Parameters.Add("@DriverName", SqlDbType.VarChar, 100).Value = clsData.DriverName
                .Parameters.Add("@PlatNumber", SqlDbType.VarChar, 10).Value = clsData.PlatNumber
                .Parameters.Add("@PPN", SqlDbType.Decimal).Value = clsData.PPN
                .Parameters.Add("@PPH", SqlDbType.Decimal).Value = clsData.PPH
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ArrivalBrutto", SqlDbType.Decimal).Value = clsData.ArrivalBrutto
                .Parameters.Add("@ArrivalTarra", SqlDbType.Decimal).Value = clsData.ArrivalTarra
                .Parameters.Add("@ArrivalNettoBefore", SqlDbType.Decimal).Value = clsData.ArrivalNettoBefore
                .Parameters.Add("@ArrivalDeduction", SqlDbType.Decimal).Value = clsData.ArrivalDeduction
                .Parameters.Add("@ArrivalNettoAfter", SqlDbType.Decimal).Value = clsData.ArrivalNettoAfter
                .Parameters.Add("@Price", SqlDbType.Decimal).Value = clsData.Price
                .Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = clsData.TotalPrice
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

        Public Shared Function GetDetail(ByVal strID As String) As VO.Sales
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.Sales
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT " & vbNewLine & _
                        "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                        "    A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MI.UomID AS UOMID, MU.Code AS UomCode, " & vbNewLine & _
                        "    A.ArrivalBrutto, A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.ArrivalReturn, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                        "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy,   " & vbNewLine & _
                        "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                        "FROM traSales A " & vbNewLine & _
                        "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                        "    A.BPID=C.ID " & vbNewLine & _
                        "INNER JOIN mstItem MI ON " & vbNewLine & _
                        "    A.ItemID=MI.ID " & vbNewLine & _
                        "INNER JOIN mstUOM MU ON " & vbNewLine & _
                        "    MI.UomID=MU.ID " & vbNewLine & _
                        "INNER JOIN mstCompany MC ON " & vbNewLine & _
                        "   A.CompanyID=MC.ID " & vbNewLine & _
                        "INNER JOIN mstProgram MP ON " & vbNewLine & _
                        "   A.ProgramID=MP.ID " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
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
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPName = .Item("BPName")
                        voReturn.SalesDate = .Item("SalesDate")
                        voReturn.PaymentTerm = .Item("PaymentTerm")
                        voReturn.DueDate = .Item("DueDate")
                        voReturn.DriverName = .Item("DriverName")
                        voReturn.PlatNumber = .Item("PlatNumber")
                        voReturn.PPN = .Item("PPN")
                        voReturn.PPH = .Item("PPH")
                        voReturn.ItemID = .Item("ItemID")
                        voReturn.ItemCode = .Item("ItemCode")
                        voReturn.ItemName = .Item("ItemName")
                        voReturn.UOMID = .Item("UOMID")
                        voReturn.ArrivalBrutto = .Item("ArrivalBrutto")
                        voReturn.ArrivalTarra = .Item("ArrivalTarra")
                        voReturn.ArrivalNettoBefore = .Item("ArrivalNettoBefore")
                        voReturn.ArrivalDeduction = .Item("ArrivalDeduction")
                        voReturn.ArrivalNettoAfter = .Item("ArrivalNettoAfter")
                        voReturn.Price = .Item("Price")
                        voReturn.TotalPrice = .Item("TotalPrice")
                        voReturn.ArrivalReturn = .Item("ArrivalReturn")
                        voReturn.TotalPayment = .Item("TotalPayment")
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
                    "UPDATE traSales " & vbNewLine & _
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
                        "FROM traSales " & vbNewLine & _
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
                        "FROM traSales " & vbNewLine & _
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
                        "FROM traSales " & vbNewLine & _
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

        Public Shared Function ListDataBonFaktur(ByVal strID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.ID, A.SalesDate, C.Name AS BPName, C.Address AS BPAddress, A.PlatNumber, A.DriverName, " & vbNewLine & _
                   "    MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto AS Brutto, A.ArrivalTarra AS Tarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore AS NettoBefore, A.ArrivalDeduction AS Deduction, A.ArrivalNettoAfter AS NettoAfter, A.Price, A.TotalPrice, " & vbNewLine & _
                   "    A.Remarks, A.CreatedBy " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstItem MI ON " & vbNewLine & _
                   "    A.ItemID=MI.ID " & vbNewLine & _
                   "INNER JOIN mstUOM MU ON " & vbNewLine & _
                   "    MI.UomID=MU.ID " & vbNewLine & _
                   "WHERE A.ID=@ID	" & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub PrintBonFaktur(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales SET " & vbNewLine & _
                    "   IDStatus=@IDStatus " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Printed
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub CalculateArrivalUsage(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales 	" & vbNewLine & _
                    "SET ArrivalUsage=	" & vbNewLine & _
                    "	ISNULL(	" & vbNewLine & _
                    "	(	" & vbNewLine & _
                    "		SELECT SUM(A.ArrivalBrutto) 	" & vbNewLine & _
                    "		FROM traReceive A 	" & vbNewLine & _
                    "		WHERE 	" & vbNewLine & _
                    "			A.ReferencesID=@ReferencesID " & vbNewLine & _
                    "			AND A.IsDeleted=0 	" & vbNewLine & _
                    "	),0)	" & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ReferencesID " & vbNewLine

                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = strID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function ListDataOutstanding() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                   "    A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MI.UomID AS UomID, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.ArrivalNettoAfter-A.ArrivalUsage-A.ArrivalReturn AS MaxBrutto, A.Price, A.TotalPrice, A.ArrivalReturn, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstItem MI ON " & vbNewLine & _
                   "    A.ItemID=MI.ID " & vbNewLine & _
                   "INNER JOIN mstUOM MU ON " & vbNewLine & _
                   "    MI.UomID=MU.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ArrivalNettoAfter-A.ArrivalUsage-A.ArrivalReturn>0 " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        'Public Shared Function ListDataOutstandingPostGL(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '           "SELECT " & vbNewLine & _
        '           "    A.CompanyID, A.ID, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, A.SubTotal,   " & vbNewLine & _
        '           "    A.TotalDiscount, A.TotalTax, A.GrandTotal, A.TotalReturn, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
        '           "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
        '           "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
        '           "FROM traSales A " & vbNewLine & _
        '           "INNER JOIN mstStatus B ON " & vbNewLine & _
        '           "    A.IDStatus=B.ID " & vbNewLine & _
        '           "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
        '           "    A.BPID=C.ID " & vbNewLine & _
        '           "WHERE  " & vbNewLine & _
        '           "    A.SalesDate>=@DateFrom AND A.SalesDate<=@DateTo " & vbNewLine & _
        '           "    AND A.IsDeleted=0 " & vbNewLine & _
        '           "    AND A.IsPostedGL=0 " & vbNewLine

        '        .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
        '        .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
        '    End With
        '    Return SQL.QueryDataTable(sqlcmdExecute)
        'End Function

        'Public Shared Function IsPostedGL(ByVal strID As String) As Boolean
        '    Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
        '    Dim bolExists As Boolean = False
        '    Try
        '        If Not SQL.bolUseTrans Then SQL.OpenConnection()
        '        With sqlcmdExecute
        '            .Connection = SQL.sqlConn
        '            .CommandText = _
        '                "SELECT TOP 1 " & vbNewLine & _
        '                "   ID " & vbNewLine & _
        '                "FROM traSales " & vbNewLine & _
        '                "WHERE  " & vbNewLine & _
        '                "   ID=@ID " & vbNewLine & _
        '                "   AND IsPostedGL=1 " & vbNewLine

        '            .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
        '            If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
        '        End With
        '        sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
        '        With sqlrdData
        '            If .HasRows Then
        '                .Read()
        '                bolExists = True
        '            End If
        '            .Close()
        '        End With
        '        If Not SQL.bolUseTrans Then SQL.CloseConnection()
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        '    Return bolExists
        'End Function

        'Public Shared Sub UpdateJournalID(ByVal strID As String, ByVal strJournalID As String)
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '            "UPDATE traSales " & vbNewLine & _
        '            "SET JournalID=@JournalID " & vbNewLine & _
        '            "WHERE " & vbNewLine & _
        '            "   ID=@ID " & vbNewLine

        '        .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
        '        .Parameters.Add("@JournalID", SqlDbType.VarChar, 20).Value = strJournalID
        '    End With
        '    Try
        '        SQL.ExecuteNonQuery(sqlcmdExecute)
        '    Catch ex As SqlException
        '        Throw ex
        '    End Try
        'End Sub

        'Public Shared Function ListDataDeliveryOrder(ByVal strID As String) As DataTable
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '            "SELECT 	" & vbNewLine & _
        '            "	TS.ID, TS.SalesDate, BP.Name AS BPName, BP.Address AS BPAddress, 	" & vbNewLine & _
        '            "	MI.Name AS ItemName, MU.Code AS UomCode, TSD.Price, TSD.Qty, TSD.Disc, TSD.TotalPrice, TS.Remarks 	" & vbNewLine & _
        '            "FROM traSales TS 	" & vbNewLine & _
        '            "INNER JOIN traSalesDet TSD ON 	" & vbNewLine & _
        '            "	TS.ID=TSD.SalesID 	" & vbNewLine & _
        '            "INNER JOIN mstBusinessPartner BP ON		" & vbNewLine & _
        '            "	TS.BPID=BP.ID 	" & vbNewLine & _
        '            "INNER JOIN mstItem MI ON 	" & vbNewLine & _
        '            "	TSD.ItemID=MI.ID 	" & vbNewLine & _
        '            "INNER JOIN mstUOM MU ON 	" & vbNewLine & _
        '            "	MI.UomID1=MU.ID 	" & vbNewLine & _
        '            "WHERE TS.ID=@ID	" & vbNewLine

        '        .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
        '    End With
        '    Return SQL.QueryDataTable(sqlcmdExecute)
        'End Function

        'Public Shared Function ListDataBonFaktur(ByVal strID As String) As DataTable
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '            "SELECT 	" & vbNewLine & _
        '            "	TS.ID, TS.SalesDate, BP.Name AS BPName, BP.Address AS BPAddress, TS.PlatNumber, TS.DriverName, " & vbNewLine & _
        '            "	MI.Name AS ItemName, MU.Code AS UomCode, TSD.Price, TSD.Brutto, TSD.Tarra, " & vbNewLine & _
        '            "	TSD.NettoBefore, TSD.Deduction, TSD.NettoAfter, TSD.TotalPrice, TS.Remarks, TS.CreatedBy 	" & vbNewLine & _
        '            "FROM traSales TS 	" & vbNewLine & _
        '            "INNER JOIN traSalesDet TSD ON 	" & vbNewLine & _
        '            "	TS.ID=TSD.SalesID 	" & vbNewLine & _
        '            "INNER JOIN mstBusinessPartner BP ON		" & vbNewLine & _
        '            "	TS.BPID=BP.ID 	" & vbNewLine & _
        '            "INNER JOIN mstItem MI ON 	" & vbNewLine & _
        '            "	TSD.ItemID=MI.ID 	" & vbNewLine & _
        '            "INNER JOIN mstUOM MU ON 	" & vbNewLine & _
        '            "	MI.UomID1=MU.ID 	" & vbNewLine & _
        '            "WHERE TS.ID=@ID	" & vbNewLine

        '        .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
        '    End With
        '    Return SQL.QueryDataTable(sqlcmdExecute)
        'End Function

        'Public Shared Sub PrintDeliveryOrder(ByVal strID As String)
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '            "UPDATE traSales SET " & vbNewLine & _
        '            "   IDStatus=@IDStatus " & vbNewLine & _
        '            "WHERE " & vbNewLine & _
        '            "   ID=@ID " & vbNewLine

        '        .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
        '        .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Printed
        '    End With
        '    Try
        '        SQL.ExecuteNonQuery(sqlcmdExecute)
        '    Catch ex As SqlException
        '        Throw ex
        '    End Try
        'End Sub

        'Public Shared Function ListDataHistoryBussinessPartners(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intItemID As Integer) As DataTable
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '           "SELECT " & vbNewLine & _
        '           "    SH.CompanyID, 'PENJUALAN' AS Trans, SH.ID, SH.SalesDate AS TransactionDate, SH.BPID, BP.Name AS BPName, A.Qty, A.UomID, C.Code AS UomCode, A.Price, A.Disc,   " & vbNewLine & _
        '           "    A.Tax, A.TotalPrice, A.Remarks, SH.IDStatus, MS.Name AS StatusInfo, SH.CreatedBy, SH.CreatedDate, SH.LogInc, SH.LogBy, SH.LogDate, SH.JournalID  " & vbNewLine & _
        '           "FROM traSalesDet A " & vbNewLine & _
        '           "INNER JOIN traSales SH ON " & vbNewLine & _
        '           "    A.SalesID=SH.ID " & vbNewLine & _
        '           "INNER JOIN mstItem B ON " & vbNewLine & _
        '           "    A.ItemID=B.ID " & vbNewLine & _
        '           "INNER JOIN mstUOM C ON " & vbNewLine & _
        '           "    A.UomID=C.ID " & vbNewLine & _
        '           "INNER JOIN mstStatus MS ON " & vbNewLine & _
        '           "    SH.IDStatus=MS.ID " & vbNewLine & _
        '           "INNER JOIN mstBusinessPartner BP ON " & vbNewLine & _
        '           "    SH.BPID=BP.ID " & vbNewLine & _
        '           "WHERE  " & vbNewLine & _
        '           "    SH.SalesDate>=@DateFrom AND SH.SalesDate<=@DateTo " & vbNewLine & _
        '           "    AND SH.IsDeleted=0 " & vbNewLine & _
        '           "    AND A.ItemID=@ItemID " & vbNewLine

        '        .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
        '        .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
        '        .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
        '    End With
        '    Return SQL.QueryDataTable(sqlcmdExecute)
        'End Function

        'Public Shared Function ListDataHistoryItem(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '           "SELECT " & vbNewLine & _
        '           "    SH.CompanyID, 'PENJUALAN' AS Trans, SH.ID, SH.SalesDate AS TransactionDate, A.ItemID, B.Code AS ItemCode, B.Name AS ItemName, A.Qty, A.UomID, C.Code AS UomCode, A.Price, A.Disc,   " & vbNewLine & _
        '           "    A.Tax, A.TotalPrice, A.Remarks, SH.IDStatus, MS.Name AS StatusInfo, SH.CreatedBy, SH.CreatedDate, SH.LogInc, SH.LogBy, SH.LogDate, SH.JournalID  " & vbNewLine & _
        '           "FROM traSalesDet A " & vbNewLine & _
        '           "INNER JOIN traSales SH ON " & vbNewLine & _
        '           "    A.SalesID=SH.ID " & vbNewLine & _
        '           "INNER JOIN mstItem B ON " & vbNewLine & _
        '           "    A.ItemID=B.ID " & vbNewLine & _
        '           "INNER JOIN mstUOM C ON " & vbNewLine & _
        '           "    A.UomID=C.ID " & vbNewLine & _
        '           "INNER JOIN mstStatus MS ON " & vbNewLine & _
        '           "    SH.IDStatus=MS.ID " & vbNewLine & _
        '           "WHERE  " & vbNewLine & _
        '           "    SH.SalesDate>=@DateFrom AND SH.SalesDate<=@DateTo " & vbNewLine & _
        '           "    AND SH.IsDeleted=0 " & vbNewLine & _
        '           "    AND SH.BPID=@BPID " & vbNewLine

        '        .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
        '        .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
        '        .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
        '    End With
        '    Return SQL.QueryDataTable(sqlcmdExecute)
        'End Function

        'Public Shared Sub PostGL(ByVal strID As String, ByVal strLogBy As String)
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '            "UPDATE traSales SET" & vbNewLine & _
        '            "   IsPostedGL=1, " & vbNewLine & _
        '            "   PostedBy=@LogBy, " & vbNewLine & _
        '            "   PostedDate=GETDATE() " & vbNewLine & _
        '            "WHERE " & vbNewLine & _
        '            "   ID=@ID " & vbNewLine

        '        .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
        '        .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = strLogBy
        '    End With
        '    Try
        '        SQL.ExecuteNonQuery(sqlcmdExecute)
        '    Catch ex As SqlException
        '        Throw ex
        '    End Try
        'End Sub

        'Public Shared Sub UnpostGL(ByVal strID As String)
        '    Dim sqlcmdExecute As New SqlCommand
        '    With sqlcmdExecute
        '        .CommandText = _
        '            "UPDATE traSales SET" & vbNewLine & _
        '            "   IsPostedGL=0, " & vbNewLine & _
        '            "   PostedBy='' " & vbNewLine & _
        '            "WHERE " & vbNewLine & _
        '            "   ID=@ID " & vbNewLine

        '        .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
        '    End With
        '    Try
        '        SQL.ExecuteNonQuery(sqlcmdExecute)
        '    Catch ex As SqlException
        '        Throw ex
        '    End Try
        'End Sub

#End Region

#Region "Sales Supplier"
        
        Public Shared Function ListDataSupplier(ByVal strSalesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	TSS.BPID, MB.Name AS BPName, MB.Address 	" & vbNewLine & _
                    "FROM traSalesSupplier TSS 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner MB ON 	" & vbNewLine & _
                    "	TSS.BPID=MB.ID 	" & vbNewLine & _
                    "WHERE	" & vbNewLine & _
                    "	TSS.SalesID=@SalesID	" & vbNewLine

                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataSupplier(ByVal clsData As VO.SalesSupplier)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traSalesSupplier " & vbNewLine & _
                   "    (ID, SalesID, BPID)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @SalesID, @BPID)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = clsData.SalesID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDSupplier(ByVal strSalesID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesSupplier " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   SalesID=@SalesID " & vbNewLine

                    .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID

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

        Public Shared Sub DeleteDataSupplier(ByVal strSalesID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traSalesStatus " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   SalesID=@SalesID " & vbNewLine

                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.SalesID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traSalesStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.SalesID=@SalesID " & vbNewLine

                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.SalesStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traSalesStatus " & vbNewLine & _
                   "    (ID, SalesID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @SalesID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = clsData.SalesID
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

        Public Shared Function GetMaxIDStatus(ByVal strSalesID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   SalesID=@SalesID " & vbNewLine

                    .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID

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

        '#Region "Detail"

        '        Public Shared Function ListDataDetail(ByVal strSalesID As String) As DataTable
        '            Dim sqlcmdExecute As New SqlCommand
        '            With sqlcmdExecute
        '                .CommandText = _
        '                   "SELECT " & vbNewLine & _
        '                   "    A.ID, A.ItemID, B.Code AS ItemCode, B.Name AS ItemName, A.UomID, C.Code AS UomCode, A.Brutto, A.Tarra, A.NettoBefore, A.Deduction, A.NettoAfter, A.ReturnNettoAfter, A.Price, A.Disc,   " & vbNewLine & _
        '                   "    A.PPN, A.PPH, A.TotalPrice, A.Remarks  " & vbNewLine & _
        '                   "FROM traSalesDet A " & vbNewLine & _
        '                   "INNER JOIN mstItem B ON " & vbNewLine & _
        '                   "    A.ItemID=B.ID " & vbNewLine & _
        '                   "INNER JOIN mstUOM C ON " & vbNewLine & _
        '                   "    A.UomID=C.ID " & vbNewLine & _
        '                   "WHERE  " & vbNewLine & _
        '                   "    A.SalesID=@SalesID" & vbNewLine

        '                .Parameters.Add("@SalesID", SqlDbType.VarChar, 20).Value = strSalesID
        '            End With
        '            Return SQL.QueryDataTable(sqlcmdExecute)
        '        End Function

        '        Public Shared Function ListDataOutstandingUsage(ByVal strSalesID As String) As DataTable
        '            Dim sqlcmdExecute As New SqlCommand
        '            With sqlcmdExecute
        '                .CommandText = _
        '                   "SELECT " & vbNewLine & _
        '                   "    A.ID, A.ItemID, B.Code AS ItemCode, B.Name AS ItemName, A.UomID, C.Code AS UomCode, A.NettoAfter-A.UsageNettoAfter-A.ReturnNettoAfter AS MaxBrutto, " & vbNewLine & _
        '                   "    A.Price, A.Disc, A.PPN, A.PPH, A.Remarks  " & vbNewLine & _
        '                   "FROM traSalesDet A " & vbNewLine & _
        '                   "INNER JOIN traSales AA ON " & vbNewLine & _
        '                   "    A.SalesID=AA.ID " & vbNewLine & _
        '                   "    AND AA.IsDeleted=0 " & vbNewLine & _
        '                   "INNER JOIN mstItem B ON " & vbNewLine & _
        '                   "    A.ItemID=B.ID " & vbNewLine & _
        '                   "INNER JOIN mstUOM C ON " & vbNewLine & _
        '                   "    A.UomID=C.ID " & vbNewLine & _
        '                   "WHERE  " & vbNewLine & _
        '                   "    AA.ID=@SalesID " & vbNewLine & _
        '                   "    AND A.NettoAfter-A.UsageNettoAfter-A.ReturnNettoAfter>0 " & vbNewLine

        '                .Parameters.Add("@SalesID", SqlDbType.VarChar, 20).Value = strSalesID
        '            End With
        '            Return SQL.QueryDataTable(sqlcmdExecute)
        '        End Function

        '        Public Shared Function ListDataOutstandingReturn(ByVal intBPID As Integer) As DataTable
        '            Dim sqlcmdExecute As New SqlCommand
        '            With sqlcmdExecute
        '                .CommandText = _
        '                   "SELECT " & vbNewLine & _
        '                   "    CAST(0 AS BIT) Pick, A.ID, A.SalesID, A.ItemID, B.Code AS ItemCode, B.Name AS ItemName, A.UomID, C.Code AS UomCode, A.Qty-A.ReturnQty AS MaxQty, A.Price, A.Disc,   " & vbNewLine & _
        '                   "    A.Tax, A.Remarks  " & vbNewLine & _
        '                   "FROM traSalesDet A " & vbNewLine & _
        '                   "INNER JOIN traSales AA ON " & vbNewLine & _
        '                   "    A.SalesID=AA.ID " & vbNewLine & _
        '                   "    AND AA.IsDeleted=0 " & vbNewLine & _
        '                   "INNER JOIN mstItem B ON " & vbNewLine & _
        '                   "    A.ItemID=B.ID " & vbNewLine & _
        '                   "INNER JOIN mstUOM C ON " & vbNewLine & _
        '                   "    A.UomID=C.ID " & vbNewLine & _
        '                   "WHERE  " & vbNewLine & _
        '                   "    AA.BPID=@BPID " & vbNewLine & _
        '                   "    AND A.Qty-A.ReturnQty>0 " & vbNewLine

        '                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
        '            End With
        '            Return SQL.QueryDataTable(sqlcmdExecute)
        '        End Function

        '        Public Shared Sub SaveDataDetail(ByVal clsData As VO.SalesDet)
        '            Dim sqlcmdExecute As New SqlCommand
        '            With sqlcmdExecute
        '                .CommandText = _
        '                    "INSERT INTO traSalesDet " & vbNewLine & _
        '                    "    (ID, SalesID, ItemID, UomID, Brutto, Tarra, NettoBefore, Deduction, NettoAfter, Price, Disc,   " & vbNewLine & _
        '                    "     PPN, PPH, TotalPrice, Remarks)   " & vbNewLine & _
        '                    "VALUES " & vbNewLine & _
        '                    "    (@ID, @SalesID, @ItemID, @UomID, @Brutto, @Tarra, @NettoBefore, @Deduction, @NettoAfter, @Price, @Disc,   " & vbNewLine & _
        '                    "     @PPN, @PPH, @TotalPrice, @Remarks)  " & vbNewLine

        '                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
        '                .Parameters.Add("@SalesID", SqlDbType.VarChar, 20).Value = clsData.SalesID
        '                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
        '                .Parameters.Add("@UomID", SqlDbType.Int).Value = clsData.UomID
        '                .Parameters.Add("@Brutto", SqlDbType.Decimal).Value = clsData.Brutto
        '                .Parameters.Add("@Tarra", SqlDbType.Decimal).Value = clsData.Tarra
        '                .Parameters.Add("@NettoBefore", SqlDbType.Decimal).Value = clsData.NettoBefore
        '                .Parameters.Add("@Deduction", SqlDbType.Decimal).Value = clsData.Deduction
        '                .Parameters.Add("@NettoAfter", SqlDbType.Decimal).Value = clsData.NettoAfter
        '                .Parameters.Add("@Price", SqlDbType.Decimal).Value = clsData.Price
        '                .Parameters.Add("@Disc", SqlDbType.Decimal).Value = clsData.Disc
        '                .Parameters.Add("@PPN", SqlDbType.Decimal).Value = clsData.PPN
        '                .Parameters.Add("@PPH", SqlDbType.Decimal).Value = clsData.PPH
        '                .Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = clsData.TotalPrice
        '                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
        '            End With
        '            Try
        '                SQL.ExecuteNonQuery(sqlcmdExecute)
        '            Catch ex As SqlException
        '                Throw ex
        '            End Try
        '        End Sub

        '        Public Shared Sub DeleteDataDetail(ByVal strSalesID As String)
        '            Dim sqlcmdExecute As New SqlCommand
        '            With sqlcmdExecute
        '                .CommandText = _
        '                    "DELETE FROM traSalesDet " & vbNewLine & _
        '                    "WHERE " & vbNewLine & _
        '                    "   SalesID=@SalesID " & vbNewLine

        '                .Parameters.Add("@SalesID", SqlDbType.VarChar, 20).Value = strSalesID
        '            End With
        '            Try
        '                SQL.ExecuteNonQuery(sqlcmdExecute)
        '            Catch ex As SqlException
        '                Throw ex
        '            End Try
        '        End Sub

        '        Public Shared Function GetMaxIDDetail(ByVal strSalesID As String) As Integer
        '            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
        '            Dim intReturn As Integer = 1
        '            Try
        '                If Not SQL.bolUseTrans Then SQL.OpenConnection()
        '                With sqlcmdExecute
        '                    .Connection = SQL.sqlConn
        '                    .CommandText = _
        '                        "SELECT TOP 1 " & vbNewLine & _
        '                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
        '                        "FROM traSalesDet " & vbNewLine & _
        '                        "WHERE  " & vbNewLine & _
        '                        "   LEFT(SalesID,17)=@SalesID " & vbNewLine

        '                    .Parameters.Add("@SalesID", SqlDbType.VarChar, 20).Value = strSalesID
        '                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
        '                End With
        '                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
        '                With sqlrdData
        '                    If .HasRows Then
        '                        .Read()
        '                        intReturn = .Item("ID") + 1
        '                    End If
        '                    .Close()
        '                End With
        '                If Not SQL.bolUseTrans Then SQL.CloseConnection()
        '            Catch ex As Exception
        '                Throw ex
        '            End Try
        '            Return intReturn
        '        End Function

        '        Public Shared Sub UpdateTotalReturn(ByVal strID As String)
        '            Dim sqlcmdExecute As New SqlCommand
        '            With sqlcmdExecute
        '                .CommandText = _
        '                    "UPDATE traSales 	" & vbNewLine & _
        '                    "SET TotalReturn= 	" & vbNewLine & _
        '                    "	ISNULL(	" & vbNewLine & _
        '                    "		(SELECT 	" & vbNewLine & _
        '                    "			SUM(SRD.TotalPrice) TotalPrice 	" & vbNewLine & _
        '                    "		FROM traSalesReturnDet SRD	" & vbNewLine & _
        '                    "		INNER JOIN traSalesReturn SR ON 	" & vbNewLine & _
        '                    "			SRD.SalesReturnID=SR.ID 	" & vbNewLine & _
        '                    "		INNER JOIN traSalesDet SD ON 	" & vbNewLine & _
        '                    "			SRD.SalesDetID=SD.ID 	" & vbNewLine & _
        '                    "		WHERE 	" & vbNewLine & _
        '                    "			SR.IsDeleted=0 	" & vbNewLine & _
        '                    "			AND SD.SalesID=@ID	" & vbNewLine & _
        '                    "		GROUP BY SD.SalesID)	" & vbNewLine & _
        '                    "	,0)	" & vbNewLine & _
        '                    "WHERE ID=@ID " & vbNewLine

        '                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
        '            End With
        '            Try
        '                SQL.ExecuteNonQuery(sqlcmdExecute)
        '            Catch ex As SqlException
        '                Throw ex
        '            End Try
        '        End Sub

        '        Public Shared Sub CalculateTotalPayment(ByVal strSalesID As String)
        '            Dim sqlcmdExecute As New SqlCommand
        '            With sqlcmdExecute
        '                .CommandText = _
        '                    "UPDATE traSales " & vbNewLine & _
        '                    "SET TotalPayment=" & vbNewLine & _
        '                    "	(" & vbNewLine & _
        '                    "		SELECT " & vbNewLine & _
        '                    "			ISNULL(SUM(ARD.Amount),0) AS Total" & vbNewLine & _
        '                    "		FROM traAccountReceivableDet ARD " & vbNewLine & _
        '                    "		INNER JOIN traAccountReceivable AR ON " & vbNewLine & _
        '                    "            ARD.ARID =AR.ID" & vbNewLine & _
        '                    "        WHERE" & vbNewLine & _
        '                    "            AR.IsDeleted=0" & vbNewLine & _
        '                    "			AND ARD.SalesID=@SalesID" & vbNewLine & _
        '                    "	)" & vbNewLine & _
        '                    "WHERE ID=@SalesID " & vbNewLine

        '                .Parameters.Add("@SalesID", SqlDbType.VarChar, 20).Value = strSalesID
        '            End With
        '            Try
        '                SQL.ExecuteNonQuery(sqlcmdExecute)
        '            Catch ex As SqlException
        '                Throw ex
        '            End Try
        '        End Sub

        '#End Region

    End Class

End Namespace