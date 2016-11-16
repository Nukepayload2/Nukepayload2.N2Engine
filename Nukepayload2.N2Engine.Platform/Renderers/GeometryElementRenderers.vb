Imports Nukepayload2.N2Engine.Platform
Imports Nukepayload2.N2Engine.UI.Elements

''' <summary>
''' 线段的渲染器
''' </summary>
<PlatformImpl(GetType(LineElement))>
Partial Friend Class LineElementRenderer
    Inherits GameElementRenderer

End Class

''' <summary>
''' 折线的渲染器
''' </summary>
<PlatformImpl(GetType(PolylineElement))>
Partial Friend Class PolylineElementRenderer
    Inherits GameElementRenderer

End Class

''' <summary>
''' 二次贝塞尔曲线的渲染器
''' </summary>
<PlatformImpl(GetType(BezierQuadraticElement))>
Partial Friend Class BezierQuadraticElementRenderer
    Inherits GameElementRenderer

End Class


''' <summary>
''' 三次贝塞尔曲线的渲染器
''' </summary>
<PlatformImpl(GetType(BezierCubicElement))>
Partial Friend Class BezierCubicElementRenderer
    Inherits GameElementRenderer

End Class


''' <summary>
''' n次贝塞尔曲线的渲染器
''' </summary>
<PlatformImpl(GetType(BezierCurveCustomElement))>
Partial Friend Class BezierCurveCustomElementRenderer
    Inherits GameElementRenderer

End Class

''' <summary>
''' 矩形的渲染器
''' </summary>
<PlatformImpl(GetType(RectangleElement))>
Partial Friend Class RectangleElementRenderer
    Inherits GameElementRenderer

End Class

''' <summary>
''' 椭圆的渲染器
''' </summary>
<PlatformImpl(GetType(EllipseElement))>
Partial Friend Class EllipseElementRenderer
    Inherits GameElementRenderer

End Class

''' <summary>
''' 三角形的渲染器
''' </summary>
<PlatformImpl(GetType(TriangleElement))>
Partial Friend Class TriangleElementRenderer
    Inherits GameElementRenderer

End Class