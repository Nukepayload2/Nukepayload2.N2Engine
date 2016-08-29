Namespace Storage

    Public Class PartialSaveFileLoader(Of T)
        Const SavExtension As String = ".n2sav"

        Private Sub New(partialDir As PlatformSaveDirectoryBase)
            Me.PartialDir = partialDir
        End Sub

        Public Shared Async Function CreateAsync(partialDir As PlatformSaveDirectoryBase) As Task(Of PartialSaveFileLoader(Of T))
            Dim ldr As New PartialSaveFileLoader(Of T)(partialDir)
            Await ldr.CraftSaveFileModel()
            Return ldr
        End Function

        Protected Property PartialDir As PlatformSaveDirectoryBase
        Protected Property Save As New List(Of SaveFile(Of T))
        Public ReadOnly Property BaseName As String = "File"
        Protected Property forEachAction As Action(Of SaveFile)

        Public Function HasBaseName(BaseName As String) As PartialSaveFileLoader(Of T)
            _BaseName = BaseName
            Return Me
        End Function

        Public Shared Widening Operator CType(loader As PartialSaveFileLoader(Of T)) As List(Of SaveFile(Of T))
            Return loader.Save
        End Operator
        Public Function ForEach(act As Action(Of SaveFile)) As PartialSaveFileLoader(Of T)
            forEachAction = act
            Return Me
        End Function
        Private Async Function CraftSaveFileModel() As Task
            Dim fileIds = From f In Await PartialDir.GetSaveFilesAsync
                          Let ori = f.OriginalFileName
                          Where ori.StartsWith(BaseName) AndAlso ori.EndsWith(SavExtension)
                          Select file = f, id = ori.Substring(BaseName.Length, ori.Length - SavExtension.Length - BaseName.Length)
            For Each f In fileIds
                With f.file
                    .BaseName = BaseName
                    If f.id.Length > 0 Then
                        .HasSaveId(CInt(f.id))
                    End If
                    .Status = SaveFileStatus.Loaded
                End With
                forEachAction.Invoke(f.file)
                Save.Add(CType(f.file, SaveFile(Of T)))
            Next
        End Function
    End Class

End Namespace