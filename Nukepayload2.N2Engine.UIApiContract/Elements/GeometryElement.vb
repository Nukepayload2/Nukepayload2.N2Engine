Imports Nukepayload2.N2Engine.Foundation

Namespace UI.Elements
    ''' <summary>
    ''' 基本图形元素
    ''' </summary>
    Public MustInherit Class GeometryElement
        Inherits GameElement
        ''' <summary>
        ''' 轮廓线的颜色。可为空。
        ''' </summary>
        Public Property Stroke As PropertyBinder(Of Color) = New ManualPropertyBinder(Of Color)
    End Class
    ''' <summary>
    ''' 表示线段
    ''' </summary>
    Public Class LineElement
        Inherits GeometryElement
        ''' <summary>
        ''' 线段的起始点
        ''' </summary>
        Public Property StartPoint As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
        ''' <summary>
        ''' 线段的结束点
        ''' </summary>
        Public Property EndPoint As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
    End Class
    ''' <summary>
    ''' 表示折线
    ''' </summary>
    Public Class PolylineElement
        Inherits GeometryElement
        ''' <summary>
        ''' 线段的每一个顶点
        ''' </summary>
        Public Property Points As PropertyBinder(Of Vector2()) = New ManualPropertyBinder(Of Vector2())
        ''' <summary>
        ''' 是否是闭合的折线段
        ''' </summary>
        Public Property IsClosed As PropertyBinder(Of Boolean) = New ManualPropertyBinder(Of Boolean)

    End Class
    ''' <summary>
    ''' 表示贝塞尔曲线
    ''' </summary>
    Public MustInherit Class BezierCurveElement
        Inherits GeometryElement
        ''' <summary>
        ''' 贝塞尔曲线的起始点
        ''' </summary>
        Public Property StartPoint As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
        ''' <summary>
        ''' 贝塞尔曲线的结束点
        ''' </summary>
        Public Property EndPoint As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)

    End Class
    ''' <summary>
    ''' 表示二次贝塞尔曲线
    ''' </summary>
    Public Class BezierQuadraticElement
        Inherits BezierCurveElement
        ''' <summary>
        ''' 二次贝塞尔曲线的控制点
        ''' </summary>
        Public Property ControlPoint As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
    End Class
    ''' <summary>
    ''' 表示三次贝塞尔曲线
    ''' </summary>
    Public Class BezierCubicElement
        Inherits BezierCurveElement
        ''' <summary>
        ''' 三次贝塞尔曲线的控制点 1
        ''' </summary>
        Public Property ControlPoint1 As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
        ''' <summary>
        ''' 三次贝塞尔曲线的控制点 2
        ''' </summary>
        Public Property ControlPoint2 As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
    End Class
    ''' <summary>
    ''' 表示自定义的贝塞尔曲线
    ''' </summary>
    Public Class BezierCurveCustomElement
        Inherits BezierCurveElement
        ''' <summary>
        ''' 贝塞尔曲线的控制点
        ''' </summary>
        Public Property ControlPoints As PropertyBinder(Of Vector2()) = New ManualPropertyBinder(Of Vector2())
        ''' <summary>
        ''' 根据高次贝塞尔曲线的公式生成折线的顶点
        ''' </summary>
        ''' <param name="t">用0到1表示的生成进度值</param>
        Public Overridable Function GetVertex(t As Single) As Vector2
            Dim p = ControlPoints.Value, n = p.Length, sum As Vector2, t2 = 1.0F - t
            For i = 0 To n
                sum += CSng(N2Math.Combination(n, i) * (t2 ^ (n - i)) * (t ^ i)) * p(i)
            Next
        End Function
    End Class
    ''' <summary>
    ''' 实心的图形元素
    ''' </summary>
    Public MustInherit Class FilledGeometryElement
        Inherits GeometryElement
        ''' <summary>
        ''' 填充色。可为空。
        ''' </summary>
        Public Property Fill As PropertyBinder(Of Color) = New ManualPropertyBinder(Of Color)
    End Class
    ''' <summary>
    ''' 代表拥有边线的实心矩形
    ''' </summary>
    Public Class RectangleElement
        Inherits FilledGeometryElement
    End Class
    ''' <summary>
    ''' 代表拥有边线的实心椭圆
    ''' </summary>
    Public Class EllipseElement
        Inherits FilledGeometryElement
    End Class
    ''' <summary>
    ''' 代表拥有边线的实心三角形
    ''' </summary>
    Public Class TriangleElement
        Inherits FilledGeometryElement
        ''' <summary>
        ''' 三角形的第 1 个点
        ''' </summary>
        Public Property Point1 As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
        ''' <summary>
        ''' 三角形的第 2 个点
        ''' </summary>
        Public Property Point2 As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
        ''' <summary>
        ''' 三角形的第 3 个点
        ''' </summary>
        Public Property Point3 As PropertyBinder(Of Vector2) = New ManualPropertyBinder(Of Vector2)
    End Class
End Namespace
