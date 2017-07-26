Imports FarseerPhysics.Common

Namespace PhysicsIntegration
    ''' <summary>
    ''' 基础碰撞器的基类。
    ''' </summary>
    Public MustInherit Class PrimitiveCollider
        Inherits ColliderBase

        Sub New(vertices As Vertices)
            Me.Vertices = vertices
        End Sub

        ''' <summary>
        ''' 顶点
        ''' </summary>
        Public Property Vertices As Vertices
        ''' <summary>
        ''' 位置
        ''' </summary>
        Public Property Position As Vector2

    End Class
End Namespace