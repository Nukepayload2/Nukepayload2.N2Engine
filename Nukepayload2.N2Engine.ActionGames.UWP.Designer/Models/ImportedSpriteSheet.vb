Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Utilities

Namespace Models

    Public Class ImportedSpriteSheet
        Implements INotifyPropertyChanged

        Protected Sub New(spritePreview As SpritePreview)
            Me.SpritePreview = spritePreview
        End Sub

        Public Shared Async Function CreateAsync(spritePreview As SpritePreview, tileWidth As Integer, tileHeight As Integer) As Task(Of ImportedSpriteSheet)
            Return New ImportedSpriteSheet(spritePreview) With {
                .TileSprites = Await New BitmapSplitter().SplitAsync(spritePreview.File, tileWidth, tileHeight)
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

        Dim _TileSprites As ImageSource(,)
        ''' <summary>
        ''' 图块的贴图
        ''' </summary>
        Public Property TileSprites As ImageSource(,)
            Get
                Return _TileSprites
            End Get
            Set(value As ImageSource(,))
                _TileSprites = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(TileSprites)))
            End Set
        End Property

        Dim _SelectedTileSprite As ImageSource
        Public Property SelectedTileSprite As ImageSource
            Get
                Return _SelectedTileSprite
            End Get
            Set(value As ImageSource)
                _SelectedTileSprite = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(SelectedTileSprite)))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    End Class

End Namespace
