Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.UI.Controls

Friend Class TextBlockRenderer

    Friend Overrides Sub OnDraw(sender As Game, args As MonogameDrawEventArgs)
        Dim view = DirectCast(Me.View, TextBlock)
        Dim txt = view.Text
        Dim dc = args.DrawingContext
        If txt.CanRead Then
            Dim fnt = view.Font
            If fnt IsNot Nothing Then
                Dim n2fnt = fnt.SpriteData
                Dim mp = view.Location.Value
                Dim i = mp.X
                Dim j = mp.Y
                For Each ch In txt.Value
                    If ch = vbLf Then
                        j += fnt.FontSize
                        i = mp.X
                    Else
                        Dim charCode = AscW(ch)
                        Dim q1 = charCode And &HFF
                        Dim q2 = charCode >> 8
                        Dim tile = n2fnt.Tiles(q2)
                        Dim glyph = tile.Glyphs(q1)
                        Dim tex = DirectCast(tile.AttachedController, N2FontTileMonoGame).Texture
                        Dim src = New Rectangle(glyph.Left, glyph.Top, glyph.Width, glyph.Height)
                        ' TODO: 在这里实行单个字的位置变换。
                        Dim dest = New Rectangle(i, j, glyph.Width, glyph.Height)
                        ' TODO: 放置其它文字效果
                        dc.DrawTexture(tex, dest, src, Color.White)
                        i += glyph.Width + 1
                    End If
                Next
            End If
        End If
    End Sub
End Class
