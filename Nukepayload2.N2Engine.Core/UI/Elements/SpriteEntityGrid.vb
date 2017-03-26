Imports FarseerPhysics.Dynamics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Models

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
            For Each tile In tiles
                If tile IsNot Nothing Then
                    If tile.Collider IsNot Nothing Then
                        tile.Body = tile.Collider.CreateBody(world)
                        tile.Body.SleepingAllowed = True
                    End If
                End If
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
        Public ReadOnly Property ClampToSourceRect As New PropertyBinder(Of Boolean)
        ''' <summary>
        ''' (Win2D) 表示贴图是不是像素风格。如果是像素风格, 则缩放使用临近算法，否则使用线性插值算法。
        ''' </summary>
        Public ReadOnly Property IsPixelStyled As New PropertyBinder(Of Boolean)
    End Class
End Namespace