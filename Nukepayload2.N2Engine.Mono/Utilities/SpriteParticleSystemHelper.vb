Imports Microsoft.Xna.Framework
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews
Imports RaisingStudio.Xna.Graphics

Friend Class SpriteParticleSystemHelper

    Friend Shared Sub Load(Of TParticle As SpriteParticle, TParticleSystem As {ICommonSpriteParticleSystem, ParticleSystemBase(Of TParticle)})(
                          sender As Game, args As MonogameCreateResourcesEventArgs, view As ParticleSystemView(Of TParticleSystem))
        Dim partSys = view.Data.Value
        partSys.RemoveFromGameCanvasCallback = AddressOf view.RemoveFromGameCanvas
        For Each img As PlatformBitmapResource In partSys.ImageList
            If Not img.IsLoaded Then
                img.Load()
            End If
        Next
    End Sub

    Friend Shared Sub Draw(Of TParticle As SpriteParticle, TParticleSystem As {ICommonSpriteParticleSystem, ParticleSystemBase(Of TParticle)})(
                          drawingSession As DrawingContext, view As ParticleSystemView(Of TParticleSystem))
        Dim partSys = view.Data.Value
        For Each part In partSys.GetParticles
            If part.ImageEnumStatus.MoveNext Then
                Dim bmp = CType(part.ImageEnumStatus.Current, PlatformBitmapResource)
                Dim loc = part.Location.AsXnaVector2
                If bmp.Bounds.HasValue Then
                    Dim sourceRect = bmp.Bounds.Value
                    drawingSession.DrawTexture(bmp.Texture, loc,
                        New Rectangle(sourceRect.Offset.X, sourceRect.Offset.Y, sourceRect.Size.X, sourceRect.Size.Y), Color.White * part.Opacity)
                Else
                    drawingSession.DrawTexture(bmp.Texture, loc, Color.White * part.Opacity)
                End If
            End If
        Next
    End Sub
End Class
