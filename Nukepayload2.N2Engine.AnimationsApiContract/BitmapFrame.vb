Imports Nukepayload2.N2Engine.Resources

Namespace Animations
    ''' <summary>
    ''' 表示共用一张图像的多个帧。
    ''' </summary>
    Public Class BitmapFrames
        Implements IEnumerable(Of BitmapResource)

        ''' <param name="bitmap">显示的位图</param>
        ''' <param name="length">此图像需要重复多少次 （至少1次）</param>
        Sub New(bitmap As BitmapResource, Optional length As Integer = 1)
            Me.Bitmap = bitmap
            Me.Length = length
            If length < 1 Then
                Throw New ArgumentOutOfRangeException("长度不得小于1", NameOf(length))
            End If
        End Sub

        ''' <summary>
        ''' 显示的位图
        ''' </summary>
        Public ReadOnly Property Bitmap As BitmapResource
        ''' <summary>
        ''' 此图像需要重复多少次
        ''' </summary>
        Public ReadOnly Property Length As Integer

        Public Function GetEnumerator() As IEnumerator(Of BitmapResource) Implements IEnumerable(Of BitmapResource).GetEnumerator
            Return New BitmapEnumerator(Me)
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        Public Class BitmapEnumerator
            Implements IEnumerator(Of BitmapResource)

            Dim resource As BitmapFrames
            Dim index As Integer = -1

            Sub New(resource As BitmapFrames)
                Me.resource = resource
            End Sub

            Public ReadOnly Property Current As BitmapResource Implements IEnumerator(Of BitmapResource).Current
                Get
                    If index <= resource.Length Then
                        Return resource.Bitmap
                    End If
                    Return Nothing
                End Get
            End Property

            Private ReadOnly Property IEnumerator_Current As Object Implements IEnumerator.Current
                Get
                    Return Current
                End Get
            End Property

            Public Sub Reset() Implements IEnumerator.Reset
                index = -1
            End Sub

            Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
                index += 1
                Return index < resource.Length
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