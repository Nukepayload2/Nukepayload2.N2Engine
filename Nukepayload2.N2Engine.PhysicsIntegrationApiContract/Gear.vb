
Imports FarseerPhysics.Dynamics
Imports FarseerPhysics.Factories

Namespace PhysicsIntegration

    Public Class Gear
        Inherits SolidCollider

        Public Sub New(density As Single, radius As Single, numberOfTeeth As Integer, tipPercentage As Single, toothHeight As Single)
            MyBase.New(density)
            Me.NumberOfTeeth = numberOfTeeth
            Me.TipPercentage = tipPercentage
            Me.ToothHeight = toothHeight
        End Sub

        Public Property Radius As Single
        Public Property NumberOfTeeth As Integer
        Public Property TipPercentage As Single
        Public Property ToothHeight As Single

        Public Overrides Function CreateBody(world As World) As Body
            Return BodyFactory.CreateGear(world, Radius, NumberOfTeeth, TipPercentage, ToothHeight, Density, Position, Rotation, BodyType)
        End Function
    End Class
End Namespace