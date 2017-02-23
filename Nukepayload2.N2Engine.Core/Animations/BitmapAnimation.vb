Imports Nukepayload2.N2Engine.Resources

Namespace Animations
    ''' <summary>
    ''' 表示切换位图的离散动画。
    ''' </summary>
    Public Class BitmapDiscreteAnimation
        Implements IEnumerable(Of BitmapResource)

        ''' <summary>
        ''' 仅使用图像列表初始化
        ''' </summary>
        ''' <param name="Frames">图像列表</param>
        Sub New(Frames As IEnumerable(Of BitmapResource))
            Me.Frames.AddRange(Frames)
        End Sub
        ''' <summary>
        ''' 已经解析过的图像。以帧的形式存放。
        ''' </summary>
        Public ReadOnly Property Frames As New List(Of BitmapResource)
        ''' <summary>
        ''' （未实施）透明度
        ''' </summary>
        Public Property Opacity As Single?
        ''' <summary>
        ''' （未实施）重写默认的循环行为
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
        Public ReadOnly Property NextAnimation As BitmapDiscreteAnimation

        Public Function GetEnumerator() As IEnumerator(Of BitmapResource) Implements IEnumerable(Of BitmapResource).GetEnumerator
            Return New BitmapEnumerator(Me)
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        Private Class BitmapEnumerator
            Implements IEnumerator(Of BitmapResource)

            Dim oriAnim As BitmapDiscreteAnimation
            Dim curAnim As BitmapDiscreteAnimation
            ' 当前动画处于第几帧
            Dim index As Integer = -1

            Sub New(anim As BitmapDiscreteAnimation)
                oriAnim = anim
                curAnim = anim
            End Sub

            Public ReadOnly Property Current As BitmapResource Implements IEnumerator(Of BitmapResource).Current
                Get
                    Return curAnim.Frames(index)
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
            End Sub

            Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
                If index < curAnim.Frames.Count - 1 Then
                    index += 1
                    Return True
                Else
                    If curAnim.NextAnimation IsNot Nothing Then
                        curAnim = curAnim.NextAnimation
                        index = -1
                        Return MoveNext
                    Else
                        Return False
                    End If
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