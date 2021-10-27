Namespace BL
    Public Class ChartOfAccountAssign

        Public Shared Function ListData(ByVal intCOAID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.ChartOfAccountAssign.ListData(intCOAID)
        End Function

        Public Shared Function SaveData(ByVal intCOAID As Integer, ByVal clsDataAll() As VO.ChartOfAccountAssign) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.ChartOfAccountAssign.DeleteData(intCOAID)

                For Each clsItem As VO.ChartOfAccountAssign In clsDataAll
                    clsItem.ID = DL.ChartOfAccountAssign.GetMaxID
                    DL.ChartOfAccountAssign.SaveData(clsItem)
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

    End Class

End Namespace

