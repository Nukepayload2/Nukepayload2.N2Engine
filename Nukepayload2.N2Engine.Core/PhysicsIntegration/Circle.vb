
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories


Namespace PhysicsIntegration
    Public Class Circle
        Inherits SolidCollider

        Public Sub New(density As Single, radius As Single)
            MyBase.New(density)
            Me.Radius = radius
        End Sub

        Public Property Radius As Single

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateCircle(world, Radius, Density, Position, BodyType)
        End Function
    End Class

End Namespace