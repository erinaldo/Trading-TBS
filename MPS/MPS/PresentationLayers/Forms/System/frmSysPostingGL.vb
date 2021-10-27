Public Class frmSysPostingGL

    Private clsData As New VO.PostGL

    Private Const _
       cPost = 0, cClose = 1

    Private Sub prvGetLastPosted()
        Try
            clsData = New VO.PostGL
            clsData = BL.PostGL.GetDetailLast

            If Not clsData.ID Is Nothing Then
                txtLastPeriod.Text = clsData.ID
                dtpDateFrom.MinDate = clsData.DateTo.Date.AddDays(1)
                dtpDateFrom.Value = clsData.DateTo.Date.AddDays(1)
                dtpDateFrom.Enabled = False
                dtpDateTo.Value = clsData.DateTo.Date.AddDays(1)
            Else
                txtLastPeriod.Text = ""
                dtpDateFrom.Value = "2000/01/01"
                dtpDateFrom.Enabled = False
                dtpDateTo.Value = Today.Date
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvPost()
        txtLastPeriod.Focus()
        If dtpDateFrom.Value.Date > dtpDateTo.Value.Date Then
            UI.usForm.frmMessageBox("Periode salah")
            dtpDateFrom.Focus()
            Exit Sub
        End If
        If Not UI.usForm.frmAskQuestion("Posting periode " & Format(dtpDateFrom.Value.Date, "dd MMMM yyyy") & " sampai " & Format(dtpDateTo.Value.Date, "dd MMMM yyyy") & "?") Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        Try
            clsData = New VO.PostGL
            clsData.DateFrom = dtpDateFrom.Value.Date
            clsData.DateTo = dtpDateTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
            clsData.PostedBy = MPSLib.UI.usUserApp.UserID
            BL.PostGL.PostData(clsData)
            UI.usForm.frmMessageBox("Posting data berhasil")
            prvGetLastPosted()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmSysPostingGL_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmSysPostingGL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvGetLastPosted()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cPost).Name Then
            prvPost()
        ElseIf e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        End If
    End Sub

#End Region

End Class