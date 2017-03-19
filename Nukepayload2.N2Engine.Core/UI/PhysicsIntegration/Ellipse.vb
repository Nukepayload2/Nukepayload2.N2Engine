
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class Ellipse
        Inherits SolidCollider

        Public Sub New(density As Single, radius As Vector2, edges As Integer)
            MyBase.New(density)
            Me.Radius = radius
            Me.Edges = edges
        End Sub

        Public Property Radius As Vector2
        Public Property Edges As Integer

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateEllipse(world, Radius.X, Radius.Y, Edges, Density, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace