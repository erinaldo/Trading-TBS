Namespace BL
    Public Class Modules

        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.Modules.ListData()
        End Function

        Public Shared Function ListDataForCombo() As DataTable
            BL.Server.ServerDefault()
            Return DL.Modules.ListDataForCombo()
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Modules) As Integer
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.Modules.GetMaxID
                    If DL.Modules.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                Else
                    If DL.Modules.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak diedit. Data telah dihapus sebelumnya")
                    End If
                End If

                DL.Modules.SaveData(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.Modules
            BL.Server.ServerDefault()
            Return DL.Modules.GetDetail(intID)
        End Function

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.Modules.IsDeleted(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.Modules.DeleteData(intID)
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

