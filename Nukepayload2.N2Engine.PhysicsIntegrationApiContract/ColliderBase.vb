Imports FarseerPhysics.Dynamics

Namespace PhysicsIntegration
    ''' <summary>
    ''' 所有碰撞器的基类
    ''' </summary>
    Public MustInherit Class ColliderBase
        Implements ICollider

        ''' <summary>
        ''' 创建物理引擎中的物体
        ''' </summary>
        ''' <param name="world">要创建到哪个世界</param>
        Public MustOverride Function CreateBody(world As World) As Body Implements ICollider.CreateBody
    End Class
End Namespace