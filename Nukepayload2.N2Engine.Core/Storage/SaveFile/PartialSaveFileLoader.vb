Namespace Storage
    ''' <summary>
    ''' 用于读取分支存档。分支存档的名称由 基础名称，编号（可选），拓展名构成。
    ''' </summary>
    ''' <typeparam name="T">每个分支存档的数据类型</typeparam>
    Public Class BranchSaveFilesLoader(Of T)
        ''' <summary>
        ''' 存档文件的拓展名
        ''' </summary>
        Public Const SaveFileExtension As String = ".n2sav"

        Private Sub New(partialDir As PlatformSaveDirectoryBase, baseName As String)
            Me.PartialDir = partialDir
            Me.BaseName = baseName
        End Sub

        ''' <summary>
        ''' 分部存档所在的文件夹
        ''' </summary>
        Protected Property PartialDir As PlatformSaveDirectoryBase

        ''' <summary>
        ''' 文件夹内筛选出的存档
        ''' </summary>
        Protected Property Save As New List(Of SaveFile(Of T))

        ''' <summary>
        ''' 要检索的存档的基础名称。默认是 File。可以指定为 Backup 等其它值。
        ''' </summary>
        Public ReadOnly Property BaseName As String

        ''' <summary>
        ''' 读取当前目录下所有存档
        ''' </summary>
        Private Async Function PopulateSaveFileModelsAsync() As Task
            Dim fileIds = From f In Await PartialDir.GetSaveFilesAsync(Of T)
                          Let ori = f.OriginalFileName
                          Where ori.StartsWith(BaseName) AndAlso ori.EndsWith(SaveFileExtension)
                          Select file = f, id = ori.Substring(BaseName.Length, ori.Length - SaveFileExtension.Length - BaseName.Length)
            For Each f In fileIds
                With f.file
                    .BaseName = BaseName
                    Dim id = 0
                    If Integer.TryParse(f.id, id) Then
                        .SaveId = id
                    End If
                    .Status = SaveFileStatus.Loaded
                End With
                Save.Add(f.file)
            Next
        End Function

        ''' <summary>
        ''' 异步加载指定目录下的分支存档
        ''' </summary>
        ''' <param name="partialDir">包含分支存档的目录</param>
        ''' <param name="baseName"></param>
        Public Shared Async Function CreateAsync(partialDir As PlatformSaveDirectoryBase, Optional baseName As String = "File") As Task(Of BranchSaveFilesLoader(Of T))
            Dim ldr As New BranchSaveFilesLoader(Of T)(partialDir, baseName)
            Await ldr.PopulateSaveFileModelsAsync()
            Return ldr
        End Function

        ''' <summary>
        ''' 完成加载过程。等价于 <see cref="Save"/> 属性。
        ''' </summary>
        Public Shared Widening Operator CType(loader As BranchSaveFilesLoader(Of T)) As List(Of SaveFile(Of T))
            Return loader.Save
        End Operator

    End Class

End Namespace