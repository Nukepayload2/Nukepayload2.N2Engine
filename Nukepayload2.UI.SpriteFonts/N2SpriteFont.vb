''' <summary>
''' N2引擎定义的位图字体
''' </summary>
Public Class N2SpriteFont
    Implements IDisposable

    ''' <summary>
    ''' 位图字体中的图块。一共有256个。
    ''' </summary>
    Public Property Tiles As N2FontTile()
    ''' <summary>
    ''' 描述这个字体的基本信息
    ''' </summary>
    Public Property Description As String

    Dim _dataOffset As Integer
    Dim _loader As Func(Of Stream)
    ''' <summary>
    ''' 打开字体文件, 准备好加载图块。
    ''' </summary>
    ''' <param name="fontLoader"></param>
    Public Sub Load(Of TTexture As {Class, IDisposable})(fontLoader As Func(Of Stream),
                        createTileController As Func(Of N2FontTile, N2FontTilePlatformControllerBase(Of TTexture)))
        _loader = fontLoader
        Using br As New BinaryReader(fontLoader.Invoke)
            Dim fnt = New N2FontFile(br)
            _dataOffset = fnt.DataOffset
            Tiles = fnt.Entries.Tiles
            For Each tile In Tiles
                tile.Parent = Me
                tile.AttachedController = createTileController(tile)
            Next
        End Using
    End Sub
    ''' <summary>
    ''' 获取图块的数据流
    ''' </summary>
    ''' <param name="offset">在字体文件中的偏移量</param>
    ''' <param name="length">在字体文件中的长度</param>
    Public Function GetTileData(offset As Integer, length As Integer) As Stream
        Dim strm = _loader.Invoke
        Using br As New BinaryReader(strm)
            strm.Position = _dataOffset + offset
            Return New MemoryStream(br.ReadBytes(length))
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                If Tiles IsNot Nothing Then
                    For Each t In Tiles
                        t.AttachedController?.Dispose()
                    Next
                End If
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class