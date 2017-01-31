Namespace Storage

    ''' <summary>
    ''' 每个平台实现的存档目录管理器
    ''' </summary>
    Public MustInherit Class PlatformSaveDirectoryBase

        ''' <summary>
        ''' 枚举这个目录下全部的存档文件
        ''' </summary>
        Public MustOverride Function GetSaveFilesAsync(Of TData)() As Task(Of IEnumerable(Of SaveFile(Of TData)))

        ''' <summary>
        ''' 枚举这个目录下嵌套的目录
        ''' </summary>
        Public MustOverride Function GetInnerDirectoriesAsync() As Task(Of IEnumerable(Of PlatformSaveDirectoryBase))

        ''' <summary>
        ''' 打开或创建此目录下的目录
        ''' </summary>
        ''' <param name="dirName">目录名称</param>
        Public MustOverride Function OpenOrCreateDirectoryAsync(dirName As String) As Task(Of PlatformSaveDirectoryBase)

        ''' <summary>
        ''' 以 Json 文本方式读取一个存档文件，然后反序列化为 <typeparamref name="TData"/>。
        ''' </summary>
        ''' <typeparam name="TData">要反序列化为这个类型</typeparam>
        ''' <param name="save">这个目录下的存档文件</param>
        ''' <param name="decrypt">获取解密文件流。如果不解密则直接返回原始的流。</param>
        ''' <returns>已读取的存档</returns>
        Public MustOverride Function LoadAsync(Of TData)(save As SaveFile(Of TData), decrypt As Func(Of Stream, Stream)) As Task

        ''' <summary>
        ''' 将存档数据对象保存成一段 Json 文本，然后加密保存。
        ''' </summary>
        ''' <typeparam name="TData"></typeparam>
        ''' <param name="save">这个目录下的存档文件</param>
        ''' <param name="encrypt">获取加密文件流。如果不加密则直接返回原始的流。</param>
        Public MustOverride Function SaveAsync(Of TData)(save As SaveFile(Of TData), encrypt As Func(Of Stream, Stream)) As Task

    End Class

End Namespace