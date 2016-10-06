Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class LineElementRenderer
    Sub New(view As LineElement)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim loc = View.Location.Value
        args.DrawingSession.DrawLine(View.StartPoint.Value + loc, View.EndPoint.Value + loc, View.Stroke.Value.AsWindowsColor)
    End Sub
End Class
