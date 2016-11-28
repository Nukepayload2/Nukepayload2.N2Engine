Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class EllipseElementRenderer
    Sub New(view As EllipseElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, EllipseElement)
        Dim loc = view.Location.Value
        Dim hsize = View.Size.Value / 2
        Dim center = loc - hsize
        If View.Fill.CanRead Then args.DrawingSession.FillEllipse(center, hsize.X, hsize.Y, View.Fill.Value.AsWindowsColor)
        If View.Stroke.CanRead Then args.DrawingSession.DrawEllipse(center, hsize.X, hsize.Y, View.Stroke.Value.AsWindowsColor)
    End Sub
End Class