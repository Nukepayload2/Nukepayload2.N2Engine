Imports Android.App
Imports Nukepayload2.N2Engine.Platform

Namespace Global.Nukepayload2.N2Engine.Storage
    Friend Class SaveManagerImpl
        Public Overrides Async Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
            Dim folder As String = Nothing
            Dim localData = Application.Context.FilesDir.AbsolutePath
            Select Case Location
                Case SaveLocations.Local
                    folder = localData
                Case SaveLocations.Roaming
                    Throw New PlatformNotSupportedException("Android 没有漫游数据目录")
                Case Else
                    Dim dirMgr = PlatformActivator.CreateBaseInstance(Of IDirectory)("/sdcard/" + VendorName)
                    If Not dirMgr.Exists Then
                        Try
                            Await dirMgr.CreateAsync
                            folder = dirMgr.DirectoryName
                        Catch ex As UnauthorizedAccessException
                        End Try
                        If folder Is Nothing Then
                            dirMgr.DirectoryName = $"data/data/com.{VendorName.ToLower}.shared"
                        End If
                    End If
                    folder = dirMgr.DirectoryName
            End Select
            Return Await SaveFolder.CreateAsync(folder)
        End Function
    End Class
End Namespace