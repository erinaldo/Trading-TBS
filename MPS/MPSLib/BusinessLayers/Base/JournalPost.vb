Namespace BL
    Public Class JournalPost

        Public Shared Function SaveData(ByVal clsData As VO.JournalPost) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.JournalPost.SaveData(Not DL.JournalPost.DataExists(), clsData)

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

        Public Shared Function GetDetail(ByVal intProgramID As Integer) As VO.JournalPost
            BL.Server.ServerDefault()
            Return DL.JournalPost.GetDetail(intProgramID)
        End Function

    End Class

End Namespace

