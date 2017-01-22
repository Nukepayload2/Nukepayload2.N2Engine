Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class TriangleElementRenderer
    Sub New(view As TriangleElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, TriangleElement)
        Dim pb As New CanvasPathBuilder(sender)
        pb.BeginFigure(View.Point1.Value)
        pb.AddLine(View.Point2.Value)
        pb.AddLine(View.Point3.Value)
        pb.EndFigure(CanvasFigureLoop.Closed)
        If View.Fill.CanRead Then args.DrawingSession.FillGeometry(CanvasGeometry.CreatePath(pb), View.Location.Value, View.Fill.Value.AsWindowsColor)
        If View.Stroke.CanRead Then args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), View.Location.Value, View.Stroke.Value.AsWindowsColor)
    End Sub
End Class
