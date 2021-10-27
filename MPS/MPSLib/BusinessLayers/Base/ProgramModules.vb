Namespace BL
    Public Class ProgramModules

        Public Shared Function ListDataByProgramID(ByVal intProgramID As VO.Program.Values) As DataTable
            BL.Server.ServerDefault()
            Return DL.ProgramModules.ListDataByProgramID(intProgramID)
        End Function

        Public Shared Function ListDataByModulesID(ByVal intModulesID As VO.Modules.Values) As DataTable
            BL.Server.ServerDefault()
            Return DL.ProgramModules.ListDataByModulesID(intModulesID)
        End Function

        Public Shared Sub SaveDataByProgramID(ByVal clsDataAll() As VO.ProgramModules)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.ProgramModules.DeleteDataByProgramID(clsDataAll(0).ProgramID)

                For Each clsItem As VO.ProgramModules In clsDataAll
                    clsItem.ID = DL.ProgramModules.GetMaxID
                    DL.ProgramModules.SaveData(clsItem)
                Next

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub SaveDataByModulesID(ByVal clsDataAll() As VO.ProgramModules)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.ProgramModules.DeleteDataByModulesID(clsDataAll(0).ModulesID)

                For Each clsItem As VO.ProgramModules In clsDataAll
                    clsItem.ID = DL.ProgramModules.GetMaxID
                    DL.ProgramModules.SaveData(clsItem)
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

