
Imports FarseerPhysics.Common
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories


Namespace PhysicsIntegration
    Public Class ChainShape
        Inherits PrimitiveCollider

        Public Sub New(vertices As Vertices)
            MyBase.New(vertices)
        End Sub

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateChainShape(world, Vertices, Position)
        End Function
    End Class
End Namespace