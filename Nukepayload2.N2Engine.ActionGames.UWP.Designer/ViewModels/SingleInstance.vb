Public Class SingleInstance(Of T As SingleInstance(Of T))
    Public Shared ReadOnly Property Current As T
    Sub New()
        If _Current IsNot Nothing Then
            Throw New InvalidOperationException("只能创建一个实例")
        End If
        _Current = Me
    End Sub
End Class
