Imports System.Numerics

Namespace Models
    Public Class TilesGridSettings
        Implements INotifyPropertyChanged

        Dim _SpawnPoint As Vector2
        Public Property SpawnPoint As Vector2
            Get
                Return _SpawnPoint
            End Get
            Set(value As Vector2)
                _SpawnPoint = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SpawnPoint)))
            End Set
        End Property

        Dim _TileSize As Vector2
        Public Property TileSize As Vector2
            Get
                Return _TileSize
            End Get
            Set(value As Vector2)
                _TileSize = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TileSize)))
            End Set
        End Property

        Dim _ColumnCount As Integer
        Public Property ColumnCount As Integer
            Get
                Return _ColumnCount
            End Get
            Set(value As Integer)
                _ColumnCount = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ColumnCount)))
            End Set
        End Property

        Dim _RowCount As Integer
        Public Property RowCount As Integer
            Get
                Return _RowCount
            End Get
            Set(value As Integer)
                _RowCount = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(RowCount)))
            End Set
        End Property

        Dim _DefaultCollider As ColliderKinds
        Public Property DefaultCollider As ColliderKinds
            Get
                Return _DefaultCollider
            End Get
            Set(value As ColliderKinds)
                _DefaultCollider = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(DefaultCollider)))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class
End Namespace
