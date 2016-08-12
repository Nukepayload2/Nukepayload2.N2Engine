' “空白应用程序”模板在 http://go.microsoft.com/fwlink/?LinkID=391641 上有介绍

''' <summary>
''' 提供特定于应用程序的行为，以补充默认的应用程序类。
''' </summary>
NotInheritable Class App
    Inherits Application

    Private _transitions As TransitionCollection

    ''' <summary>
    ''' 初始化单一实例应用程序对象。这是创作的代码的第一行
    ''' 已执行，逻辑上等同于 main() 或 WinMain()。
    ''' </summary>
    Public Sub New()
        InitializeComponent()

    End Sub

    ''' <summary>
    ''' 在最终用户正常启动应用程序时调用。将在启动应用程序
    ''' 当启动应用程序以打开特定的文件或显示时使用
    ''' 搜索结果等
    ''' </summary>
    ''' <param name="e">有关启动请求和过程的详细信息。</param>
    Protected Overrides Sub OnLaunched(e As LaunchActivatedEventArgs)
#If DEBUG Then
        If System.Diagnostics.Debugger.IsAttached Then
            DebugSettings.EnableFrameRateCounter = True
        End If
#End If

        Dim rootPage As Page = TryCast(Window.Current.Content, Page)

        ' 不要在窗口已包含内容时重复应用程序初始化，
        ' 只需确保窗口处于活动状态
        If rootPage Is Nothing Then
            ' 注册引擎实现
            Nukepayload2.N2Engine.Wp81.MonoImplRegistration.Register()
            ' 创建要充当导航上下文的框架，并导航到第一页
            rootPage = New MainPage

            '设置默认语言
            rootPage.Language = Windows.Globalization.ApplicationLanguages.Languages(0)

            If e.PreviousExecutionState = ApplicationExecutionState.Terminated Then
                ' TODO: 从之前挂起的应用程序加载状态
            End If
            ' 将框架放在当前窗口中
            Window.Current.Content = rootPage
        End If

        If rootPage.Content Is Nothing Then
            ' 删除用于启动的旋转门导航。
            If rootPage.Transitions IsNot Nothing Then
                _transitions = New TransitionCollection()
                For Each transition As Transition In rootPage.Transitions
                    _transitions.Add(transition)
                Next
            End If

            rootPage.Transitions = Nothing

        End If

        ' 确保当前窗口处于活动状态
        Window.Current.Activate()
    End Sub

    ''' <summary>
    ''' 启动应用程序后还原内容转换。
    ''' </summary>
    Private Sub RootFrame_FirstNavigated(sender As Object, e As NavigationEventArgs)
        Dim newTransitions As TransitionCollection
        If _transitions Is Nothing Then
            newTransitions = New TransitionCollection()
            newTransitions.Add(New NavigationThemeTransition())
        Else
            newTransitions = _transitions
        End If

        Dim rootFrame As Frame = DirectCast(sender, Frame)
        rootFrame.ContentTransitions = newTransitions
        RemoveHandler rootFrame.Navigated, AddressOf RootFrame_FirstNavigated
    End Sub

    ''' <summary>
    ''' 在将要挂起应用程序执行时调用。将保存应用程序状态
    ''' 无需知道应用程序会被终止还是会恢复，
    ''' 并让内存内容保持不变。
    ''' </summary>
    Private Sub OnSuspending(sender As Object, e As SuspendingEventArgs) Handles Me.Suspending
        Dim deferral As SuspendingDeferral = e.SuspendingOperation.GetDeferral()

        ' TODO: 保存应用程序状态并停止任何后台活动
        deferral.Complete()
    End Sub

End Class
