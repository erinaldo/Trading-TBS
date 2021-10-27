Namespace VO
    Public Class Modules
        Inherits Common
        Property ID As Integer
        Property Name As String
        Property Remarks As String

        Enum Values
            All = 0

            '# Master
            MasterCompany = 1
            MasterChartOfAccountGroup = 2
            MasterChartOfAccount = 3
            MasterUser = 4
            MasterBusinessPartners = 5
            MasterPaymentTerm = 6
            MasterPaymentReferences = 7
            MasterSalesDiscount = 8
            MasterUOM = 9
            MasterItem = 10

            '# Settings
            MasterPostingTransaction = 11
            MasterUnPostingTransaction = 12
            MasterPostingJournalTransaction = 13

            '# Transactions
            TransactionSales = 14
            TransactionReceive = 15
            TransactionSalesReturn = 16
            TransactionReceiveReturn = 17
            TransactionAccountPayable = 18
            TransactionAccountReceivable = 19
            TransactionCost = 20
            TransactionJournal = 21

            '# Reports
            ReportProfitAndLoss = 22
            ReportBalanceSheet = 23
        End Enum

    End Class
End Namespace

