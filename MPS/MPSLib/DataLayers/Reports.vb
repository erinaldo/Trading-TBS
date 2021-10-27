Namespace DL
    Public Class Reports

        Public Shared Function SalesReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	SH.ID AS SalesNo, SH.SalesDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, SD.Qty AS OrderQty, SD.Disc, SD.Price, SD.TotalPrice, 	" & vbNewLine & _
                    "	SH.CreatedBy, SH.Remarks 	" & vbNewLine & _
                    "FROM traSaleS SH 	" & vbNewLine & _
                    "INNER JOIN traSalesDet SD ON 	" & vbNewLine & _
                    "	SH.ID=SD.SalesID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	SH.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	SD.ItemID=MI.ID 	" & vbNewLine & _
                    "INNER JOIN mstUom MU ON 	" & vbNewLine & _
                    "	MI.UomID1=MU.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	SH.IsDeleted=0 " & vbNewLine & _
                    "	AND SH.SalesDate>=@DateFrom AND SH.SalesDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND SH.BPID=@BPID "
                End If

                If intItemID > 0 Then
                    .CommandText += "   AND SD.ItemID=@ItemID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function SalesReturnReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	SRH.ID AS SalesReturnNo, SRH.SalesReturnDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	SD.SalesID AS SalesNo, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, SRD.Qty AS OrderQty, SRD.Disc, SRD.Price, SRD.TotalPrice, 	" & vbNewLine & _
                    "	SRH.CreatedBy, SRH.Remarks 	" & vbNewLine & _
                    "FROM traSalesReturn SRH 	" & vbNewLine & _
                    "INNER JOIN traSalesReturnDet SRD ON 	" & vbNewLine & _
                    "	SRH.ID=SRD.SalesReturnID 	" & vbNewLine & _
                    "INNER JOIN traSalesDet SD ON 	" & vbNewLine & _
                    "	SRD.SalesDetID=SD.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	SRH.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	SRD.ItemID=MI.ID 	" & vbNewLine & _
                    "INNER JOIN mstUom MU ON 	" & vbNewLine & _
                    "	MI.UomID1=MU.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	SRH.IsDeleted=0 " & vbNewLine & _
                    "	AND SRH.SalesReturnDate>=@DateFrom AND SRH.SalesReturnDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND SRH.BPID=@BPID "
                End If

                If intItemID > 0 Then
                    .CommandText += "   AND SRD.ItemID=@ItemID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function PurchaseReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	RH.ID AS ReceiveNo, RH.ReceiveDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, RHD.Qty AS OrderQty, RHD.Disc, RHD.Price, RHD.TotalPrice, 	" & vbNewLine & _
                    "	RH.CreatedBy, RH.Remarks 	" & vbNewLine & _
                    "FROM traReceive RH 	" & vbNewLine & _
                    "INNER JOIN traReceiveDet RHD ON 	" & vbNewLine & _
                    "	RH.ID=RHD.ReceiveID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	RH.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	RHD.ItemID=MI.ID 	" & vbNewLine & _
                    "INNER JOIN mstUom MU ON 	" & vbNewLine & _
                    "	MI.UomID1=MU.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	RH.IsDeleted=0	" & vbNewLine & _
                    "	AND RH.ReceiveDate>=@DateFrom AND RH.ReceiveDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND RH.BPID=@BPID "
                End If

                If intItemID > 0 Then
                    .CommandText += "   AND RHD.ItemID=@ItemID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function PurchaseReturnReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	SRH.ID AS ReceiveReturnNo, SRH.ReceiveReturnDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	SD.ReceiveID AS ReceiveNo, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, SRD.Qty AS OrderQty, SRD.Disc, SRD.Price, SRD.TotalPrice, 	" & vbNewLine & _
                    "	SRH.CreatedBy, SRH.Remarks 	" & vbNewLine & _
                    "FROM traReceiveReturn SRH 	" & vbNewLine & _
                    "INNER JOIN traReceiveReturnDet SRD ON 	" & vbNewLine & _
                    "	SRH.ID=SRD.ReceiveReturnID 	" & vbNewLine & _
                    "INNER JOIN traReceiveDet SD ON 	" & vbNewLine & _
                    "	SRD.ReceiveDetID=SD.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	SRH.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	SRD.ItemID=MI.ID 	" & vbNewLine & _
                    "INNER JOIN mstUom MU ON 	" & vbNewLine & _
                    "	MI.UomID1=MU.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	SRH.IsDeleted=0 " & vbNewLine & _
                    "	AND SRH.ReceiveReturnDate>=@DateFrom AND SRH.ReceiveReturnDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND SRH.BPID=@BPID "
                End If

                If intItemID > 0 Then
                    .CommandText += "   AND SRD.ItemID=@ItemID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function AccountPayableReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	AP.ID AS APNo, AP.APDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	MPR.Name AS PaymentReference, AP.ReferencesNote, 	" & vbNewLine & _
                    "	APD.ReceiveID, APD.Amount, AP.CreatedBy, AP.Remarks	" & vbNewLine & _
                    "FROM traAccountPayable AP 	" & vbNewLine & _
                    "INNER JOIN traAccountPayableDet APD ON 	" & vbNewLine & _
                    "	AP.ID=APD.APID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	AP.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstPaymentReferences MPR ON 	" & vbNewLine & _
                    "	AP.PaymentReferencesID=MPR.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	AP.IsDeleted=0 	" & vbNewLine & _
                    "	AND AP.APDate>=@DateFrom AND AP.APDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND AP.BPID=@BPID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function AccountReceivableReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	AR.ID AS ARNo, AR.ARDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	MPR.Name AS PaymentReference, AR.ReferencesNote, 	" & vbNewLine & _
                    "	ARD.SalesID, ARD.Amount, AR.CreatedBy, AR.Remarks	" & vbNewLine & _
                    "FROM traAccountReceivable AR 	" & vbNewLine & _
                    "INNER JOIN traAccountReceivableDet ARD ON 	" & vbNewLine & _
                    "	AR.ID=ARD.ARID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	AR.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstPaymentReferences MPR ON 	" & vbNewLine & _
                    "	AR.PaymentReferencesID=MPR.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	AR.IsDeleted=0 	" & vbNewLine & _
                    "	AND AR.ARDate>=@DateFrom AND AR.ARDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND AR.BPID=@BPID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function CostReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	TC.ID, TC.CostDate, CC.Code + ' | ' + CC.Name AS CreditAccount, TC.TotalAmount, 	" & vbNewLine & _
                    "	CD.Code AS DebitCode, CD.Name AS DebitName, TCD.Remarks AS RemarksDetail, TCD.Amount, 	" & vbNewLine & _
                    "	TC.CreatedBy, TC.Remarks 	" & vbNewLine & _
                    "FROM traCost TC 	" & vbNewLine & _
                    "INNER JOIN traCostDet TCD ON 	" & vbNewLine & _
                    "	TC.ID=TCD.CostID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount CC ON 	" & vbNewLine & _
                    "	TC.CoAID=CC.ID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount CD ON 	" & vbNewLine & _
                    "	TCD.CoAID=CD.ID 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	TC.IsDeleted=0	" & vbNewLine & _
                    "	AND TC.CostDate>=@DateFrom AND TC.CostDate<=@DateTo	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function JournalReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	JH.ID, JH.JournalDate, JH.TotalAmount, COA.Code AS AccountCode, COA.Name AS AccountName, JD.DebitAmount, JD.CreditAmount, 	" & vbNewLine & _
                    "	JH.CreatedBy, JH.Remarks 	" & vbNewLine & _
                    "FROM traJournal JH 	" & vbNewLine & _
                    "INNER JOIN traJournalDet JD ON 	" & vbNewLine & _
                    "	JH.ID=JD.JournalID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "	JD.CoAID=COA.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	JH.IsDeleted=0 	" & vbNewLine & _
                    "	AND JH.IsAutoGenerate=0 	" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#Region "Profit and Loss"

        Public Shared Function RevenueReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 		" & vbNewLine & _
                    "	COA.Name, SUM(JD.CreditAmount)-SUM(JD.DebitAmount) AS TotalAmount	" & vbNewLine & _
                    "FROM traJournalDet JD 		" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 		" & vbNewLine & _
                    "	JD.JournalID=JH.ID 		" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "	JD.CoAID=COA.ID 	" & vbNewLine & _
                    "	AND COA.AccountGroupID=12	" & vbNewLine & _
                    "WHERE 		" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1))	" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "GROUP BY COA.Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function DiscountRevenueReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 		" & vbNewLine & _
                    "	COA.Name, SUM(JD.CreditAmount)-SUM(JD.DebitAmount) AS TotalAmount	" & vbNewLine & _
                    "FROM traJournalDet JD 		" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 		" & vbNewLine & _
                    "	JD.JournalID=JH.ID 		" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "	JD.CoAID=COA.ID 	" & vbNewLine & _
                    "	AND COA.AccountGroupID=17	" & vbNewLine & _
                    "WHERE 		" & vbNewLine & _
                    "	JH.IsDeleted=0 	" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 	" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "GROUP BY COA.Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function FirstStockReport(ByVal dtmDateFrom As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 			" & vbNewLine & _
                    "	'PERSEDIAAN AWAL' AS Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount		" & vbNewLine & _
                    "INTO #TFirstStock 	" & vbNewLine & _
                    "FROM traJournalDet JD 			" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 			" & vbNewLine & _
                    "	JD.JournalID=JH.ID 			" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 		" & vbNewLine & _
                    "	JD.CoAID=COA.ID 		" & vbNewLine & _
                    "	AND COA.AccountGroupID=3		" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 		" & vbNewLine & _
                    "	AND JH.JournalDate<@DateFrom 		" & vbNewLine & _
                    "GROUP BY COA.Name		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL	" & vbNewLine & _
                    "SELECT 			" & vbNewLine & _
                    "	'PERSEDIAAN AWAL' AS Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount		" & vbNewLine & _
                    "FROM traJournalDet JD 			" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 			" & vbNewLine & _
                    "	JD.JournalID=JH.ID 			" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 		" & vbNewLine & _
                    "	JD.CoAID=COA.ID 		" & vbNewLine & _
                    "	AND COA.AccountGroupID=18		" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 		" & vbNewLine & _
                    "	AND JH.JournalDate<@DateFrom 		" & vbNewLine & _
                    "GROUP BY COA.Name		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	Name, SUM(TotalAmount) AS TotalAmount  	" & vbNewLine & _
                    "FROM #TFirstStock 	" & vbNewLine & _
                    "GROUP BY Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom.AddDays(-1)
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function PurchaseStockReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 			" & vbNewLine & _
                    "	'PEMBELIAN' AS Name, ISNULL(SUM(RH.GrandTotal),0) AS TotalAmount		" & vbNewLine & _
                    "INTO #T_PurchaseStock	" & vbNewLine & _
                    "FROM traReceive RH 			" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	RH.IsDeleted=0 		" & vbNewLine & _
                    "	AND RH.IsPostedGL=1		" & vbNewLine & _
                    "	AND RH.ReceiveDate>=@DateFrom AND RH.ReceiveDate<=@DateTo		" & vbNewLine & _
                    "		" & vbNewLine & _
                    "UNION ALL 	" & vbNewLine & _
                    "SELECT 			" & vbNewLine & _
                    "	'PEMBELIAN' AS Name, ISNULL(SUM(RH.GrandTotal),0)*-1 AS TotalAmount		" & vbNewLine & _
                    "FROM traReceiveReturn RH 			" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	RH.IsDeleted=0 		" & vbNewLine & _
                    "	AND RH.IsPostedGL=1		" & vbNewLine & _
                    "	AND RH.ReceiveReturnDate>=@DateFrom AND RH.ReceiveReturnDate<=@DateTo		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	Name, SUM(TotalAmount) AS TotalAmount	" & vbNewLine & _
                    "FROM #T_PurchaseStock	" & vbNewLine & _
                    "GROUP BY Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function LastStockReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 			" & vbNewLine & _
                    "	'PERSEDIAAN AKHIR' AS Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount		" & vbNewLine & _
                    "INTO #TLastStock 	" & vbNewLine & _
                    "FROM traJournalDet JD 			" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 			" & vbNewLine & _
                    "	JD.JournalID=JH.ID 			" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 		" & vbNewLine & _
                    "	JD.CoAID=COA.ID 		" & vbNewLine & _
                    "	AND COA.AccountGroupID=3		" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 		" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 		" & vbNewLine & _
                    "GROUP BY COA.Name		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL	" & vbNewLine & _
                    "SELECT 			" & vbNewLine & _
                    "	'PERSEDIAAN AKHIR' AS Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount		" & vbNewLine & _
                    "FROM traJournalDet JD 			" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 			" & vbNewLine & _
                    "	JD.JournalID=JH.ID 			" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 		" & vbNewLine & _
                    "	JD.CoAID=COA.ID 		" & vbNewLine & _
                    "	AND COA.AccountGroupID=18		" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 		" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 		" & vbNewLine & _
                    "GROUP BY COA.Name		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	Name, SUM(TotalAmount) AS TotalAmount  	" & vbNewLine & _
                    "FROM #TLastStock 	" & vbNewLine & _
                    "GROUP BY Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ExpensesReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 		" & vbNewLine & _
                    "	COA.Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount	" & vbNewLine & _
                    "FROM traJournalDet JD 		" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 		" & vbNewLine & _
                    "	JD.JournalID=JH.ID 		" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "	JD.CoAID=COA.ID 	" & vbNewLine & _
                    "	AND COA.AccountGroupID IN (14,15)	" & vbNewLine & _
                    "WHERE 		" & vbNewLine & _
                    "	JH.IsDeleted=0 	" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 	" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "GROUP BY COA.Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

#Region "Balance Sheet"

        Public Shared Function BalanceSheetDebitReport(ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	COA.Code AS CoACode, COA.Name AS CoAName, 	" & vbNewLine & _
                    "	DebitAmount=ISNULL(	" & vbNewLine & _
                    "	COA.FirstBalance+	" & vbNewLine & _
                    "	CASE WHEN 	" & vbNewLine & _
                    "		COA.AccountGroupID=1 OR COA.AccountGroupID=2 OR COA.AccountGroupID=3 OR COA.AccountGroupID=4 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=5 OR COA.AccountGroupID=6 OR COA.AccountGroupID=7 OR COA.AccountGroupID=17 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=18 OR COA.AccountGroupID=13 OR COA.AccountGroupID=14 OR COA.AccountGroupID=15 OR COA.AccountGroupID=16 	" & vbNewLine & _
                    "	THEN ISNULL(JH.TotalDebit,0)-ISNULL(JH.TotalCredit,0) " & vbNewLine & _
                    "	ELSE ISNULL(JH.TotalCredit,0)-ISNULL(JH.TotalDebit,0) " & vbNewLine & _
                    "	END,0),	" & vbNewLine & _
                    "	CreditAmount=CAST(0 AS DECIMAL(18,2))" & vbNewLine & _
                    "FROM mstChartOfAccount COA 	" & vbNewLine & _
                    "INNER JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		JD.CoAID, SUM(JD.DebitAmount) AS TotalDebit, SUM(JD.CreditAmount) AS TotalCredit	" & vbNewLine & _
                    "	FROM traJournalDet JD 	" & vbNewLine & _
                    "	INNER JOIN traJournal JH ON 	" & vbNewLine & _
                    "		JD.JournalID=JH.ID 	" & vbNewLine & _
                    "	WHERE 	" & vbNewLine & _
                    "		JH.IsDeleted=0 	" & vbNewLine & _
                    "	    AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 	" & vbNewLine & _
                    "	    AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "	GROUP BY JD.CoAID 	" & vbNewLine & _
                    ") JH ON COA.ID=JH.CoAID	" & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    COA.AccountGroupID IN (1,2,4,5,6,7)" & vbNewLine & _
                    "ORDER BY " & vbNewLine & _
                    "    COA.Code ASC " & vbNewLine

                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function BalanceSheetCreditReport(ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	COA.Code AS CoACode, COA.Name AS CoAName, 	" & vbNewLine & _
                    "	DebitAmount=CAST(0 AS DECIMAL(18,2))," & vbNewLine & _
                    "	CreditAmount=ISNULL(	" & vbNewLine & _
                    "	COA.FirstBalance+	" & vbNewLine & _
                    "	CASE WHEN 	" & vbNewLine & _
                    "		COA.AccountGroupID=1 OR COA.AccountGroupID=2 OR COA.AccountGroupID=3 OR COA.AccountGroupID=4 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=5 OR COA.AccountGroupID=6 OR COA.AccountGroupID=7 OR COA.AccountGroupID=17 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=18 OR COA.AccountGroupID=13 OR COA.AccountGroupID=14 OR COA.AccountGroupID=15 OR COA.AccountGroupID=16 	" & vbNewLine & _
                    "	THEN ISNULL(JH.TotalDebit,0)-ISNULL(JH.TotalCredit,0) " & vbNewLine & _
                    "	ELSE ISNULL(JH.TotalCredit,0)-ISNULL(JH.TotalDebit,0) " & vbNewLine & _
                    "	END,0) " & vbNewLine & _
                    "FROM mstChartOfAccount COA 	" & vbNewLine & _
                    "INNER JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		JD.CoAID, SUM(JD.DebitAmount) AS TotalDebit, SUM(JD.CreditAmount) AS TotalCredit	" & vbNewLine & _
                    "	FROM traJournalDet JD 	" & vbNewLine & _
                    "	INNER JOIN traJournal JH ON 	" & vbNewLine & _
                    "		JD.JournalID=JH.ID 	" & vbNewLine & _
                    "	WHERE 	" & vbNewLine & _
                    "		JH.IsDeleted=0 	" & vbNewLine & _
                    "	    AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 	" & vbNewLine & _
                    "	    AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "	GROUP BY JD.CoAID 	" & vbNewLine & _
                    ") JH ON COA.ID=JH.CoAID	" & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    COA.AccountGroupID IN (8,9,10,11)" & vbNewLine & _
                    "ORDER BY " & vbNewLine & _
                    "    COA.Code ASC " & vbNewLine

                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function


#End Region

    End Class
End Namespace

