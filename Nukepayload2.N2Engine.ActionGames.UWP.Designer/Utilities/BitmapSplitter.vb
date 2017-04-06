Imports Microsoft.Graphics.Canvas
Imports Nukepayload2.N2Engine.ActionGames.UWP.Designer.Models
Imports Windows.Storage

Namespace Utilities

    Public Class BitmapSplitter
        ''' <summary>
        ''' 分割一个位图文件为分散的图块。
        ''' </summary>
        ''' <param name="imageFile">源文件</param>
        ''' <param name="tileWidth">图块宽度</param>
        ''' <param name="tileHeight">图块高度</param>
        Public Async Function SplitAsync(imageFile As StorageFile, tileWidth As Integer, tileHeight As Integer) As Task(Of EditableTile(,))
            If imageFile Is Nothing Then
                Throw New ArgumentNullException(NameOf(imageFile))
            End If
            If tileWidth <= 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(tileWidth))
            End If
            If tileHeight <= 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(tileHeight))
            End If
            Dim device = CanvasDevice.GetSharedDevice
            Using strm = Await imageFile.OpenReadAsync
                Using source = Await CanvasBitmap.LoadAsync(device, strm)
                    Dim rowCount = source.SizeInPixels.Height \ tileHeight
                    Dim colCount = source.SizeInPixels.Width \ tileWidth
                    Dim subImages(rowCount - 1, colCount - 1) As EditableTile
                    Dim logicalDpi = DisplayInformation.GetForCurrentView().LogicalDpi
                    For i = 0 To rowCount - 1
                        For j = 0 To colCount - 1
                            Dim bmp As New WriteableBitmap(tileWidth, tileHeight)
                            Dim srcTop = i * tileHeight
                            Dim srcLeft = j * tileWidth
                            Using subImgRt = New CanvasRenderTarget(device, tileWidth, tileHeight, logicalDpi)
                                Using ds = subImgRt.CreateDrawingSession
                                    ds.DrawImage(source, New Rect(0, 0, tileWidth, tileHeight), New Rect(srcTop, srcLeft, tileWidth, tileHeight))
                                End Using
                                Using ms = bmp.PixelBuffer.AsStream
                                    ms.Position = 0
                                    Dim pixels = subImgRt.GetPixelBytes
                                    Await ms.WriteAsync(pixels, 0, pixels.Length)
                                End Using
                            End Using
                            subImages(i, j) = New EditableTile With {
                                .Sprite = bmp, .SpriteIndex = (i, j)
                            }
                        Next
                    Next
                    Return subImages
                End Using
            End Using
        End Function
    End Class

End Namespace