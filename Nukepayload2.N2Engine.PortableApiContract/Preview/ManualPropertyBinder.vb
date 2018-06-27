Namespace Foundation
    ''' <summary>
    ''' 表示手动进行的数据绑定。值需要手动与数据源同步。通常适合代码生成器相关的场合。
    ''' </summary>
    Public Class ManualPropertyBinder(Of T)
        Implements PropertyBinder(Of T)

        Sub New()

        End Sub

        Sub New(initialValue As T)
            Value = initialValue
        End Sub

        Public Function GetValueOrDefault() As T Implements PropertyBinder(Of T).GetValueOrDefault
            Return Value
        End Function

        Dim _Value As T
        Public Property Value As T Implements PropertyBinder(Of T).Value
            Get
                Return _Value
            End Get
            Set(value As T)
                If Not Equals(value, _Value) Then
                    Dim old = _Value
                    _Value = value
                    RaiseEvent DataChanged(Me, New PropertyBinderDataChangedEventArgs(Of T)(old, value))
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(value)))
                End If
            End Set
        End Property

        Public Property CanRead As Boolean = True Implements PropertyBinder(Of T).CanRead

        Public Property CanWrite As Boolean = True Implements PropertyBinder(Of T).CanWrite

        Public Event DataChanged As EventHandler(Of PropertyBinderDataChangedEventArgs(Of T)) Implements PropertyBinder(Of T).DataChanged

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace