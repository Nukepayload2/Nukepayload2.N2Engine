Imports Nukepayload2.N2Engine.UI
Imports Nukepayload2.N2Engine.UI.Elements

Public Class MonoGameEventMediator
    Implements IEventMediator

    Sub New(attachedView As GameCanvas)
        Me.AttachedView = attachedView
        Dim keyboardMediator As New MonoGameKeyboardEventMediator(attachedView)
        Dim mouseMediator As New MonoGameMouseEventMediator(keyboardMediator)
        Dim touchMediator As New MonoGameTouchEventMediator(attachedView)
        Mediators.AddRange({keyboardMediator, mouseMediator, touchMediator})
    End Sub

    Public Property AttachedView As GameCanvas Implements IEventMediator.AttachedView

    Public ReadOnly Property Mediators As New List(Of IEventMediator)

    Public Sub TryRaiseEvent() Implements IEventMediator.TryRaiseEvent
        For Each med In Mediators
            med.TryRaiseEvent()
        Next
    End Sub

End Class
