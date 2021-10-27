Namespace BL
    Public Class PostGL
        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.PostGL.ListData()
        End Function

        Public Shared Function ListDataForUnpost() As DataTable
            BL.Server.ServerDefault()
            Return DL.PostGL.ListDataForUnpost
        End Function

        Public Shared Function PostData(ByVal clsData As VO.PostGL) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If Format(clsData.DateTo, "yyyyMMdd") <= DL.PostGL.LastPostedDate Then
                    Err.Raise(515, "", "Periode posting harus lebih besar dari tanggal terakhir posting")
                End If

                clsData.ID = Format(clsData.DateFrom, "yyyyMMdd") & "-" & Format(clsData.DateTo, "yyyyMMdd")
                If DL.PostGL.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID telah ada sebelumnya")
                End If

                BL.Receive.PostData(clsData.DateFrom, clsData.DateTo)
                BL.Sales.PostData(clsData.DateFrom, clsData.DateTo)
                BL.SalesReturn.PostData(clsData.DateFrom, clsData.DateTo)
                BL.ReceiveReturn.PostData(clsData.DateFrom, clsData.DateTo)
                BL.AccountReceivable.PostData(clsData.DateFrom, clsData.DateTo)
                BL.AccountPayable.PostData(clsData.DateFrom, clsData.DateTo)
                BL.Cost.PostData(clsData.DateFrom, clsData.DateTo)
                BL.Journal.PostData(clsData.DateFrom, clsData.DateTo)

                DL.PostGL.SaveData(clsData)

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

        Public Shared Function UnpostData(ByVal clsData As VO.PostGL) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If Format(clsData.DateTo, "yyyyMMdd") < DL.PostGL.LastPostedDate Then
                    Err.Raise(515, "", "Periode posting harus mulai dari tanggal terakhir posting")
                End If

                BL.ReceiveReturn.UnpostData(clsData.DateFrom, clsData.DateTo)
                BL.SalesReturn.UnpostData(clsData.DateFrom, clsData.DateTo)
                BL.Sales.UnpostData(clsData.DateFrom, clsData.DateTo)
                BL.Receive.UnpostData(clsData.DateFrom, clsData.DateTo)
                BL.AccountReceivable.UnpostData(clsData.DateFrom, clsData.DateTo)
                BL.AccountPayable.UnpostData(clsData.DateFrom, clsData.DateTo)
                BL.Cost.UnpostData(clsData.DateFrom, clsData.DateTo)
                BL.Journal.UnpostData(clsData.DateFrom, clsData.DateTo)

                DL.PostGL.DeleteData(clsData.ID)

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

        Public Shared Function GetDetail(ByVal strID As String) As VO.PostGL
            BL.Server.ServerDefault() 
            Return DL.PostGL.GetDetail(strID)
        End Function 

        Public Shared Function GetDetailLast() As VO.PostGL
            BL.Server.ServerDefault()
            Return DL.PostGL.GetDetailLast
        End Function

        Public Shared Sub DeleteData(ByVal strID As String)
            BL.Server.ServerDefault() 
            Try
                DL.SQL.OpenConnection() 
                DL.SQL.BeginTransaction() 

                DL.PostGL.DeleteData(strID)

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

