
Imports FarseerPhysics
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
            Location = New RelayPropertyBinder(Of Vector2)(
            Function()
                Dim v = Body.Position
                Return New Vector2(ConvertUnits.ToDisplayUnits(v.X),
                                   ConvertUnits.ToDisplayUnits(v.Y))
            End Function,
            Sub(v)
                Body.Position = New Vector2(ConvertUnits.ToSimUnits(v.X),
                                            ConvertUnits.ToSimUnits(v.Y))
            End Sub)
        End Sub

        ''' <summary>
        ''' 创建 Body。在开始游戏前必须调用此方法，否则物理引擎将不能正常工作。
        ''' </summary>
        Public Sub CreateBody()
            If Collider Is Nothing Then
                Throw New InvalidOperationException("碰撞器为空的情况下不能创建 Body。")
            End If
            If Parent Is Nothing Then
                Throw New InvalidOperationException("物体必须先添加到实体层再创建物理属性。")
            End If
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

    End Class
End Namespace