Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors
    ''' <summary>
    ''' 让物体在出场瞬间受力，用于模拟某些物体飞扬的效果。
    ''' </summary>
    Public Class ApplyForceBehavior
        Inherits BehaviorBase(Of GameEntity)
        Public ReadOnly Property Force As New PropertyBinder(Of Vector2)
        Protected Overrides Sub OnAttached(visual As GameEntity)
            MyBase.OnAttached(visual)
            Dim body = visual.Body.Value
            body.ApplyLinearImpulse(Force.Value)
        End Sub
    End Class
End Namespace