Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors
    ''' <summary>
    ''' 导致被附加的元素持续发抖。
    ''' </summary>
    Public Class ShakeBehavior
        Implements IGameBehavior

        Dim _needRecover As Boolean

        WithEvents Target As GameVisual

        Sub New(offset As Vector2)
            Me.Offset = offset
        End Sub

        Public ReadOnly Property Offset As Vector2

        Public Sub Attach(visual As GameVisual) Implements IGameBehavior.Attach
            Target = visual
            visual.AddBehavior(Me)
        End Sub

        Public Sub Remove(visual As GameVisual) Implements IGameBehavior.Remove
            If _needRecover Then
                Update()
            End If
            Target = Nothing
            visual.RemoveBehavior(Me)
        End Sub

        Private Sub Target_Updating(sender As GameVisual, e As EventArgs) Handles Target.Updating
            Update()
        End Sub

        Private Sub Update()
            _Offset *= -1
            _needRecover = Not _needRecover
            Target.Location.Value += _Offset
        End Sub
    End Class
End Namespace
