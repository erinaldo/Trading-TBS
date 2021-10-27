Namespace VO
    Public Class ChartOfAccountGroup
        Inherits Common
        Property ID As Integer
        Property Name As String
        Property AliasName As String
        Property IDStatus As Integer

        Enum Values
            All = 0
            CashAndBank = 1 'Activa / Current Assets
            AccountReceivable = 2 'Activa / Current Assets
            Stock = 3 'Activa / Current Assets
            OtherCurrentAssets = 4 'Activa / Current Assets
            FixedAssets = 5 'Activa / Fixed Assets
            AccumulatedDepreciation = 6 'Activa / Fixed Assets
            OtherAssets = 7 'Activa / Fixed Assets
            SalesDisc = 17 'Activa
            PurchaseDisc = 18 'Activa
            PurchaseTax = 19 'Activa
            AccountPayable = 8 'Liabilities
            ShortObligation = 9 'Liabilities
            LongTermObligation = 10 'Liabilities
            Capital = 11 'Capital
            Revenue = 12 'Revenue
            CostOfGoodsSold = 13 'Revenue
            Expenses = 14 'Expenses
            OtherExpenses = 15 'Expenses
            OtherIncome = 16 'Expenses
        End Enum
    End Class 
End Namespace

