Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class PolylineElementRenderer
    Sub New(view As PolylineElement)
        MyBase.New(view)
    End Sub
    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, PolylineElement)
        Dim pb As New CanvasPathBuilder(sender)
        Dim lines = View.Points.Value
        pb.BeginFigure(lines(0))
        For i = 1 To lines.Length - 1
            pb.AddLine(lines(i))
        Next
        pb.EndFigure(If(View.IsClosed.Value, CanvasFigureLoop.Closed, CanvasFigureLoop.Open))
        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), View.Location.Value, View.Stroke.Value.AsWindowsColor)
    End Sub
End Class
