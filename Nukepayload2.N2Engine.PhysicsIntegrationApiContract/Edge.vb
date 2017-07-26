
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class Edge
        Inherits ColliderBase

        Public Property Point1 As Vector2
        Public Property Point2 As Vector2

        Sub New(point1 As Vector2, point2 As Vector2)
            Me.Point1 = point1
            Me.Point2 = point2
        End Sub

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateEdge(world, Point1, Point2)
        End Function
    End Class
End Namespace