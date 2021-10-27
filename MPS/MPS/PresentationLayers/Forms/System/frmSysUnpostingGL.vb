Public Class frmSysUnpostingGL

    Private clsData As New VO.PostGL
    Private dtData As New DataTable

    Private Const _
       cUnpost = 0, cClose = 1

    Private Sub prvFillCombo()
        Try
            cboPeriode.Items.Clear()
            dtData = BL.PostGL.ListDataForUnpost
            For Each dr As DataRow In dtData.Rows
                cboPeriode.Items.Add(dr.Item("ID"))
            Next
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvGetDetail()
        Try
            clsData = New VO.PostGL
            clsData = BL.PostGL.GetDetail(cboPeriode.Text.Trim)
            dtpDateFrom.Value = clsData.DateFrom.Date
            dtpDateTo.Value = clsData.DateTo.Date
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvUnpost()
        ToolBar.Focus()
        If Not UI.usForm.frmAskQuestion("Unposting periode ID " & cboPeriode.Text.Trim & "?") Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        Try
            clsData = New VO.PostGL
            clsData.ID = cboPeriode.Text.Trim
            clsData.DateFrom = dtpDateFrom.Value.Date
            clsData.DateTo = dtpDateTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
            clsData.PostedBy = MPSLib.UI.usUserApp.UserID
            BL.PostGL.UnpostData(clsData)
            UI.usForm.frmMessageBox("Unposting data berhasil")
            prvFillCombo()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmSysUnpostingGL_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmSysUnpostingGL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillCombo()
        If dtData.Rows.Count > 0 Then cboPeriode.SelectedIndex = 0 Else ToolBar.Buttons(cUnpost).Enabled = False
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cUnpost).Name Then
            prvUnpost()
        ElseIf e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        End If
    End Sub

    Private Sub cboPeriode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPeriode.SelectedIndexChanged
        prvGetDetail()
    End Sub

#End Region

End Class