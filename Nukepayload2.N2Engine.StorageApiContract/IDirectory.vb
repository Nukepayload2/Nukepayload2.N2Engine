Namespace Storage
    Public Interface IDirectory
        Property CreationTime As Date
        Property CreationTimeUtc As Date
        Property DirectoryName As String
        Property LastAccessTime As Date
        Property LastAccessTimeUtc As Date
        Property LastWriteTime As Date
        Property LastWriteTimeUtc As Date
        Function CreateAsync() As Task
        Function DeleteAsync() As Task
        Function EnumerateDirectoriesAsync() As Task(Of String())
        Function EnumerateDirectoriesAsync(searchPattern As String) As Task(Of String())
        Function EnumerateFilesAsync() As Task(Of String())
        Function EnumerateFilesAsync(searchPattern As String) As Task(Of String())
        Function Exists() As Boolean
        Function MoveAsync(dest As String) As Task
        Function OpenStreamForReadAsync(fileName As String) As Task(Of Stream)
        Function OpenStreamForWriteAsync(fileName As String) As Task(Of Stream)
    End Interface
End Namespace