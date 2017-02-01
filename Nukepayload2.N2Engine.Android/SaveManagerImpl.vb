Imports Android.App
Imports Nukepayload2.N2Engine.Platform

Namespace Global.Nukepayload2.N2Engine.Storage
    Friend Class SaveManagerImpl
        Public Overrides Async Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
            Dim folder As String = Nothing
            Dim localData = Application.Context.FilesDir.Path
            Select Case Location
                Case SaveLocations.Local
                    folder = localData
                Case SaveLocations.Roaming
                    Throw New PlatformNotSupportedException("Android 没有漫游数据目录")
                Case Else
                    Dim dirMgr = GetStoragePath()
                    Await AddFolderAsync(dirMgr, VendorName)
                    folder = dirMgr.DirectoryName
            End Select
            Dim fol = Await SaveFolder.CreateAsync(folder)
            Return fol
        End Function

        Private Shared Async Function AddFolderAsync(folder As IDirectory, newFolder As String) As Task
            folder.DirectoryName = Path.Combine(folder.DirectoryName, newFolder)
            If Not folder.Exists Then
                Await folder.CreateAsync
            End If
        End Function

        Private Function GetStoragePath() As IDirectory
            Dim dirMgr = PlatformActivator.CreateBaseInstance(Of IDirectory)("/sdcard")
            If Not dirMgr.Exists Then
                dirMgr.DirectoryName = "/emulated/0"
                If Not dirMgr.Exists Then
                    Throw New PlatformNotSupportedException($"当前版本的 Android 不能通过 {dirMgr.DirectoryName} 读写文件。")
                End If
            End If
            Return dirMgr
        End Function
    End Class
End Namespace