Imports System.Reflection
Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Triggers
    Public MustInherit Class GameDataTriggerBase(Of TVisual As GameVisual, TData)
        Inherits GameTriggerBase(Of TVisual)

        Sub New(condition As Predicate(Of TData), action As Action)
            Me.Condition = condition
            Me.Action = action
        End Sub

        Public Property Condition As Predicate(Of TData)
        Public Property Action As Action

        Public Overrides Sub Attach(visual As TVisual)
            AddHandler GetPropBinder(visual).DataChanged, AddressOf CallAction
            MyBase.Attach(visual)
        End Sub

        Protected MustOverride Function GetPropBinder(visual As TVisual) As PropertyBinder(Of TData)

        Private Sub CallAction(sender As Object, e As PropertyBinderDataChangedEventArgs(Of TData))
            If Condition(e.NewValue) Then Action.Invoke
        End Sub

        Public Overrides Sub Detach(visual As TVisual)
            RemoveHandler GetPropBinder(visual).DataChanged, AddressOf CallAction
            MyBase.Detach(visual)
        End Sub
    End Class

    ''' <summary>
    ''' 使用方便但是性能较差的数据触发器
    ''' </summary>
    Public Class GameDataTrigger(Of TVisual As GameVisual, TData)
        Inherits GameDataTriggerBase(Of TVisual, TData)

        Sub New(propertyName As String, condition As Predicate(Of TData), action As Action)
            MyBase.New(condition, action)
            Me.PropertyName = propertyName
        End Sub
        Public Property PropertyName$
        Protected Overrides Function GetPropBinder(visual As TVisual) As PropertyBinder(Of TData)
            Return DirectCast(visual.GetType.GetRuntimeProperty(PropertyName).GetValue(visual), PropertyBinder(Of TData))
        End Function
    End Class

    ''' <summary>
    ''' 高性能的数据触发器
    ''' </summary>
    Public Class GameCustomDataTrigger(Of TVisual As GameVisual, TData)
        Inherits GameDataTriggerBase(Of TVisual, TData)

        Public Sub New(propertyBinderGetter As Func(Of TVisual, PropertyBinder(Of TData)), condition As Predicate(Of TData), action As Action)
            MyBase.New(condition, action)
            Me.PropertyBinderGetter = propertyBinderGetter
        End Sub

        Public Property PropertyBinderGetter As Func(Of TVisual, PropertyBinder(Of TData))

        Protected Overrides Function GetPropBinder(visual As TVisual) As PropertyBinder(Of TData)
            Return PropertyBinderGetter.Invoke(visual)
        End Function
    End Class
End Namespace