Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews

Friend Class SpriteParticleSystemHelper

    Friend Shared Sub Load(Of TParticle As SpriteParticle, TParticleSystem As {ICommonSpriteParticleSystem, ParticleSystemBase(Of TParticle)})(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs, view As ParticleSystemView(Of TParticleSystem))
        Dim partSys = view.Data.Value
        partSys.RemoveFromGameCanvasCallback = AddressOf view.RemoveFromGameCanvas
        args.TrackAsyncAction((Async Function()
                                   For Each img As PlatformBitmapResource In partSys.ImageList
                                       If Not img.IsLoaded Then
                                           Await img.LoadAsync(sender)
                                       End If
                                   Next
                               End Function)().AsAsyncAction)
    End Sub

    Friend Shared Sub Draw(Of TParticle As SpriteParticle, TParticleSystem As {ICommonSpriteParticleSystem, ParticleSystemBase(Of TParticle)})(drawingSession As CanvasDrawingSession, view As ParticleSystemView(Of TParticleSystem))
        Dim partSys = view.Data.Value
        ' 同时绘制多组对象时需要注意：
        ' 如果要绘制的对象没有共用的形状，
        ' 则必须先绘制到 CanvasCommandList 上面，然后绘制到 args.DrawingSession。
        ' 否则会遇到平面变换 Bug。
        Using cl = New CanvasCommandList(drawingSession)
            Using ds = cl.CreateDrawingSession
                For Each part In partSys.GetParticles
                    If part.ImageEnumStatus.MoveNext Then
                        Dim currentFrame = CType(part.ImageEnumStatus.Current, PlatformBitmapResource).Texture
                        ds.DrawImage(currentFrame, part.Location, currentFrame.Bounds, part.Opacity)
                    End If
                Next
            End Using
            drawingSession.DrawImage(cl)
        End Using
    End Sub
End Class
