
Imports FarseerPhysics.Common
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class LoopShape
        Inherits PrimitiveCollider

        Public Sub New(vertices As Vertices)
            MyBase.New(vertices)
        End Sub

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateLoopShape(world, Vertices, Position)
        End Function
    End Class
End Namespace