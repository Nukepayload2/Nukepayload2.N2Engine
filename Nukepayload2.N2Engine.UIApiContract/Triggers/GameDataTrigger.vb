Imports System.Reflection
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Triggers
    ''' <summary>
    ''' 数据触发器的基类
    ''' </summary>
    ''' <typeparam name="TData">绑定的数据的类型</typeparam>
    Public MustInherit Class GameDataTriggerBase(Of TData)
        Implements IGameTrigger
        ''' <summary>
        ''' 决定是否执行 Action
        ''' </summary>
        ''' <param name="data">新的数据</param>
        ''' <returns>如果返回真，则执行 Action</returns>
        Public MustOverride Function Condition(data As TData) As Boolean

        Public MustOverride Sub Action(data As TData)

        Protected MustOverride Function GetPropertyBinder(visual As GameVisual) As PropertyBinder(Of TData)

        Private Sub CallAction(sender As Object, e As PropertyBinderDataChangedEventArgs(Of TData))
            If Condition(e.NewValue) Then Action(e.NewValue)
        End Sub

        Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
            AddHandler GetPropertyBinder(visual).DataChanged, AddressOf CallAction
        End Sub

        Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
            RemoveHandler GetPropertyBinder(visual).DataChanged, AddressOf CallAction
        End Sub
    End Class
    ''' <summary>
    ''' 中继数据触发器的基类
    ''' </summary>
    ''' <typeparam name="TData">绑定的数据的类型</typeparam>
    Public MustInherit Class GameRelayDataTriggerBase(Of TData)
        Implements IGameTrigger

        Sub New(condition As Predicate(Of TData), action As Action)
            Me.Condition = condition
            Me.Action = action
        End Sub
        ''' <summary>
        ''' 触发器的条件
        ''' </summary>
        Public Property Condition As Predicate(Of TData)
        ''' <summary>
        ''' 触发器的动作。满足条件才执行。
        ''' </summary>
        Public Property Action As Action

        Protected MustOverride Function GetPropertyBinder(visual As GameVisual) As PropertyBinder(Of TData)

        Private Sub CallAction(sender As Object, e As PropertyBinderDataChangedEventArgs(Of TData))
            If Condition(e.NewValue) Then Action.Invoke
        End Sub

        Public Sub Attach(visual As GameVisual) Implements IGameTrigger.Attach
            AddHandler GetPropertyBinder(visual).DataChanged, AddressOf CallAction
        End Sub

        Public Sub Detach(visual As GameVisual) Implements IGameTrigger.Detach
            RemoveHandler GetPropertyBinder(visual).DataChanged, AddressOf CallAction
        End Sub
    End Class

    ''' <summary>
    ''' 中继数据触发器
    ''' </summary>
    Public Class GameRelayDataTrigger(Of TData)
        Inherits GameRelayDataTriggerBase(Of TData)

        Sub New(propertyName As String, condition As Predicate(Of TData), action As Action)
            MyBase.New(condition, action)
            Me.PropertyName = propertyName
        End Sub
        Public Property PropertyName$
        Protected Overrides Function GetPropertyBinder(visual As GameVisual) As PropertyBinder(Of TData)
            Return DirectCast(visual.GetType.GetRuntimeProperty(PropertyName).GetValue(visual), PropertyBinder(Of TData))
        End Function
    End Class

    ''' <summary>
    ''' 自定义获取数据绑定器操作的中继数据触发器
    ''' </summary>
    Public Class CustomGameRelayDataTrigger(Of TData)
        Inherits GameRelayDataTriggerBase(Of TData)

        Public Sub New(propertyBinderGetter As Func(Of GameVisual, PropertyBinder(Of TData)), condition As Predicate(Of TData), action As Action)
            MyBase.New(condition, action)
            Me.PropertyBinderGetter = propertyBinderGetter
        End Sub

        Public Property PropertyBinderGetter As Func(Of GameVisual, PropertyBinder(Of TData))

        Protected Overrides Function GetPropertyBinder(visual As GameVisual) As PropertyBinder(Of TData)
            Return PropertyBinderGetter.Invoke(visual)
        End Function
    End Class
End Namespace