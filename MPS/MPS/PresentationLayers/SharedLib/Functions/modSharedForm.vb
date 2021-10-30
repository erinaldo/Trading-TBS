Module modSharedForm

    Public Sub ShowSplitReceive(ByVal strID As String, ByVal clsCS As VO.CS)
        Dim frmDetail As New frmTraSalesSplitReceive
        With frmDetail
            .pubCS = clsCS
            .pubSalesID = strID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

End Module
