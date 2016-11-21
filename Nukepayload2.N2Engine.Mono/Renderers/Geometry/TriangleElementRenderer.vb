Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class TriangleElementRenderer
    Sub New(view As TriangleElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, TriangleElement)
        Dim loc = view.Location.Value.AsXnaVector2
        Dim v1 = View.Point1.Value.AsXnaVector2 + loc
        Dim v2 = View.Point2.Value.AsXnaVector2 + loc
        Dim v3 = View.Point3.Value.AsXnaVector2 + loc
        With args.DrawingContext
            Dim fill = view.Fill
            If fill.CanRead Then .DrawFilledTriangle(v1, v2, v3, fill.Value.AsXnaColor)
            Dim stroke = view.Stroke
            If stroke.CanRead Then .DrawTriangle(v1, v2, v3, stroke.Value.AsXnaColor)
        End With
    End Sub
End Class
