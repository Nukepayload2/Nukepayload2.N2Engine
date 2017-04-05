Imports System.Numerics
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models

Namespace ViewModels
    ''' <summary>
    ''' 管理图片资源的导入
    ''' </summary>
    Public Class SpriteSheetsViewModel
        Inherits SingleInstance(Of SpriteSheetsViewModel)
        Implements INotifyPropertyChanged

        Dim _Gravity As New Vector2(0F, 9.8F)
        Public Property Gravity As Vector2
            Get
                Return _Gravity
            End Get
            Set(value As Vector2)
                _Gravity = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Gravity)))
            End Set
        End Property

        Dim _Tools As SpriteSheetEditTool
        Public Property Tools As SpriteSheetEditTool
            Get
                Return _Tools
            End Get
            Set(value As SpriteSheetEditTool)
                _Tools = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Tools)))
            End Set
        End Property

        Dim _TileSize As New Vector2(64, 64)
        Public Property TileSize As Vector2
            Get
                Return _TileSize
            End Get
            Set(value As Vector2)
                _TileSize = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TileSize)))
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