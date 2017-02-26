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
                ' TODO: SpriteBatch 不支持透明度。当支持的时候改写成 SpriteBatch。
                For Each part In partSys.GetParticles
                    If part.ImageEnumStatus.MoveNext Then
                        Dim bmp = CType(part.ImageEnumStatus.Current, PlatformBitmapResource)
                        Dim texture = bmp.Texture
                        If bmp.Bounds.HasValue Then
                            Dim sourceRect = bmp.Bounds.Value
                            drawingSession.DrawImage(texture, part.Location,
                                                     New Rect(sourceRect.Offset.X, sourceRect.Offset.Y,
                                                              sourceRect.Size.X, sourceRect.Size.Y), part.Opacity)
                        Else
                            drawingSession.DrawImage(texture, part.Location,
                                                     New Rect(New Point, texture.Size), part.Opacity)
                        End If
                    End If
                Next
            End Using
            drawingSession.DrawImage(cl)
        End Using
    End Sub
End Class
