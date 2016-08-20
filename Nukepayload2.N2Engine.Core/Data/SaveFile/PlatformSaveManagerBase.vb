Public MustInherit Class PlatformSaveManagerBase
    ''' <summary>
    ''' 自动漫游的数据
    ''' </summary>
    Public MustOverride ReadOnly Property RoamingData As PlatformSaveDirectoryBase
    ''' <summary>
    ''' 本地主数据
    ''' </summary>
    Public MustOverride ReadOnly Property LocalMasterData As PlatformSaveDirectoryBase
    ''' <summary>
    ''' 本地分部存档数据
    ''' </summary>
    Public MustOverride ReadOnly Property LocalPartialData As PlatformSaveDirectoryBase
    ''' <summary>
    ''' 开发者的游戏共享的存档目录
    ''' </summary>
    Public MustOverride ReadOnly Property VendorSharedData As PlatformSaveDirectoryBase
End Class
