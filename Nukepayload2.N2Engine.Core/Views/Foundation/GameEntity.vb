Imports Box2D
''' <summary>
''' 游戏中的实体，具备一些物理属性, 如位置，形状，质量，旋转，连接情况等。
''' </summary>
Public MustInherit Class GameEntity
    Inherits GameElement
    ''' <summary>
    ''' 表示视图中物体的物理属性
    ''' </summary>
    Public ReadOnly Property Body As New PropertyBinder(Of Body)
    ''' <summary>
    ''' 视图的位置。这通常是物体的左上角的坐标。此属性默认情况下绑定到 <see cref="Body"/> 的 <see cref="XForm"/> 上。 
    ''' </summary>
    Public Overrides ReadOnly Property Location As New PropertyBinder(Of Vector2)(
        Function() Body.Value.Position,
        Sub(v)
            Dim bdy = Body.Value
            bdy.SetXForm(v, bdy.Angle)
        End Sub)

End Class