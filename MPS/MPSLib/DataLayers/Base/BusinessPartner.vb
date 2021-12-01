Namespace DL
    Public Class BusinessPartner

#Region "Main"

        Public Shared Function ListData(ByVal decOnAmount As Decimal, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT  	" & vbNewLine & _
                    "   A.ID, A.Name, A.Address, A.PICName, A.PICPhoneNumber, A.PaymentTermID, A.IsUsePurchaseLimit, A.MaxPurchaseLimit, 	" & vbNewLine & _
                    "   ISNULL(TR.TotalPrice1,0) AS TotalPurchase1, ISNULL(TR.TotalPrice2,0) AS TotalPurchase2, A.APBalance, A.ARBalance, 	" & vbNewLine & _
                    "   A.IDStatus, B.Name AS StatusInfo, A.CreatedBy, A.CreatedDate, A.LogBy, A.LogDate,   	" & vbNewLine & _
                    "   IsAssign=CAST(CASE WHEN ISNULL((SELECT TOP 1 ID FROM mstBusinessPartnerAssign WHERE BPID=A.ID),0)=0 THEN 0 ELSE 1 END AS BIT), " & vbNewLine & _
                    "   A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2 " & vbNewLine & _
                    "FROM mstBusinessPartner A  	" & vbNewLine & _
                    "INNER JOIN mstStatus B ON   	" & vbNewLine & _
                    "    A.IDStatus=B.ID 	" & vbNewLine & _
                    "LEFT JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		TR.BPID, SUM(TR.TotalPrice1) AS TotalPrice1, SUM(TR.TotalPrice2) AS TotalPrice2	" & vbNewLine & _
                    "	FROM traReceive TR 	" & vbNewLine & _
                    "	WHERE	" & vbNewLine & _
                    "		TR.IsDeleted=0 	" & vbNewLine & _
                    "		AND YEAR(TR.ReceiveDate)=YEAR(GETDATE())	" & vbNewLine & _
                    "	GROUP BY 	" & vbNewLine & _
                    "		TR.BPID 	" & vbNewLine & _
                    ") TR ON A.ID=TR.BPID 	" & vbNewLine


                If intCompanyID <> 0 And intProgramID <> 0 Then
                    .CommandText += _
                        "INNER JOIN mstBusinessPartnerAssign MBPA ON  	" & vbNewLine & _
                        "    A.ID=MBPA.BPID " & vbNewLine & _
                        "    AND MBPA.CompanyID=@CompanyID " & vbNewLine & _
                        "    AND MBPA.ProgramID=@ProgramID " & vbNewLine
                End If

                .CommandText += _
                    "WHERE 	" & vbNewLine & _
                    "	A.IsUsePurchaseLimit=0 OR (A.IsUsePurchaseLimit=1 AND A.MaxPurchaseLimit>=ISNULL(TR.TotalPrice1,0)+@OnAmount)	" & vbNewLine

                .Parameters.Add("@OnAmount", SqlDbType.Decimal).Value = decOnAmount
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataForUpdatePrice() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT  	" & vbNewLine & _
                    "   A.ID, A.Name, A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2, " & vbNewLine & _
                    "   CAST(0 AS DECIMAL(18,2)) AS SalesPriceNew, CAST(0 AS DECIMAL(18,2)) AS PurchasePrice1New, CAST(0 AS DECIMAL(18,2)) AS PurchasePrice2New " & vbNewLine & _
                    "FROM mstBusinessPartner A  	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.BusinessPartner)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO mstBusinessPartner " & vbNewLine & _
                       "    (ID, Name, Address, PICName, PICPhoneNumber, PaymentTermID,   " & vbNewLine & _
                       "     IsUsePurchaseLimit, MaxPurchaseLimit, SalesPrice, PurchasePrice1, PurchasePrice2, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @Name, @Address, @PICName, @PICPhoneNumber, @PaymentTermID,   " & vbNewLine & _
                       "     @IsUsePurchaseLimit, @MaxPurchaseLimit, @SalesPrice, @PurchasePrice1, @PurchasePrice2, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE mstBusinessPartner SET " & vbNewLine & _
                    "    Name=@Name, " & vbNewLine & _
                    "    Address=@Address, " & vbNewLine & _
                    "    PICName=@PICName, " & vbNewLine & _
                    "    PICPhoneNumber=@PICPhoneNumber, " & vbNewLine & _
                    "    PaymentTermID=@PaymentTermID, " & vbNewLine & _
                    "    IsUsePurchaseLimit=@IsUsePurchaseLimit, " & vbNewLine & _
                    "    MaxPurchaseLimit=@MaxPurchaseLimit, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = clsData.Name
                .Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = clsData.Address
                .Parameters.Add("@PICName", SqlDbType.VarChar, 150).Value = clsData.PICName
                .Parameters.Add("@PICPhoneNumber", SqlDbType.VarChar, 100).Value = clsData.PICPhoneNumber
                .Parameters.Add("@PaymentTermID", SqlDbType.Int).Value = clsData.PaymentTermID
                .Parameters.Add("@IsUsePurchaseLimit", SqlDbType.Bit).Value = clsData.IsUsePurchaseLimit
                .Parameters.Add("@MaxPurchaseLimit", SqlDbType.Decimal).Value = clsData.MaxPurchaseLimit
                .Parameters.Add("@SalesPrice", SqlDbType.Decimal).Value = clsData.SalesPrice
                .Parameters.Add("@PurchasePrice1", SqlDbType.Decimal).Value = clsData.PurchasePrice1
                .Parameters.Add("@PurchasePrice2", SqlDbType.Decimal).Value = clsData.PurchasePrice2
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.BusinessPartner
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.BusinessPartner
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.Name, A.Address, A.PICName, A.PICPhoneNumber, A.PaymentTermID, " & vbNewLine & _
                       "    A.IsUsePurchaseLimit, A.MaxPurchaseLimit, A.APBalance, A.ARBalance, " & vbNewLine & _
                       "    A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2, A.IDStatus, A.LogBy, A.LogDate  " & vbNewLine & _
                       "FROM mstBusinessPartner A " & vbNewLine & _
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
                        voReturn.Address = .Item("Address")
                        voReturn.PICName = .Item("PICName")
                        voReturn.PICPhoneNumber = .Item("PICPhoneNumber")
                        voReturn.PaymentTermID = .Item("PaymentTermID")
                        voReturn.IsUsePurchaseLimit = .Item("IsUsePurchaseLimit")
                        voReturn.MaxPurchaseLimit = .Item("MaxPurchaseLimit")
                        voReturn.APBalance = .Item("APBalance")
                        voReturn.ARBalance = .Item("ARBalance")
                        voReturn.SalesPrice = .Item("SalesPrice")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
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
                    "UPDATE mstBusinessPartner " & vbNewLine & _
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
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM mstBusinessPartner " & vbNewLine

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
                        "FROM mstBusinessPartner " & vbNewLine & _
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
                        "FROM mstBusinessPartner " & vbNewLine & _
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

        Public Shared Sub UpdatePrice(ByVal clsData As VO.BusinessPartner)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE mstBusinessPartner SET " & vbNewLine & _
                    "    SalesPrice=CASE WHEN @SalesPrice=0 THEN SalesPrice ELSE @SalesPrice END, " & vbNewLine & _
                    "    PurchasePrice1=CASE WHEN @PurchasePrice1=0 THEN PurchasePrice1 ELSE @PurchasePrice1 END, " & vbNewLine & _
                    "    PurchasePrice2=CASE WHEN @PurchasePrice2=0 THEN PurchasePrice2 ELSE @PurchasePrice2 END, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@SalesPrice", SqlDbType.Decimal).Value = clsData.SalesPrice
                .Parameters.Add("@PurchasePrice1", SqlDbType.Decimal).Value = clsData.PurchasePrice1
                .Parameters.Add("@PurchasePrice2", SqlDbType.Decimal).Value = clsData.PurchasePrice2
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.BPID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM mstBusinessPartnerStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.BPID=@BPID " & vbNewLine

                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.BusinessPartnerStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO mstBusinessPartnerStatus " & vbNewLine & _
                   "    (ID, BPID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @BPID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
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

        Public Shared Function GetMaxIDStatus() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM mstBusinessPartnerStatus " & vbNewLine

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

    End Class

End Namespace

