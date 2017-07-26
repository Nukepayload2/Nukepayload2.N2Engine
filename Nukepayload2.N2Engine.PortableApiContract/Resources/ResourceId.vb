Namespace Resources
    <Obsolete("使用 System.Uri 替代")>
    Public Class ResourceId
        Implements IEquatable(Of ResourceId)

        Sub New()

        End Sub
        Sub New(value As String)
            Me.Value = value
        End Sub

        Public Property Value As String

        Public Overloads Function Equals(other As ResourceId) As Boolean Implements IEquatable(Of ResourceId).Equals
            Return other.Value = Value
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is ResourceId Then
                Return DirectCast(obj, ResourceId).Value = Value
            End If
            Return False
        End Function
        Public Overrides Function GetHashCode() As Integer
            Return Value.GetHashCode
        End Function
        Public Overrides Function ToString() As String
            Return Value
        End Function

        Public Shared Operator =(value1 As ResourceId, value2 As ResourceId) As Boolean
            Return value1.Value = value2.Value
        End Operator

        Public Shared Operator <>(value1 As ResourceId, value2 As ResourceId) As Boolean
            Return value1.Value <> value2.Value
        End Operator
    End Class
End Namespace