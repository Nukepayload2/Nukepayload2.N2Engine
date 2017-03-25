﻿Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class SpriteEntityGridRenderer

    Dim _sprites As PlatformBitmapResource()

    Friend Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        MyBase.OnCreateResources(sender, args)
        Dim view = DirectCast(Me.View, SpriteEntityGrid)
        _sprites = Aggregate s In view.Sprites Select New PlatformBitmapResource(s) Into ToArray
        args.TrackAsyncAction((Async Function() As Task
                                   Await Task.WhenAll(From spr In _sprites Select spr.LoadAsync(sender))
                               End Function)().AsAsyncAction)
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim view = DirectCast(Me.View, SpriteEntityGrid)
        Dim clamp = view.ClampToSourceRect.GetValueOrDefault
        Dim pixelStyled = view.IsPixelStyled.GetValueOrDefault
        Using sb = args.DrawingSession.CreateSpriteBatch(CanvasSpriteSortMode.None,
                                                         If(pixelStyled, CanvasImageInterpolation.NearestNeighbor, CanvasImageInterpolation.Linear),
                                                         If(clamp, CanvasSpriteOptions.ClampToSourceRect,
                                                         CanvasSpriteOptions.None))
            Dim tileSize = view.TileSize
            Dim destOfs As New Vector2 With {
                .X = tileSize.X,
                .Y = tileSize.Y
            }
            Dim srcRect As New Rect With {
                .Width = tileSize.X,
                .Height = tileSize.Y
            }
            For i = 0 To view.Tiles.GetLength(1) - 1
                destOfs.X = 0.0
                For j = 0 To view.Tiles.GetLength(0) - 1
                    Dim tile = view.Tiles(i, j)
                    Dim spriteSheet = _sprites(tile.SpriteSheetIndex)
                    srcRect.X = tile.X * tileSize.X
                    srcRect.Y = tile.Y * tileSize.Y
                    sb.DrawFromSpriteSheet(spriteSheet.Texture, destOfs, srcRect)
                    destOfs.X += tileSize.X
                Next
                destOfs.Y += tileSize.Y
            Next
        End Using
        MyBase.OnDraw(sender, args)
    End Sub
End Class
