Namespace BL
    Public Class StatusModules

        Public Shared Function ListDataByIDStatus(ByVal intIDStatus As VO.Status.Values) As DataTable
            BL.Server.ServerDefault()
            Return DL.StatusModules.ListDataByIDStatus(intIDStatus)
        End Function

        Public Shared Function ListDataByModulesID(ByVal intModulesID As VO.Modules.Values) As DataTable
            BL.Server.ServerDefault()
            Return DL.StatusModules.ListDataByModulesID(intModulesID)
        End Function

        Public Shared Sub SaveDataByIDStatus(ByVal clsDataAll() As VO.StatusModules)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.StatusModules.DeleteDataByIDStatus(clsDataAll(0).IDStatus)

                For Each clsItem As VO.StatusModules In clsDataAll
                    clsItem.ID = DL.StatusModules.GetMaxID
                    DL.StatusModules.SaveData(clsItem)
                Next

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub SaveDataByModulesID(ByVal clsDataAll() As VO.StatusModules)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.StatusModules.DeleteDataByModulesID(clsDataAll(0).ModulesID)

                For Each clsItem As VO.StatusModules In clsDataAll
                    clsItem.ID = DL.StatusModules.GetMaxID
                    DL.StatusModules.SaveData(clsItem)
                Next

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

