Public Class frmMstBusinessPartnerDet

#Region "Property"
    Private frmParent As frmMstBusinessPartner
    Private clsData As VO.BusinessPartner
    Property pubID As Integer
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub
#End Region

    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvSetTitleForm()
        If pubIsNew Then
            Me.Text += " [baru] "
        Else
            Me.Text += " [edit] "
        End If
    End Sub

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.MasterBusinessPartners), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillPaymentTerm()
        Try
            UI.usForm.FillComboBox(cboPaymentTerm, BL.PaymentTerm.ListDataForCombo, "ID", "Name")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillCombo()
        prvFillStatus()
        prvFillPaymentTerm()
    End Sub

    Private Sub prvFillForm()
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.BusinessPartner
                clsData = BL.BusinessPartner.GetDetail(pubID)
                txtName.Text = clsData.Name
                txtAddress.Text = clsData.Address
                txtPICName.Text = clsData.PICName
                txtPICPhoneNumber.Text = clsData.PICPhoneNumber
                cboPaymentTerm.SelectedValue = clsData.PaymentTermID
                chkIsUsePurchaseLimit.Checked = clsData.IsUsePurchaseLimit
                txtMaxPurchaseLimit.Value = clsData.MaxPurchaseLimit
                txtAPBalance.Value = clsData.APBalance
                txtARBalance.Value = clsData.ARBalance
                cboStatus.SelectedValue = clsData.IDStatus
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)

                If cboStatus.SelectedValue = VO.Status.Values.InActive Then cboStatus.Enabled = True
                If clsData.IsUsePurchaseLimit Then txtMaxPurchaseLimit.Enabled = True
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        If txtName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Nama belum diinput")
            txtName.Focus()
            Exit Sub
            'ElseIf txtAddress.Text.Trim = "" Then
            '    UI.usForm.frmMessageBox("Alamat belum diinput")
            '    txtAddress.Focus()
            '    Exit Sub
        ElseIf chkIsUsePurchaseLimit.Checked AndAlso txtMaxPurchaseLimit.Value <= 0 Then
            UI.usForm.frmMessageBox(chkIsUsePurchaseLimit.Text & " harus lebih besar dari 0 jika tercentang")
            txtMaxPurchaseLimit.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        clsData = New VO.BusinessPartner
        clsData.ID = pubID
        clsData.Name = txtName.Text.Trim
        clsData.Address = txtAddress.Text.Trim
        clsData.PICName = txtPICName.Text.Trim
        clsData.PICPhoneNumber = txtPICPhoneNumber.Text.Trim
        clsData.PaymentTermID = cboPaymentTerm.SelectedValue
        clsData.IsUsePurchaseLimit = chkIsUsePurchaseLimit.Checked
        clsData.MaxPurchaseLimit = txtMaxPurchaseLimit.Value
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Try
            BL.BusinessPartner.SaveData(pubIsNew, clsData)
            If pubIsNew Then
                UI.usForm.frmMessageBox("Data berhasil disimpan.")
                frmParent.pubRefresh(clsData.ID)
                prvClear()
            Else
                pubIsSave = True
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvClear()
        txtName.Focus()
        txtName.Text = ""
        txtAddress.Text = ""
        txtPICName.Text = ""
        txtPICPhoneNumber.Text = ""
        cboPaymentTerm.SelectedIndex = -1
        chkIsUsePurchaseLimit.Checked = False
        txtMaxPurchaseLimit.Value = 0
        txtAPBalance.Value = 0
        txtARBalance.Value = 0
        cboStatus.SelectedValue = VO.Status.Values.Active
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmMstBusinessPartnerDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstBusinessPartnerDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetTitleForm()
        prvFillForm()
        prvUserAccess()

        AddHandler chkIsUsePurchaseLimit.CheckedChanged, AddressOf chkIsUsePurchaseLimit_CheckedChanged
    End Sub

    Private Sub ToolBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub chkIsUsePurchaseLimit_CheckedChanged(sender As Object, e As EventArgs)
        txtMaxPurchaseLimit.Enabled = chkIsUsePurchaseLimit.Checked
    End Sub

#End Region

End Class


