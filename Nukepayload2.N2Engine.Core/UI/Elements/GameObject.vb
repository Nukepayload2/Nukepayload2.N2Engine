Imports Nukepayload2.N2Engine.Resources

Namespace UI.Elements
    ''' <summary>
    ''' 游戏中最基础的元素，可以是可见的，也可以不可见。
    ''' </summary>
    Public MustInherit Class GameObject
        ''' <summary>
        ''' 平台无关的资源
        ''' </summary>
        Public Property Resources As New Dictionary(Of String, GameResourceBase)
        ''' <summary>
        ''' 初始化<see cref="Resources"/> 
        ''' </summary>
        Protected Overridable Sub CreateResources()

        End Sub
        Sub New()
            CreateResources()
        End Sub
    End Class
End Namespace