Namespace SharedLib
    Public Class StringUtility
        Public Shared Function RemoveWhiteSpace(ByVal strFullString As String) As String
            Return New String(strFullString.Where(Function(e) Not Char.IsWhiteSpace(e)).ToArray())
        End Function
    End Class
End Namespace
