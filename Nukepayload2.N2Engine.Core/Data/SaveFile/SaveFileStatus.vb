Public Enum SaveFileStatus
    ''' <summary>
    ''' 数据结构已经初始化，准备好加载。
    ''' </summary>
    Initialized
    ''' <summary>
    ''' 从存储介质上加载了数据
    ''' </summary>
    Loaded
    ''' <summary>
    ''' 此存档与存储介质上不同步
    ''' </summary>
    Modified
    ''' <summary>
    ''' 下次保存的时候会删除这个文件
    ''' </summary>
    Deleting
    ''' <summary>
    ''' 试图对此存档进行操作时出错
    ''' </summary>
    Invalid
End Enum
