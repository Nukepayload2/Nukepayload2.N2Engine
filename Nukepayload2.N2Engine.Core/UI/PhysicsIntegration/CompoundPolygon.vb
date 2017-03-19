
Imports FarseerPhysics.Common
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class CompoundPolygon
        Inherits SolidCollider

        Public Sub New(density As Single, vertices As List(Of Vertices))
            MyBase.New(density)
            Me.Vertices = vertices
        End Sub

        Public Property Vertices As List(Of Vertices)

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateCompoundPolygon(world, Vertices, Density, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace