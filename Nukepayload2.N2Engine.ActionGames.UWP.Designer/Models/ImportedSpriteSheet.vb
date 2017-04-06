Imports System.Numerics
Imports Nukepayload2.Collections.Specialized
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Utilities

Namespace Models

    Public Class ImportedSpriteSheet
        Implements INotifyPropertyChanged

        Protected Sub New(spritePreview As SpritePreview)
            Me.SpritePreview = spritePreview
        End Sub

        Public Shared Async Function CreateAsync(spritePreview As SpritePreview, tileWidth As Integer, tileHeight As Integer) As Task(Of ImportedSpriteSheet)
            Return New ImportedSpriteSheet(spritePreview) With {
                .TileSprites = New ObservableFixedArray2D(Of EditableTile)(Await New BitmapSplitter().SplitAsync(spritePreview.File, tileWidth, tileHeight))
            }
        End Function
        ''' <summary>
        ''' 原始的贴图
        ''' </summary>
        Public Property SpritePreview As SpritePreview

        Dim _ReferenceCount As Integer
        ''' <summary>
        ''' 此贴图的引用计数
        ''' </summary>
        Public Property ReferenceCount As Integer
            Get
                Return _ReferenceCount
            End Get
            Set(value As Integer)
                _ReferenceCount = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ReferenceCount)))
            End Set
        End Property

        Dim _TileSprites As ObservableFixedArray2D(Of EditableTile)
        ''' <summary>
        ''' 图块的贴图
        ''' </summary>
        Public Property TileSprites As ObservableFixedArray2D(Of EditableTile)
            Get
                Return _TileSprites
            End Get
            Set(value As ObservableFixedArray2D(Of EditableTile))
                _TileSprites = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TileSprites)))
            End Set
        End Property

        Dim _SelectedTileX As Integer = -1
        Public Property SelectedTileX As Integer
            Get
                Return _SelectedTileX
            End Get
            Set(value As Integer)
                _SelectedTileX = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedTileX)))
            End Set
        End Property

        Dim _SelectedTileY As Integer = -1
        Public Property SelectedTileY As Integer
            Get
                Return _SelectedTileY
            End Get
            Set(value As Integer)
                _SelectedTileY = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedTileY)))
            End Set
        End Property

        Public ReadOnly Property SelectedTileSprite As EditableTile
            Get
                If TileSprites.InRange(SelectedTileX, SelectedTileY) Then
                    Return TileSprites(SelectedTileX, SelectedTileY)
                End If
                Return Nothing
            End Get
        End Property

        Public ReadOnly Property TileSize As Vector2
            Get
                Return New Vector2(SpritePreview.TileWidth, SpritePreview.TileHeight)
            End Get
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class

End Namespace
