Imports Microsoft.Graphics.Canvas
Imports Nukepayload2.N2Engine.Resources

Public Class PlatformBitmapResource
    Public Property Texture As CanvasBitmap

    Private Async Function LoadAsync(uriPath As Uri) As Task
        Dim resmgr = ResourceLoader.GetForCurrentView()
        Texture = Await CanvasBitmap.LoadAsync(CanvasDevice.GetSharedDevice, resmgr.GetResourceEmbeddedStream(uriPath).AsRandomAccessStream)
    End Function

    Private Sub ReleaseBitmap()
        Texture?.Dispose()
    End Sub
End Class
