Public Class frmTraSalesPrintBonFaktur

    Public Property pubData As VO.Sales
    Public Property pubIsPrint As Boolean = False

    Private Sub prvPrint()
        If chkAsli.Checked = False And chkCopy.Checked = False Then
            UI.usForm.frmMessageBox("Pilih jenis bon yang ingin dicetak")
            Exit Sub
        End If

        Dim strDOColor As New List(Of String)

        Dim doColor As New List(Of SharedLib.usDOColors)
        Dim printDO As New SharedLib.usDOColors
        If chkAsli.Checked Then
            With printDO
                .DisplayName = "BON ASLI"
                .TextColor = Color.Transparent
                .TextName = "BON ASLI"
            End With
            strDOColor.Add(printDO.TextName)
            doColor.Add(printDO)
        End If

        If chkCopy.Checked Then
            printDO = New SharedLib.usDOColors
            With printDO
                .DisplayName = "BON COPY"
                .TextColor = Color.Transparent
                .TextName = "BON COPY"
            End With
            strDOColor.Add(printDO.TextName)
            doColor.Add(printDO)
        End If

        UI.usForm.PrintBonFaktur(Me, pubData.ID, doColor)
        Try
            BL.Sales.PrintBonFaktur(pubData, strDOColor)
            pubIsPrint = True
            Me.Close()
        Catch ex As Exception
            pubIsPrint = False
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmTraSalesPrintBonFaktur_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        End If
    End Sub

    Private Sub frmTraSalesPrintBonFaktur_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text
            Case "Cetak" : prvPrint()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

#End Region

End Class