Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.N2Math
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors
    Public Class ShakeBehavior
        Inherits BehaviorBase(Of GameEntity)

        Public ReadOnly Property ShakeRate As New PropertyBinder(Of Vector2)

        Protected Overrides Sub OnAttached(visual As GameEntity)
            Dim body = visual.Body.Value
            AddHandler visual.Updating, Sub() body.ApplyImpulse(ShakeRate.Value * RandomGenerator.RandomSingle, body.WorldCenter)
        End Sub
    End Class
End Namespace
