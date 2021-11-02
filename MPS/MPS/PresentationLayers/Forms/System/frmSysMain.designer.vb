<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysMain
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterProgram = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterModule = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterAkses = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterPerusahaan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterGroupAkunPerkiraan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterAkunPerkiraan = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMasterKaryawan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterRekanBisnis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterJenisPembayaran = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterReferensiPembayaran = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterDiskonPenjualan = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMasterSatuan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMasterBarang = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksi = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksiPenjualan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksiReturPenjualan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksiSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuTransaksiPembelian = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksiReturPembelian = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksiSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuTransaksiPenagihan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksiPembayaran = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksiSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuTransaksiBiaya = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTransaksiJurnalUmum = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanLaporanPenjualan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanLaporanReturPenjualan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLaporanLaporanPembelian = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanLaporanReturPembelian = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanSep2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLaporanLaporanTagihan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanLaporanPembayaran = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanSep3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLaporanLaporanBiaya = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanLaporanJurnalUmum = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanSep4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLaporanLaporanLabaRugi = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLaporanLaporanNeraca = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetting = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettingUbahPassword = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettingPostingJurnalTransaksi = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSysPostingDataTransaksi = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettingUnpostingDataTransaksi = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTampilan = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLogout = New System.Windows.Forms.ToolStripMenuItem()
        Me.ssMain = New System.Windows.Forms.StatusStrip()
        Me.tssUserID = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssProgram = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tssCompany = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1.SuspendLayout()
        Me.ssMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMaster, Me.mnuTransaksi, Me.mnuLaporan, Me.mnuSetting, Me.mnuTampilan, Me.mnuLogout})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.MdiWindowListItem = Me.mnuTampilan
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(797, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuMaster
        '
        Me.mnuMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMasterProgram, Me.mnuMasterStatus, Me.mnuMasterModule, Me.mnuMasterAkses, Me.mnuMasterPerusahaan, Me.mnuMasterGroupAkunPerkiraan, Me.mnuMasterAkunPerkiraan, Me.ToolStripSeparator1, Me.mnuMasterKaryawan, Me.mnuMasterRekanBisnis, Me.mnuMasterJenisPembayaran, Me.mnuMasterReferensiPembayaran, Me.mnuMasterDiskonPenjualan, Me.ToolStripSeparator2, Me.mnuMasterSatuan, Me.mnuMasterBarang})
        Me.mnuMaster.Name = "mnuMaster"
        Me.mnuMaster.Size = New System.Drawing.Size(55, 20)
        Me.mnuMaster.Text = "Master"
        '
        'mnuMasterProgram
        '
        Me.mnuMasterProgram.Name = "mnuMasterProgram"
        Me.mnuMasterProgram.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterProgram.Text = "Program"
        '
        'mnuMasterStatus
        '
        Me.mnuMasterStatus.Name = "mnuMasterStatus"
        Me.mnuMasterStatus.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterStatus.Text = "Status"
        '
        'mnuMasterModule
        '
        Me.mnuMasterModule.Name = "mnuMasterModule"
        Me.mnuMasterModule.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterModule.Text = "Module"
        '
        'mnuMasterAkses
        '
        Me.mnuMasterAkses.Name = "mnuMasterAkses"
        Me.mnuMasterAkses.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterAkses.Text = "Akses"
        '
        'mnuMasterPerusahaan
        '
        Me.mnuMasterPerusahaan.Name = "mnuMasterPerusahaan"
        Me.mnuMasterPerusahaan.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterPerusahaan.Text = "Perusahaan"
        '
        'mnuMasterGroupAkunPerkiraan
        '
        Me.mnuMasterGroupAkunPerkiraan.Name = "mnuMasterGroupAkunPerkiraan"
        Me.mnuMasterGroupAkunPerkiraan.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterGroupAkunPerkiraan.Text = "Group Akun Perkiraan"
        '
        'mnuMasterAkunPerkiraan
        '
        Me.mnuMasterAkunPerkiraan.Name = "mnuMasterAkunPerkiraan"
        Me.mnuMasterAkunPerkiraan.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterAkunPerkiraan.Text = "Akun Perkiraan"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(188, 6)
        '
        'mnuMasterKaryawan
        '
        Me.mnuMasterKaryawan.Name = "mnuMasterKaryawan"
        Me.mnuMasterKaryawan.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterKaryawan.Text = "Karyawan"
        '
        'mnuMasterRekanBisnis
        '
        Me.mnuMasterRekanBisnis.Name = "mnuMasterRekanBisnis"
        Me.mnuMasterRekanBisnis.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterRekanBisnis.Text = "Rekan Bisnis"
        '
        'mnuMasterJenisPembayaran
        '
        Me.mnuMasterJenisPembayaran.Name = "mnuMasterJenisPembayaran"
        Me.mnuMasterJenisPembayaran.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterJenisPembayaran.Text = "Jenis Pembayaran"
        '
        'mnuMasterReferensiPembayaran
        '
        Me.mnuMasterReferensiPembayaran.Name = "mnuMasterReferensiPembayaran"
        Me.mnuMasterReferensiPembayaran.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterReferensiPembayaran.Text = "Referensi Pembayaran"
        '
        'mnuMasterDiskonPenjualan
        '
        Me.mnuMasterDiskonPenjualan.Name = "mnuMasterDiskonPenjualan"
        Me.mnuMasterDiskonPenjualan.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterDiskonPenjualan.Text = "Diskon Penjualan"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(188, 6)
        '
        'mnuMasterSatuan
        '
        Me.mnuMasterSatuan.Name = "mnuMasterSatuan"
        Me.mnuMasterSatuan.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterSatuan.Text = "Satuan"
        '
        'mnuMasterBarang
        '
        Me.mnuMasterBarang.Name = "mnuMasterBarang"
        Me.mnuMasterBarang.Size = New System.Drawing.Size(191, 22)
        Me.mnuMasterBarang.Text = "Barang"
        '
        'mnuTransaksi
        '
        Me.mnuTransaksi.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuTransaksiPenjualan, Me.mnuTransaksiReturPenjualan, Me.mnuTransaksiSep1, Me.mnuTransaksiPembelian, Me.mnuTransaksiReturPembelian, Me.mnuTransaksiSep2, Me.mnuTransaksiPenagihan, Me.mnuTransaksiPembayaran, Me.mnuTransaksiSep3, Me.mnuTransaksiBiaya, Me.mnuTransaksiJurnalUmum})
        Me.mnuTransaksi.Name = "mnuTransaksi"
        Me.mnuTransaksi.Size = New System.Drawing.Size(66, 20)
        Me.mnuTransaksi.Text = "Transaksi"
        '
        'mnuTransaksiPenjualan
        '
        Me.mnuTransaksiPenjualan.Name = "mnuTransaksiPenjualan"
        Me.mnuTransaksiPenjualan.Size = New System.Drawing.Size(161, 22)
        Me.mnuTransaksiPenjualan.Text = "Penjualan"
        '
        'mnuTransaksiReturPenjualan
        '
        Me.mnuTransaksiReturPenjualan.Name = "mnuTransaksiReturPenjualan"
        Me.mnuTransaksiReturPenjualan.Size = New System.Drawing.Size(161, 22)
        Me.mnuTransaksiReturPenjualan.Text = "Retur Penjualan"
        '
        'mnuTransaksiSep1
        '
        Me.mnuTransaksiSep1.Name = "mnuTransaksiSep1"
        Me.mnuTransaksiSep1.Size = New System.Drawing.Size(158, 6)
        '
        'mnuTransaksiPembelian
        '
        Me.mnuTransaksiPembelian.Name = "mnuTransaksiPembelian"
        Me.mnuTransaksiPembelian.Size = New System.Drawing.Size(161, 22)
        Me.mnuTransaksiPembelian.Text = "Pembelian"
        '
        'mnuTransaksiReturPembelian
        '
        Me.mnuTransaksiReturPembelian.Name = "mnuTransaksiReturPembelian"
        Me.mnuTransaksiReturPembelian.Size = New System.Drawing.Size(161, 22)
        Me.mnuTransaksiReturPembelian.Text = "Retur Pembelian"
        '
        'mnuTransaksiSep2
        '
        Me.mnuTransaksiSep2.Name = "mnuTransaksiSep2"
        Me.mnuTransaksiSep2.Size = New System.Drawing.Size(158, 6)
        '
        'mnuTransaksiPenagihan
        '
        Me.mnuTransaksiPenagihan.Name = "mnuTransaksiPenagihan"
        Me.mnuTransaksiPenagihan.Size = New System.Drawing.Size(161, 22)
        Me.mnuTransaksiPenagihan.Text = "Penagihan"
        '
        'mnuTransaksiPembayaran
        '
        Me.mnuTransaksiPembayaran.Name = "mnuTransaksiPembayaran"
        Me.mnuTransaksiPembayaran.Size = New System.Drawing.Size(161, 22)
        Me.mnuTransaksiPembayaran.Text = "Pembayaran"
        '
        'mnuTransaksiSep3
        '
        Me.mnuTransaksiSep3.Name = "mnuTransaksiSep3"
        Me.mnuTransaksiSep3.Size = New System.Drawing.Size(158, 6)
        '
        'mnuTransaksiBiaya
        '
        Me.mnuTransaksiBiaya.Name = "mnuTransaksiBiaya"
        Me.mnuTransaksiBiaya.Size = New System.Drawing.Size(161, 22)
        Me.mnuTransaksiBiaya.Text = "Biaya"
        '
        'mnuTransaksiJurnalUmum
        '
        Me.mnuTransaksiJurnalUmum.Name = "mnuTransaksiJurnalUmum"
        Me.mnuTransaksiJurnalUmum.Size = New System.Drawing.Size(161, 22)
        Me.mnuTransaksiJurnalUmum.Text = "Jurnal Umum"
        '
        'mnuLaporan
        '
        Me.mnuLaporan.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLaporanLaporanPenjualan, Me.mnuLaporanLaporanReturPenjualan, Me.mnuLaporanSep1, Me.mnuLaporanLaporanPembelian, Me.mnuLaporanLaporanReturPembelian, Me.mnuLaporanSep2, Me.mnuLaporanLaporanTagihan, Me.mnuLaporanLaporanPembayaran, Me.mnuLaporanSep3, Me.mnuLaporanLaporanBiaya, Me.mnuLaporanLaporanJurnalUmum, Me.mnuLaporanSep4, Me.mnuLaporanLaporanLabaRugi, Me.mnuLaporanLaporanNeraca})
        Me.mnuLaporan.Name = "mnuLaporan"
        Me.mnuLaporan.Size = New System.Drawing.Size(62, 20)
        Me.mnuLaporan.Text = "Laporan"
        '
        'mnuLaporanLaporanPenjualan
        '
        Me.mnuLaporanLaporanPenjualan.Name = "mnuLaporanLaporanPenjualan"
        Me.mnuLaporanLaporanPenjualan.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanPenjualan.Text = "Laporan Penjualan"
        '
        'mnuLaporanLaporanReturPenjualan
        '
        Me.mnuLaporanLaporanReturPenjualan.Name = "mnuLaporanLaporanReturPenjualan"
        Me.mnuLaporanLaporanReturPenjualan.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanReturPenjualan.Text = "Laporan Retur Penjualan"
        '
        'mnuLaporanSep1
        '
        Me.mnuLaporanSep1.Name = "mnuLaporanSep1"
        Me.mnuLaporanSep1.Size = New System.Drawing.Size(204, 6)
        '
        'mnuLaporanLaporanPembelian
        '
        Me.mnuLaporanLaporanPembelian.Name = "mnuLaporanLaporanPembelian"
        Me.mnuLaporanLaporanPembelian.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanPembelian.Text = "Laporan Pembelian"
        '
        'mnuLaporanLaporanReturPembelian
        '
        Me.mnuLaporanLaporanReturPembelian.Name = "mnuLaporanLaporanReturPembelian"
        Me.mnuLaporanLaporanReturPembelian.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanReturPembelian.Text = "Laporan Retur Pembelian"
        '
        'mnuLaporanSep2
        '
        Me.mnuLaporanSep2.Name = "mnuLaporanSep2"
        Me.mnuLaporanSep2.Size = New System.Drawing.Size(204, 6)
        '
        'mnuLaporanLaporanTagihan
        '
        Me.mnuLaporanLaporanTagihan.Name = "mnuLaporanLaporanTagihan"
        Me.mnuLaporanLaporanTagihan.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanTagihan.Text = "Laporan Tagihan"
        '
        'mnuLaporanLaporanPembayaran
        '
        Me.mnuLaporanLaporanPembayaran.Name = "mnuLaporanLaporanPembayaran"
        Me.mnuLaporanLaporanPembayaran.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanPembayaran.Text = "Laporan Pembayaran"
        '
        'mnuLaporanSep3
        '
        Me.mnuLaporanSep3.Name = "mnuLaporanSep3"
        Me.mnuLaporanSep3.Size = New System.Drawing.Size(204, 6)
        '
        'mnuLaporanLaporanBiaya
        '
        Me.mnuLaporanLaporanBiaya.Name = "mnuLaporanLaporanBiaya"
        Me.mnuLaporanLaporanBiaya.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanBiaya.Text = "Laporan Biaya"
        '
        'mnuLaporanLaporanJurnalUmum
        '
        Me.mnuLaporanLaporanJurnalUmum.Name = "mnuLaporanLaporanJurnalUmum"
        Me.mnuLaporanLaporanJurnalUmum.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanJurnalUmum.Text = "Laporan Jurnal Umum"
        '
        'mnuLaporanSep4
        '
        Me.mnuLaporanSep4.Name = "mnuLaporanSep4"
        Me.mnuLaporanSep4.Size = New System.Drawing.Size(204, 6)
        '
        'mnuLaporanLaporanLabaRugi
        '
        Me.mnuLaporanLaporanLabaRugi.Name = "mnuLaporanLaporanLabaRugi"
        Me.mnuLaporanLaporanLabaRugi.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanLabaRugi.Text = "Laporan Laba Rugi"
        '
        'mnuLaporanLaporanNeraca
        '
        Me.mnuLaporanLaporanNeraca.Name = "mnuLaporanLaporanNeraca"
        Me.mnuLaporanLaporanNeraca.Size = New System.Drawing.Size(207, 22)
        Me.mnuLaporanLaporanNeraca.Text = "Laporan Neraca"
        '
        'mnuSetting
        '
        Me.mnuSetting.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSettingUbahPassword, Me.mnuSettingPostingJurnalTransaksi, Me.mnuSysPostingDataTransaksi, Me.mnuSettingUnpostingDataTransaksi})
        Me.mnuSetting.Name = "mnuSetting"
        Me.mnuSetting.Size = New System.Drawing.Size(56, 20)
        Me.mnuSetting.Text = "Setting"
        '
        'mnuSettingUbahPassword
        '
        Me.mnuSettingUbahPassword.Name = "mnuSettingUbahPassword"
        Me.mnuSettingUbahPassword.Size = New System.Drawing.Size(206, 22)
        Me.mnuSettingUbahPassword.Text = "Ubah Password"
        '
        'mnuSettingPostingJurnalTransaksi
        '
        Me.mnuSettingPostingJurnalTransaksi.Name = "mnuSettingPostingJurnalTransaksi"
        Me.mnuSettingPostingJurnalTransaksi.Size = New System.Drawing.Size(206, 22)
        Me.mnuSettingPostingJurnalTransaksi.Text = "Posting Jurnal Transaksi"
        '
        'mnuSysPostingDataTransaksi
        '
        Me.mnuSysPostingDataTransaksi.Name = "mnuSysPostingDataTransaksi"
        Me.mnuSysPostingDataTransaksi.Size = New System.Drawing.Size(206, 22)
        Me.mnuSysPostingDataTransaksi.Text = "Posting Data Transaksi"
        '
        'mnuSettingUnpostingDataTransaksi
        '
        Me.mnuSettingUnpostingDataTransaksi.Name = "mnuSettingUnpostingDataTransaksi"
        Me.mnuSettingUnpostingDataTransaksi.Size = New System.Drawing.Size(206, 22)
        Me.mnuSettingUnpostingDataTransaksi.Text = "Unposting Data Transaksi"
        '
        'mnuTampilan
        '
        Me.mnuTampilan.Name = "mnuTampilan"
        Me.mnuTampilan.Size = New System.Drawing.Size(67, 20)
        Me.mnuTampilan.Text = "Tampilan"
        '
        'mnuLogout
        '
        Me.mnuLogout.Name = "mnuLogout"
        Me.mnuLogout.Size = New System.Drawing.Size(57, 20)
        Me.mnuLogout.Text = "Logout"
        '
        'ssMain
        '
        Me.ssMain.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.ssMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssUserID, Me.tssVersion, Me.tssProgram, Me.tssCompany})
        Me.ssMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ssMain.Location = New System.Drawing.Point(0, 252)
        Me.ssMain.Name = "ssMain"
        Me.ssMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode
        Me.ssMain.Size = New System.Drawing.Size(797, 22)
        Me.ssMain.TabIndex = 3
        Me.ssMain.Text = "StatusStrip1"
        '
        'tssUserID
        '
        Me.tssUserID.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tssUserID.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tssUserID.Name = "tssUserID"
        Me.tssUserID.Size = New System.Drawing.Size(44, 17)
        Me.tssUserID.Text = "UserID"
        '
        'tssVersion
        '
        Me.tssVersion.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tssVersion.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tssVersion.Name = "tssVersion"
        Me.tssVersion.Size = New System.Drawing.Size(46, 17)
        Me.tssVersion.Text = "Version"
        '
        'tssProgram
        '
        Me.tssProgram.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tssProgram.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tssProgram.Name = "tssProgram"
        Me.tssProgram.Size = New System.Drawing.Size(51, 17)
        Me.tssProgram.Text = "Program"
        '
        'tssCompany
        '
        Me.tssCompany.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tssCompany.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tssCompany.Name = "tssCompany"
        Me.tssCompany.Size = New System.Drawing.Size(56, 17)
        Me.tssCompany.Text = "Company"
        '
        'frmSysMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 274)
        Me.Controls.Add(Me.ssMain)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmSysMain"
        Me.Text = "iPOS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ssMain.ResumeLayout(False)
        Me.ssMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterKaryawan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterBarang As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterRekanBisnis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksiPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksiReturPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksiSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuTransaksiPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksiReturPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksiSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuTransaksiPenagihan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksiPembayaran As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanLaporanPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanLaporanReturPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuLaporanLaporanPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanLaporanReturPembelian As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanSep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuLaporanLaporanTagihan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanLaporanPembayaran As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTampilan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLogout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterSatuan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterModule As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMasterJenisPembayaran As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterReferensiPembayaran As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterDiskonPenjualan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterPerusahaan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ssMain As System.Windows.Forms.StatusStrip
    Friend WithEvents tssUserID As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssVersion As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mnuMasterGroupAkunPerkiraan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterAkunPerkiraan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksiSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuTransaksiBiaya As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTransaksiJurnalUmum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanSep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuLaporanLaporanBiaya As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanLaporanJurnalUmum As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterAkses As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSetting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSettingUbahPassword As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSettingPostingJurnalTransaksi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSysPostingDataTransaksi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSettingUnpostingDataTransaksi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanSep4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuLaporanLaporanLabaRugi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLaporanLaporanNeraca As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMasterProgram As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssProgram As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tssCompany As System.Windows.Forms.ToolStripStatusLabel
End Class
