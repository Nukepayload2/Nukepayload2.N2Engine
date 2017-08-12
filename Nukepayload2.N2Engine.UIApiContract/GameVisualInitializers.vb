Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI

    Public Module GameVisualInitializers
        <Extension>
        Public Function Bind(Of TObject, TProperty)(obj As TObject, getProperty As Func(Of TObject, PropertyBinder(Of TProperty)), value As TProperty) As TObject
            getProperty(obj).Bind(value)
            Return obj
        End Function
        <Extension>
        Public Function Bind(Of TObject, TProperty)(obj As TObject, getProperty As Func(Of TObject, PropertyBinder(Of TProperty)), getter As Func(Of TProperty)) As TObject
            getProperty(obj).Bind(getter)
            Return obj
        End Function
        <Extension>
        Public Function Bind(Of TObject, TProperty)(obj As TObject, getProperty As Func(Of TObject, PropertyBinder(Of TProperty)), getter As Func(Of TProperty), setter As Action(Of TProperty)) As TObject
            getProperty(obj).Bind(getter, setter)
            Return obj
        End Function

        <Extension>
        Public Function OnUpdate(Of TObject As GameVisual)(obj As TObject, update As Action(Of UpdatingEventArgs)) As TObject
            obj.UpdateAction = update
            Return obj
        End Function

        <Extension>
        Public Function AddChild(Of T As GameVisualContainer)(obj As T, child As GameVisual) As T
            obj.Children.Add(child)
            Return obj
        End Function
    End Module

End Namespace