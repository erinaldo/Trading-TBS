﻿Public Class frmTraDownPaymentDet

#Region "Property"

    Private frmParent As frmTraDownPayment
    Private clsData As VO.DownPayment
    Private intBPID As Integer
    Private dtItem As New DataTable
    Private intPos As Integer = 0
    Private strJournalID As String = ""
    Private intDPType As VO.DownPayment.Type
    Property pubID As String
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False
    Private bolValid As Boolean = True
    Private intCoAIDOfActiva As Integer = 0
    Property pubCS As New VO.CS

    Public WriteOnly Property pubDPType As VO.DownPayment.Type
        Set(value As VO.DownPayment.Type)
            intDPType = value
        End Set
    End Property

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

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdStatusView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "DPID", "DPID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "Status", "Status", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusBy", "Oleh", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusDate", "Tanggal", 180, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdStatusView, "Remarks", "Keterangan", 300, UI.usDefGrid.gString)
    End Sub

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionAccountReceivable), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub prvFillPaymentTerm()
        Try
            UI.usForm.FillComboBox(cboPaymentReferences, BL.PaymentReferences.ListDataForCombo, "ID", "Name")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub prvFillCombo()
        prvFillStatus()
        prvFillPaymentTerm()
    End Sub

    Private Sub prvFillForm()
        pgMain.Value = 30
        Me.Cursor = Cursors.WaitCursor
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.DownPayment
                clsData = BL.DownPayment.GetDetail(pubID)
                txtID.Text = clsData.ID
                intBPID = clsData.BPID
                txtBPName.Text = clsData.BPName
                dtpAPDate.Value = clsData.DPDate
                txtTotalAmount.Value = clsData.TotalAmount
                cboPaymentReferences.SelectedValue = clsData.PaymentReferencesID
                txtReferencesNote.Text = clsData.ReferencesNote
                cboStatus.SelectedValue = clsData.IDStatus
                txtRemarks.Text = clsData.Remarks
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)

                ToolBar.Buttons(cSave).Enabled = Not clsData.IsDeleted
                strJournalID = clsData.JournalID
                intCoAIDOfActiva = clsData.CoAIDOfActiva
                txtCoACodeOfActiva.Text = clsData.CoACodeOfActiva
                txtCoANameOfActiva.Text = clsData.CoANameOfActiva
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvSave()
        ToolBar.Focus()
        If bolValid = False Then Exit Sub
        If txtBPName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih pemasok terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtBPName.Focus()
            Exit Sub
        ElseIf cboPaymentReferences.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Pilih kolom pembayaran melalui terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            cboPaymentReferences.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            tcHeader.SelectedTab = tpMain
            cboStatus.Focus()
            Exit Sub
        ElseIf txtTotalAmount.Value <= 0 Then
            UI.usForm.frmMessageBox("Total Panjar harus lebih besar dari 0")
            tcHeader.SelectedTab = tpMain
            txtTotalAmount.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data panjar?") Then Exit Sub

        clsData = New VO.DownPayment
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = txtID.Text.Trim
        clsData.BPID = intBPID
        clsData.BPName = txtBPName.Text.Trim
        clsData.DPType = intDPType
        clsData.DPDate = dtpAPDate.Value
        clsData.TotalAmount = txtTotalAmount.Value
        clsData.PaymentReferencesID = cboPaymentReferences.SelectedValue
        clsData.ReferencesNote = txtReferencesNote.Text.Trim
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.Remarks = txtRemarks.Text.Trim
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.JournalID = strJournalID
        clsData.CoAIDOfActiva = intCoAIDOfActiva
        clsData.CoACodeOfActiva = txtCoACodeOfActiva.Text.Trim
        clsData.CoANameOfActiva = txtCoANameOfActiva.Text.Trim

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            Dim strID As String = BL.DownPayment.SaveData(pubIsNew, clsData)
            pgMain.Value = 50
            If strID.Trim <> "" Then
                If pubIsNew Then
                    pgMain.Value = 100
                    UI.usForm.frmMessageBox("Data berhasil disimpan. " & vbCrLf & "Nomor: " & strID)
                    frmParent.pubRefresh(clsData.ID)
                    prvClear()
                    prvQueryHistory()
                Else
                    pgMain.Value = 100
                    pubIsSave = True
                    Me.Close()
                End If
            Else
                UI.usForm.frmMessageBox("Proses simpan data tidak berhasil")
                Exit Sub
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvClear()
        txtID.Text = ""
        intBPID = 0
        txtBPName.Text = ""
        dtpAPDate.Value = Now
        txtTotalAmount.Value = 0
        cboPaymentReferences.SelectedIndex = -1
        txtReferencesNote.Text = ""
        cboStatus.SelectedValue = VO.Status.Values.Draft
        txtRemarks.Text = ""
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
        intCoAIDOfActiva = MPSLib.UI.usUserApp.JournalPost.CoAofCash
        txtCoACodeOfActiva.Text = MPSLib.UI.usUserApp.JournalPost.CoACodeofCash
        txtCoANameOfActiva.Text = MPSLib.UI.usUserApp.JournalPost.CoANameofCash
    End Sub

    Private Sub prvChooseBP()
        Dim frmDetail As New frmMstBusinessPartner
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intBPID = .pubLUdtRow.Item("ID")
                txtBPName.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, IIf(intDPType = VO.DownPayment.Type.Sales, VO.Modules.Values.TransactionSalesDownPayment, VO.Modules.Values.TransactionReceiveDownPayment), IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
    End Sub

#Region "History Handle"

    Private Sub prvQueryHistory()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdStatus.DataSource = BL.DownPayment.ListDataStatus(txtID.Text.Trim)
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

#End Region

#Region "Form Handle"

    Private Sub frmTraDownPaymentDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            tcHeader.SelectedTab = tpMain
        ElseIf e.KeyCode = Keys.F2 Then
            tcHeader.SelectedTab = tpHistory
        ElseIf e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmTraDownPaymentDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvFillForm()
        prvQueryHistory()
        prvUserAccess()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub btnBP_Click(sender As Object, e As EventArgs) Handles btnBP.Click
        prvChooseBP()
    End Sub

    Private Sub btnCoAOfActiva_Click(sender As Object, e As EventArgs) Handles btnCoAOfActiva.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .pubFilterGroup = VO.ChartOfAccount.FilterGroup.CashOrBank
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDOfActiva = .pubLUdtRow.Item("ID")
                txtCoACodeOfActiva.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfActiva.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

#End Region

End Class