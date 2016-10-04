Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.Resources

Public Class PlatformBitmapResource
    Public Property Texture As Texture2D

    Public Sub Load(uriPath As Uri)
        Dim resmgr = ResourceLoader.GetForCurrentView()
        Texture = Texture2D.FromStream(GraphicsDeviceManagerExtension.SharedDevice, resmgr.GetResourceEmbeddedStream(uriPath))
    End Sub

    Private Sub ReleaseBitmap()
        Texture?.Dispose()
    End Sub
End Class