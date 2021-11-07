Namespace BL
    Public Class PostGL
        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.PostGL.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                         ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Return clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-" & Format(dtmDateFrom, "yyMMdd") & "-" & Format(dtmDateTo, "yyMMdd")
        End Function

        Public Shared Function PostData(ByVal clsData As VO.PostGL) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If MPSLib.UI.usUserApp.JournalPost.LogBy Is Nothing Then
                    Err.Raise(515, "", "Mohon agar Setup Posting Jurnal Transaksi terlebih dahulu melalui menu Setting -> Setup Posting Jurnal Transaksi")
                End If

                If Format(clsData.DateTo, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Periode posting harus lebih besar dari tanggal terakhir posting")
                End If

                clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                If DL.PostGL.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID telah ada sebelumnya")
                End If

                BL.Receive.PostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.Sales.PostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.SalesReturn.PostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.ReceiveReturn.PostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.AccountReceivable.PostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.AccountPayable.PostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.Cost.PostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.Journal.PostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)

                DL.PostGL.SaveData(clsData)

                DL.SQL.CommitTransaction()
                bolReturn = True
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return bolReturn
        End Function

        Public Shared Function UnpostData(ByVal clsData As VO.PostGL) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If Format(clsData.DateTo, "yyyyMMdd") < DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Periode posting harus mulai dari tanggal terakhir posting")
                End If

                BL.ReceiveReturn.UnpostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.SalesReturn.UnpostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.Sales.UnpostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.Receive.UnpostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.AccountReceivable.UnpostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.AccountPayable.UnpostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.Cost.UnpostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                BL.Journal.UnpostData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)

                DL.PostGL.DeleteData(clsData.ID)

                DL.SQL.CommitTransaction()
                bolReturn = True
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return bolReturn
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.PostGL
            BL.Server.ServerDefault() 
            Return DL.PostGL.GetDetail(strID)
        End Function 

        Public Shared Function GetDetailLast(ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As VO.PostGL
            BL.Server.ServerDefault()
            Return DL.PostGL.GetDetailLast(intCompanyID, intProgramID)
        End Function

    End Class 

End Namespace

