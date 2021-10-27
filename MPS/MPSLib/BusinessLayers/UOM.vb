Namespace BL
    Public Class UOM
        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.UOM.ListData
        End Function

        Public Shared Function ListDataForCombo() As DataTable
            BL.Server.ServerDefault()
            Return DL.UOM.ListDataForCombo
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.UOM) As VO.UOM
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.UOM.GetMaxID
                    If DL.UOM.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                End If

                DL.UOM.SaveData(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData
        End Function

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.UOM
            BL.Server.ServerDefault() 
            Return DL.UOM.GetDetail(intID)
        End Function 

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault() 
            Try
                DL.SQL.OpenConnection() 
                DL.SQL.BeginTransaction() 
                
                If DL.UOM.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.UOM.DeleteData(intID)
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

