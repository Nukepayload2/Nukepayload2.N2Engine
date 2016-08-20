''' <summary>
''' 对于不支持伴随语法的语言, 提供简化的初始化存档文件的方式。
''' </summary>
Public Module SaveFileHelper
    <Extension>
    Public Function Indelible(Of T As SaveFile)(file As T) As T
        file.IsIndelible = True
        Return file
    End Function
    <Extension>
    Public Function Roaming(Of T As SaveFile)(file As T) As T
        file.IsRoaming = True
        Return file
    End Function
    <Extension>
    Public Function Master(Of T As SaveFile)(file As T) As T
        file.IsMaster = True
        Return file
    End Function
    <Extension>
    Public Function HasBaseName(Of T As SaveFile)(file As T, baseName As String) As T
        file.BaseName = baseName
        Return file
    End Function
    <Extension>
    Public Function HasSaveId(Of T As SaveFile)(file As T, saveId As Integer) As T
        file.SaveId = saveId
        Return file
    End Function

End Module
