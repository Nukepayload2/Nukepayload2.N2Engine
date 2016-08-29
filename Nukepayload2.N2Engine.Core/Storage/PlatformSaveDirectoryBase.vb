Namespace Storage

    Public MustInherit Class PlatformSaveDirectoryBase
        Public MustOverride Function GetSaveFilesAsync() As Task(Of IEnumerable(Of SaveFile))
        Public MustOverride Function LoadAsync(Of TData)(save As SaveFile(Of TData), decrypt As Func(Of Stream, Stream)) As Task
        Public MustOverride Function SaveAsync(Of TData)(save As SaveFile(Of TData), encrypt As Func(Of Stream, Stream)) As Task

    End Class

End Namespace