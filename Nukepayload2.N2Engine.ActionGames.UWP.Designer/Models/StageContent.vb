Imports System.Numerics
Imports Nukepayload2.Collections.Specialized
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Commands
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.ViewModels

Namespace Models
    Public Class StageModel
        ''' <summary>
        ''' 一个关卡内的所有图块
        ''' </summary>
        Public ReadOnly Property Tiles As New ObservableFixedArray2D(Of EditableTile)
        ''' <summary>
        ''' 被编辑器选中的图块的 X 坐标
        ''' </summary>
        Public Property SelectedTileX As Integer
        ''' <summary>
        ''' 被编辑器选中的图块的 Y 坐标
        ''' </summary>
        Public Property SelectedTileY As Integer
        ''' <summary>
        ''' 获取被选中的图块
        ''' </summary>
        Public ReadOnly Property SelectedTile As EditableTile
            Get
                If Tiles.InRange(SelectedTileX, SelectedTileY) Then
                    Return Tiles(SelectedTileX, SelectedTileY)
                End If
                Return Nothing
            End Get
        End Property
        ''' <summary>
        ''' 从其它vm读取图块大小
        ''' </summary>
        Public ReadOnly Property TileSize As Vector2
            Get
                Dim curSprite = SpritesViewModel.Current.SelectedSprite
                If curSprite Is Nothing Then
                    Return New Vector2(64, 64)
                End If
                Return New Vector2(curSprite.TileWidth, curSprite.TileHeight)
            End Get
        End Property
        ''' <summary>
        ''' 删除选定的图块
        ''' </summary>
        Public ReadOnly Property DeleteSelectedTile As New DeleteSelectedTileCommand
        ''' <summary>
        ''' 向选中的图块绘制
        ''' </summary>
        Public ReadOnly Property DrawDotCommand As New DrawDotCommand
    End Class
End Namespace
