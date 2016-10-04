Namespace UI.Elements
    Public MustInherit Class GameLayer
        Inherits GameVisualContainter

    End Class
    Public MustInherit Class GameLayer(Of TVisual As GameVisual)
        Inherits GameLayer
        ''' <summary>
        ''' 添加一个新的 <typeparamref name="TVisual"/>
        ''' </summary>
        Public Overloads Sub Add(visual As TVisual)
            MyBase.Add(visual)
        End Sub
        ''' <summary>
        ''' 添加一组新的 <typeparamref name="TVisual"/>
        ''' </summary>
        Public Overloads Sub AddRange(visuals As IEnumerable(Of TVisual))
            MyBase.AddRange(visuals)
        End Sub
        ''' <summary>
        ''' 删除指定的 <typeparamref name="TVisual"/>
        ''' </summary>
        Public Overloads Sub Remove(visual As TVisual)
            MyBase.Remove(visual)
        End Sub
        ''' <summary>
        ''' 访问子元素
        ''' </summary>
        Default Overloads Property Item(index As Integer) As TVisual
            Get
                Return DirectCast(MyBase.Item(index), TVisual)
            End Get
            Set(value As TVisual)
                MyBase.Item(index) = value
            End Set
        End Property
    End Class
End Namespace