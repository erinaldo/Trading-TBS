Namespace VO
    Public Class BusinessPartner
        Inherits Common
        Property ID As Integer
        Property Name As String
        Property Address As String
        Property PICName As String
        Property PICPhoneNumber As String
        Property PaymentTermID As Integer
        Property IsUsePurchaseLimit As Boolean
        Property MaxPurchaseLimit As Decimal
        Property APBalance As Decimal
        Property ARBalance As Decimal
        Property IDStatus As Integer

        Enum HistoryFilter
            All = 0
            Sales = 1
            Receive = 2
            SalesReturn = 3
            ReceiveReturn = 4
        End Enum

        Public Shared Function DataHistoryFilter() As DataTable
            Dim dtData As New DataTable
            dtData.Columns.Add("ID", GetType(Integer))
            dtData.Columns.Add("Name", GetType(String))

            Dim dr As DataRow
            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.All
                .Item("Name") = "SEMUA"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)


            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.Sales
                .Item("Name") = "PENJUALAN"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)

            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.Receive
                .Item("Name") = "PEMBELIAN"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)

            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.SalesReturn
                .Item("Name") = "RETUR PENJUALAN"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)

            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.ReceiveReturn
                .Item("Name") = "RETUR PEMBELIAN"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)
            dtData.AcceptChanges()

            Return dtData
        End Function

    End Class 
End Namespace

