﻿Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.Core
''' <summary>
''' UWP的渲染器的基类。
''' </summary>
''' <typeparam name="T">N2引擎视图</typeparam>
Public MustInherit Class UWPRenderer(Of T As GameVisual)
    Inherits RendererBase(Of T)
    Implements IDisposable

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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                DisposeResources
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub
    ''' <summary>
    ''' 派生类继承时定义要如何处置已经申请的资源
    ''' </summary>
    Public MustOverride Sub DisposeResources()

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
