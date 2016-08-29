Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.Resources

Namespace Animations
    ''' <summary>
    ''' 表示位图动画
    ''' </summary>
    Public Class BitmapAnimation
        ''' <summary>
        ''' 仅使用图像列表初始化
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        Sub New(Frames As IList(Of BitmapResource))
            Me.Frames = Frames
        End Sub
        ''' <summary>
        ''' 创建带二维变换和透明度的动画
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        ''' <param name="Transform">二位变换</param>
        ''' <param name="Opacity">透明度。0透明，1不透明</param>
        Sub New(Frames As IList(Of BitmapResource), Transform As Matrix3x2, Opacity!)
            MyClass.New(Frames)
            Me.Transform = New Matrix3x2?(Transform)
            Me.Opacity = Opacity
        End Sub
        ''' <summary>
        ''' 创建带二维变换和透明度的动画到带有透视的空间中
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        ''' <param name="Transform">二位变换</param>
        ''' <param name="Opacity">透明度。0透明，1不透明</param>
        ''' <param name="Perspective">三维场景的透视</param>
        Sub New(Frames As IList(Of BitmapResource), Transform As Matrix3x2, Opacity!, Perspective As Matrix4x4)
            MyClass.New(Frames, Transform, Opacity)
            Me.Perspective = New Matrix4x4?(Perspective)
        End Sub
        ''' <summary>
        ''' 已经解析过的图像。以帧的形式存放。
        ''' </summary>
        Public Property Frames As IList(Of BitmapResource)
        ''' <summary>
        ''' 透明度
        ''' </summary>
        Public Property Opacity!
        ''' <summary>
        ''' 二维的平移，旋转和缩放。
        ''' </summary>
        Public Property Transform As Matrix3x2?
        ''' <summary>
        ''' 透视视角。注意: 如果使用这个功能就不能使用Effect功能了。
        ''' </summary>
        Public Property Perspective As Matrix4x4?
        ''' <summary>
        ''' 重写默认的循环行为
        ''' </summary>
        Public Property LoopInformation As AnimatioLoopInformation
        ''' <summary>
        ''' 计算当前所处的下标
        ''' </summary>
        Public Function GetImageIndex%(FrameCount%)
            Return If(LoopInformation Is Nothing, FrameCount Mod Frames.Count, LoopInformation.LoopStart + (FrameCount \ LoopInformation.Rate) Mod (LoopInformation.LoopEnd - LoopInformation.LoopStart + 1))
        End Function
        ''' <summary>
        ''' 判断是否已经播放完成了
        ''' </summary>
        Public ReadOnly Property IsEnded As Boolean
            Get
                Return If(LoopInformation Is Nothing, False, LoopInformation.LoopCount >= 0 AndAlso ((FrameCount \ LoopInformation.Rate) \ (LoopInformation.LoopEnd - LoopInformation.LoopStart + 1) > LoopInformation.LoopCount))
            End Get
        End Property
        ''' <summary>
        ''' 如果不是空值，则会产生尾焰
        ''' </summary>
        Public Property Trailer As Trailer
        ''' <summary>
        ''' 当视图报告此动画播放完毕时切换到这些动画继续播放。
        ''' </summary>
        Public ReadOnly Property NextAnimations As ICollection(Of BitmapAnimation)
        ''' <summary>
        ''' 如果切换到了下一个动画，则用这个属性记录切换到了哪个动画
        ''' </summary>
        ''' <returns></returns>
        Public Property NextAnimationIndex% = -1
        ''' <summary>
        ''' 更新画面了多少次。
        ''' </summary>
        Public Property FrameCount% = 0
        ''' <summary>
        ''' 更新画面的命令
        ''' </summary>
        Public Property UpdateCommand As New SimpleCommand(Sub() FrameCount = (FrameCount + 1) Mod Frames.Count)
    End Class
End Namespace