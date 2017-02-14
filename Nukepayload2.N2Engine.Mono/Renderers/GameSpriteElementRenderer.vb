Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class GameSpriteElementRenderer
    Sub New(view As SpriteElement)
        MyBase.New(view)
    End Sub

    Dim drawOperation As TypedEventHandler(Of Game, MonogameDrawEventArgs)

    Friend Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        MyBase.OnCreateResources(sender, args)
        Dim view = DirectCast(Me.View, SpriteElement)
        Dim bmp = DirectCast(view.Sprite.Value, PlatformBitmapResource)
        If Not view.DefferedLoadLevel.CanRead OrElse view.DefferedLoadLevel.Value <= 0 Then
            bmp.Load()
            drawOperation = AddressOf DrawImage
        Else
            drawOperation = AddressOf DrawColor
            Task.Run(AddressOf bmp.Load).ContinueWith(Sub(th) drawOperation = AddressOf DrawImage)
        End If
        AddHandler view.Sprite.DataChanged,
            Sub(snd, e)
                drawOperation = AddressOf DrawColor
                Task.Run(AddressOf DirectCast(view.Sprite.Value, PlatformBitmapResource).Load).ContinueWith(Sub(th) drawOperation = AddressOf DrawImage)
            End Sub
    End Sub
    Sub DrawImage(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, SpriteElement)
        Dim bmp = DirectCast(view.Sprite.Value, PlatformBitmapResource)
        Dim loc = view.Location.Value
        Dim size = view.Size.Value
        args.DrawingContext.DrawTexture(bmp.Texture, New Rectangle(loc.X, loc.Y, size.X, size.Y), Color.White)
    End Sub
    Sub DrawColor(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, SpriteElement)
        Dim loc = view.Location.Value
        Dim size = View.Size.Value
        args.DrawingContext.DrawFilledRectangle(New Rectangle(loc.X, loc.Y, size.X, size.Y), view.LoadingColor.Value.AsXnaColor)
    End Sub
    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        drawOperation.Invoke(sender, args)
    End Sub
End Class
