Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class CustomCapsule
        Inherits SolidCollider

        Public Sub New(density As Single, height As Single, bottomEdges As Integer, bottomRadius As Single, topEdges As Integer, topRadius As Single)
            MyBase.New(density)
            Me.Height = height
            Me.BottomEdges = bottomEdges
            Me.BottomRadius = bottomRadius
            Me.TopEdges = topEdges
            Me.TopRadius = topRadius
        End Sub

        Public Property Height As Single
        Public Property BottomEdges As Integer
        Public Property BottomRadius As Single
        Public Property TopEdges As Integer
        Public Property TopRadius As Single

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateCapsule(world, Height, TopRadius, TopEdges, BottomRadius, BottomEdges, Density, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace