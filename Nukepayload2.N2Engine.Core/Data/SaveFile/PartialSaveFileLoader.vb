Public Class PartialSaveFileLoader(Of T)
    Const SavExtension As String = ".n2sav"

    Sub New(partialDir As PlatformSaveDirectoryBase)
        Me.PartialDir = partialDir
        CraftSaveFileModel()
    End Sub

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
    Private Sub CraftSaveFileModel()
        Dim fileIds = From f In PartialDir.SaveFiles
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
    End Sub
End Class
