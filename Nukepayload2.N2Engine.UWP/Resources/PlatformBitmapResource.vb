Imports Microsoft.Graphics.Canvas
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Resources

Friend Class PlatformBitmapResource
    Public Property Texture As CanvasBitmap

    Public Async Function LoadAsync(device As ICanvasResourceCreator) As Task
        Dim resmgr = ResourceLoader.GetForCurrentView()
        Texture = Await CanvasBitmap.LoadAsync(device, resmgr.GetResourceEmbeddedStream(UriPath).AsRandomAccessStream)
    End Function

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get
            Return Texture IsNot Nothing
        End Get
    End Property

    Public Overrides ReadOnly Property PixelSize As SizeInInteger
        Get
            Dim bmpSize = Texture.SizeInPixels
            Return New SizeInInteger(bmpSize.Width, bmpSize.Height)
        End Get
    End Property

    Private Sub ReleaseBitmap()
        Texture?.Dispose()
        Texture = Nothing
    End Sub
End Class
