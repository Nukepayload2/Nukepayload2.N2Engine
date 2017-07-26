Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class LineArc
        Inherits HollowCollider

        Public Sub New(radians As Single, sides As Integer, radius As Single, closed As Boolean)
            Me.Radians = radians
            Me.Sides = sides
            Me.Radius = radius
            Me.Closed = closed
        End Sub

        Public Property Radians As Single
        Public Property Sides As Integer
        Public Property Radius As Single
        Public Property Closed As Boolean

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateLineArc(world, Radians, Sides, Radius, Closed, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace