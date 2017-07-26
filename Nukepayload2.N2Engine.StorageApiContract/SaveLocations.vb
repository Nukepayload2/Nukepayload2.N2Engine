Namespace Storage
    ''' <summary>
    ''' 存档的保存位置
    ''' </summary>
    Public Enum SaveLocations
        ''' <summary>
        ''' 非漫游主存档文件夹。这个文件夹在任何平台都可用。
        ''' </summary>
        Local
        ''' <summary>
        ''' 漫游存档文件夹。这个文件夹仅在 Windows 平台 可用。
        ''' </summary>
        Roaming
        ''' <summary>
        ''' 开发商存档文件夹。这个文件夹在 iOS 和 WP8.1 平台 不可用。
        ''' </summary>
        VendorShared
    End Enum
End Namespace