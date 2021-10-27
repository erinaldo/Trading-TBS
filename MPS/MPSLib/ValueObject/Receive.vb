Namespace VO
    Public Class Receive
        Inherits Common
        Property ID As String
        Property ReferencesID As String
        Property BPID As Integer
        Property BPName As String
        Property ReceiveDate As DateTime
        Property TimeIn As TimeSpan = Today.TimeOfDay
        Property TimeOut As TimeSpan = Today.TimeOfDay
        Property PaymentTerm As Integer
        Property DueDate As DateTime
        Property DriverName As String
        Property PlatNumber As String
        Property PPN As Decimal
        Property PPH As Decimal
        Property ItemID As Integer
        Property ItemCode As String
        Property ItemName As String
        Property UOMID As Integer
        Property ArrivalBrutto As Decimal
        Property ArrivalTarra As Decimal
        Property ArrivalNettoBefore As Decimal
        Property ArrivalDeduction As Decimal
        Property ArrivalNettoAfter As Decimal
        Property MaxBrutto As Decimal
        Property Price1 As Decimal
        Property Price2 As Decimal
        Property TotalPrice1 As Decimal
        Property TotalPrice2 As Decimal
        Property ArrivalReturn As Decimal
        Property TotalPayment As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
    End Class
End Namespace