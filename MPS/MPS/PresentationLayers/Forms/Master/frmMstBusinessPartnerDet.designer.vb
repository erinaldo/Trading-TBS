<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMstBusinessPartnerDet
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
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripEmpty = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogInc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogBy = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblARBalance = New System.Windows.Forms.Label()
        Me.lblAPBalance = New System.Windows.Forms.Label()
        Me.lblPICPhoneNumber = New System.Windows.Forms.Label()
        Me.lblPICName = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.cboPaymentTerm = New MPS.usComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtARBalance = New MPS.usNumeric()
        Me.txtAPBalance = New MPS.usNumeric()
        Me.cboStatus = New MPS.usComboBox()
        Me.txtName = New MPS.usTextBox()
        Me.txtAddress = New MPS.usTextBox()
        Me.txtPICName = New MPS.usTextBox()
        Me.txtPICPhoneNumber = New MPS.usTextBox()
        Me.ToolBar = New MPS.usToolBar()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.chkIsUsePurchaseLimit = New DevExpress.XtraEditors.CheckEdit()
        Me.txtMaxPurchaseLimit = New MPS.usNumeric()
        Me.StatusStrip.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        CType(Me.txtARBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAPBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsUsePurchaseLimit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaxPurchaseLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.CadetBlue
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(0, 28)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(843, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Rekan Bisnis Detail"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 231)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(843, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripEmpty
        '
        Me.ToolStripEmpty.Name = "ToolStripEmpty"
        Me.ToolStripEmpty.Size = New System.Drawing.Size(720, 17)
        Me.ToolStripEmpty.Spring = True
        '
        'ToolStripLogInc
        '
        Me.ToolStripLogInc.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLogInc.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLogInc.Name = "ToolStripLogInc"
        Me.ToolStripLogInc.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripLogInc.Text = "Log Inc : "
        '
        'ToolStripLogBy
        '
        Me.ToolStripLogBy.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLogBy.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLogBy.Name = "ToolStripLogBy"
        Me.ToolStripLogBy.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripLogBy.Text = "Last Log :"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripLogDate
        '
        Me.ToolStripLogDate.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLogDate.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLogDate.Name = "ToolStripLogDate"
        Me.ToolStripLogDate.Size = New System.Drawing.Size(12, 17)
        Me.ToolStripLogDate.Text = "-"
        '
        'lblARBalance
        '
        Me.lblARBalance.AutoSize = True
        Me.lblARBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblARBalance.ForeColor = System.Drawing.Color.Black
        Me.lblARBalance.Location = New System.Drawing.Point(486, 103)
        Me.lblARBalance.Name = "lblARBalance"
        Me.lblARBalance.Size = New System.Drawing.Size(72, 13)
        Me.lblARBalance.TabIndex = 93
        Me.lblARBalance.Text = "Saldo Piutang"
        '
        'lblAPBalance
        '
        Me.lblAPBalance.AutoSize = True
        Me.lblAPBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblAPBalance.ForeColor = System.Drawing.Color.Black
        Me.lblAPBalance.Location = New System.Drawing.Point(486, 75)
        Me.lblAPBalance.Name = "lblAPBalance"
        Me.lblAPBalance.Size = New System.Drawing.Size(71, 13)
        Me.lblAPBalance.TabIndex = 93
        Me.lblAPBalance.Text = "Saldo Hutang"
        '
        'lblPICPhoneNumber
        '
        Me.lblPICPhoneNumber.AutoSize = True
        Me.lblPICPhoneNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblPICPhoneNumber.ForeColor = System.Drawing.Color.Black
        Me.lblPICPhoneNumber.Location = New System.Drawing.Point(31, 130)
        Me.lblPICPhoneNumber.Name = "lblPICPhoneNumber"
        Me.lblPICPhoneNumber.Size = New System.Drawing.Size(60, 13)
        Me.lblPICPhoneNumber.TabIndex = 93
        Me.lblPICPhoneNumber.Text = "No. HP PIC"
        '
        'lblPICName
        '
        Me.lblPICName.AutoSize = True
        Me.lblPICName.BackColor = System.Drawing.Color.Transparent
        Me.lblPICName.ForeColor = System.Drawing.Color.Black
        Me.lblPICName.Location = New System.Drawing.Point(31, 103)
        Me.lblPICName.Name = "lblPICName"
        Me.lblPICName.Size = New System.Drawing.Size(54, 13)
        Me.lblPICName.TabIndex = 93
        Me.lblPICName.Text = "Nama PIC"
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress.ForeColor = System.Drawing.Color.Black
        Me.lblAddress.Location = New System.Drawing.Point(31, 49)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(40, 13)
        Me.lblAddress.TabIndex = 93
        Me.lblAddress.Text = "Alamat"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(31, 22)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(34, 13)
        Me.lblName.TabIndex = 93
        Me.lblName.Text = "Nama"
        '
        'lblIDStatus
        '
        Me.lblIDStatus.AutoSize = True
        Me.lblIDStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblIDStatus.ForeColor = System.Drawing.Color.Black
        Me.lblIDStatus.Location = New System.Drawing.Point(486, 130)
        Me.lblIDStatus.Name = "lblIDStatus"
        Me.lblIDStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblIDStatus.TabIndex = 95
        Me.lblIDStatus.Text = "Status"
        '
        'pnlDetail
        '
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.txtMaxPurchaseLimit)
        Me.pnlDetail.Controls.Add(Me.chkIsUsePurchaseLimit)
        Me.pnlDetail.Controls.Add(Me.cboPaymentTerm)
        Me.pnlDetail.Controls.Add(Me.Label1)
        Me.pnlDetail.Controls.Add(Me.txtARBalance)
        Me.pnlDetail.Controls.Add(Me.txtAPBalance)
        Me.pnlDetail.Controls.Add(Me.cboStatus)
        Me.pnlDetail.Controls.Add(Me.lblIDStatus)
        Me.pnlDetail.Controls.Add(Me.lblName)
        Me.pnlDetail.Controls.Add(Me.txtName)
        Me.pnlDetail.Controls.Add(Me.lblAddress)
        Me.pnlDetail.Controls.Add(Me.txtAddress)
        Me.pnlDetail.Controls.Add(Me.lblPICName)
        Me.pnlDetail.Controls.Add(Me.txtPICName)
        Me.pnlDetail.Controls.Add(Me.lblPICPhoneNumber)
        Me.pnlDetail.Controls.Add(Me.txtPICPhoneNumber)
        Me.pnlDetail.Controls.Add(Me.lblAPBalance)
        Me.pnlDetail.Controls.Add(Me.lblARBalance)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetail.Location = New System.Drawing.Point(0, 50)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(843, 181)
        Me.pnlDetail.TabIndex = 2
        '
        'cboPaymentTerm
        '
        Me.cboPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentTerm.FormattingEnabled = True
        Me.cboPaymentTerm.Location = New System.Drawing.Point(626, 19)
        Me.cboPaymentTerm.Name = "cboPaymentTerm"
        Me.cboPaymentTerm.Size = New System.Drawing.Size(160, 21)
        Me.cboPaymentTerm.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(486, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "Jenis Pembayaran"
        '
        'txtARBalance
        '
        Me.txtARBalance.Enabled = False
        Me.txtARBalance.Location = New System.Drawing.Point(626, 100)
        Me.txtARBalance.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtARBalance.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtARBalance.Name = "txtARBalance"
        Me.txtARBalance.Size = New System.Drawing.Size(160, 21)
        Me.txtARBalance.TabIndex = 7
        Me.txtARBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtARBalance.ThousandsSeparator = True
        '
        'txtAPBalance
        '
        Me.txtAPBalance.Enabled = False
        Me.txtAPBalance.Location = New System.Drawing.Point(626, 73)
        Me.txtAPBalance.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtAPBalance.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtAPBalance.Name = "txtAPBalance"
        Me.txtAPBalance.Size = New System.Drawing.Size(160, 21)
        Me.txtAPBalance.TabIndex = 6
        Me.txtAPBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAPBalance.ThousandsSeparator = True
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Enabled = False
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(626, 127)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(160, 21)
        Me.cboStatus.TabIndex = 8
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtName.Location = New System.Drawing.Point(123, 19)
        Me.txtName.MaxLength = 250
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(300, 21)
        Me.txtName.TabIndex = 0
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress.Location = New System.Drawing.Point(123, 46)
        Me.txtAddress.MaxLength = 250
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(300, 48)
        Me.txtAddress.TabIndex = 1
        '
        'txtPICName
        '
        Me.txtPICName.BackColor = System.Drawing.Color.White
        Me.txtPICName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPICName.Location = New System.Drawing.Point(123, 100)
        Me.txtPICName.MaxLength = 250
        Me.txtPICName.Name = "txtPICName"
        Me.txtPICName.Size = New System.Drawing.Size(300, 21)
        Me.txtPICName.TabIndex = 2
        '
        'txtPICPhoneNumber
        '
        Me.txtPICPhoneNumber.BackColor = System.Drawing.Color.White
        Me.txtPICPhoneNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPICPhoneNumber.Location = New System.Drawing.Point(123, 127)
        Me.txtPICPhoneNumber.MaxLength = 250
        Me.txtPICPhoneNumber.Name = "txtPICPhoneNumber"
        Me.txtPICPhoneNumber.Size = New System.Drawing.Size(300, 21)
        Me.txtPICPhoneNumber.TabIndex = 3
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarRefresh, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(843, 28)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'BarRefresh
        '
        Me.BarRefresh.Name = "BarRefresh"
        Me.BarRefresh.Tag = "Save"
        Me.BarRefresh.Text = "Simpan"
        '
        'BarClose
        '
        Me.BarClose.Name = "BarClose"
        Me.BarClose.Tag = "Close"
        Me.BarClose.Text = "Tutup"
        '
        'chkIsUsePurchaseLimit
        '
        Me.chkIsUsePurchaseLimit.Location = New System.Drawing.Point(486, 47)
        Me.chkIsUsePurchaseLimit.Name = "chkIsUsePurchaseLimit"
        Me.chkIsUsePurchaseLimit.Properties.Caption = "Maks. Limit Pembelian"
        Me.chkIsUsePurchaseLimit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.chkIsUsePurchaseLimit.Size = New System.Drawing.Size(127, 19)
        Me.chkIsUsePurchaseLimit.TabIndex = 99
        '
        'txtMaxPurchaseLimit
        '
        Me.txtMaxPurchaseLimit.Enabled = False
        Me.txtMaxPurchaseLimit.Location = New System.Drawing.Point(626, 46)
        Me.txtMaxPurchaseLimit.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtMaxPurchaseLimit.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtMaxPurchaseLimit.Name = "txtMaxPurchaseLimit"
        Me.txtMaxPurchaseLimit.Size = New System.Drawing.Size(160, 21)
        Me.txtMaxPurchaseLimit.TabIndex = 5
        Me.txtMaxPurchaseLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaxPurchaseLimit.ThousandsSeparator = True
        '
        'frmMstBusinessPartnerDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(843, 253)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMstBusinessPartnerDet"
        Me.Text = "Rekan Bisnis"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        CType(Me.txtARBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAPBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsUsePurchaseLimit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaxPurchaseLimit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripEmpty As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogInc As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogBy As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblARBalance As System.Windows.Forms.Label
    Friend WithEvents lblAPBalance As System.Windows.Forms.Label
    Friend WithEvents txtPICPhoneNumber As MPS.usTextBox
    Friend WithEvents lblPICPhoneNumber As System.Windows.Forms.Label
    Friend WithEvents txtPICName As MPS.usTextBox
    Friend WithEvents lblPICName As System.Windows.Forms.Label
    Friend WithEvents txtAddress As MPS.usTextBox
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents txtName As MPS.usTextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
    Friend WithEvents cboStatus As MPS.usComboBox
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents txtARBalance As MPS.usNumeric
    Friend WithEvents txtAPBalance As MPS.usNumeric
    Friend WithEvents cboPaymentTerm As MPS.usComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMaxPurchaseLimit As MPS.usNumeric
    Friend WithEvents chkIsUsePurchaseLimit As DevExpress.XtraEditors.CheckEdit
End Class


