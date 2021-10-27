Namespace BL
    Public Class Status

        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.Status.ListData()
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Status) As Integer
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.Status.GetMaxID
                    If DL.Status.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                Else
                    If DL.Status.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak diedit. Data telah dihapus sebelumnya")
                    End If
                End If

                DL.Status.SaveData(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.Status
            BL.Server.ServerDefault()
            Return DL.Status.GetDetail(intID)
        End Function

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.Status.IsDeleted(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.Status.DeleteData(intID)
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

