Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class RectangleElementRenderer
    Sub New(view As RectangleElement)
        MyBase.New(view)
    End Sub
    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, RectangleElement)
        Dim loc = view.Location.Value
        Dim size = view.Size.Value
        If View.Fill.CanRead Then args.DrawingSession.FillRectangle(loc.X, loc.Y, size.X, size.Y, View.Fill.Value.AsWindowsColor)
        If view.Stroke.CanRead Then args.DrawingSession.DrawRectangle(loc.X, loc.Y, size.X, size.Y, view.Stroke.Value.AsWindowsColor)
    End Sub
End Class
