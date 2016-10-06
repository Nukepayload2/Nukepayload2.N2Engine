Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class BezierCurveCustomElementRenderer
    Sub New(view As BezierCurveCustomElement)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim pb As New CanvasPathBuilder(sender)
        pb.BeginFigure(View.StartPoint.Value)
        For t = 0F To 0.99F Step 0.01F
            pb.AddLine(View.GetVertex(t))
        Next
        pb.EndFigure(CanvasFigureLoop.Open)
        args.DrawingSession.DrawGeometry(CanvasGeometry.CreatePath(pb), View.Location.Value, View.Stroke.Value.AsWindowsColor)
    End Sub

End Class
