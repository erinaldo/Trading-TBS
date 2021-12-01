Namespace BL
    Public Class BusinessPartner

#Region "Main"

        Public Shared Function ListData(ByVal decOnAmount As Decimal, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListData(decOnAmount, intCompanyID, intProgramID)
        End Function

        Public Shared Function ListDataForUpdatePrice() As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListDataForUpdatePrice
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.BusinessPartner) As Integer
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.BusinessPartner.GetMaxID
                    If DL.BusinessPartner.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                End If

                DL.BusinessPartner.SaveData(bolNew, clsData)

                '# Save Data Status
                Dim strRemarks As String = ""
                If clsData.SalesPrice > 0 Then strRemarks = "HARGA JUAL: " & Format(clsData.SalesPrice, "#,##0.00") & " "
                If clsData.PurchasePrice1 > 0 Then strRemarks = "HARGA BELI 1: " & Format(clsData.PurchasePrice1, "#,##0.00") & " "
                If clsData.PurchasePrice2 > 0 Then strRemarks = "HARGA BELI 2: " & Format(clsData.PurchasePrice2, "#,##0.00") & " "
                SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, strRemarks)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.BusinessPartner
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.GetDetail(intID)
        End Function

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.BusinessPartner.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.BusinessPartner.DeleteData(intID)
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Function UpdatePrice(ByVal clsDataAll() As VO.BusinessPartner) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                Dim strRemarks As String = ""
                For Each clsData As VO.BusinessPartner In clsDataAll
                    strRemarks = ""
                    DL.BusinessPartner.UpdatePrice(clsData)

                    '# Save Data Status
                    If clsData.SalesPrice > 0 Then strRemarks += "HARGA JUAL: " & Format(clsData.SalesPrice, "#,##0.00") & " "
                    If clsData.PurchasePrice1 > 0 Then strRemarks += "HARGA BELI 1: " & Format(clsData.PurchasePrice1, "#,##0.00") & " "
                    If clsData.PurchasePrice2 > 0 Then strRemarks += "HARGA BELI 2: " & Format(clsData.PurchasePrice2, "#,##0.00") & " "
                    SaveDataStatus(clsData.ID, "UBAH HARGA", clsData.LogBy, strRemarks)
                Next

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

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListDataStatus(intBPID)
        End Function

        Public Shared Sub SaveDataStatus(ByVal intBPID As Integer, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.BusinessPartnerStatus
            clsData.ID = DL.BusinessPartner.GetMaxIDStatus
            clsData.BPID = intBPID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.BusinessPartner.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace

