Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class RectangleElementRenderer
    Sub New(view As RectangleElement)
        MyBase.New(view)
    End Sub
    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim size = View.Size.Value
        Dim loc = View.Location.Value
        Dim rect = New Rectangle(loc.X, loc.Y, size.X, size.Y)
        With args.DrawingContext
            Dim fill = View.Fill
            If fill.CanRead Then .DrawFilledRectangle(rect, fill.Value.AsXnaColor)
            Dim stroke = View.Stroke
            If stroke.CanRead Then .DrawRectangle(rect, stroke.Value.AsXnaColor)
        End With
    End Sub
End Class
