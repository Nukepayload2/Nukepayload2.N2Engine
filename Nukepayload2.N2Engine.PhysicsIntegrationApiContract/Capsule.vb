
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories


Namespace PhysicsIntegration
    Public Class Capsule
        Inherits SolidCollider

        Public Sub New(density As Single, height As Single, endRadius As Single)
            MyBase.New(density)
            Me.Height = height
            Me.EndRadius = endRadius
        End Sub

        Public Property Height As Single
        Public Property EndRadius As Single

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateCapsule(world, Height, EndRadius, Density, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace