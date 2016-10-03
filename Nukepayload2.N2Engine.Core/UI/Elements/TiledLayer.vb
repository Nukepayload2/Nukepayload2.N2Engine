Imports Nukepayload2.N2Engine.Foundation

Namespace UI.Elements
    Public Class TiledLayer(Of TTile As TileElement)
        Inherits GameLayer(Of TileElement)
        ''' <summary>
        ''' 图块数据
        ''' </summary>
        Public Property Tiles As TTile(,)
        ''' <summary>
        ''' 一个图块的大小
        ''' </summary>
        Public ReadOnly Property TileSize As New PropertyBinder(Of Vector2)
        ''' <summary>
        ''' 图块的边界吻合处是否应该无缝
        ''' </summary>
        Public ReadOnly Property SnapsBorder As New PropertyBinder(Of Boolean)
    End Class
End Namespace