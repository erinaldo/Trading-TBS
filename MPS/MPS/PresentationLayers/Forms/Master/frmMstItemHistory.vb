Imports DevExpress.XtraGrid

Public Class frmMstItemHistory

    Public Property pubItemID As Integer = 0

    Private Const _
       cRefresh = 0, cClose = 1

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "Trans", "Transaksi", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "ID", "Nomor", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "BPID", "BPID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "BPName", "Pelanggan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "TransactionDate", "Tanggal", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "Qty", "Jumlah", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "UomID", "UomID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "UomCode", "Satuan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Price", "Harga", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Disc", "Disc %", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Tax", "Tax", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "TotalPrice", "Total Harga", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Remarks", "Remarks", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "IDStatus", "IDStatus", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CreatedBy", "Dibuat Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CreatedDate", "Tanggal Buat", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "LogBy", "Dibuat Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "LogDate", "Tanggal Edit", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "LogInc", "LogInc", 100, UI.usDefGrid.gIntNum)
        UI.usForm.SetGrid(grdView, "StatusInfo", "Status", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "JournalID", "JournalID", 100, UI.usDefGrid.gString, False)
    End Sub

    Private Sub prvFillCombo()
        UI.usForm.FillComboBox(cboTransaction, VO.Item.DataHistoryFilter, "ID", "Name")
    End Sub

    Private Sub prvQuery()
        Me.Cursor = Cursors.WaitCursor
        progressBar.Visible = True
        Try
            grdMain.DataSource = BL.History.ListDataHistoryBussinessPartners(dtpDateFrom.Value.Date, dtpDateTo.Value.Date, cboTransaction.SelectedValue, pubItemID)
            prvSumGrid()
            If grdView.GroupCount > 0 Then grdView.ExpandAllGroups()
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            progressBar.Visible = False
        End Try
    End Sub
    
    Private Sub prvSumGrid()
        Dim SumQty As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "Total Jumlah: {0:#,##0.00}")
        Dim SumTotalPrice As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "Total Harga: {0:#,##0.00}")

        If grdView.Columns("Qty").SummaryText.Trim = "" Then
            grdView.Columns("Qty").Summary.Add(SumQty)
        End If

        If grdView.Columns("TotalPrice").SummaryText.Trim = "" Then
            grdView.Columns("TotalPrice").Summary.Add(SumTotalPrice)
        End If
    End Sub

    Public Sub pubRefresh(Optional ByVal strSearch As String = "")
        With grdView
            If Not grdView.FocusedValue Is Nothing And strSearch = "" Then
                strSearch = grdView.GetDataRow(grdView.FocusedRowHandle).Item("ID")
            End If
            prvQuery()
            If grdView.RowCount > 0 Then UI.usForm.GridMoveRow(grdView, "ID", strSearch)
        End With
    End Sub

    Private Sub prvClear()
        grdMain.DataSource = Nothing
        grdView.Columns.Clear()
        prvSetGrid()
    End Sub

#Region "Form Handle"

    Private Sub frmMstItemHistory_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstItemHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillCombo()
        prvSetGrid()
        cboTransaction.SelectedValue = VO.Status.Values.All
        dtpDateFrom.Value = Today.Date.AddDays(-7)
        dtpDateTo.Value = Today.Date
        prvQuery()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cRefresh).Name Then
            pubRefresh()
        ElseIf e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        End If
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        prvQuery()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        prvClear()
    End Sub

    Private Sub cboTransaction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTransaction.SelectedIndexChanged
        prvClear()
    End Sub

#End Region

End Class