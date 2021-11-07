Namespace DL

    Public Class JournalPost

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.JournalPost)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO sysJournalPost " & vbNewLine & _
                       "    (ProgramID, CoAofRevenue, CoAofAccountReceivable, CoAofSalesDisc, CoAofCOGS, CoAofStock, CoAofCash, CoAofAccountPayable,   " & vbNewLine & _
                       "     CoAofPurchaseDisc, CoAofPurchaseEquipments, Remarks, CreatedBy, CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ProgramID, @CoAofRevenue, @CoAofAccountReceivable, @CoAofSalesDisc, @CoAofCOGS, @CoAofStock, @CoAofCash, @CoAofAccountPayable,   " & vbNewLine & _
                       "     @CoAofPurchaseDisc, @CoAofPurchaseEquipments, @Remarks, @LogBy, GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE sysJournalPost SET " & vbNewLine & _
                    "    ProgramID=@ProgramID, " & vbNewLine & _
                    "    CoAofRevenue=@CoAofRevenue, " & vbNewLine & _
                    "    CoAofAccountReceivable=@CoAofAccountReceivable, " & vbNewLine & _
                    "    CoAofSalesDisc=@CoAofSalesDisc, " & vbNewLine & _
                    "    CoAofCOGS=@CoAofCOGS, " & vbNewLine & _
                    "    CoAofStock=@CoAofStock, " & vbNewLine & _
                    "    CoAofCash=@CoAofCash, " & vbNewLine & _
                    "    CoAofAccountPayable=@CoAofAccountPayable, " & vbNewLine & _
                    "    CoAofPurchaseDisc=@CoAofPurchaseDisc, " & vbNewLine & _
                    "    CoAofPurchaseEquipments=@CoAofPurchaseEquipments, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine
                End If

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@CoAofRevenue", SqlDbType.Int).Value = clsData.CoAofRevenue
                .Parameters.Add("@CoAofAccountReceivable", SqlDbType.Int).Value = clsData.CoAofAccountReceivable
                .Parameters.Add("@CoAofSalesDisc", SqlDbType.Int).Value = clsData.CoAofSalesDisc
                .Parameters.Add("@CoAofCOGS", SqlDbType.Int).Value = clsData.CoAofCOGS
                .Parameters.Add("@CoAofStock", SqlDbType.Int).Value = clsData.CoAofStock
                .Parameters.Add("@CoAofCash", SqlDbType.Int).Value = clsData.CoAofCash
                .Parameters.Add("@CoAofAccountPayable", SqlDbType.Int).Value = clsData.CoAofAccountPayable
                .Parameters.Add("@CoAofPurchaseDisc", SqlDbType.Int).Value = clsData.CoAofPurchaseDisc
                .Parameters.Add("@CoAofPurchaseEquipments", SqlDbType.Int).Value = clsData.CoAofPurchaseEquipments
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal intProgramID As Integer) As VO.JournalPost
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.JournalPost
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 	" & vbNewLine & _
                        "   A.CoAofRevenue, CR.Code AS CoACodeofRevenue, CR.Name AS CoANameofRevenue, 	" & vbNewLine & _
                        "	A.CoAofAccountReceivable, CAR.Code AS CoACodeofAccountReceivable, CAR.Name AS CoANameofAccountReceivable, 	" & vbNewLine & _
                        "	A.CoAofSalesDisc, CSD.Code AS CoACodeofSalesDisc, CSD.Name AS CoANameofSalesDisc, 	" & vbNewLine & _
                        "	A.CoAofCOGS, CCOGS.Code AS CoACodeofCOGS, CCOGS.Name AS CoANameofCOGS, 	" & vbNewLine & _
                        "	A.CoAofStock, CST.Code AS CoACodeofStock, CST.Name AS CoANameofStock, 	" & vbNewLine & _
                        "	A.CoAofCash, CCASH.Code AS CoACodeofCash, CCASH.Name AS CoANameofCash, 	" & vbNewLine & _
                        "	A.CoAofAccountPayable, CAP.Code AS CoACodeofAccountPayable, CAP.Name AS CoANameofAccountPayable, 	" & vbNewLine & _
                        "   A.CoAofPurchaseDisc, CPD.Code AS CoACodeofPurchaseDisc, CPD.Name AS CoANameofPurchaseDisc, 	" & vbNewLine & _
                        "	A.CoAofPurchaseEquipments, CPE.Code AS CoACodeofPurchaseEquipments, CPE.Name AS CoANameofPurchaseEquipments, 	" & vbNewLine & _
                        "	A.Remarks, A.LogBy, A.LogDate, A.LogInc  	" & vbNewLine & _
                        "FROM sysJournalPost A 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CR ON 	" & vbNewLine & _
                        "	A.CoAofRevenue=CR.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CAR ON 	" & vbNewLine & _
                        "	A.CoAofAccountReceivable=CAR.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CSD ON 	" & vbNewLine & _
                        "	A.CoAofSalesDisc=CSD.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CCOGS ON 	" & vbNewLine & _
                        "	A.CoAofCOGS=CCOGS.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CST ON 	" & vbNewLine & _
                        "	A.CoAofStock=CST.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CCASH ON 	" & vbNewLine & _
                        "	A.CoAofCash=CCASH.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CAP ON 	" & vbNewLine & _
                        "	A.CoAofAccountPayable=CAP.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CPD ON 	" & vbNewLine & _
                        "	A.CoAofPurchaseDisc=CPD.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount CPE ON 	" & vbNewLine & _
                        "	A.CoAofPurchaseEquipments=CPE.ID	" & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "	A.ProgramID=@ProgramID " & vbNewLine

                    .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.CoAofRevenue = .Item("CoAofRevenue")
                        voReturn.CoACodeofRevenue = .Item("CoACodeofRevenue")
                        voReturn.CoANameofRevenue = .Item("CoANameofRevenue")

                        voReturn.CoAofAccountReceivable = .Item("CoAofAccountReceivable")
                        voReturn.CoACodeofAccountReceivable = .Item("CoACodeofAccountReceivable")
                        voReturn.CoANameofAccountReceivable = .Item("CoANameofAccountReceivable")

                        voReturn.CoAofSalesDisc = .Item("CoAofSalesDisc")
                        voReturn.CoACodeofSalesDisc = .Item("CoACodeofSalesDisc")
                        voReturn.CoANameofSalesDisc = .Item("CoANameofSalesDisc")

                        voReturn.CoAofCOGS = .Item("CoAofCOGS")
                        voReturn.CoACodeofCOGS = .Item("CoACodeofCOGS")
                        voReturn.CoANameofCOGS = .Item("CoANameofCOGS")

                        voReturn.CoAofStock = .Item("CoAofStock")
                        voReturn.CoACodeofStock = .Item("CoACodeofStock")
                        voReturn.CoANameofStock = .Item("CoANameofStock")

                        voReturn.CoAofCash = .Item("CoAofCash")
                        voReturn.CoACodeofCash = .Item("CoACodeofCash")
                        voReturn.CoANameofCash = .Item("CoANameofCash")

                        voReturn.CoAofAccountPayable = .Item("CoAofAccountPayable")
                        voReturn.CoACodeofAccountPayable = .Item("CoACodeofAccountPayable")
                        voReturn.CoANameofAccountPayable = .Item("CoANameofAccountPayable")

                        voReturn.CoAofPurchaseDisc = .Item("CoAofPurchaseDisc")
                        voReturn.CoACodeofPurchaseDisc = .Item("CoACodeofPurchaseDisc")
                        voReturn.CoANameofPurchaseDisc = .Item("CoANameofPurchaseDisc")

                        voReturn.CoAofPurchaseEquipments = .Item("CoAofPurchaseEquipments")
                        voReturn.CoACodeofPurchaseEquipments = .Item("CoACodeofPurchaseEquipments")
                        voReturn.CoANameofPurchaseEquipments = .Item("CoANameofPurchaseEquipments")

                        voReturn.Remarks = .Item("Remarks")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.LogInc = .Item("LogInc")
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

        Public Shared Function DataExists() As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   CoAofRevenue " & vbNewLine & _
                        "FROM sysJournalPost " & vbNewLine

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

    End Class

End Namespace

