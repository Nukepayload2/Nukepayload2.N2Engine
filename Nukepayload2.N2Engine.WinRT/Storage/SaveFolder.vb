Imports System.Text
Imports Newtonsoft.Json
Imports Windows.Storage

Namespace Global.Nukepayload2.N2Engine.Storage
    ''' <summary>
    ''' 表示 Windows RT 下保存文件用的目录
    ''' </summary>
    Public Class SaveFolder
        Inherits PlatformSaveDirectoryBase

        Dim curFolder As StorageFolder
        Private Sub New()

        End Sub
        ''' <summary>
        ''' 打开或创建存档文件夹
        ''' </summary>
        Public Shared Async Function CreateAsync(saveLocation As SaveLocations) As Task(Of SaveFolder)
            Dim appdata = ApplicationData.Current
            Dim sav As New SaveFolder
            Select Case saveLocation
                Case SaveLocations.LocalMaster
                    sav.curFolder = appdata.LocalFolder
                Case SaveLocations.LocalPartial
                    sav.curFolder = Await appdata.LocalFolder.CreateFolderAsync(SaveManagerImpl.SharedFolderName, CreationCollisionOption.OpenIfExists)
                Case SaveLocations.Roaming
                    sav.curFolder = appdata.RoamingFolder
                Case Else
#If WINDOWS_UWP Then
                    sav.curFolder = appdata.SharedLocalFolder
#Else
                    Throw New PlatformNotSupportedException("WP 8.1 没有共享本地数据文件夹")
#End If
            End Select
            Return sav
        End Function

        Public Overrides Async Function GetSaveFilesAsync() As Task(Of IEnumerable(Of SaveFile))
            Return From f In Await curFolder.GetFilesAsync()
                   Where f.Name.ToLowerInvariant.EndsWith(".n2sav")
                   Select New SaveFile() With {.OriginalFileName = f.Name}
        End Function

        Public Overrides Async Function LoadAsync(Of TData)(save As SaveFile(Of TData), decrypt As Func(Of Stream, Stream)) As Task
            Using strm = Await curFolder.OpenStreamForReadAsync(save.FileName), sr = New StreamReader(decrypt(strm), Encoding.Unicode)
                save.SaveData = JsonConvert.DeserializeObject(Of TData)((Await sr.ReadToEndAsync))
            End Using
        End Function

        Public Overrides Async Function SaveAsync(Of TData)(save As SaveFile(Of TData), encrypt As Func(Of Stream, Stream)) As Task
            Select Case save.Status
                Case SaveFileStatus.Loaded
                Case SaveFileStatus.Modified
                    Using strm = Await curFolder.OpenStreamForWriteAsync(save.FileName, CreationCollisionOption.ReplaceExisting), sw = New StreamWriter(encrypt(strm), Encoding.Unicode)
                        Await sw.WriteAsync(JsonConvert.SerializeObject(save.SaveData))
                        Await sw.FlushAsync()
                    End Using
                Case Else
                    Throw New InvalidOperationException($"存档文件的状态{save.Status}导致无法存档")
            End Select
        End Function

    End Class
End Namespace