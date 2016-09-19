Namespace Foundation

    Public Class PropertyBinderDataChangedEventArgs(Of T)
        Inherits EventArgs

        Sub New(oldValue As T, newValue As T)
            Me.OldValue = oldValue
            Me.NewValue = newValue
        End Sub

        Public Property OldValue As T
        Public Property NewValue As T
    End Class

End Namespace