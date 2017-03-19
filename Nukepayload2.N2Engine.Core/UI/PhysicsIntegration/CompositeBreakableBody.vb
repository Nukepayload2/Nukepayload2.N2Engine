Imports FarseerPhysics.Collision.Shapes
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories


Namespace PhysicsIntegration
    Public Class CompositeBreakableBody
        Implements IJointedCollider

        Public Property Shapes As IEnumerable(Of Shape)
        Public Property Rotation As Single
        Public Property Position As Vector2

        Public Function CreateBreakableBody(world As World) As BreakableBody Implements IJointedCollider.CreateBreakableBody
            Return BodyFactory.CreateBreakableBody(world, Shapes, Position, Rotation)
        End Function
    End Class
End Namespace