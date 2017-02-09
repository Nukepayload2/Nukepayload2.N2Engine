Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal
Imports Windows.UI.Core

Public Class UWPEventMediator
    Sub New(attachedView As GameCanvas)
        Me.AttachedView = attachedView
        GameWindow = Window.Current.CoreWindow
    End Sub

    Private Sub GameWindow_KeyDown(sender As CoreWindow, args As KeyEventArgs) Handles GameWindow.KeyDown
        If Not args.KeyStatus.WasKeyDown Then
            AttachedView.RaiseKeyDown(MakeGameKeyEventArgs(args))
        End If
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Private Shared Function MakeGameKeyEventArgs(args As KeyEventArgs) As GameKeyboardEventArgs
        Return New GameKeyboardEventArgs(CType(args.VirtualKey, Input.Key), args.KeyStatus.AsN2KeyStatus)
    End Function

    Private Sub GameWindow_KeyUp(sender As CoreWindow, args As KeyEventArgs) Handles GameWindow.KeyUp
        AttachedView.RaiseKeyUp(MakeGameKeyEventArgs(args))
    End Sub

    Public Property AttachedView As GameCanvas

    WithEvents GameWindow As CoreWindow

End Class
