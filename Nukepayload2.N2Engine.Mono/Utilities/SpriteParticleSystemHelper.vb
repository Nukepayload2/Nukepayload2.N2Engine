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
                Dim currentFrame = CType(part.ImageEnumStatus.Current, PlatformBitmapResource).Texture
                drawingSession.DrawTexture(currentFrame, part.Location.AsXnaVector2, Color.White * part.Opacity)
            End If
        Next
    End Sub
End Class
