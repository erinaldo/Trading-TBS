Public Class frmSysMain

#Region "Variable Handle"

    Dim bolLogOut As Boolean

    '# Master
    Dim frmMainMstProgram As frmMstProgram
    Dim frmMainMstStatus As frmMstStatus
    Dim frmMainMstModules As frmMstModules
    Dim frmMainMstAccess As frmMstAccess
    Dim frmMainMstCompany As frmMstCompany
    Dim frmMainMstChartOfAccountGroup As frmMstChartOfAccountGroup
    Dim frmMainMstChartOfAccount As frmMstChartOfAccount
    Dim frmMainMstUser As frmMstUser
    Dim frmMainMstBusinessPartner As frmMstBusinessPartner
    Dim frmMainMstPaymentTerm As frmMstPaymentTerm
    Dim frmMainMstPaymentReferences As frmMstPaymentReferences
    Dim frmMainMstSalesDiscount As frmMstSalesDiscount
    Dim frmMainMstUOM As frmMstUOM
    Dim frmMainMstItem As frmMstItem

    '# Transaction
    Dim frmMainTraSales As frmTraSales
    Dim frmMainTraSalesReturn As frmTraSalesReturn
    Dim frmMainTraReceive As frmTraReceive
    Dim frmMainTraReceiveReturn As frmTraReceiveReturn
    Dim frmMainTraAccountReceivable As frmTraAccountReceivable
    Dim frmMainTraAccountPayable As frmTraAccountPayable
    Dim frmMainTraCost As frmTraCost
    Dim frmMainTraJournal As frmTraJournal

    ''# Reports
    'Dim frmMainRptSalesReport As frmRptSalesReport
    'Dim frmMainRptSalesReturnReport As frmRptSalesReturnReport
    'Dim frmMainRptPurhaseReport As frmRptPurhaseReport
    'Dim frmMainRptPurchaseReturnReport As frmRptPurchaseReturnReport
    'Dim frmMainRptAccountReceivableReport As frmRptAccountReceivableReport
    'Dim frmMainRptAccountPayableReport As frmRptAccountPayableReport
    'Dim frmMainRptCostReport As frmRptCostReport
    'Dim frmMainRptJournalReport As frmRptJournalReport
    'Dim frmMainRptProfitAndLossReport As frmRptProfitAndLossReport
    'Dim frmMainRptBalanceSheet As frmRptBalanceSheet

    '# Setting
    Dim frmMainMstUserChangePassword As frmMstUserChangePassword
    Dim frmMainSysJournalPost As frmSysJournalPost
    Dim frmMainSysPostingOrCancelPostingGL As frmSysPostingOrCancelPostingGL

#End Region

    Private Sub prvSetupStatusStrip()
        tssUserID.Text = MPSLib.UI.usUserApp.UserID
        tssVersion.Text = "Versi: " & FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion
        tssProgram.Text = MPSLib.UI.usUserApp.ProgramName
        tssCompany.Text = MPSLib.UI.usUserApp.CompanyName
    End Sub

    Private Sub prvUserAccess()
        '# Master
        mnuMasterProgram.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterStatus.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterModule.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterAkses.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterPerusahaan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterCompany, VO.Access.Values.ViewAccess)
        mnuMasterGroupAkunPerkiraan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterChartOfAccountGroup, VO.Access.Values.ViewAccess)
        mnuMasterAkunPerkiraan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterChartOfAccount, VO.Access.Values.ViewAccess)
        mnuMasterKaryawan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUser, VO.Access.Values.ViewAccess)
        mnuMasterRekanBisnis.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, VO.Access.Values.ViewAccess)
        mnuMasterJenisPembayaran.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterPaymentTerm, VO.Access.Values.ViewAccess)
        mnuMasterReferensiPembayaran.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterPaymentReferences, VO.Access.Values.ViewAccess)
        mnuMasterDiskonPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterSalesDiscount, VO.Access.Values.ViewAccess)
        mnuMasterSatuan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUOM, VO.Access.Values.ViewAccess)
        mnuMasterBarang.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterItem, VO.Access.Values.ViewAccess)

        '# Transaction
        mnuTransaksiPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSales, VO.Access.Values.ViewAccess)
        mnuTransaksiReturPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesReturn, VO.Access.Values.ViewAccess)

        mnuTransaksiPembelian.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceive, VO.Access.Values.ViewAccess)
        mnuTransaksiReturPembelian.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceiveReturn, VO.Access.Values.ViewAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceive, VO.Access.Values.ViewAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceiveReturn, VO.Access.Values.ViewAccess) = False Then
            mnuTransaksiSep1.Visible = False
        End If

        mnuTransaksiPenagihan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountReceivable, VO.Access.Values.ViewAccess)
        mnuTransaksiPembayaran.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountPayable, VO.Access.Values.ViewAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountReceivable, VO.Access.Values.ViewAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountPayable, VO.Access.Values.ViewAccess) = False Then
            mnuTransaksiSep2.Visible = False
        End If

        mnuTransaksiBiaya.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.ViewAccess)
        mnuTransaksiJurnalUmum.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.ViewAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.ViewAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.ViewAccess) = False Then
            mnuTransaksiSep3.Visible = False
        End If

        '# Reports
        mnuLaporanLaporanPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSales, VO.Access.Values.PrintReportAccess)
        mnuLaporanLaporanReturPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesReturn, VO.Access.Values.PrintReportAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceive, VO.Access.Values.PrintReportAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceiveReturn, VO.Access.Values.PrintReportAccess) = False Then
            mnuLaporanSep1.Visible = False
        End If

        mnuLaporanLaporanPembelian.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceive, VO.Access.Values.PrintReportAccess)
        mnuLaporanLaporanReturPembelian.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceiveReturn, VO.Access.Values.PrintReportAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountReceivable, VO.Access.Values.PrintReportAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountPayable, VO.Access.Values.PrintReportAccess) = False Then
            mnuLaporanSep2.Visible = False
        End If

        mnuLaporanLaporanTagihan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountReceivable, VO.Access.Values.PrintReportAccess)
        mnuLaporanLaporanPembayaran.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountPayable, VO.Access.Values.PrintReportAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.PrintReportAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.ViewAccess) = False Then
            mnuLaporanSep3.Visible = False
        End If

        mnuLaporanLaporanBiaya.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.PrintReportAccess)
        mnuLaporanLaporanJurnalUmum.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.ViewAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportProfitAndLoss, VO.Access.Values.PrintReportAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportBalanceSheet, VO.Access.Values.ViewAccess) = False Then
            mnuLaporanSep4.Visible = False
        End If

        mnuLaporanLaporanLabaRugi.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportProfitAndLoss, VO.Access.Values.ViewAccess)
        mnuLaporanLaporanNeraca.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportBalanceSheet, VO.Access.Values.ViewAccess)

        '# Settings
        mnuSettingSetupPostingJurnalTransaksi.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.SettingSetupPostingJournalTransaction, VO.Access.Values.ViewAccess)
        mnuSysPostingAndCancelPostingDataTransaksi.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.SettingPostingTransaction, VO.Access.Values.ViewAccess)
    End Sub

#Region "Form Handle"

    Private Sub frmSysMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        bolLogOut = False
        prvSetupStatusStrip()
        prvUserAccess()
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not bolLogOut Then
            If UI.usForm.frmAskQuestion("Keluar dari sistem ?") Then
                Application.Exit()
            Else
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Master"

    Private Sub mnuMasterProgram_Click(sender As Object, e As EventArgs) Handles mnuMasterProgram.Click
        UI.usForm.frmOpen(frmMainMstProgram, "frmMstProgram", Me)
    End Sub

    Private Sub mnuMasterStatus_Click(sender As Object, e As EventArgs) Handles mnuMasterStatus.Click
        UI.usForm.frmOpen(frmMainMstStatus, "frmMstStatus", Me)
    End Sub

    Private Sub mnuMasterModule_Click(sender As Object, e As EventArgs) Handles mnuMasterModule.Click
        UI.usForm.frmOpen(frmMainMstModules, "frmMstModules", Me)
    End Sub

    Private Sub mnuMasterAkses_Click(sender As Object, e As EventArgs) Handles mnuMasterAkses.Click
        UI.usForm.frmOpen(frmMainMstAccess, "frmMstAccess", Me)
    End Sub

    Private Sub mnuMasterPerusahaan_Click(sender As Object, e As EventArgs) Handles mnuMasterPerusahaan.Click
        UI.usForm.frmOpen(frmMainMstCompany, "frmMstCompany", Me)
    End Sub

    Private Sub mnuMasterGroupAkunPerkiraan_Click(sender As Object, e As EventArgs) Handles mnuMasterGroupAkunPerkiraan.Click
        UI.usForm.frmOpen(frmMainMstChartOfAccountGroup, "frmMstChartOfAccountGroup", Me)
    End Sub

    Private Sub mnuMasterAkunPerkiraan_Click(sender As Object, e As EventArgs) Handles mnuMasterAkunPerkiraan.Click
        UI.usForm.frmOpen(frmMainMstChartOfAccount, "frmMstChartOfAccount", Me)
    End Sub

    Private Sub mnuMasterKaryawan_Click(sender As Object, e As EventArgs) Handles mnuMasterKaryawan.Click
        UI.usForm.frmOpen(frmMainMstUser, "frmMstUser", Me)
    End Sub

    Private Sub mnuMasterRekanBisnis_Click(sender As Object, e As EventArgs) Handles mnuMasterRekanBisnis.Click
        UI.usForm.frmOpen(frmMainMstBusinessPartner, "frmMstBusinessPartner", Me)
    End Sub

    Private Sub mnuMasterJenisPembayaran_Click(sender As Object, e As EventArgs) Handles mnuMasterJenisPembayaran.Click
        UI.usForm.frmOpen(frmMainMstPaymentTerm, "frmMstPaymentTerm", Me)
    End Sub

    Private Sub mnuMasterReferensiPembayaran_Click(sender As Object, e As EventArgs) Handles mnuMasterReferensiPembayaran.Click
        UI.usForm.frmOpen(frmMainMstPaymentReferences, "frmMstPaymentReferences", Me)
    End Sub

    Private Sub mnuMasterDiskonPenjualan_Click(sender As Object, e As EventArgs) Handles mnuMasterDiskonPenjualan.Click
        UI.usForm.frmOpen(frmMainMstSalesDiscount, "frmMstSalesDiscount", Me)
    End Sub

    Private Sub mnuMasterSatuan_Click(sender As Object, e As EventArgs) Handles mnuMasterSatuan.Click
        UI.usForm.frmOpen(frmMainMstUOM, "frmMstUOM", Me)
    End Sub

    Private Sub mnuMasterBarang_Click(sender As Object, e As EventArgs) Handles mnuMasterBarang.Click
        UI.usForm.frmOpen(frmMainMstItem, "frmMstItem", Me)
    End Sub

#End Region

#Region "Transaction"

    Private Sub mnuTransaksiPenjualan_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenjualan.Click
        UI.usForm.frmOpen(frmMainTraSales, "frmTraSales", Me)
    End Sub

    Private Sub mnuTransaksiReturPenjualan_Click(sender As Object, e As EventArgs) Handles mnuTransaksiReturPenjualan.Click
        UI.usForm.frmOpen(frmMainTraSalesReturn, "frmTraSalesReturn", Me)
    End Sub

    Private Sub mnuTransaksiPembelian_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPembelian.Click
        UI.usForm.frmOpen(frmMainTraReceive, "frmTraReceive", Me)
    End Sub

    Private Sub mnuTransaksiReturPembelian_Click(sender As Object, e As EventArgs) Handles mnuTransaksiReturPembelian.Click
        UI.usForm.frmOpen(frmMainTraReceiveReturn, "frmTraReceiveReturn", Me)
    End Sub

    Private Sub mnuTransaksiPenagihan_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenagihan.Click
        UI.usForm.frmOpen(frmMainTraAccountReceivable, "frmTraAccountReceivable", Me)
    End Sub

    Private Sub mnuTransaksiPembayaran_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPembayaran.Click
        UI.usForm.frmOpen(frmMainTraAccountPayable, "frmTraAccountPayable", Me)
    End Sub

    Private Sub mnuTransaksiBiaya_Click(sender As Object, e As EventArgs) Handles mnuTransaksiBiaya.Click
        UI.usForm.frmOpen(frmMainTraCost, "frmTraCost", Me)
    End Sub

    Private Sub mnuTransaksiJurnalUmum_Click(sender As Object, e As EventArgs) Handles mnuTransaksiJurnalUmum.Click
        UI.usForm.frmOpen(frmMainTraJournal, "frmTraJournal", Me)
    End Sub

#End Region

#Region "Settings"

    Private Sub mnuSettingUbahPassword_Click(sender As Object, e As EventArgs) Handles mnuSettingUbahPassword.Click
        UI.usForm.frmOpen(frmMainMstUserChangePassword, "frmMstUserChangePassword", Me)
    End Sub

    Private Sub mnuSettingPostingJurnalTransaksi_Click(sender As Object, e As EventArgs) Handles mnuSettingSetupPostingJurnalTransaksi.Click
        UI.usForm.frmOpen(frmMainSysJournalPost, "frmSysJournalPost", Me)
    End Sub

    Private Sub mnuSysPostingAndCancelPostingDataTransaksi_Click(sender As Object, e As EventArgs) Handles mnuSysPostingAndCancelPostingDataTransaksi.Click
        UI.usForm.frmOpen(frmMainSysPostingOrCancelPostingGL, "frmSysPostingOrCancelPostingGL", Me)
    End Sub

#End Region

#Region "Reports"

    'Private Sub mnuLaporanLaporanPenjualan_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanPenjualan.Click
    '    UI.usForm.frmOpen(frmMainRptSalesReport, "frmRptSalesReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanReturPenjualan_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanReturPenjualan.Click
    '    UI.usForm.frmOpen(frmMainRptSalesReturnReport, "frmRptSalesReturnReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanPembelian_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanPembelian.Click
    '    UI.usForm.frmOpen(frmMainRptPurhaseReport, "frmRptPurhaseReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanReturPembelian_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanReturPembelian.Click
    '    UI.usForm.frmOpen(frmMainRptPurchaseReturnReport, "frmRptPurchaseReturnReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanTagihan_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanTagihan.Click
    '    UI.usForm.frmOpen(frmMainRptAccountReceivableReport, "frmRptAccountReceivableReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanPembayaran_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanPembayaran.Click
    '    UI.usForm.frmOpen(frmMainRptAccountPayableReport, "frmRptAccountPayableReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanBiaya_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanBiaya.Click
    '    UI.usForm.frmOpen(frmMainRptCostReport, "frmRptCostReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanJurnalUmum_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanJurnalUmum.Click
    '    UI.usForm.frmOpen(frmMainRptJournalReport, "frmRptJournalReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanLabaRugi_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanLabaRugi.Click
    '    UI.usForm.frmOpen(frmMainRptProfitAndLossReport, "frmRptProfitAndLossReport", Me)
    'End Sub

    'Private Sub mnuLaporanLaporanNeraca_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanNeraca.Click
    '    UI.usForm.frmOpen(frmMainRptBalanceSheet, "frmRptBalanceSheet", Me)
    'End Sub

#End Region

#Region "Windows"

    Private Sub mnuWindowsVertical_Click(sender As Object, e As EventArgs) Handles mnuWindowsVertical.Click
        LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub mnuWindowsHorizontal_Click(sender As Object, e As EventArgs) Handles mnuWindowsHorizontal.Click
        LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub mnuWindowsCascade_Click(sender As Object, e As EventArgs) Handles mnuWindowsCascade.Click
        LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub mnuWindowsCloseAll_Click(sender As Object, e As EventArgs) Handles mnuWindowsCloseAll.Click
        For Each Form As Form In Me.MdiChildren
            Form.Close()
        Next
    End Sub

#End Region

    Private Sub mnuLogout_Click(sender As Object, e As EventArgs) Handles mnuLogout.Click
        If Not UI.usForm.frmAskQuestion("Keluar dari program?") Then Exit Sub
        Application.Exit()
    End Sub

End Class