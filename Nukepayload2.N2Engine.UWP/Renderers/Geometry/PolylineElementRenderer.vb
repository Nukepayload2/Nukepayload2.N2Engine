Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Numerics
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class PolylineElementRenderer
    Sub New(view As PolylineElement)
        MyBase.New(view)
    End Sub
    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, PolylineElement)
        Dim pb As New CanvasPathBuilder(sender)
        Dim lines = view.Points.Value
        If view.Transform IsNot Nothing Then
            Dim matrix = view.Transform.GetTransformMatrix
            pb.BeginFigure(lines(0).ApplyTransform(matrix))
            For i = 1 To lines.Length - 1
                pb.AddLine(lines(i).ApplyTransform(matrix))
            Next
        Else
            pb.BeginFigure(lines(0))
            For i = 1 To lines.Length - 1
                pb.AddLine(lines(i))
            Next
        End If
        pb.EndFigure(If(view.IsClosed.Value, CanvasFigureLoop.Closed, CanvasFigureLoop.Open))
        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), view.Location.Value, view.Stroke.Value.AsWindowsColor)
    End Sub
End Class
