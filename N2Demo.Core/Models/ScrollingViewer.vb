Imports System.ComponentModel
Imports System.Numerics

Public Class ScrollingViewer
    Implements INotifyPropertyChanged

    Public Property Offset As Vector2
    Public Property Zoom As Single

    Dim _BufferSize As Vector2
    Public Property BufferSize As Vector2
        Get
            Return _BufferSize
        End Get
        Set(value As Vector2)
            _BufferSize = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(BufferSize)))
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
