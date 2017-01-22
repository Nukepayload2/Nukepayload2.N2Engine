Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.Resources

Friend Class PlatformBitmapResource
    Public Property Texture As Texture2D

    Public Sub Load()
        Dim resmgr = ResourceLoader.GetForCurrentView()
        Texture = Texture2D.FromStream(GraphicsDeviceManagerExtension.SharedDevice, resmgr.GetResourceEmbeddedStream(UriPath))
    End Sub

    Private Sub ReleaseBitmap()
        Texture?.Dispose()
    End Sub
End Class