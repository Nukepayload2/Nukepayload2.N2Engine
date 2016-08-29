Imports Nukepayload2.N2Engine.Resources

Namespace Animations
    ''' <summary>
    ''' 带有动态信息的动画。但由于动态性能较低，此类动画中的动态信息多用于Mod。
    ''' </summary>
    Public Class DynamicBitmapAnimation
        Inherits BitmapAnimation
        ''' <summary>
        ''' 仅使用图像列表初始化
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        Sub New(Frames As IList(Of BitmapResource))
            MyBase.New(Frames)
        End Sub
        ''' <summary>
        ''' 创建带二维变换和透明度的动画
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        ''' <param name="Transform">二位变换</param>
        ''' <param name="Opacity">透明度。0透明，1不透明</param>
        Sub New(Frames As IList(Of BitmapResource), Transform As Matrix3x2, Opacity!)
            MyBase.New(Frames, Transform, Opacity)
        End Sub
        ''' <summary>
        ''' 创建带二维变换和透明度的动画到带有透视的空间中
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        ''' <param name="Transform">二位变换</param>
        ''' <param name="Opacity">透明度。0透明，1不透明</param>
        ''' <param name="Perspective">三维场景的透视</param>
        Sub New(Frames As IList(Of BitmapResource), Transform As Matrix3x2, Opacity!, Perspective As Matrix4x4)
            MyBase.New(Frames, Transform, Opacity, Perspective)
        End Sub
        ''' <summary>
        ''' 用于存储拓展的属性。使用<see cref="Dynamic.ExpandoObject"/>存放内容。使用此成员时请设置Option Strict Off (Visual Basic) 或者用 dynamic 关键字 (Visual C#)
        ''' </summary>
        Public ReadOnly Property Tag As New Dynamic.ExpandoObject
    End Class
End Namespace