
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class Rectangle
        Inherits SolidCollider

        Public Sub New(density As Single, size As Vector2)
            MyBase.New(density)
            Me.Size = size
        End Sub

        Public Property Size As Vector2

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateRectangle(world, Size.X, Size.Y, Density, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace