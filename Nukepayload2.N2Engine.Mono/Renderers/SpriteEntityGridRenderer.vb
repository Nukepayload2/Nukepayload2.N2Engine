Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.UI.Elements

Friend Class SpriteEntityGridRenderer

    Dim _sprites As PlatformBitmapResource()

    Friend Overrides Sub OnCreateResources(sender As Game, args As MonogameCreateResourcesEventArgs)
        MyBase.OnCreateResources(sender, args)
        Dim view = DirectCast(Me.View, SpriteEntityGrid)
        _sprites = Aggregate s In view.Sprites Select New PlatformBitmapResource(s) Into ToArray
        For Each spr In _sprites
            spr.Load()
        Next
    End Sub

    Protected Overrides Sub OnBackgroundDraw(sb As SpriteBatch)
        MyBase.OnBackgroundDraw(sb)
        Dim view = DirectCast(Me.View, SpriteEntityGrid)
        Dim tileSize = view.TileSize
        Dim destOfs As New Vector2 With {
            .X = tileSize.X,
            .Y = tileSize.Y
        }
        Dim srcRect As New Rectangle With {
            .Width = tileSize.X,
            .Height = tileSize.Y
        }
        For i = 0 To view.Tiles.GetLength(0) - 1
            destOfs.X = 0.0
            For j = 0 To view.Tiles.GetLength(1) - 1
                Dim tile = view.Tiles(i, j)
                If tile IsNot Nothing Then
                    Dim spriteSheet = _sprites(tile.SpriteSheetIndex)
                    srcRect.Y = tile.X * tileSize.X
                    srcRect.X = tile.Y * tileSize.Y
                    sb.Draw(spriteSheet.Texture, destOfs, srcRect, Color.White)
                End If
                destOfs.X += tileSize.X
            Next
            destOfs.Y += tileSize.Y
        Next
    End Sub
End Class
