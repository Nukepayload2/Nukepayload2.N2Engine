Imports FarseerPhysics.Common
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class PrimitiveBreakableBody
        Implements IJointedCollider
        Sub New(vertices As Vertices, density As Single)
            Me.Vertices = vertices
            Me.Density = density
        End Sub

        Public Property Vertices As Vertices
        Public Property Density As Single
        Public Property Rotation As Single
        Public Property Position As Vector2

        Public Function CreateBreakableBody(world As World) As BreakableBody Implements IJointedCollider.CreateBreakableBody
            Return BodyFactory.CreateBreakableBody(world, Vertices, Density, Position, Rotation)
        End Function
    End Class
End Namespace