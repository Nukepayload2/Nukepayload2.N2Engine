Option Strict Off
Public Class SingleInstance(Of T As SingleInstance(Of T))
    Public Shared ReadOnly Property Current As T
    Sub New()
        _Current = Me
    End Sub
End Class