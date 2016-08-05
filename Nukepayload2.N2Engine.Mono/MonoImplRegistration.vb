Imports Nukepayload2.N2Engine.Core
''' <summary>
''' 启动应用时，注册Mono的实现到引擎核心。
''' </summary>
Public Class MonoImplRegistration
    ''' <summary>
    ''' 将Mono的实现全部注册到 <see cref="PlatformImplRegistration"/> 。这个操作必须在引擎正式使用前执行。
    ''' </summary>
    Public Shared Sub Register()
        Using reg As New PlatformImplRegistration(MonoAPIContract.Platform)
            Dim regSuccess = reg.RegsterImplAssembly(GetType(MonoImplRegistration))
            If Not regSuccess Then
                Throw New InvalidOperationException("检测到不完整的UWP实现重复注册。这通常说明存在失败的平台实现注册。")
            End If
        End Using
    End Sub
End Class