Namespace BL
    Public Class Reports

        Public Shared Function SalesReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.SalesReport(dtmDateFrom, dtmDateTo, intBPID, intItemID)
        End Function

        Public Shared Function SalesReturnReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.SalesReturnReport(dtmDateFrom, dtmDateTo, intBPID, intItemID)
        End Function

        Public Shared Function PurchaseReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.PurchaseReport(dtmDateFrom, dtmDateTo, intBPID, intItemID)
        End Function

        Public Shared Function PurchaseReturnReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.PurchaseReturnReport(dtmDateFrom, dtmDateTo, intBPID, intItemID)
        End Function

        Public Shared Function AccountPayableReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.AccountPayableReport(dtmDateFrom, dtmDateTo, intBPID)
        End Function

        Public Shared Function AccountReceivableReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.AccountReceivableReport(dtmDateFrom, dtmDateTo, intBPID)
        End Function

        Public Shared Function CostReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.CostReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function JournalReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.JournalReport(dtmDateFrom, dtmDateTo)
        End Function

#Region "Profit and Loss"

        Public Shared Function RevenueReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.RevenueReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function DiscountRevenueReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.DiscountRevenueReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function FirstStockReport(ByVal dtmDateFrom As DateTime) As DataTable
            BL.Server.ServerDefault()
            Return DL.Reports.FirstStockReport(dtmDateFrom)
        End Function

        Public Shared Function PurchaseStockReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.PurchaseStockReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function LastStockReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.LastStockReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function ExpensesReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.ExpensesReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function CalculateProfitAndLoss(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As Decimal
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Dim decReturn As Decimal = 0, _
                decRevenue As Decimal = 0, decCOGS As Decimal = 0, decExpenses As Decimal = 0

            Dim dtRevenue As New DataTable, dtDiscountRevenue As New DataTable, _
                dtFirstStock As New DataTable, dtPurchaseStock As New DataTable, dtLastStock As New DataTable, _
                dtExpenses As New DataTable

            '# Revenue
            dtRevenue = BL.Reports.RevenueReport(dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtRevenue.Rows
                decRevenue += dr.Item("TotalAmount")
            Next

            '# Discount Revenue
            dtDiscountRevenue = BL.Reports.DiscountRevenueReport(dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtDiscountRevenue.Rows
                decRevenue += dr.Item("TotalAmount")
            Next

            '# COGS -> First Stock
            dtFirstStock = BL.Reports.FirstStockReport(dtmDateFrom)
            For Each dr As DataRow In dtFirstStock.Rows
                decCOGS += dr.Item("TotalAmount")
            Next

            '# COGS -> Purchase Stock
            dtPurchaseStock = BL.Reports.PurchaseStockReport(dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtPurchaseStock.Rows
                decCOGS += dr.Item("TotalAmount")
            Next

            '# COGS -> Last Stock
            dtLastStock = BL.Reports.LastStockReport(dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtLastStock.Rows
                decCOGS -= dr.Item("TotalAmount")
            Next

            '# Expenses
            dtExpenses = BL.Reports.ExpensesReport(dtmDateFrom, dtmDateTo)
            For Each dr As DataRow In dtExpenses.Rows
                decExpenses += dr.Item("TotalAmount")
            Next

            decReturn = decRevenue - decCOGS - decExpenses
            Return decReturn
        End Function

#End Region

#Region "Balance Sheet"

        Public Shared Function BalanceSheetReport(ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Dim dtDebit As DataTable = DL.Reports.BalanceSheetDebitReport(dtmDateTo)
            Dim dtStock As DataTable = DL.Reports.LastStockReport("2000/01/01", dtmDateTo)
            Dim drDebit As DataRow
            If dtStock.Rows.Count = 0 Then
                drDebit = dtDebit.NewRow
                drDebit.BeginEdit()
                drDebit.Item("CoACode") = "104"
                drDebit.Item("CoAName") = "PERSEDIAAN"
                drDebit.Item("DebitAmount") = 0
                drDebit.Item("CreditAmount") = 0
                drDebit.EndEdit()
            Else
                drDebit = dtDebit.NewRow
                drDebit.BeginEdit()
                drDebit.Item("CoACode") = "101"
                drDebit.Item("CoAName") = "PERSEDIAAN"
                drDebit.Item("DebitAmount") = 0
                For Each dr As DataRow In dtStock.Rows
                    drDebit.Item("DebitAmount") += dr.Item("TotalAmount")
                Next
                drDebit.Item("CreditAmount") = 0
                drDebit.EndEdit()
            End If
            dtDebit.Rows.Add(drDebit)
            dtDebit.AcceptChanges()

            Dim dtCredit As DataTable = DL.Reports.BalanceSheetCreditReport(dtmDateTo)
            Dim drCredit As DataRow
            Dim decProfitOrLoss As Decimal = CalculateProfitAndLoss("2000/01/01", dtmDateTo.Date)
            drCredit = dtCredit.NewRow
            drCredit.BeginEdit()
            drCredit.Item("CoACode") = "999"
            drCredit.Item("CoAName") = IIf(decProfitOrLoss > 0, "LABA", "RUGI") & " BULAN BERJALAN"
            drCredit.Item("DebitAmount") = 0
            drCredit.Item("CreditAmount") = decProfitOrLoss
            drCredit.EndEdit()
            dtCredit.Rows.Add(drCredit)
            dtCredit.AcceptChanges()

            Dim dtReturn As New DataTable
            dtReturn.Merge(dtDebit)
            dtReturn.Merge(dtCredit)
            dtReturn.DefaultView.Sort = "CoACode ASC"

            Return dtReturn
        End Function

#End Region

    End Class
End Namespace

