Imports Nukepayload2.N2Engine.Win32

Public Class GameWindow
    Inherits Window

    Sub New(game As MonoGameHandler)
        Me.Game = game
    End Sub

    Public ReadOnly Property Game As MonoGameHandler

    Protected Overrides Sub OnRender(drawingContext As DrawingContext)
        '阻止常规的绘制，以便 MonoGame 绘制
    End Sub

    Private Sub GameWindow_Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub
End Class
