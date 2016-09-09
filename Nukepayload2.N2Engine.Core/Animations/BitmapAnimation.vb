Imports Nukepayload2.N2Engine.Resources

Namespace Animations
    ''' <summary>
    ''' 表示位图动画
    ''' </summary>
    Public Class BitmapAnimation
        Implements IEnumerable(Of BitmapResource)

        ''' <summary>
        ''' 仅使用图像列表初始化
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        Sub New(Frames As IList(Of BitmapFrame))
            Me.Frames = Frames
        End Sub
        ''' <summary>
        ''' 创建带二维变换和透明度的动画
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        ''' <param name="Transform">二位变换</param>
        ''' <param name="Opacity">透明度。0透明，1不透明</param>
        Sub New(Frames As IList(Of BitmapFrame), Transform As Matrix3x2, Opacity!)
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
        Sub New(Frames As IList(Of BitmapFrame), Transform As Matrix3x2, Opacity!, Perspective As Matrix4x4)
            MyClass.New(Frames, Transform, Opacity)
            Me.Perspective = New Matrix4x4?(Perspective)
        End Sub
        ''' <summary>
        ''' 已经解析过的图像。以帧的形式存放。
        ''' </summary>
        Public Property Frames As IList(Of BitmapFrame)
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
        ''' 如果不是空值，则会产生尾焰
        ''' </summary>
        Public Property Trailer As Trailer
        ''' <summary>
        ''' 当视图报告此动画播放完毕时切换到这些动画继续播放。这会影响到枚举图像的行为。
        ''' </summary>
        Public ReadOnly Property NextAnimation As BitmapAnimation

        Public Function GetEnumerator() As IEnumerator(Of BitmapResource) Implements IEnumerable(Of BitmapResource).GetEnumerator
            Return New BitmapEnumerator(Me)
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        Private Class BitmapEnumerator
            Implements IEnumerator(Of BitmapResource)

            Dim oriAnim As BitmapAnimation
            Dim curAnim As BitmapAnimation
            ' 当前动画处于第几帧
            Dim index As Integer = -1
            Dim curFrameEnum As IEnumerator(Of BitmapResource)

            Sub New(anim As BitmapAnimation)
                oriAnim = anim
                curAnim = anim
            End Sub

            Public ReadOnly Property Current As BitmapResource Implements IEnumerator(Of BitmapResource).Current
                Get
                    Return curFrameEnum?.Current
                End Get
            End Property

            Private ReadOnly Property IEnumerator_Current As Object Implements IEnumerator.Current
                Get
                    Return Current
                End Get
            End Property

            Public Sub Reset() Implements IEnumerator.Reset
                index = -1
                curAnim = oriAnim
                curFrameEnum = Nothing
            End Sub

            Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
                If index >= 0 AndAlso curFrameEnum.MoveNext() Then
                    Return True
                Else
                    index += 1
                    If curAnim.Frames.Count <= index Then
                        If curAnim.NextAnimation Is Nothing Then
                            Return False
                        Else
                            curAnim = curAnim.NextAnimation
                            index = -1
                            Return MoveNext
                        End If
                    End If
                    curFrameEnum = curAnim.Frames(index).GetEnumerator
                    Return curFrameEnum.MoveNext()
                End If
            End Function

#Region "IDisposable Support"
            Private disposedValue As Boolean ' 要检测冗余调用


            ' IDisposable
            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not disposedValue Then
                    If disposing Then
                        ' TODO: 释放托管状态(托管对象)。
                    End If

                    ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
                    ' TODO: 将大型字段设置为 null。
                End If
                disposedValue = True
            End Sub

            ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
            'Protected Overrides Sub Finalize()
            '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
            '    Dispose(False)
            '    MyBase.Finalize()
            'End Sub

            ' Visual Basic 添加此代码以正确实现可释放模式。
            Public Sub Dispose() Implements IDisposable.Dispose
                ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
                Dispose(True)
                ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
                ' GC.SuppressFinalize(Me)
            End Sub
#End Region
        End Class
    End Class
End Namespace