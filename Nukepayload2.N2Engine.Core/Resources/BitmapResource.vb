Imports Newtonsoft.Json
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Platform

Namespace Resources
    ''' <summary>
    ''' 代表平台特定的位图资源。需要加载才可以使用。
    ''' </summary>
    Public MustInherit Class BitmapResource
        Inherits GameResourceBase

        Sub New(uriPath As Uri)
            Me.UriPath = uriPath
        End Sub

        ''' <summary>
        ''' 位图的路径
        ''' </summary>
        Public Property UriPath As Uri

        ''' <summary>
        ''' 这个位图是否已经加载完毕
        ''' </summary>
        <JsonIgnore>
        Public MustOverride ReadOnly Property IsLoaded As Boolean

        ''' <summary>
        ''' 这个位图实际需要显示的边界。默认值是显示整张位图。
        ''' </summary>
        Public Property Bounds As RectangleBounds?

        ''' <summary>
        ''' 为当前位图指定一个新的边框
        ''' </summary>
        ''' <param name="newBounds">要应用的边框</param>
        Public Function SubBitmap(newBounds As RectangleBounds) As BitmapResource
            Dim nBmp = ClonePreserveTexture()
            If nBmp.Bounds.HasValue Then
                Dim oldBounds = nBmp.Bounds.Value
                Dim ofs = oldBounds.Offset + newBounds.Offset
                Dim size = Vector2.Min(newBounds.Size, oldBounds.Size - newBounds.Offset)
                nBmp.Bounds = New RectangleBounds(ofs, size)
            Else
                nBmp.Bounds = newBounds
            End If
            Return nBmp
        End Function

        ''' <summary>
        ''' 按照给定的尺寸分割贴图。不足一个小贴图的部分舍弃。顺序: 代码的阅读顺序。
        ''' </summary>
        ''' <param name="pixelX">分割后的宽度</param>
        ''' <param name="pixelY">分割后的高度</param>
        ''' <returns></returns>
        Public Iterator Function Split(pixelX As Integer, pixelY As Integer) As IEnumerable(Of BitmapResource)
            Dim pxSize = PixelSize
            Dim xCount = pxSize.Width \ pixelX
            Dim yCount = pxSize.Height \ pixelY
            Dim spriteSize As New Vector2(pixelX, pixelY)
            For i = 0 To yCount - 1
                For j = 0 To xCount - 1
                    Yield SubBitmap(New RectangleBounds(New Vector2(j * pixelX, i * pixelY), spriteSize))
                Next
            Next
        End Function
        ''' <summary>
        ''' 派生类重写时，提供复制这个对象但保留纹理引用的功能。
        ''' </summary>
        Protected MustOverride Function ClonePreserveTexture() As BitmapResource
        ''' <summary>
        ''' 派生类重写时，提供图片的原始大小（像素为单位）
        ''' </summary>
        Public MustOverride ReadOnly Property PixelSize As SizeInInteger

        ''' <summary>
        ''' 从 Uri 创建平台特定的位图资源
        ''' </summary>
        Public Shared Function Create(uriPath As Uri) As BitmapResource
            Return PlatformActivator.CreateBaseInstance(Of BitmapResource)(uriPath)
        End Function
    End Class
End Namespace