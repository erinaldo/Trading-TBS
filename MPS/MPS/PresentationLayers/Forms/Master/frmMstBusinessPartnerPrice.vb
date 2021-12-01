Imports DevExpress.XtraGrid.Columns

Public Class frmMstBusinessPartnerPrice

#Region "Property"

    Private frmParent As frmMstBusinessPartner
    Private clsData As VO.BusinessPartner
    Private dtData As New DataTable
    Private bolValid As Boolean = True

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "Name", "Nama", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "SalesPrice", "Harga Jual", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "PurchasePrice1", "Harga Beli 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "PurchasePrice2", "Harga Beli 2", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "SalesPriceNew", "Harga Jual [Baru]", 100, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdView, "PurchasePrice1New", "Harga Beli 1 [Baru]", 100, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdView, "PurchasePrice2New", "Harga Beli 2 [Baru]", 100, UI.usDefGrid.gReal2Num, True, False)
        grdView.Columns("SalesPriceNew").ColumnEdit = rpiAmount
        grdView.Columns("PurchasePrice1New").ColumnEdit = rpiAmount
        grdView.Columns("PurchasePrice2New").ColumnEdit = rpiAmount
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cSave).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            dtData = BL.BusinessPartner.ListDataForUpdatePrice
            grdMain.DataSource = dtData
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
    End Sub

    Private Sub prvSave()
        If bolValid = False Then Exit Sub
        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub
        grdView.ClearColumnsFilter()
        Dim drPrice() As DataRow = dtData.Select("SalesPriceNew>0 OR PurchasePrice1New>0 OR PurchasePrice2New>0")
        If drPrice.Count = 0 Then UI.usForm.frmMessageBox("Mohon diubah harga terlebih dahulu") : Exit Sub

        Dim clsDataAll(drPrice.Count - 1) As VO.BusinessPartner
        For i As Integer = 0 To drPrice.Count - 1
            clsData = New VO.BusinessPartner
            clsData.ID = drPrice(i).Item("ID")
            clsData.SalesPrice = IIf(drPrice(i).Item("SalesPriceNew").Equals(DBNull.Value), 0, drPrice(i).Item("SalesPriceNew"))
            clsData.PurchasePrice1 = IIf(drPrice(i).Item("PurchasePrice1New").Equals(DBNull.Value), 0, drPrice(i).Item("PurchasePrice1New"))
            clsData.PurchasePrice2 = IIf(drPrice(i).Item("PurchasePrice2New").Equals(DBNull.Value), 0, drPrice(i).Item("PurchasePrice2New"))
            clsData.LogBy = MPSLib.UI.usUserApp.UserID
            clsDataAll(i) = clsData
        Next

        Try
            If BL.BusinessPartner.UpdatePrice(clsDataAll) Then
                UI.usForm.frmMessageBox("Ubah harga berhasil")
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, VO.Access.Values.ChangePriceAccess)
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmMstBusinessPartnerPrice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetGrid()
        prvQuery()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    'Private Sub grdView_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles grdView.ValidatingEditor
    '    With grdView
    '        Dim col As GridColumn = .FocusedColumn
    '        Dim intFocus As Integer = .FocusedRowHandle
    '        If col.Name = "SalesPriceNew" Or col.Name = "PurchasePrice1New" Or col.Name = "PurchasePrice2New" Then
    '            Dim oldValue As Decimal = IIf(.GetFocusedRowCellValue(col).Equals(DBNull.Value), 0, .GetFocusedRowCellValue(col))
    '            If e.Value Is Nothing Then e.Value = 0
    '            Dim newValue As Decimal = IIf((e.Value = "") Or (e.Value.Equals(DBNull.Value) Or (e.Value = ".")), 0, e.Value)
    '            Dim strErrorMessage As String = ""
    '            If newValue < 0 Then
    '                bolValid = False
    '                strErrorMessage = "Nilai harus lebih besar dari 0"
    '                UI.usForm.frmMessageBox(strErrorMessage)
    '            End If

    '            If bolValid Then
    '                .SetRowCellValue(intFocus, col.Name, newValue)
    '                .UpdateCurrentRow()
    '            End If
    '        End If
    '    End With
    'End Sub

#End Region

End Class