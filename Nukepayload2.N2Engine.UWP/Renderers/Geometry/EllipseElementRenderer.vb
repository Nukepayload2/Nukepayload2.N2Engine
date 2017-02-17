﻿Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class EllipseElementRenderer
    Sub New(view As EllipseElement)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, EllipseElement)
        Dim loc = view.Location.Value
        Dim hsize = view.Size.Value / 2
        Dim center = loc - hsize
        If view.Transform IsNot Nothing Then
            Dim matrix = view.Transform.GetTransformMatrix
            ' TODO: 非位图的矩阵变换
            DrawWithTransform2D(args.DrawingSession,
                     Sub(ds)
                         DrawGeometry(ds, view, hsize, center)
                     End Sub)
        Else
            DrawGeometry(args.DrawingSession, view, hsize, center)
        End If
    End Sub

    Private Shared Sub DrawGeometry(ds As CanvasDrawingSession, view As EllipseElement, hsize As System.Numerics.Vector2, center As System.Numerics.Vector2)
        If view.Fill.CanRead Then
            ds.FillEllipse(center, hsize.X, hsize.Y, view.Fill.Value.AsWindowsColor)
        End If
        If view.Stroke.CanRead Then
            ds.DrawEllipse(center, hsize.X, hsize.Y, view.Stroke.Value.AsWindowsColor)
        End If
    End Sub
End Class