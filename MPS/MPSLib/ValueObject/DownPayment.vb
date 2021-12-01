Namespace VO
    Public Class DownPayment
        Inherits Common
        Property ID As String
        Property BPID As Integer
        Property BPName As String
        Property DPType As Integer
        Property DPDate As DateTime
        Property PaymentReferencesID As Integer
        Property PaymentReferencesName As String
        Property ReferencesNote As String
        Property TotalAmount As Decimal
        Property TotalUsage As Decimal
        Property CoAIDOfActiva As Integer
        Property CoACodeOfActiva As String
        Property CoANameOfActiva As String
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer

        Enum Type
            All = 0
            Purchase = 1
            Sales = 2
        End Enum
    End Class
End Namespace

