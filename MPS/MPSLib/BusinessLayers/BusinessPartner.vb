Namespace BL
    Public Class BusinessPartner

        Public Shared Function ListData(ByVal decOnAmount As Decimal) As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListData(decOnAmount)
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

    End Class

End Namespace

