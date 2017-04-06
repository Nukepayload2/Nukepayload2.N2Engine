Namespace Models
    Public Class TilesGridSettings
        Implements INotifyPropertyChanged

        Dim _ColumnCount As Integer = 16
        Public Property ColumnCount As Integer
            Get
                Return _ColumnCount
            End Get
            Set(value As Integer)
                _ColumnCount = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ColumnCount)))
            End Set
        End Property

        Dim _RowCount As Integer = 8
        Public Property RowCount As Integer
            Get
                Return _RowCount
            End Get
            Set(value As Integer)
                _RowCount = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(RowCount)))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace
