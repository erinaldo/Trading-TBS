Public Class frmSysJournalPost

#Region "Property"

    Private _
        intCoAIDofRevenue As Integer = 0, intCoAIDofAccountReceivable As Integer = 0, intCoAIDofSalesDiscount As Integer = 0, _
        intCoAIDofCOGS As Integer = 0, intCoAIDofStock As Integer = 0, intCoAIDofCashOrBank As Integer = 0, _
        intCoAIDofAccountPayable As Integer = 0, intCoAIDofPurchaseDiscount As Integer = 0, intCoAIDofPurchaseEquipments As Integer = 0
    Private clsData As New VO.JournalPost

    Private Const _
       cSave = 0, cClose = 1

#End Region

    Private Sub prvFillForm()
        Try
            clsData = DL.JournalPost.GetDetail
            intCoAIDofRevenue = clsData.CoAofRevenue
            txtCoACodeOfRevenue.Text = clsData.CoACodeofRevenue
            txtCoANameOfRevenue.Text = clsData.CoANameofRevenue

            intCoAIDofAccountReceivable = clsData.CoAofAccountReceivable
            txtCoACodeOfAccountReceivable.Text = clsData.CoACodeofAccountReceivable
            txtCoANameOfAccountReceivable.Text = clsData.CoANameofAccountReceivable

            intCoAIDofSalesDiscount = clsData.CoAofSalesDisc
            txtCoACodeOfSalesDiscount.Text = clsData.CoACodeofSalesDisc
            txtCoANameOfSalesDiscount.Text = clsData.CoANameofSalesDisc

            intCoAIDofCOGS = clsData.CoAofCOGS
            txtCoACodeOfCOGS.Text = clsData.CoACodeofCOGS
            txtCoANameOfCOGS.Text = clsData.CoANameofCOGS

            intCoAIDofStock = clsData.CoAofStock
            txtCoACodeOfStock.Text = clsData.CoACodeofStock
            txtCoANameOfStock.Text = clsData.CoANameofStock

            intCoAIDofCashOrBank = clsData.CoAofCash
            txtCoACodeOfCashOrBank.Text = clsData.CoACodeofCash
            txtCoANameOfCashOrBank.Text = clsData.CoANameofCash

            intCoAIDofAccountPayable = clsData.CoAofAccountPayable
            txtCoACodeOfAccountPayable.Text = clsData.CoACodeofAccountPayable
            txtCoANameOfAccountPayable.Text = clsData.CoANameofAccountPayable

            intCoAIDofPurchaseDiscount = clsData.CoAofPurchaseDisc
            txtCoACodeOfPurchaseDiscount.Text = clsData.CoACodeofPurchaseDisc
            txtCoANameOfPurchaseDiscount.Text = clsData.CoANameofPurchaseDisc

            intCoAIDofPurchaseEquipments = clsData.CoAofPurchaseEquipments
            txtCoACodeOfEquipments.Text = clsData.CoACodeofPurchaseEquipments
            txtCoANameOfEquipments.Text = clsData.CoANameofPurchaseEquipments

            ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
            ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
            ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)

        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        If txtCoACodeOfRevenue.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun pendapatan belum dipilih")
            txtCoACodeOfRevenue.Focus()
            Exit Sub
        ElseIf txtCoACodeOfAccountReceivable.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun piutang usaha belum dipilih")
            txtCoACodeOfAccountReceivable.Focus()
            Exit Sub
        ElseIf txtCoACodeOfSalesDiscount.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun potongan penjualan belum dipilih")
            txtCoACodeOfSalesDiscount.Focus()
            Exit Sub
        ElseIf txtCoACodeOfCOGS.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun harga pokok penjualan belum dipilih")
            txtCoACodeOfCOGS.Focus()
            Exit Sub
        ElseIf txtCoACodeOfStock.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun persediaan belum dipilih")
            txtCoACodeOfStock.Focus()
            Exit Sub
        ElseIf txtCoACodeOfCashOrBank.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun kas / bank belum dipilih")
            txtCoACodeOfCashOrBank.Focus()
            Exit Sub
        ElseIf txtCoACodeOfAccountPayable.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun hutang usaha belum dipilih")
            txtCoACodeOfAccountPayable.Focus()
            Exit Sub
        ElseIf txtCoACodeOfPurchaseDiscount.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun potongan pembelian belum dipilih")
            txtCoACodeOfPurchaseDiscount.Focus()
            Exit Sub
        ElseIf txtCoACodeOfEquipments.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Akun peralatan belum dipilih")
            txtCoACodeOfEquipments.Focus()
            Exit Sub
        End If

        clsData = New VO.JournalPost
        clsData.CoAofRevenue = intCoAIDofRevenue
        clsData.CoAofAccountReceivable = intCoAIDofAccountReceivable
        clsData.CoAofSalesDisc = intCoAIDofSalesDiscount
        clsData.CoAofCOGS = intCoAIDofCOGS
        clsData.CoAofStock = intCoAIDofStock
        clsData.CoAofCash = intCoAIDofCashOrBank
        clsData.CoAofAccountPayable = intCoAIDofAccountPayable
        clsData.CoAofPurchaseDisc = intCoAIDofPurchaseDiscount
        clsData.CoAofPurchaseEquipments = intCoAIDofPurchaseEquipments
        clsData.Remarks = ""
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Me.Cursor = Cursors.WaitCursor
        Try
            If BL.JournalPost.SaveData(clsData) Then
                UI.usForm.frmMessageBox("Simpan data berhasil.")
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterPostingJournalTransaction, VO.Access.Values.EditAccess)
    End Sub

#Region "Form Handle"

    Private Sub frmSysJournalPost_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmSysJournalPost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillForm()
        prvUserAccess()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub btnCoAOfRevenue_Click(sender As Object, e As EventArgs) Handles btnCoAOfRevenue.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofRevenue = .pubLUdtRow.Item("ID")
                txtCoACodeOfRevenue.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfRevenue.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfAccountReceivable_Click(sender As Object, e As EventArgs) Handles btnCoAOfAccountReceivable.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofAccountReceivable = .pubLUdtRow.Item("ID")
                txtCoACodeOfAccountReceivable.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfAccountReceivable.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfPurchaseDiscount_Click(sender As Object, e As EventArgs) Handles btnCoAOfPurchaseDiscount.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofPurchaseDiscount = .pubLUdtRow.Item("ID")
                txtCoACodeOfPurchaseDiscount.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfPurchaseDiscount.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfAccountPayable_Click(sender As Object, e As EventArgs) Handles btnCoAOfAccountPayable.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofAccountPayable = .pubLUdtRow.Item("ID")
                txtCoACodeOfAccountPayable.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfAccountPayable.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfCashOrBank_Click(sender As Object, e As EventArgs) Handles btnCoAOfCashOrBank.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofCashOrBank = .pubLUdtRow.Item("ID")
                txtCoACodeOfCashOrBank.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfCashOrBank.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfStock_Click(sender As Object, e As EventArgs) Handles btnCoAOfStock.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofStock = .pubLUdtRow.Item("ID")
                txtCoACodeOfStock.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfStock.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfCOGS_Click(sender As Object, e As EventArgs) Handles btnCoAOfCOGS.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofCOGS = .pubLUdtRow.Item("ID")
                txtCoACodeOfCOGS.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfCOGS.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfSalesDiscount_Click(sender As Object, e As EventArgs) Handles btnCoAOfSalesDiscount.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofSalesDiscount = .pubLUdtRow.Item("ID")
                txtCoACodeOfSalesDiscount.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfSalesDiscount.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfEquipments_Click(sender As Object, e As EventArgs) Handles btnCoAOfEquipments.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofPurchaseEquipments = .pubLUdtRow.Item("ID")
                txtCoACodeOfEquipments.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfEquipments.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

#End Region

End Class