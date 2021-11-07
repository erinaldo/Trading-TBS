<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysJournalPost
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSysJournalPost))
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.btnCoAOfEquipments = New DevExpress.XtraEditors.SimpleButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnCoAOfPurchaseDiscount = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCoAOfAccountPayable = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCoAOfCashOrBank = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCoAOfStock = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCoAOfCOGS = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCoAOfSalesDiscount = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCoAOfAccountReceivable = New DevExpress.XtraEditors.SimpleButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCoAOfRevenue = New DevExpress.XtraEditors.SimpleButton()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.lblUomID1 = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripEmpty = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogInc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogBy = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtCoACodeOfEquipments = New MPS.usTextBox()
        Me.txtCoANameOfEquipments = New MPS.usTextBox()
        Me.txtCoACodeOfPurchaseDiscount = New MPS.usTextBox()
        Me.txtCoANameOfPurchaseDiscount = New MPS.usTextBox()
        Me.txtCoACodeOfAccountPayable = New MPS.usTextBox()
        Me.txtCoANameOfAccountPayable = New MPS.usTextBox()
        Me.txtCoACodeOfCashOrBank = New MPS.usTextBox()
        Me.txtCoANameOfCashOrBank = New MPS.usTextBox()
        Me.txtCoACodeOfStock = New MPS.usTextBox()
        Me.txtCoANameOfStock = New MPS.usTextBox()
        Me.txtCoACodeOfCOGS = New MPS.usTextBox()
        Me.txtCoANameOfCOGS = New MPS.usTextBox()
        Me.txtCoACodeOfSalesDiscount = New MPS.usTextBox()
        Me.txtCoANameOfSalesDiscount = New MPS.usTextBox()
        Me.txtCoACodeOfAccountReceivable = New MPS.usTextBox()
        Me.txtCoANameOfAccountReceivable = New MPS.usTextBox()
        Me.txtCoACodeOfRevenue = New MPS.usTextBox()
        Me.txtCoANameOfRevenue = New MPS.usTextBox()
        Me.ToolBar = New MPS.usToolBar()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.pgMain = New System.Windows.Forms.ProgressBar()
        Me.pnlDetail.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
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
        Me.lblInfo.Size = New System.Drawing.Size(644, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Posting Jurnal Transaksi"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDetail
        '
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfEquipments)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfEquipments)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfEquipments)
        Me.pnlDetail.Controls.Add(Me.Label6)
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfPurchaseDiscount)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfPurchaseDiscount)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfPurchaseDiscount)
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfAccountPayable)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfAccountPayable)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfAccountPayable)
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfCashOrBank)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfCashOrBank)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfCashOrBank)
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfStock)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfStock)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfStock)
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfCOGS)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfCOGS)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfCOGS)
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfSalesDiscount)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfSalesDiscount)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfSalesDiscount)
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfAccountReceivable)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfAccountReceivable)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfAccountReceivable)
        Me.pnlDetail.Controls.Add(Me.Label5)
        Me.pnlDetail.Controls.Add(Me.Label4)
        Me.pnlDetail.Controls.Add(Me.Label3)
        Me.pnlDetail.Controls.Add(Me.Label2)
        Me.pnlDetail.Controls.Add(Me.txtCoACodeOfRevenue)
        Me.pnlDetail.Controls.Add(Me.Label1)
        Me.pnlDetail.Controls.Add(Me.btnCoAOfRevenue)
        Me.pnlDetail.Controls.Add(Me.lblCode)
        Me.pnlDetail.Controls.Add(Me.txtCoANameOfRevenue)
        Me.pnlDetail.Controls.Add(Me.lblUomID1)
        Me.pnlDetail.Controls.Add(Me.lblQty)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetail.Location = New System.Drawing.Point(0, 50)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(644, 326)
        Me.pnlDetail.TabIndex = 2
        '
        'btnCoAOfEquipments
        '
        Me.btnCoAOfEquipments.Image = CType(resources.GetObject("btnCoAOfEquipments.Image"), System.Drawing.Image)
        Me.btnCoAOfEquipments.Location = New System.Drawing.Point(566, 230)
        Me.btnCoAOfEquipments.Name = "btnCoAOfEquipments"
        Me.btnCoAOfEquipments.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfEquipments.TabIndex = 26
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(28, 236)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "Peralatan"
        '
        'btnCoAOfPurchaseDiscount
        '
        Me.btnCoAOfPurchaseDiscount.Image = CType(resources.GetObject("btnCoAOfPurchaseDiscount.Image"), System.Drawing.Image)
        Me.btnCoAOfPurchaseDiscount.Location = New System.Drawing.Point(566, 203)
        Me.btnCoAOfPurchaseDiscount.Name = "btnCoAOfPurchaseDiscount"
        Me.btnCoAOfPurchaseDiscount.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfPurchaseDiscount.TabIndex = 23
        '
        'btnCoAOfAccountPayable
        '
        Me.btnCoAOfAccountPayable.Image = CType(resources.GetObject("btnCoAOfAccountPayable.Image"), System.Drawing.Image)
        Me.btnCoAOfAccountPayable.Location = New System.Drawing.Point(566, 176)
        Me.btnCoAOfAccountPayable.Name = "btnCoAOfAccountPayable"
        Me.btnCoAOfAccountPayable.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfAccountPayable.TabIndex = 20
        '
        'btnCoAOfCashOrBank
        '
        Me.btnCoAOfCashOrBank.Image = CType(resources.GetObject("btnCoAOfCashOrBank.Image"), System.Drawing.Image)
        Me.btnCoAOfCashOrBank.Location = New System.Drawing.Point(566, 149)
        Me.btnCoAOfCashOrBank.Name = "btnCoAOfCashOrBank"
        Me.btnCoAOfCashOrBank.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfCashOrBank.TabIndex = 17
        '
        'btnCoAOfStock
        '
        Me.btnCoAOfStock.Image = CType(resources.GetObject("btnCoAOfStock.Image"), System.Drawing.Image)
        Me.btnCoAOfStock.Location = New System.Drawing.Point(566, 122)
        Me.btnCoAOfStock.Name = "btnCoAOfStock"
        Me.btnCoAOfStock.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfStock.TabIndex = 14
        '
        'btnCoAOfCOGS
        '
        Me.btnCoAOfCOGS.Image = CType(resources.GetObject("btnCoAOfCOGS.Image"), System.Drawing.Image)
        Me.btnCoAOfCOGS.Location = New System.Drawing.Point(566, 95)
        Me.btnCoAOfCOGS.Name = "btnCoAOfCOGS"
        Me.btnCoAOfCOGS.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfCOGS.TabIndex = 11
        '
        'btnCoAOfSalesDiscount
        '
        Me.btnCoAOfSalesDiscount.Image = CType(resources.GetObject("btnCoAOfSalesDiscount.Image"), System.Drawing.Image)
        Me.btnCoAOfSalesDiscount.Location = New System.Drawing.Point(566, 68)
        Me.btnCoAOfSalesDiscount.Name = "btnCoAOfSalesDiscount"
        Me.btnCoAOfSalesDiscount.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfSalesDiscount.TabIndex = 8
        '
        'btnCoAOfAccountReceivable
        '
        Me.btnCoAOfAccountReceivable.Image = CType(resources.GetObject("btnCoAOfAccountReceivable.Image"), System.Drawing.Image)
        Me.btnCoAOfAccountReceivable.Location = New System.Drawing.Point(566, 41)
        Me.btnCoAOfAccountReceivable.Name = "btnCoAOfAccountReceivable"
        Me.btnCoAOfAccountReceivable.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfAccountReceivable.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(28, 209)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 13)
        Me.Label5.TabIndex = 99
        Me.Label5.Text = "Potongan Pembelian"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(28, 182)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 98
        Me.Label4.Text = "Hutang Usaha"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(28, 155)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 97
        Me.Label3.Text = "Kas / Bank"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(28, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 96
        Me.Label2.Text = "Persediaan"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(28, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 94
        Me.Label1.Text = "Piutang Usaha"
        '
        'btnCoAOfRevenue
        '
        Me.btnCoAOfRevenue.Image = CType(resources.GetObject("btnCoAOfRevenue.Image"), System.Drawing.Image)
        Me.btnCoAOfRevenue.Location = New System.Drawing.Point(566, 14)
        Me.btnCoAOfRevenue.Name = "btnCoAOfRevenue"
        Me.btnCoAOfRevenue.Size = New System.Drawing.Size(23, 23)
        Me.btnCoAOfRevenue.TabIndex = 2
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.ForeColor = System.Drawing.Color.Black
        Me.lblCode.Location = New System.Drawing.Point(28, 20)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(65, 13)
        Me.lblCode.TabIndex = 93
        Me.lblCode.Text = "Pendapatan"
        '
        'lblUomID1
        '
        Me.lblUomID1.AutoSize = True
        Me.lblUomID1.BackColor = System.Drawing.Color.Transparent
        Me.lblUomID1.ForeColor = System.Drawing.Color.Black
        Me.lblUomID1.Location = New System.Drawing.Point(28, 74)
        Me.lblUomID1.Name = "lblUomID1"
        Me.lblUomID1.Size = New System.Drawing.Size(103, 13)
        Me.lblUomID1.TabIndex = 93
        Me.lblUomID1.Text = "Potongan Penjualan"
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.BackColor = System.Drawing.Color.Transparent
        Me.lblQty.ForeColor = System.Drawing.Color.Black
        Me.lblQty.Location = New System.Drawing.Point(28, 101)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(117, 13)
        Me.lblQty.TabIndex = 93
        Me.lblQty.Text = "Harga Pokok Penjualan"
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 331)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(644, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripEmpty
        '
        Me.ToolStripEmpty.Name = "ToolStripEmpty"
        Me.ToolStripEmpty.Size = New System.Drawing.Size(521, 17)
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
        'txtCoACodeOfEquipments
        '
        Me.txtCoACodeOfEquipments.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfEquipments.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfEquipments.Location = New System.Drawing.Point(156, 232)
        Me.txtCoACodeOfEquipments.MaxLength = 250
        Me.txtCoACodeOfEquipments.Name = "txtCoACodeOfEquipments"
        Me.txtCoACodeOfEquipments.ReadOnly = True
        Me.txtCoACodeOfEquipments.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfEquipments.TabIndex = 24
        Me.txtCoACodeOfEquipments.TabStop = False
        '
        'txtCoANameOfEquipments
        '
        Me.txtCoANameOfEquipments.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfEquipments.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfEquipments.Location = New System.Drawing.Point(220, 232)
        Me.txtCoANameOfEquipments.MaxLength = 250
        Me.txtCoANameOfEquipments.Name = "txtCoANameOfEquipments"
        Me.txtCoANameOfEquipments.ReadOnly = True
        Me.txtCoANameOfEquipments.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfEquipments.TabIndex = 25
        Me.txtCoANameOfEquipments.TabStop = False
        '
        'txtCoACodeOfPurchaseDiscount
        '
        Me.txtCoACodeOfPurchaseDiscount.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfPurchaseDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfPurchaseDiscount.Location = New System.Drawing.Point(156, 205)
        Me.txtCoACodeOfPurchaseDiscount.MaxLength = 250
        Me.txtCoACodeOfPurchaseDiscount.Name = "txtCoACodeOfPurchaseDiscount"
        Me.txtCoACodeOfPurchaseDiscount.ReadOnly = True
        Me.txtCoACodeOfPurchaseDiscount.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfPurchaseDiscount.TabIndex = 21
        Me.txtCoACodeOfPurchaseDiscount.TabStop = False
        '
        'txtCoANameOfPurchaseDiscount
        '
        Me.txtCoANameOfPurchaseDiscount.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfPurchaseDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfPurchaseDiscount.Location = New System.Drawing.Point(220, 205)
        Me.txtCoANameOfPurchaseDiscount.MaxLength = 250
        Me.txtCoANameOfPurchaseDiscount.Name = "txtCoANameOfPurchaseDiscount"
        Me.txtCoANameOfPurchaseDiscount.ReadOnly = True
        Me.txtCoANameOfPurchaseDiscount.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfPurchaseDiscount.TabIndex = 22
        Me.txtCoANameOfPurchaseDiscount.TabStop = False
        '
        'txtCoACodeOfAccountPayable
        '
        Me.txtCoACodeOfAccountPayable.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfAccountPayable.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfAccountPayable.Location = New System.Drawing.Point(156, 178)
        Me.txtCoACodeOfAccountPayable.MaxLength = 250
        Me.txtCoACodeOfAccountPayable.Name = "txtCoACodeOfAccountPayable"
        Me.txtCoACodeOfAccountPayable.ReadOnly = True
        Me.txtCoACodeOfAccountPayable.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfAccountPayable.TabIndex = 18
        Me.txtCoACodeOfAccountPayable.TabStop = False
        '
        'txtCoANameOfAccountPayable
        '
        Me.txtCoANameOfAccountPayable.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfAccountPayable.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfAccountPayable.Location = New System.Drawing.Point(220, 178)
        Me.txtCoANameOfAccountPayable.MaxLength = 250
        Me.txtCoANameOfAccountPayable.Name = "txtCoANameOfAccountPayable"
        Me.txtCoANameOfAccountPayable.ReadOnly = True
        Me.txtCoANameOfAccountPayable.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfAccountPayable.TabIndex = 19
        Me.txtCoANameOfAccountPayable.TabStop = False
        '
        'txtCoACodeOfCashOrBank
        '
        Me.txtCoACodeOfCashOrBank.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfCashOrBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfCashOrBank.Location = New System.Drawing.Point(156, 151)
        Me.txtCoACodeOfCashOrBank.MaxLength = 250
        Me.txtCoACodeOfCashOrBank.Name = "txtCoACodeOfCashOrBank"
        Me.txtCoACodeOfCashOrBank.ReadOnly = True
        Me.txtCoACodeOfCashOrBank.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfCashOrBank.TabIndex = 15
        Me.txtCoACodeOfCashOrBank.TabStop = False
        '
        'txtCoANameOfCashOrBank
        '
        Me.txtCoANameOfCashOrBank.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfCashOrBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfCashOrBank.Location = New System.Drawing.Point(220, 151)
        Me.txtCoANameOfCashOrBank.MaxLength = 250
        Me.txtCoANameOfCashOrBank.Name = "txtCoANameOfCashOrBank"
        Me.txtCoANameOfCashOrBank.ReadOnly = True
        Me.txtCoANameOfCashOrBank.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfCashOrBank.TabIndex = 16
        Me.txtCoANameOfCashOrBank.TabStop = False
        '
        'txtCoACodeOfStock
        '
        Me.txtCoACodeOfStock.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfStock.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfStock.Location = New System.Drawing.Point(156, 124)
        Me.txtCoACodeOfStock.MaxLength = 250
        Me.txtCoACodeOfStock.Name = "txtCoACodeOfStock"
        Me.txtCoACodeOfStock.ReadOnly = True
        Me.txtCoACodeOfStock.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfStock.TabIndex = 12
        Me.txtCoACodeOfStock.TabStop = False
        '
        'txtCoANameOfStock
        '
        Me.txtCoANameOfStock.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfStock.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfStock.Location = New System.Drawing.Point(220, 124)
        Me.txtCoANameOfStock.MaxLength = 250
        Me.txtCoANameOfStock.Name = "txtCoANameOfStock"
        Me.txtCoANameOfStock.ReadOnly = True
        Me.txtCoANameOfStock.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfStock.TabIndex = 13
        Me.txtCoANameOfStock.TabStop = False
        '
        'txtCoACodeOfCOGS
        '
        Me.txtCoACodeOfCOGS.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfCOGS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfCOGS.Location = New System.Drawing.Point(156, 97)
        Me.txtCoACodeOfCOGS.MaxLength = 250
        Me.txtCoACodeOfCOGS.Name = "txtCoACodeOfCOGS"
        Me.txtCoACodeOfCOGS.ReadOnly = True
        Me.txtCoACodeOfCOGS.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfCOGS.TabIndex = 9
        Me.txtCoACodeOfCOGS.TabStop = False
        '
        'txtCoANameOfCOGS
        '
        Me.txtCoANameOfCOGS.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfCOGS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfCOGS.Location = New System.Drawing.Point(220, 97)
        Me.txtCoANameOfCOGS.MaxLength = 250
        Me.txtCoANameOfCOGS.Name = "txtCoANameOfCOGS"
        Me.txtCoANameOfCOGS.ReadOnly = True
        Me.txtCoANameOfCOGS.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfCOGS.TabIndex = 10
        Me.txtCoANameOfCOGS.TabStop = False
        '
        'txtCoACodeOfSalesDiscount
        '
        Me.txtCoACodeOfSalesDiscount.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfSalesDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfSalesDiscount.Location = New System.Drawing.Point(156, 70)
        Me.txtCoACodeOfSalesDiscount.MaxLength = 250
        Me.txtCoACodeOfSalesDiscount.Name = "txtCoACodeOfSalesDiscount"
        Me.txtCoACodeOfSalesDiscount.ReadOnly = True
        Me.txtCoACodeOfSalesDiscount.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfSalesDiscount.TabIndex = 6
        Me.txtCoACodeOfSalesDiscount.TabStop = False
        '
        'txtCoANameOfSalesDiscount
        '
        Me.txtCoANameOfSalesDiscount.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfSalesDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfSalesDiscount.Location = New System.Drawing.Point(220, 70)
        Me.txtCoANameOfSalesDiscount.MaxLength = 250
        Me.txtCoANameOfSalesDiscount.Name = "txtCoANameOfSalesDiscount"
        Me.txtCoANameOfSalesDiscount.ReadOnly = True
        Me.txtCoANameOfSalesDiscount.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfSalesDiscount.TabIndex = 7
        Me.txtCoANameOfSalesDiscount.TabStop = False
        '
        'txtCoACodeOfAccountReceivable
        '
        Me.txtCoACodeOfAccountReceivable.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfAccountReceivable.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfAccountReceivable.Location = New System.Drawing.Point(156, 43)
        Me.txtCoACodeOfAccountReceivable.MaxLength = 250
        Me.txtCoACodeOfAccountReceivable.Name = "txtCoACodeOfAccountReceivable"
        Me.txtCoACodeOfAccountReceivable.ReadOnly = True
        Me.txtCoACodeOfAccountReceivable.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfAccountReceivable.TabIndex = 3
        Me.txtCoACodeOfAccountReceivable.TabStop = False
        '
        'txtCoANameOfAccountReceivable
        '
        Me.txtCoANameOfAccountReceivable.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfAccountReceivable.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfAccountReceivable.Location = New System.Drawing.Point(220, 43)
        Me.txtCoANameOfAccountReceivable.MaxLength = 250
        Me.txtCoANameOfAccountReceivable.Name = "txtCoANameOfAccountReceivable"
        Me.txtCoANameOfAccountReceivable.ReadOnly = True
        Me.txtCoANameOfAccountReceivable.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfAccountReceivable.TabIndex = 4
        Me.txtCoANameOfAccountReceivable.TabStop = False
        '
        'txtCoACodeOfRevenue
        '
        Me.txtCoACodeOfRevenue.BackColor = System.Drawing.Color.Azure
        Me.txtCoACodeOfRevenue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoACodeOfRevenue.Location = New System.Drawing.Point(156, 16)
        Me.txtCoACodeOfRevenue.MaxLength = 250
        Me.txtCoACodeOfRevenue.Name = "txtCoACodeOfRevenue"
        Me.txtCoACodeOfRevenue.ReadOnly = True
        Me.txtCoACodeOfRevenue.Size = New System.Drawing.Size(63, 21)
        Me.txtCoACodeOfRevenue.TabIndex = 0
        Me.txtCoACodeOfRevenue.TabStop = False
        '
        'txtCoANameOfRevenue
        '
        Me.txtCoANameOfRevenue.BackColor = System.Drawing.Color.Azure
        Me.txtCoANameOfRevenue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCoANameOfRevenue.Location = New System.Drawing.Point(220, 16)
        Me.txtCoANameOfRevenue.MaxLength = 250
        Me.txtCoANameOfRevenue.Name = "txtCoANameOfRevenue"
        Me.txtCoANameOfRevenue.ReadOnly = True
        Me.txtCoANameOfRevenue.Size = New System.Drawing.Size(341, 21)
        Me.txtCoANameOfRevenue.TabIndex = 1
        Me.txtCoANameOfRevenue.TabStop = False
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarRefresh, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(644, 28)
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
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pgMain.Location = New System.Drawing.Point(0, 353)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(644, 23)
        Me.pgMain.TabIndex = 4
        '
        'frmSysJournalPost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 376)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Name = "frmSysJournalPost"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Setup Posting Jurnal Transaksi"
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCoAOfRevenue As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtCoANameOfRevenue As MPS.usTextBox
    Friend WithEvents lblUomID1 As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents txtCoACodeOfRevenue As MPS.usTextBox
    Friend WithEvents txtCoACodeOfPurchaseDiscount As MPS.usTextBox
    Friend WithEvents btnCoAOfPurchaseDiscount As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCoANameOfPurchaseDiscount As MPS.usTextBox
    Friend WithEvents txtCoACodeOfAccountPayable As MPS.usTextBox
    Friend WithEvents btnCoAOfAccountPayable As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCoANameOfAccountPayable As MPS.usTextBox
    Friend WithEvents txtCoACodeOfCashOrBank As MPS.usTextBox
    Friend WithEvents btnCoAOfCashOrBank As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCoANameOfCashOrBank As MPS.usTextBox
    Friend WithEvents txtCoACodeOfStock As MPS.usTextBox
    Friend WithEvents btnCoAOfStock As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCoANameOfStock As MPS.usTextBox
    Friend WithEvents txtCoACodeOfCOGS As MPS.usTextBox
    Friend WithEvents btnCoAOfCOGS As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCoANameOfCOGS As MPS.usTextBox
    Friend WithEvents txtCoACodeOfSalesDiscount As MPS.usTextBox
    Friend WithEvents btnCoAOfSalesDiscount As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCoANameOfSalesDiscount As MPS.usTextBox
    Friend WithEvents txtCoACodeOfAccountReceivable As MPS.usTextBox
    Friend WithEvents btnCoAOfAccountReceivable As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCoANameOfAccountReceivable As MPS.usTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCoACodeOfEquipments As MPS.usTextBox
    Friend WithEvents btnCoAOfEquipments As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCoANameOfEquipments As MPS.usTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripEmpty As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogInc As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogBy As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pgMain As System.Windows.Forms.ProgressBar
End Class
