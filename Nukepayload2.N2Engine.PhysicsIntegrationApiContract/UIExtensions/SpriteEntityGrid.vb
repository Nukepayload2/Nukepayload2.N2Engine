Imports FarseerPhysics.Dynamics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Models
Imports Nukepayload2.N2Engine.PhysicsIntegration

Namespace UI.Elements
    ''' <summary>
    ''' 由相等大小, 不同贴图的具有物理属性的长方形组成的网格。
    ''' </summary>
    Public Class SpriteEntityGrid
        Inherits EntityLayer

        Sub New(world As World, sprites As Uri(), tiles As ITile(,), tileSize As Vector2)
            MyBase.New(world)
            Me.Sprites = sprites
            Me.Tiles = tiles
            Me.TileSize = tileSize
            For i = 0 To tiles.GetLength(0) - 1
                For j = 0 To tiles.GetLength(1) - 1
                    Dim tile = tiles(i, j)
                    If tile IsNot Nothing Then
                        If tile.Collider IsNot Nothing Then
                            tile.Body = tile.Collider.CreateBody(world)
                            tile.Body.Position = New Vector2(j * tileSize.X, i * tileSize.Y).ToPhysicsUnit
                        End If
                    End If
                Next
            Next
        End Sub

        ''' <summary>
        ''' 图块要用到的贴图资源。
        ''' </summary>
        Public ReadOnly Property Sprites As Uri()
        ''' <summary>
        ''' 图块数据。空气为空。
        ''' </summary>
        Public ReadOnly Property Tiles As ITile(,)
        ''' <summary>
        ''' 一个图块的大小。
        ''' </summary>
        Public ReadOnly Property TileSize As Vector2
        ''' <summary>
        ''' (Win2D) 图块的边界吻合处是否应该无缝。
        ''' </summary>
        Public Property ClampToSourceRect As PropertyBinder(Of Boolean) = New ManualPropertyBinder(Of Boolean)
        ''' <summary>
        ''' (Win2D) 表示贴图是不是像素风格。如果是像素风格, 则缩放使用临近算法，否则使用线性插值算法。
        ''' </summary>
        Public Property IsPixelStyled As PropertyBinder(Of Boolean) = New ManualPropertyBinder(Of Boolean)
    End Class
End Namespace