Namespace BL
    Public Class COGS

        Public Shared Function CalculateCOGS(ByVal dtItem As DataTable, ByVal strReferencesID As String, ByVal dtmReferencesDate As DateTime) As Decimal
            Dim decReturn As Decimal = 0
            Dim dtStockIn As New DataTable
            For Each dr As DataRow In dtItem.Rows
                Dim decQty As Decimal = dr.Item("Qty")

                dtStockIn = DL.StockIN.ListDataOutstanding(dr.Item("ItemID"))

                For Each drIn As DataRow In dtStockIn.Rows
                    If drIn.Item("Qty") - drIn.Item("QtyOut") >= decQty Then
                        DL.StockIN.UpdateQtyStockOut(drIn.Item("ID"), drIn.Item("QtyOut") + decQty)

                        Dim clsOut As New VO.StockOut
                        clsOut.ID = DL.StockOut.GetMaxID
                        clsOut.IDStockIN = drIn.Item("ID")
                        clsOut.ItemID = drIn.Item("ItemID")
                        clsOut.ReferencesIDDetail = dr.Item("ID")
                        clsOut.ReferencesID = strReferencesID
                        clsOut.ReferencesDate = dtmReferencesDate
                        clsOut.Qty = decQty
                        DL.StockOut.SaveData(clsOut)

                        decReturn += decQty * drIn.Item("NetPrice")
                        Exit For
                    ElseIf drIn.Item("Qty") - drIn.Item("QtyOut") < decQty Then
                        DL.StockIN.UpdateQtyStockOut(drIn.Item("ID"), drIn.Item("QtyOut") + drIn.Item("Qty") - drIn.Item("QtyOut")) '# Qty Out + Qty Sisa
                        decQty = decQty - drIn.Item("Qty") - drIn.Item("QtyOut")

                        Dim clsOut As New VO.StockOut
                        clsOut.ID = DL.StockOut.GetMaxID
                        clsOut.IDStockIN = drIn.Item("ID")
                        clsOut.ItemID = drIn.Item("ItemID")
                        clsOut.ReferencesIDDetail = dr.Item("ID")
                        clsOut.ReferencesID = strReferencesID
                        clsOut.ReferencesDate = dtmReferencesDate
                        clsOut.Qty = drIn.Item("Qty") - drIn.Item("QtyOut")
                        DL.StockOut.SaveData(clsOut)

                        decReturn += (drIn.Item("Qty") - drIn.Item("QtyOut")) * drIn.Item("NetPrice")
                    End If
                Next
            Next

            Return decReturn
        End Function

        Public Shared Function RevertCalculateCOGS(ByVal dtItem As DataTable, ByVal strReferencesID As String, ByVal dtmReferencesDate As DateTime) As Decimal
            Dim decReturn As Decimal = 0
            Dim dtStockIn As New DataTable
            Dim clsStockIn As New VO.StockIN
            For Each dr As DataRow In dtItem.Rows
                Dim decQty As Decimal = dr.Item("Qty")

                dtStockIn = DL.StockIN.ListDataByReferencesIDDetail(dr.Item("SalesDetID"))

                For Each drIn As DataRow In dtStockIn.Rows
                    If drIn.Item("Qty") >= decQty Then
                        decReturn += decQty * drIn.Item("NetPrice")

                        clsStockIn = New VO.StockIN
                        clsStockIn.ID = DL.StockIN.GetMaxID
                        clsStockIn.ItemID = dr.Item("ItemID")
                        clsStockIn.ReferencesID = strReferencesID
                        clsStockIn.ReferencesIDDetail = dr.Item("ID")
                        clsStockIn.ReferencesDate = dtmReferencesDate
                        clsStockIn.Qty = decQty
                        clsStockIn.Price = drIn.Item("Price")
                        clsStockIn.NetPrice = drIn.Item("NetPrice")
                        clsStockIn.QtyOut = 0
                        DL.StockIN.SaveData(True, clsStockIn)
                        Exit For
                    ElseIf drIn.Item("Qty") < decQty Then
                        decReturn += drIn.Item("Qty") * drIn.Item("NetPrice")

                        clsStockIn = New VO.StockIN
                        clsStockIn.ID = DL.StockIN.GetMaxID
                        clsStockIn.ItemID = dr.Item("ItemID")
                        clsStockIn.ReferencesID = strReferencesID
                        clsStockIn.ReferencesIDDetail = dr.Item("ID")
                        clsStockIn.ReferencesDate = dtmReferencesDate
                        clsStockIn.Qty = drIn.Item("Qty")
                        clsStockIn.Price = drIn.Item("Price")
                        clsStockIn.NetPrice = drIn.Item("NetPrice")
                        clsStockIn.QtyOut = 0
                        DL.StockIN.SaveData(True, clsStockIn)
                    End If
                Next
            Next

            Return decReturn
        End Function

        Public Shared Sub UnpostCOGS(ByVal strReferencesID As String)
            Dim dtStockOut As New DataTable
            dtStockOut = DL.StockOut.ListDataByReferencesID(strReferencesID)

            For Each drOut As DataRow In dtStockOut.Rows
                DL.StockIN.DecreaseQtyStockOut(drOut.Item("IDStockIN"), drOut.Item("Qty"))
            Next
        End Sub

    End Class
End Namespace