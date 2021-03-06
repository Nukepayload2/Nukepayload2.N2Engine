﻿Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class LineElementRenderer
    Sub New(view As LineElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, LineElement)
        Dim loc = view.Location.Value
        Dim start = view.StartPoint.Value + loc
        Dim [end] = view.EndPoint.Value + loc
        args.DrawingSession.DrawLine(start, [end], view.Stroke.Value.AsWindowsColor)
    End Sub
End Class
