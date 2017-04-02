Imports Microsoft.Graphics.Canvas
Imports Windows.Storage
Imports Windows.UI

Namespace Utilities

    Public Class ThumbnailGenerator
        Public Property ThumbnailSize As Integer
        Sub New()
            MyClass.New(300)
        End Sub
        Sub New(thumbnailSize As Integer)
            Me.ThumbnailSize = thumbnailSize
        End Sub
        Public Async Function GenerateThumbnailAsync(imageFile As StorageFile) As Task(Of WriteableBitmap)
            Dim device = CanvasDevice.GetSharedDevice
            Dim thumbnail As New WriteableBitmap(ThumbnailSize, ThumbnailSize)
            Using thumbnailRt = New CanvasRenderTarget(device, ThumbnailSize, ThumbnailSize, DisplayInformation.GetForCurrentView().LogicalDpi)
                Using strm = Await imageFile.OpenReadAsync
                    Using original = Await CanvasBitmap.LoadAsync(device, strm)
                        Using ds = thumbnailRt.CreateDrawingSession
                            Dim pxSize = original.SizeInPixels
                            Dim scale = Math.Max(pxSize.Width / ThumbnailSize, pxSize.Height / ThumbnailSize)
                            If scale > 1 Then
                                pxSize.Height /= scale
                                pxSize.Width /= scale
                            End If
                            ds.Clear(Colors.Transparent)
                            ds.DrawImage(original, New Rect((ThumbnailSize - pxSize.Width) / 2, (ThumbnailSize - pxSize.Height) / 2, pxSize.Width, pxSize.Height))
                        End Using
                        Using ms = thumbnail.PixelBuffer.AsStream
                            ms.Position = 0
                            Dim pixels = thumbnailRt.GetPixelBytes
                            Await ms.WriteAsync(pixels, 0, pixels.Length)
                        End Using
                    End Using
                End Using
            End Using
            Return thumbnail
        End Function
    End Class

End Namespace