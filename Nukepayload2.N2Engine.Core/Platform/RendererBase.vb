''' <summary>
''' 渲染器的基类
''' </summary>
Public MustInherit Class RendererBase
    Sub New(view As GameVisual)
        Me.View = view
    End Sub

    ''' <summary>
    ''' 注册渲染器后，元素视图可以用这个方法创建渲染器
    ''' </summary>
    Friend Shared Sub CreateElementRenderer(view As GameElement)
        Activator.CreateInstance(PlatformImplRegistration.Registered(view.GetType), view)
    End Sub

    Public Property View As GameVisual
End Class

Public MustInherit Class RendererBase(Of T As GameVisual)
    Inherits RendererBase

    Sub New(view As T)
        MyBase.New(view)
    End Sub

    Public Overloads Property View As T
        Get
            Return DirectCast(MyBase.View, T)
        End Get
        Set(value As T)
            MyBase.View = value
        End Set
    End Property
End Class