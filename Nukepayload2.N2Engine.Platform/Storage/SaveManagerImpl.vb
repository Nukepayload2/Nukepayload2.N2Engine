Imports Nukepayload2.N2Engine.Platform

Namespace Global.Nukepayload2.N2Engine.Storage

    ''' <summary>
    ''' 存档管理器的平台实现
    ''' </summary>
    <PlatformImpl(GetType(SaveManager))>
    Partial Friend Class SaveManagerImpl
        Inherits PlatformSaveManagerBase
#If Not WINDOWS_UWP Then
        ''' <summary>
        ''' 开发商的目录名称。默认是 Nukepayload2。
        ''' </summary>
        Public Shared Property VendorName As String = "Nukepayload2"
#End If
        Public Shared Property SharedFolderName As String = "Partial"
    End Class
End Namespace
