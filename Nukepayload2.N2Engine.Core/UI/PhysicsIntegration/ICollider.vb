Imports FarseerPhysics.Dynamics

Namespace PhysicsIntegration
    Public Interface ICollider
        Function CreateBody(world As World) As Body
    End Interface
End Namespace
