Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class RoundedRectangle
        Inherits SolidCollider

        Public Property Radius As Vector2
        Public Property Segments As Integer
        Public Property Height As Single
        Public Property Width As Single

        Public Sub New(density As Single, radius As Vector2, segments As Integer, height As Single, width As Single)
            MyBase.New(density)
            Me.Radius = radius
            Me.Segments = segments
            Me.Height = height
            Me.Width = width
        End Sub

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateRoundedRectangle(world, Width, Height, Radius.X, Radius.Y, Segments, Density, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace