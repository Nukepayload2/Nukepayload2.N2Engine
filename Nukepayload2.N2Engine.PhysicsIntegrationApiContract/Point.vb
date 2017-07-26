
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class Point
        Inherits HollowCollider

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateBody(world, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace