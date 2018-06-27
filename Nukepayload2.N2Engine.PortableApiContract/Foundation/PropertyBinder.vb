Namespace Foundation
    ' 升级:
    ' New PropertyBinder\(Of (.+)\)
    ' PropertyBinder(Of $1) = New ManualPropertyBinder(Of $1)
    Public Interface PropertyBinder(Of T)
        Inherits INotifyPropertyChanged
        Property Value As T
        ReadOnly Property CanRead As Boolean
        ReadOnly Property CanWrite As Boolean

        Event DataChanged As EventHandler(Of PropertyBinderDataChangedEventArgs(Of T))
        Function GetValueOrDefault() As T
    End Interface
End Namespace