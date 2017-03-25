
Imports FarseerPhysics.Common
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class Polygon
        Inherits SolidCollider

        Public Sub New(density As Single, vertices As Vertices)
            MyBase.New(density)
            Me.Vertices = vertices
        End Sub

        Public Property Vertices As Vertices

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreatePolygon(world, Vertices, Density, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace