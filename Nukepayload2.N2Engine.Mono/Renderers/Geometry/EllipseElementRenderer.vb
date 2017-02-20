Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class EllipseElementRenderer
    Sub New(view As EllipseElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, EllipseElement)
        Dim size = view.Size.Value
        Dim loc = View.Location.Value
        Dim rect = New Rectangle(loc.X - size.X / 2, loc.Y - size.Y / 2, size.X, size.Y)
        With args.DrawingContext
            Dim fill = view.Fill
            If fill.CanRead Then .DrawFilledEllipse(rect, fill.Value.AsXnaColor)
            Dim stroke = view.Stroke
            If stroke.CanRead Then .DrawEllipse(rect, stroke.Value.AsXnaColor)
        End With
    End Sub
End Class
