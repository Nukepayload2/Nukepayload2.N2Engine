Imports Microsoft.Xna.Framework.Graphics
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Resources

Friend Class PlatformBitmapResource
    Public Property Texture As Texture2D

    Public Sub Load()
        Dim resmgr = ResourceLoader.GetForCurrentView()
        Texture = Texture2D.FromStream(GraphicsDeviceManagerExtension.SharedDevice, resmgr.GetResourceEmbeddedStream(UriPath))
    End Sub

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get
            Return Texture IsNot Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property PixelSize As SizeInInteger
        Get
            Return New SizeInInteger(Texture.Width, Texture.Height)
        End Get
    End Property

    Private Sub ReleaseBitmap()
        Texture?.Dispose()
        Texture = Nothing
    End Sub
End Class