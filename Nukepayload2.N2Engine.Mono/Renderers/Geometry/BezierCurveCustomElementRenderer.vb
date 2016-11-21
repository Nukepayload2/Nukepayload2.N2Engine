Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements
Imports RaisingStudio.Xna.Graphics

Friend Class BezierCurveCustomElementRenderer
    Sub New(view As BezierCurveCustomElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, BezierCurveCustomElement)
        Dim loc = view.Location.Value.AsXnaVector2
        args.DrawingContext.DrawBezier(loc + view.StartPoint.Value.AsXnaVector2,
                                       loc + view.EndPoint.Value.AsXnaVector2,
                                       view.Stroke.Value.AsXnaColor, Function(t) loc + view.GetVertex(t).AsXnaVector2)
    End Sub
End Class
