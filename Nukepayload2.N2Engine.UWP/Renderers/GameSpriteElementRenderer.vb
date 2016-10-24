Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class GameSpriteElementRenderer
    Sub New(view As SpriteElement)
        MyBase.New(view)
    End Sub
    Dim drawOperation As TypedEventHandler(Of ICanvasAnimatedControl, CanvasAnimatedDrawEventArgs)
    Protected Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        MyBase.OnCreateResources(sender, args)
        Dim bmp = DirectCast(View.Sprite.Value, PlatformBitmapResource)
        If View.DefferedLoadLevel.Value <= 0 Then
            args.TrackAsyncAction(bmp.LoadAsync().AsAsyncAction)
            drawOperation = AddressOf DrawImage
        Else
            drawOperation = AddressOf DrawColor
            bmp.LoadAsync.ContinueWith(Sub(th) drawOperation = AddressOf DrawImage)
        End If
        AddHandler View.Sprite.DataChanged,
            Sub(snd, e)
                drawOperation = AddressOf DrawColor
                DirectCast(View.Sprite.Value, PlatformBitmapResource).LoadAsync.ContinueWith(Sub(th) drawOperation = AddressOf DrawImage)
            End Sub
    End Sub
    Sub DrawImage(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim bmp = DirectCast(View.Sprite.Value, PlatformBitmapResource)
        Dim loc = View.Location.Value
        Dim size = View.Size.Value
        args.DrawingSession.DrawImage(bmp.Texture, New Rect(loc.X, loc.Y, size.X, size.Y))
    End Sub
    Sub DrawColor(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim loc = View.Location.Value
        Dim size = View.Size.Value
        args.DrawingSession.FillRectangle(New Rect(loc.X, loc.Y, size.X, size.Y), View.LoadingColor.Value.AsWindowsColor())
    End Sub
    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        drawOperation.Invoke(sender, args)
    End Sub
End Class