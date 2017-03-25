Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class SolidArc
        Inherits SolidCollider

        Public Sub New(density As Single, radians As Single, sides As Integer, radius As Single)
            MyBase.New(density)
            Me.Sides = sides
            Me.Radius = radius
        End Sub

        Public Property Radians As Single
        Public Property Sides As Integer
        Public Property Radius As Single

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateSolidArc(world, Density, Radians, Sides, Radius, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace