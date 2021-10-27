Public Class frmTraReceiveListOutstandingUsage

    Public pubLUdtRow As DataRow
    Public pubIsLookUpGet As Boolean = False
    Private strSalesID As String = ""
    Private intPos As Integer = 0

    Public WriteOnly Property pubSalesID As String
        Set(value As String)
            strSalesID = value
        End Set
    End Property

    Private Const _
       cGet = 0, cSep1 = 1, cRefresh = 2, cClose = 3

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ID", "Nomor", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "BPID", "BPID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "BPName", "Pelanggan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "SalesDate", "Tanggal", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "PaymentTerm", "PaymentTerm", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "DueDate", "Jatuh Tempo", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "DriverName", "Nama Supir", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PlatNumber", "Nomor Plat", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PPN", " PPN", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "PPH", " PPH", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "ItemID", "ItemID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ItemCode", "Kode Barang", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "ItemName", "Nama Barang", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "UomID", "UomID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "UomCode", "Satuan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "ArrivalBrutto", "Brutto", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalTarra", "Tarra", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalNettoBefore", "Netto 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalDeduction", "Potongan", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalNettoAfter", "Netto 2", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "MaxBrutto", "Maksimal Brutto", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Price", "Price", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalPrice", "Total Price", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalReturn", "Total Retur", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "TotalPayment", "Total Bayar", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "IsPostedGL", "IsPostedGL", 100, UI.usDefGrid.gBoolean, False)
        UI.usForm.SetGrid(grdView, "PostedBy", "PostedBy", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "PostedDate", "PostedDate", 100, UI.usDefGrid.gFullDate, False)
        UI.usForm.SetGrid(grdView, "IsDeleted", "IsDeleted", 100, UI.usDefGrid.gBoolean, False)
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

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cGet).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            grdMain.DataSource = BL.Sales.ListDataOutstanding
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
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

    Private Sub prvGet()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        pubLUdtRow = grdView.GetDataRow(grdView.FocusedRowHandle)
        pubIsLookUpGet = True
        Me.Close()
    End Sub

#Region "Form Handle"

    Private Sub frmTraReceiveListOutstandingUsage_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmTraReceiveListOutstandingUsage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetGrid()
        prvQuery()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        ElseIf e.Button.Name = ToolBar.Buttons(cRefresh).Name Then
            pubRefresh()
        ElseIf grdView.FocusedRowHandle >= 0 Then
            Select Case e.Button.Name
                Case ToolBar.Buttons(cGet).Name : prvGet()
            End Select
        End If
    End Sub

    Private Sub grdView_DoubleClick(sender As Object, e As EventArgs) Handles grdView.DoubleClick
        prvGet()
    End Sub

#End Region
    
End Class