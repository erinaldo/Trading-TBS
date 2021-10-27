Namespace BL
    Public Class UserCompany
        Public Shared Function ListData(ByVal intID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.UserCompany.ListData(intID)
        End Function

        Public Shared Function ListDataByUserID(ByVal strUserID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.UserCompany.ListDataByUserID(strUserID)
        End Function

        Public Shared Function SaveData(ByVal strUserID As String, ByVal clsDataAll() As VO.UserCompany) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.UserCompany.DeleteDataByUserID(strUserID)

                For Each clsItem As VO.UserCompany In clsDataAll
                    clsItem.ID = DL.UserCompany.GetMaxID
                    DL.UserCompany.SaveData(clsItem)
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

