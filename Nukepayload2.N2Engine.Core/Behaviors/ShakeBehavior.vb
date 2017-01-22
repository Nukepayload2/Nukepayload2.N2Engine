Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.N2Math
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors
    Public Class ShakeBehavior
        Inherits BehaviorBase(Of GameEntity)

        Public ReadOnly Property ShakeRate As New PropertyBinder(Of Vector2)

        Protected Overrides Sub OnAttached(visual As GameEntity)
            Dim body = visual.Body.Value
            AddHandler visual.Updating, Sub() body.ApplyLinearImpulse(ShakeRate.Value * RandomGenerator.RandomSingle)
        End Sub
    End Class
End Namespace
