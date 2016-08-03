Namespace Animations
    ''' <summary>
    ''' 动画循环相关
    ''' </summary>
    Public Class AnimatioLoopInformation
        ''' <summary>
        ''' 一共最多播放多少遍
        ''' </summary>
        Public Property LoopCount% = -1
        ''' <summary>
        ''' 在<see cref="BitmapAnimation.Frames"/>的起始下标, 从0开始
        ''' </summary>
        Public Property LoopStart%
        ''' <summary>
        ''' 在<see cref="BitmapAnimation.Frames"/>的终止下标, 最大不要超过上界，否则会发生<see cref="IndexOutOfRangeException"/>或者<see cref="NullReferenceException"/>。
        ''' </summary>
        Public Property LoopEnd%
        ''' <summary>
        ''' 当播放过的帧数增长多少时才会切换到下一张图像
        ''' </summary>
        Public Property Rate%
    End Class
End Namespace
