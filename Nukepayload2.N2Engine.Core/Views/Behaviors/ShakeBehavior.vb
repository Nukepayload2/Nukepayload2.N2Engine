Public Class ShakeBehavior
    Inherits BehaviorBase(Of GameEntity)

    Public ReadOnly Property ShakeRate As New PropertyBinder(Of Vector2)

    Protected Overrides Sub OnAttached(visual As GameEntity)
        Dim body = visual.Body.Value
        AddHandler visual.Updating, Sub() body.ApplyImpulse(ShakeRate.Value, body.WorldCenter)
    End Sub
End Class