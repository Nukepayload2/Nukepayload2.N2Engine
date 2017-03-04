Imports System.Reflection
Imports Nukepayload2.CodeAnalysis
Imports Nukepayload2.N2Engine.Foundation

Public Class PropertyBinderUnderlyingTypeNameConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Dim propBinderType = value.GetType
        If propBinderType.GetGenericTypeDefinition Is GetType(PropertyBinder(Of)) Then
            Return VBObjectViewer.GetTypeName(propBinderType.GenericTypeArguments(0), propBinderType.GetTypeInfo.GetCustomAttribute(Of TupleElementNamesAttribute), 0)
        End If
        Return Nothing
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Throw New NotSupportedException()
    End Function
End Class
