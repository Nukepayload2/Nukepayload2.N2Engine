Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Namespace Global.Nukepayload2.N2Engine.Storage
    Public Class SaveFolder
        Inherits PlatformSaveDirectoryBase

        Dim curFolder As String
        Private Sub New(curFolder As String)
            Me.curFolder = curFolder
        End Sub
        ''' <summary>
        ''' 打开或创建存档文件夹
        ''' </summary>
        Public Shared Async Function CreateAsync(curFolder As String) As Task(Of PlatformSaveDirectoryBase)
            Dim dir As New SaveFolder(curFolder)
            If Not Directory.Exists(curFolder) Then
                Await Task.Run(Sub() Directory.CreateDirectory(curFolder))
            End If
            Return dir
        End Function

        Public Overrides Async Function GetInnerDirectoriesAsync() As Task(Of IEnumerable(Of PlatformSaveDirectoryBase))
            Return From f In Await Task.Run(Function() Directory.EnumerateDirectories(curFolder).ToArray)
                   Select New SaveFolder(f)
        End Function

        Public Overrides Async Function GetSaveFilesAsync(Of T)() As Task(Of IEnumerable(Of SaveFile(Of T)))
            Return From f In Await Task.Run(Function() Directory.EnumerateFiles(curFolder).ToArray)
                   Where f.ToLowerInvariant.EndsWith(".n2sav")
                   Select New SaveFile(Of T)() With {.OriginalFileName = f}
        End Function

        Public Overrides Async Function LoadAsync(Of TData)(save As SaveFile(Of TData), decrypt As Func(Of Stream, Stream)) As Task
            Using strm = File.OpenRead(Path.Combine(curFolder, save.FileName)), sr = New StreamReader(decrypt(strm), Encoding.Unicode)
                save.SaveData = JsonConvert.DeserializeObject(Of TData)((Await sr.ReadToEndAsync))
            End Using
        End Function

        Public Overrides Async Function OpenOrCreateDirectoryAsync(dirName As String) As Task(Of PlatformSaveDirectoryBase)
            Return Await CreateAsync(Path.Combine(curFolder, dirName))
        End Function

        Public Overrides Async Function SaveAsync(Of TData)(save As SaveFile(Of TData), encrypt As Func(Of Stream, Stream)) As Task
            Select Case save.Status
                Case SaveFileStatus.Loaded
                Case SaveFileStatus.Modified
                    Using strm = File.OpenWrite(Path.Combine(curFolder, save.FileName)), sw = New StreamWriter(encrypt(strm), Encoding.Unicode)
                        Await sw.WriteAsync(JsonConvert.SerializeObject(save.SaveData))
                        Await sw.FlushAsync()
                    End Using
                Case Else
                    Throw New InvalidOperationException($"存档文件的状态{save.Status}导致无法存档")
            End Select
        End Function
    End Class
End Namespace