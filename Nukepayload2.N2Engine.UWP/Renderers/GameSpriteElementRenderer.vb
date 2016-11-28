Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class GameSpriteElementRenderer
    Sub New(view As SpriteElement)
        MyBase.New(view)
    End Sub
    Dim drawOperation As TypedEventHandler(Of ICanvasAnimatedControl, CanvasAnimatedDrawEventArgs)
    Friend Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        MyBase.OnCreateResources(sender, args)
        Dim view = DirectCast(Me.View, SpriteElement)
        Dim bmp = DirectCast(view.Sprite.Value, PlatformBitmapResource)
        If view.DefferedLoadLevel.Value <= 0 Then
            args.TrackAsyncAction(bmp.LoadAsync(sender).AsAsyncAction)
            drawOperation = AddressOf DrawImage
        Else
            drawOperation = AddressOf DrawColor
            bmp.LoadAsync(sender).ContinueWith(Sub(th) drawOperation = AddressOf DrawImage)
        End If
        AddHandler view.Sprite.DataChanged,
            Sub(snd, e)
                drawOperation = AddressOf DrawColor
                DirectCast(view.Sprite.Value, PlatformBitmapResource).LoadAsync(sender).ContinueWith(Sub(th) drawOperation = AddressOf DrawImage)
            End Sub
    End Sub
    Sub DrawImage(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, SpriteElement)
        Dim bmp = DirectCast(view.Sprite.Value, PlatformBitmapResource)
        Dim loc = view.Location.Value
        Dim size = view.Size.Value
        args.DrawingSession.DrawImage(bmp.Texture, New Rect(loc.X, loc.Y, size.X, size.Y))
    End Sub
    Sub DrawColor(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, SpriteElement)
        Dim loc = view.Location.Value
        Dim size = view.Size.Value
        args.DrawingSession.FillRectangle(New Rect(loc.X, loc.Y, size.X, size.Y), view.LoadingColor.Value.AsWindowsColor())
    End Sub
    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        drawOperation.Invoke(sender, args)
    End Sub
End Class