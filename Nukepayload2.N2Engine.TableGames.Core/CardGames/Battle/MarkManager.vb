Namespace Battle
    Public Class MarkManager
        Implements IMarkManager
        Public Shared ReadOnly Property Current As IMarkManager
        Sub New()
            _Current = Me
        End Sub
        Dim _RegisteredMarks As New List(Of Type)
        Public ReadOnly Property RegisteredMarks As IList(Of Type) Implements IMarkManager.RegisteredMarks
            Get
                Return _RegisteredMarks
            End Get
        End Property
        Public Function CreateMarkControl(Mark As Type) As IMark Implements IMarkManager.CreateMarkControl
            Return CType(Activator.CreateInstance(Mark), IMark)
        End Function
    End Class
End Namespace