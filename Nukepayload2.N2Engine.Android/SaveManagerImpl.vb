Imports Android.App

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
                    folder = dirMgr
            End Select
            Dim fol = Await SaveFolder.CreateAsync(folder)
            Return fol
        End Function

        Private Shared Async Function AddFolderAsync(folder As String, newFolder As String) As Task
            folder = Path.Combine(folder, newFolder)
            If Not Directory.Exists(folder) Then
                Await Task.Run(Sub() Directory.CreateDirectory(folder))
            End If
        End Function

        Private Function GetStoragePath() As String
            For Each p In {"/sdcard", "/emulated/0"}
                If Directory.Exists(p) Then
                    Return p
                End If
            Next
            Throw New PlatformNotSupportedException("找不到合适的存档位置。")
        End Function
    End Class
End Namespace