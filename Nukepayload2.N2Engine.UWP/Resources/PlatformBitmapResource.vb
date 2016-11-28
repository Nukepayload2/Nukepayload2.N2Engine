﻿Imports Microsoft.Graphics.Canvas
Imports Nukepayload2.N2Engine.Resources

Friend Class PlatformBitmapResource
    Public Property Texture As CanvasBitmap

    Public Async Function LoadAsync(device As ICanvasResourceCreator) As Task
        Dim resmgr = ResourceLoader.GetForCurrentView()
        Texture = Await CanvasBitmap.LoadAsync(device, resmgr.GetResourceEmbeddedStream(UriPath).AsRandomAccessStream)
    End Function

    Private Sub ReleaseBitmap()
        Texture?.Dispose()
    End Sub
End Class
