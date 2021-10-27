Namespace VO
    Public Class ReceiveReturn
        Inherits Common
        Property ID As String
        Property BPID As Integer
        Property BPName As String
        Property ReceiveReturnDate As DateTime
        Property PaymentTerm As Integer
        Property DueDate As DateTime
        Property SubTotal As Decimal
        Property TotalDiscount As Decimal
        Property TotalTax As Decimal
        Property GrandTotal As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
    End Class 
End Namespace

