
Imports FarseerPhysics.Dynamics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.PhysicsIntegration

Namespace UI.Elements
    ''' <summary>
    ''' 游戏中的实体，具备一些物理属性, 如位置，形状，质量，旋转，连接情况等。
    ''' </summary>
    Public MustInherit Class GameEntity
        Inherits GameElement

        ''' <param name="collider">碰撞器信息。用于创建 Body。</param>
        Sub New(collider As ICollider)
            Me.Collider = collider
        End Sub
        ''' <summary>
        ''' 创建 Body。在开始游戏前必须调用此方法，否则物理引擎将不能正常工作。
        ''' </summary>
        Public Sub CreateBody()
            Dim world = DirectCast(Parent， EntityLayer).World
            _Body = Collider.CreateBody(world)
        End Sub
        ''' <summary>
        ''' 碰撞器信息。用于创建 Body。
        ''' </summary>
        Public ReadOnly Property Collider As ICollider
        ''' <summary>
        ''' 表示视图中物体的物理属性
        ''' </summary>
        Public ReadOnly Property Body As Body
        ''' <summary>
        ''' 视图的位置。这通常是物体的左上角的坐标。此属性默认情况下绑定到 <see cref="Body"/> 上。 
        ''' </summary>
        Public Overrides ReadOnly Property Location As New PropertyBinder(Of Vector2)(
            Function() Body.Position,
            Sub(v)
                Body.Position = v
            End Sub)

    End Class
End Namespace