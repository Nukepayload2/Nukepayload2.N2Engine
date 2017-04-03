Imports System.Numerics

Namespace Models
    Public Class TileSettings
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

        Dim _TileCountX As Integer
        Public Property TileCountX As Integer
            Get
                Return _TileCountX
            End Get
            Set(value As Integer)
                _TileCountX = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TileCountX)))
            End Set
        End Property

        Dim _TileCountY As Integer
        Public Property TileCountY As Integer
            Get
                Return _TileCountY
            End Get
            Set(value As Integer)
                _TileCountY = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TileCountY)))
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
