Imports FarseerPhysics.Dynamics
Imports Nukepayload2.N2Engine.PhysicsIntegration
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Models

    ''' <summary>
    ''' 图块类型
    ''' </summary>
    Public Interface ITile
        ''' <summary>
        ''' 贴图在 <see cref="SpriteEntityGrid.Sprites"/> 的索引，不带透明度。
        ''' </summary>
        Property SpriteSheetIndex As Integer
        ''' <summary>
        ''' 碰撞器, 用于创建 Body。如果不设置这个值，就不会生成 Body。
        ''' </summary>
        ReadOnly Property Collider As ICollider
        ''' <summary>
        ''' 自动生成的 Body。
        ''' </summary>
        Property Body As Body
        ''' <summary>
        ''' 在贴图中的横向索引（从 0 开始）
        ''' </summary>
        Property X As Integer
        ''' <summary>
        ''' 在贴图中的纵向索引（从 0 开始）
        ''' </summary>
        Property Y As Integer
    End Interface

End Namespace