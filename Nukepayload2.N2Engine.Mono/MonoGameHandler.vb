Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports RaisingStudio.Xna.Graphics

Public Class MonoGameHandler
    Inherits Game

    Dim graphics As GraphicsDeviceManager
    Dim drawingContext As DrawingContext

    Public Event GameLoopStarting As TypedEventHandler(Of Game, Object)
    Public Event CreateResources As TypedEventHandler(Of Game, MonogameCreateResourcesEventArgs)
    Public Event GameLoopEnded As TypedEventHandler(Of Game, Object)
    Public Event Drawing As TypedEventHandler(Of Game, MonogameDrawEventArgs)
    Public Event Updating As TypedEventHandler(Of Game, MonogameUpdateEventArgs)

    Sub New()
        graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"

        graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft Or DisplayOrientation.LandscapeRight
    End Sub

    ''' <summary>
    ''' 在执行游戏之前初始化游戏逻辑。
    ''' 这里可以查询任何所需的服务和装载与图像无关的内容。
    ''' MyBase.Initialize 能够初始化基础组件。
    ''' </summary>
    Protected Overrides Sub Initialize()
        ' TODO: 初始化游戏逻辑
        RaiseEvent GameLoopStarting(Me, Nothing)

        MyBase.Initialize()
    End Sub

    ''' <summary>
    ''' 每个游戏中只会调用一次。用于装载游戏特定的资源。
    ''' </summary>
    Protected Overrides Sub LoadContent()
        ' 新建用于绘制纹理的 SpriteBatch
        drawingContext = New DrawingContext(GraphicsDevice)
        ' TODO: 使用 Me.Content 装载游戏内容
        RaiseEvent CreateResources(Me, New MonogameCreateResourcesEventArgs(GraphicsDevice))
    End Sub

    ''' <summary>
    ''' 每个游戏中只会调用一次。用于卸载游戏特定的资源。
    ''' </summary>
    Protected Overrides Sub UnloadContent()
        ' TODO: 卸载任何非 ContentManager 内容
        RaiseEvent GameLoopEnded(Me, Nothing)
    End Sub

    ''' <summary>
    ''' 允许游戏逻辑运行，例如更新世界，检测碰撞，收集输入 和 播放声音。
    ''' </summary>
    ''' <param name="timing">提供时间的快照</param>
    Protected Overrides Sub Update(timing As GameTime)
        If GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed Then
#If iOS_APP Then
            Throw New PlatformNotSupportedException("iOS系统不支持使用代码退出应用")
#Else
            Me.Exit()
#End If
        End If
        ' TODO: 在此添加更新逻辑
        RaiseEvent Updating(Me, New MonogameUpdateEventArgs(timing))

        MyBase.Update(timing)
    End Sub

    ''' <summary>
    ''' 在游戏应该绘制的时候调用
    ''' </summary>
    ''' <param name="timing">提供时间的快照</param>
    Protected Overrides Sub Draw(timing As GameTime)
        GraphicsDevice.Clear(Color.CornflowerBlue)

        ' TODO: 在此添加绘制代码
        drawingContext.Begin()
        RaiseEvent Drawing(Me, New MonogameDrawEventArgs(drawingContext, timing))
        drawingContext.End()

        MyBase.Draw(timing)
    End Sub
End Class