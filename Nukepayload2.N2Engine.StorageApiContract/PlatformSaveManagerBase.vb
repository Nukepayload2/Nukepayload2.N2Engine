Namespace Storage

    Public MustInherit Class PlatformSaveManagerBase
        ''' <summary>
        ''' 开发商的目录名称。默认是 Nukepayload2。
        ''' </summary>
        Public Shared Property VendorName As String = "Nukepayload2"
        ''' <summary>
        ''' 异步打开存档文件夹
        ''' </summary>
        Public MustOverride Function OpenSaveFolderAsync(Location As SaveLocations) As Task(Of PlatformSaveDirectoryBase)
    End Class
End Namespace