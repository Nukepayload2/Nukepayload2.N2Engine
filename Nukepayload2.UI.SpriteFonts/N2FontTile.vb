Imports Newtonsoft.Json
''' <summary>
''' 字体图块，包含一个缓存的纹理。一个图块包含 256 个字体轮廓。平台相关实现应当允许 <see cref="GC"/> 可以随时回收纹理。
''' </summary>
Public Class N2FontTile

    ''' <summary>
    ''' 字体的形状信息。应当有256个。
    ''' </summary>
    Public Property Glyphs As N2FontGlyph()
    ''' <summary>
    ''' 字体纹理在字体文件数据部分中的偏移量 (字节)
    ''' </summary>
    Public Property PngOffset As Integer
    ''' <summary>
    ''' 字体纹理的大小 (字节)
    ''' </summary>
    Public Property PngLength As Integer
    ''' <summary>
    ''' 与这个图块对应的字体
    ''' </summary>
    <JsonIgnore>
    Public Property Parent As N2SpriteFont
    ''' <summary>
    ''' 为了正确实现处置方式而存在的
    ''' </summary>
    <JsonIgnore>
    Friend Property AttachedController As IDisposable
End Class
''' <summary>
''' 用于控制字体图块逻辑
''' </summary>
''' <typeparam name="TTexture">平台相关的纹理类型。例如 Texture2D，CanvasBitmap。</typeparam>
Public MustInherit Class N2FontTilePlatformControllerBase(Of TTexture As {Class, IDisposable})
    Implements IDisposable

    Public Sub New(data As N2FontTile)
        _data = data
    End Sub

    Protected _data As N2FontTile
    Dim _textureCache As WeakReference(Of TTexture)
    ''' <summary>
    ''' 从字体纹理缓存中获取字体纹理，或者加载纹理
    ''' </summary>
    Public ReadOnly Property Texture As TTexture
        Get
            Dim tex As TTexture = Nothing
            If _textureCache IsNot Nothing Then
                If Not _textureCache.TryGetTarget(tex) Then
                    tex = LoadTexture()
                    _textureCache.SetTarget(tex)
                End If
            Else
                tex = LoadTexture()
                _textureCache = New WeakReference(Of TTexture)(tex)
            End If
            Return tex
        End Get
    End Property

    Protected MustOverride Function LoadTexture() As TTexture

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                Dim tex As TTexture = Nothing
                If (_textureCache?.TryGetTarget(tex)).GetValueOrDefault Then
                    tex.Dispose()
                End If
                _data.AttachedController = Nothing
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class