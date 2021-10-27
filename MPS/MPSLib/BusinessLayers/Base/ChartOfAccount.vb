Namespace BL
    Public Class ChartOfAccount

        Public Shared Function ListData(ByVal enumFilterGroup As VO.ChartOfAccount.FilterGroup) As DataTable
            BL.Server.ServerDefault()
            Return DL.ChartOfAccount.ListData(enumFilterGroup)
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.ChartOfAccount) As Integer
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.ChartOfAccount.GetMaxID
                    If DL.ChartOfAccount.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                End If

                If DL.ChartOfAccount.CodeExists(clsData.Code, clsData.ID) Then
                    Err.Raise(515, "", "Kode akun sudah ada sebelumnya")
                End If

                DL.ChartOfAccount.SaveData(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.ChartOfAccount
            BL.Server.ServerDefault()
            Return DL.ChartOfAccount.GetDetail(intID)
        End Function

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.ChartOfAccount.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.ChartOfAccount.DeleteData(intID)
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

