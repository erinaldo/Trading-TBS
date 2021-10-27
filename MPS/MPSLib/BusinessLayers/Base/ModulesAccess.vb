Namespace BL
    Public Class ModulesAccess
        
        Public Shared Function ListDataByAccessID(ByVal intAccessID As VO.Access.Values) As DataTable
            BL.Server.ServerDefault()
            Return DL.ModulesAccess.ListDataByAccessID(intAccessID)
        End Function

        Public Shared Function ListDataByModulesID(ByVal intModulesID As VO.Modules.Values) As DataTable
            BL.Server.ServerDefault()
            Return DL.ModulesAccess.ListDataByModulesID(intModulesID)
        End Function

        Public Shared Sub SaveDataByAccessID(ByVal clsDataAll() As VO.ModulesAccess)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.ModulesAccess.DeleteDataByAccessID(clsDataAll(0).AccessID)

                For Each clsItem As VO.ModulesAccess In clsDataAll
                    clsItem.ID = DL.ModulesAccess.GetMaxID
                    DL.ModulesAccess.SaveData(clsItem)
                Next

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub SaveDataByModulesID(ByVal clsDataAll() As VO.ModulesAccess)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.ModulesAccess.DeleteDataByModulesID(clsDataAll(0).ModulesID)

                For Each clsItem As VO.ModulesAccess In clsDataAll
                    clsItem.ID = DL.ModulesAccess.GetMaxID
                    DL.ModulesAccess.SaveData(clsItem)
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

