Imports FarseerPhysics.Dynamics

Namespace PhysicsIntegration
    Public Interface IJointedCollider
        Function CreateBreakableBody(world As World) As BreakableBody
    End Interface
End Namespace
