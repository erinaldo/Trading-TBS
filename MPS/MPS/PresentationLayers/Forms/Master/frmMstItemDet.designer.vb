<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMstItemDet
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
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.txtPurchasePrice2 = New MPS.usNumeric()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPurchasePrice1 = New MPS.usNumeric()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSalesPrice = New MPS.usNumeric()
        Me.txtBalanceQty = New MPS.usNumeric()
        Me.txtMinQty = New MPS.usNumeric()
        Me.cboUOMID = New MPS.usComboBox()
        Me.cboStatus = New MPS.usComboBox()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtCode = New MPS.usTextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New MPS.usTextBox()
        Me.lblUomID1 = New System.Windows.Forms.Label()
        Me.lblMinQty = New System.Windows.Forms.Label()
        Me.lblBalanceQty = New System.Windows.Forms.Label()
        Me.lblSalesPrice = New System.Windows.Forms.Label()
        Me.Toolbar = New MPS.usToolBar()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.txtTolerance = New MPS.usNumeric()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.StatusStrip.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        CType(Me.txtPurchasePrice2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchasePrice1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBalanceQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMinQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lblInfo.Size = New System.Drawing.Size(622, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Barang Detail"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 296)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(622, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripEmpty
        '
        Me.ToolStripEmpty.Name = "ToolStripEmpty"
        Me.ToolStripEmpty.Size = New System.Drawing.Size(499, 17)
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
        'pnlDetail
        '
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.txtTolerance)
        Me.pnlDetail.Controls.Add(Me.Label3)
        Me.pnlDetail.Controls.Add(Me.txtPurchasePrice2)
        Me.pnlDetail.Controls.Add(Me.Label2)
        Me.pnlDetail.Controls.Add(Me.txtPurchasePrice1)
        Me.pnlDetail.Controls.Add(Me.Label1)
        Me.pnlDetail.Controls.Add(Me.txtSalesPrice)
        Me.pnlDetail.Controls.Add(Me.txtBalanceQty)
        Me.pnlDetail.Controls.Add(Me.txtMinQty)
        Me.pnlDetail.Controls.Add(Me.cboUOMID)
        Me.pnlDetail.Controls.Add(Me.cboStatus)
        Me.pnlDetail.Controls.Add(Me.lblIDStatus)
        Me.pnlDetail.Controls.Add(Me.lblCode)
        Me.pnlDetail.Controls.Add(Me.txtCode)
        Me.pnlDetail.Controls.Add(Me.lblName)
        Me.pnlDetail.Controls.Add(Me.txtName)
        Me.pnlDetail.Controls.Add(Me.lblUomID1)
        Me.pnlDetail.Controls.Add(Me.lblMinQty)
        Me.pnlDetail.Controls.Add(Me.lblBalanceQty)
        Me.pnlDetail.Controls.Add(Me.lblSalesPrice)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetail.Location = New System.Drawing.Point(0, 50)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(622, 246)
        Me.pnlDetail.TabIndex = 2
        '
        'txtPurchasePrice2
        '
        Me.txtPurchasePrice2.DecimalPlaces = 2
        Me.txtPurchasePrice2.Location = New System.Drawing.Point(116, 188)
        Me.txtPurchasePrice2.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPurchasePrice2.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPurchasePrice2.Name = "txtPurchasePrice2"
        Me.txtPurchasePrice2.Size = New System.Drawing.Size(160, 21)
        Me.txtPurchasePrice2.TabIndex = 5
        Me.txtPurchasePrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPurchasePrice2.ThousandsSeparator = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(31, 192)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 99
        Me.Label2.Text = "Harga Beli 2"
        '
        'txtPurchasePrice1
        '
        Me.txtPurchasePrice1.DecimalPlaces = 2
        Me.txtPurchasePrice1.Location = New System.Drawing.Point(116, 161)
        Me.txtPurchasePrice1.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPurchasePrice1.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPurchasePrice1.Name = "txtPurchasePrice1"
        Me.txtPurchasePrice1.Size = New System.Drawing.Size(160, 21)
        Me.txtPurchasePrice1.TabIndex = 4
        Me.txtPurchasePrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPurchasePrice1.ThousandsSeparator = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(31, 165)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 97
        Me.Label1.Text = "Harga Beli 1"
        '
        'txtSalesPrice
        '
        Me.txtSalesPrice.DecimalPlaces = 2
        Me.txtSalesPrice.Location = New System.Drawing.Point(116, 134)
        Me.txtSalesPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtSalesPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtSalesPrice.Name = "txtSalesPrice"
        Me.txtSalesPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtSalesPrice.TabIndex = 3
        Me.txtSalesPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSalesPrice.ThousandsSeparator = True
        '
        'txtBalanceQty
        '
        Me.txtBalanceQty.DecimalPlaces = 2
        Me.txtBalanceQty.Enabled = False
        Me.txtBalanceQty.Location = New System.Drawing.Point(422, 161)
        Me.txtBalanceQty.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtBalanceQty.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtBalanceQty.Name = "txtBalanceQty"
        Me.txtBalanceQty.Size = New System.Drawing.Size(160, 21)
        Me.txtBalanceQty.TabIndex = 8
        Me.txtBalanceQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBalanceQty.ThousandsSeparator = True
        '
        'txtMinQty
        '
        Me.txtMinQty.DecimalPlaces = 2
        Me.txtMinQty.Location = New System.Drawing.Point(422, 134)
        Me.txtMinQty.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtMinQty.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtMinQty.Name = "txtMinQty"
        Me.txtMinQty.Size = New System.Drawing.Size(160, 21)
        Me.txtMinQty.TabIndex = 7
        Me.txtMinQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMinQty.ThousandsSeparator = True
        '
        'cboUOMID
        '
        Me.cboUOMID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUOMID.FormattingEnabled = True
        Me.cboUOMID.Location = New System.Drawing.Point(116, 107)
        Me.cboUOMID.Name = "cboUOMID"
        Me.cboUOMID.Size = New System.Drawing.Size(160, 21)
        Me.cboUOMID.TabIndex = 2
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Enabled = False
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(422, 188)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(160, 21)
        Me.cboStatus.TabIndex = 9
        '
        'lblIDStatus
        '
        Me.lblIDStatus.AutoSize = True
        Me.lblIDStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblIDStatus.ForeColor = System.Drawing.Color.Black
        Me.lblIDStatus.Location = New System.Drawing.Point(337, 192)
        Me.lblIDStatus.Name = "lblIDStatus"
        Me.lblIDStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblIDStatus.TabIndex = 95
        Me.lblIDStatus.Text = "Status"
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.ForeColor = System.Drawing.Color.Black
        Me.lblCode.Location = New System.Drawing.Point(31, 18)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(31, 13)
        Me.lblCode.TabIndex = 93
        Me.lblCode.Text = "Kode"
        '
        'txtCode
        '
        Me.txtCode.BackColor = System.Drawing.Color.LightYellow
        Me.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.Location = New System.Drawing.Point(116, 14)
        Me.txtCode.MaxLength = 250
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ReadOnly = True
        Me.txtCode.Size = New System.Drawing.Size(160, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.TabStop = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(31, 45)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(34, 13)
        Me.lblName.TabIndex = 93
        Me.lblName.Text = "Nama"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtName.Location = New System.Drawing.Point(116, 41)
        Me.txtName.MaxLength = 250
        Me.txtName.Multiline = True
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(466, 60)
        Me.txtName.TabIndex = 1
        '
        'lblUomID1
        '
        Me.lblUomID1.AutoSize = True
        Me.lblUomID1.BackColor = System.Drawing.Color.Transparent
        Me.lblUomID1.ForeColor = System.Drawing.Color.Black
        Me.lblUomID1.Location = New System.Drawing.Point(31, 112)
        Me.lblUomID1.Name = "lblUomID1"
        Me.lblUomID1.Size = New System.Drawing.Size(41, 13)
        Me.lblUomID1.TabIndex = 93
        Me.lblUomID1.Text = "Satuan"
        '
        'lblMinQty
        '
        Me.lblMinQty.AutoSize = True
        Me.lblMinQty.BackColor = System.Drawing.Color.Transparent
        Me.lblMinQty.ForeColor = System.Drawing.Color.Black
        Me.lblMinQty.Location = New System.Drawing.Point(337, 138)
        Me.lblMinQty.Name = "lblMinQty"
        Me.lblMinQty.Size = New System.Drawing.Size(62, 13)
        Me.lblMinQty.TabIndex = 93
        Me.lblMinQty.Text = "Minimal Qty"
        '
        'lblBalanceQty
        '
        Me.lblBalanceQty.AutoSize = True
        Me.lblBalanceQty.BackColor = System.Drawing.Color.Transparent
        Me.lblBalanceQty.ForeColor = System.Drawing.Color.Black
        Me.lblBalanceQty.Location = New System.Drawing.Point(337, 165)
        Me.lblBalanceQty.Name = "lblBalanceQty"
        Me.lblBalanceQty.Size = New System.Drawing.Size(54, 13)
        Me.lblBalanceQty.TabIndex = 93
        Me.lblBalanceQty.Text = "Saldo Qty"
        '
        'lblSalesPrice
        '
        Me.lblSalesPrice.AutoSize = True
        Me.lblSalesPrice.BackColor = System.Drawing.Color.Transparent
        Me.lblSalesPrice.ForeColor = System.Drawing.Color.Black
        Me.lblSalesPrice.Location = New System.Drawing.Point(31, 138)
        Me.lblSalesPrice.Name = "lblSalesPrice"
        Me.lblSalesPrice.Size = New System.Drawing.Size(58, 13)
        Me.lblSalesPrice.TabIndex = 93
        Me.lblSalesPrice.Text = "Harga Jual"
        '
        'Toolbar
        '
        Me.Toolbar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.Toolbar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarRefresh, Me.BarClose})
        Me.Toolbar.DropDownArrows = True
        Me.Toolbar.Location = New System.Drawing.Point(0, 0)
        Me.Toolbar.Name = "Toolbar"
        Me.Toolbar.ShowToolTips = True
        Me.Toolbar.Size = New System.Drawing.Size(622, 28)
        Me.Toolbar.TabIndex = 0
        Me.Toolbar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
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
        'txtTolerance
        '
        Me.txtTolerance.DecimalPlaces = 2
        Me.txtTolerance.Location = New System.Drawing.Point(422, 107)
        Me.txtTolerance.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTolerance.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.Size = New System.Drawing.Size(160, 21)
        Me.txtTolerance.TabIndex = 6
        Me.txtTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTolerance.ThousandsSeparator = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(337, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 101
        Me.Label3.Text = "Toleransi"
        '
        'frmMstItemDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 318)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.Toolbar)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMstItemDet"
        Me.Text = "Barang"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        CType(Me.txtPurchasePrice2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchasePrice1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBalanceQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMinQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtCode As usTextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As usTextBox
    Friend WithEvents lblUomID1 As System.Windows.Forms.Label
    Friend WithEvents lblMinQty As System.Windows.Forms.Label
    Friend WithEvents lblBalanceQty As System.Windows.Forms.Label
    Friend WithEvents lblSalesPrice As System.Windows.Forms.Label
    Friend WithEvents cboStatus As MPS.usComboBox
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
    Friend WithEvents txtSalesPrice As MPS.usNumeric
    Friend WithEvents txtBalanceQty As MPS.usNumeric
    Friend WithEvents txtMinQty As MPS.usNumeric
    Friend WithEvents cboUOMID As MPS.usComboBox
    Friend WithEvents Toolbar As MPS.usToolBar
    Friend WithEvents BarRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtPurchasePrice2 As MPS.usNumeric
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPurchasePrice1 As MPS.usNumeric
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTolerance As MPS.usNumeric
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class


