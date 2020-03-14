#If WINDOWS_DESKTOP Or DESKTOP_OPENGL Then
Imports System.Runtime.CompilerServices
#End If

Imports System.Reflection
Imports Nukepayload2.N2Engine.Platform

''' <summary>
''' 启动应用时，注册Mono的实现到引擎核心。
''' </summary>
Public Class MonoImplRegistration
    ''' <summary>
    ''' 将 Mono 的实现全部注册到 <see cref="PlatformImplRegistration"/> 。这个操作必须在引擎正式使用前执行。
    ''' </summary>
    Public Shared Sub Register(Optional extraRegister As Assembly = Nothing)
        Using reg As New PlatformImplRegistration(MonoAPIContract.Platform)
            If Not reg.RegsterImplAssembly(GetType(MonoImplRegistration)) Then
                Throw New InvalidOperationException("检测到不完整的 Mono 实现重复注册。这通常说明存在失败的平台实现注册。")
            End If
            If extraRegister IsNot Nothing AndAlso Not reg.RegsterImplAssembly(extraRegister) Then
                Throw New InvalidOperationException("额外的注册内容无法注册。")
            End If
            Information.GameEnvironment.Platform = MonoAPIContract.Platform
            Information.GameEnvironment.Renderer = MonoAPIContract.Renderer
        End Using
    End Sub
End Class