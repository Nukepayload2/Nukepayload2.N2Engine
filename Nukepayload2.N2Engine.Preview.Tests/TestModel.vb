Imports System.ComponentModel

Public Class TestModel
    Implements INotifyPropertyChanged

    Public Property TestInt32Value As Integer

    Dim _TestInt32ValueNotify As Integer
    Public Property TestInt32ValueNotify As Integer
        Get
            Return _TestInt32ValueNotify
        End Get
        Set(value As Integer)
            _TestInt32ValueNotify = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TestInt32ValueNotify)))
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
