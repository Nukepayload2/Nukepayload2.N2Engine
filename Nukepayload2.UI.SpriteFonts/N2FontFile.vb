Imports System.Text
Imports Newtonsoft.Json
''' <summary>
''' 位图字体文件
''' </summary>
Public Class N2FontFile

    Public Const FontFileMagicId = 1313238644
    ''' <summary>
    ''' 字体文件的标识符
    ''' </summary>
    Public Property MagicId As Integer
    ''' <summary>
    ''' 数据的偏移量（在 Header 之后）
    ''' </summary>
    Public Property DataOffset As Integer
    ''' <summary>
    ''' 用 Json 存储的字体定义
    ''' </summary>
    Public Property Entries As N2SpriteFont

    Private Sub New()

    End Sub
    ''' <summary>
    ''' 载入字体文件信息
    ''' </summary>
    Friend Sub New(br As BinaryReader)
        br.BaseStream.Seek(0, SeekOrigin.Begin)
        MagicId = br.ReadInt32
        If MagicId <> FontFileMagicId Then
            Throw New ArgumentException("输入的流不是N2字体文件流")
        End If
        DataOffset = br.ReadInt32
        If DataOffset >= br.BaseStream.Length Then
            Throw New ArgumentException("输入的N2字体文件流太短")
        End If
        Dim curPos = br.BaseStream.Position
        Dim headerLength = DataOffset - curPos
        Dim utf16 = Encoding.Unicode
        Dim headerData = br.ReadBytes(headerLength)
        Dim head = utf16.GetString(headerData, 0, headerData.Length)
        Entries = JsonConvert.DeserializeObject(Of N2SpriteFont)(head)
    End Sub
    ''' <summary>
    ''' 保存字体的整个头部信息
    ''' </summary>
    Public Shared Sub SaveHeader(bw As BinaryWriter, entries As N2SpriteFont)
        bw.Seek(0, SeekOrigin.Begin)
        bw.Write(FontFileMagicId)
        Dim headerString = JsonConvert.SerializeObject(entries)
        Dim utf16 = Encoding.Unicode
        Dim headerData = utf16.GetBytes(headerString)
        bw.Write(headerData.Length + 8)
        bw.Write(headerData)
    End Sub
End Class