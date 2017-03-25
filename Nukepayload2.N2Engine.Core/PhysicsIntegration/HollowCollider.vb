
Imports FarseerPhysics.Dynamics

Namespace PhysicsIntegration
    ''' <summary>
    ''' 空心的碰撞器
    ''' </summary>
    Public MustInherit Class HollowCollider
        Inherits ColliderBase
        ''' <summary>
        ''' 位置
        ''' </summary>
        Public Property Position As Vector2
        ''' <summary>
        ''' 旋转
        ''' </summary>
        Public Property Rotation As Single
        ''' <summary>
        ''' 物体类型
        ''' </summary>
        Public Property BodyType As BodyType
    End Class
End Namespace