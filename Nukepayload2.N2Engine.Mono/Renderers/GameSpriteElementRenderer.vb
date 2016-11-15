Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class GameSpriteElementRenderer
    Sub New(view As SpriteElement)
        MyBase.New(view)
    End Sub

    Dim drawOperation As TypedEventHandler(Of Game, MonogameDrawEventArgs)

    Friend Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        MyBase.OnCreateResources(sender, args)
        Dim bmp = DirectCast(View.Sprite.Value, PlatformBitmapResource)
        If View.DefferedLoadLevel.Value <= 0 Then
            bmp.Load()
            drawOperation = AddressOf DrawImage
        Else
            drawOperation = AddressOf DrawColor
            Task.Run(AddressOf bmp.Load).ContinueWith(Sub(th) drawOperation = AddressOf DrawImage)
        End If
        AddHandler View.Sprite.DataChanged,
            Sub(snd, e)
                drawOperation = AddressOf DrawColor
                Task.Run(AddressOf DirectCast(View.Sprite.Value, PlatformBitmapResource).Load).ContinueWith(Sub(th) drawOperation = AddressOf DrawImage)
            End Sub
    End Sub
    Sub DrawImage(sender As Game, args As MonogameDrawEventArgs)
        Dim bmp = DirectCast(View.Sprite.Value, PlatformBitmapResource)
        Dim loc = View.Location.Value
        Dim size = View.Size.Value
        args.DrawingContext.DrawTexture(bmp.Texture, New Rectangle(loc.X, loc.Y, size.X, size.Y), Color.White)
    End Sub
    Sub DrawColor(sender As Game, args As MonogameDrawEventArgs)
        Dim loc = View.Location.Value
        Dim size = View.Size.Value
        args.DrawingContext.DrawFilledRectangle(New Rectangle(loc.X, loc.Y, size.X, size.Y), View.LoadingColor.Value.AsXnaColor)
    End Sub
    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        drawOperation.Invoke(sender, args)
    End Sub
End Class
