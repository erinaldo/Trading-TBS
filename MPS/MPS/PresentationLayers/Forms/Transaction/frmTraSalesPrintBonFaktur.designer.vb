<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraSalesPrintBonFaktur
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ToolBar = New MPS.usToolBar()
        Me.BarPrint = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.gboColor = New System.Windows.Forms.GroupBox()
        Me.chkCopy = New DevExpress.XtraEditors.CheckEdit()
        Me.chkAsli = New DevExpress.XtraEditors.CheckEdit()
        Me.gboColor.SuspendLayout()
        CType(Me.chkCopy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAsli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarPrint, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(277, 28)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'BarPrint
        '
        Me.BarPrint.Name = "BarPrint"
        Me.BarPrint.Tag = "Print"
        Me.BarPrint.Text = "Cetak"
        '
        'BarClose
        '
        Me.BarClose.Name = "BarClose"
        Me.BarClose.Tag = "Close"
        Me.BarClose.Text = "Tutup"
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.Teal
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(0, 28)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(277, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Print Bon Faktur"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gboColor
        '
        Me.gboColor.Controls.Add(Me.chkCopy)
        Me.gboColor.Controls.Add(Me.chkAsli)
        Me.gboColor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gboColor.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.gboColor.Location = New System.Drawing.Point(32, 65)
        Me.gboColor.Name = "gboColor"
        Me.gboColor.Size = New System.Drawing.Size(190, 70)
        Me.gboColor.TabIndex = 2
        Me.gboColor.TabStop = False
        Me.gboColor.Text = "Pilih Jenis Bon"
        '
        'chkCopy
        '
        Me.chkCopy.EditValue = True
        Me.chkCopy.Location = New System.Drawing.Point(95, 30)
        Me.chkCopy.Name = "chkCopy"
        Me.chkCopy.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chkCopy.Properties.Appearance.Options.UseForeColor = True
        Me.chkCopy.Properties.Caption = "Copy"
        Me.chkCopy.Size = New System.Drawing.Size(75, 19)
        Me.chkCopy.TabIndex = 1
        '
        'chkAsli
        '
        Me.chkAsli.EditValue = True
        Me.chkAsli.Location = New System.Drawing.Point(27, 30)
        Me.chkAsli.Name = "chkAsli"
        Me.chkAsli.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.chkAsli.Properties.Appearance.Options.UseForeColor = True
        Me.chkAsli.Properties.Caption = "Asli"
        Me.chkAsli.Size = New System.Drawing.Size(62, 19)
        Me.chkAsli.TabIndex = 0
        '
        'frmTraSalesPrintBonFaktur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(277, 172)
        Me.Controls.Add(Me.gboColor)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTraSalesPrintBonFaktur"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print Bon Faktur"
        Me.gboColor.ResumeLayout(False)
        CType(Me.chkCopy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAsli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents gboColor As System.Windows.Forms.GroupBox
    Friend WithEvents chkCopy As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkAsli As DevExpress.XtraEditors.CheckEdit
End Class
