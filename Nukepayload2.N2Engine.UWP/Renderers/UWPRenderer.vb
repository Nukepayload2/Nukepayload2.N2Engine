Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Renderers
Imports Nukepayload2.N2Engine.UI.Elements
''' <summary>
''' UWP的渲染器的基类。
''' </summary>
''' <typeparam name="T">N2引擎视图</typeparam>
Public MustInherit Class UWPRenderer(Of T As GameVisual)
    Inherits RendererBase(Of T)

    Sub New(view As T)
        MyBase.New(view)
    End Sub
    ''' <summary>
    ''' 派生类继承时，定义手动引发 CreateResources 事件的行为 
    ''' </summary>
    Protected MustOverride Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
    ''' <summary>
    ''' 派生类继承时，定义手动引发 Draw 事件的行为 
    ''' </summary>
    Protected MustOverride Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
    ''' <summary>
    ''' 派生类继承时，定义手动引发 GameLoopStarting 事件的行为 
    ''' </summary>
    Protected MustOverride Sub OnGameLoopStarting(sender As ICanvasAnimatedControl, args As Object)
    ''' <summary>
    ''' 派生类继承时，定义手动引发 GameLoopStopped 事件的行为 
    ''' </summary>
    Protected MustOverride Sub OnGameLoopStopped(sender As ICanvasAnimatedControl, args As Object)
    ''' <summary>
    ''' 派生类继承时，定义手动引发 Update 事件的行为 
    ''' </summary>
    Protected MustOverride Sub OnUpdate(sender As ICanvasAnimatedControl, args As CanvasAnimatedUpdateEventArgs)

End Class
