Namespace PhysicsIntegration
    ''' <summary>
    ''' 实心碰撞器形状的基类。
    ''' </summary>
    Public MustInherit Class SolidCollider
        Inherits HollowCollider

        Sub New(density As Single)
            Me.Density = density
        End Sub
        ''' <summary>
        ''' 密度
        ''' </summary>
        Public Property Density As Single

    End Class
End Namespace