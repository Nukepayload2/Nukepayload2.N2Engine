Imports System.Text
Namespace Storage

    Public Interface IFile
        Property CreationTime As Date
        Property CreationTimeUtc As Date
        Property FileName As String
        Property LastAccessTime As Date
        Property LastAccessTimeUtc As Date
        Property LastWriteTime As Date
        Property LastWriteTimeUtc As Date
        Function CopyAndOverwriteAsync(target As String) As Task
        Function CopyAsync(target As String) As Task
        Function CreateAsync() As Task(Of Stream)
        Function DeleteAsync() As Task
        Function Exists() As Boolean
        Function MoveAsync(target As String) As Task
        Function OpenForReadAsync() As Task(Of Stream)
        Function OpenForWriteAsync() As Task(Of Stream)
        Function ReadAllBytesAsync() As Task(Of Byte())
        Function ReadAllTextAsync() As Task(Of String)
        Function ReadAllTextAsync(encoding As Encoding) As Task(Of String)
        Function WriteAllBytesAsync(data As Byte()) As Task
        Function WriteAllTextAsync(data As String) As Task
        Function WriteAllTextAsync(data As String, encoding As Encoding) As Task
    End Interface
End Namespace