Option Strict Off

Namespace N2Math
    Public Module Vector2Extensions
        <Extension>
        Public Sub LimitMag(ByRef this As Vector2, lUponNum As Single)
            Dim tempMag As Single = this.Length
            If tempMag > lUponNum Then
                this = New Vector2(this.X * (lUponNum / tempMag), this.Y * (lUponNum / tempMag))
            End If
        End Sub
        <Extension>
        Public Sub SetMag(ByRef this As Vector2, sPutNum As Single)
            Dim tempMag As Single = this.Length
            If tempMag > 0 Then
                this = New Vector2(this.X * (sPutNum / tempMag), this.Y * (sPutNum / tempMag))
            End If
        End Sub
        <Extension>
        Public Function WithLength(this As Vector2, sPutNum As Single) As Vector2
            Dim tempMag As Single = this.Length
            If tempMag > 0 Then
                this = New Vector2(this.X * (sPutNum / tempMag), this.Y * (sPutNum / tempMag))
            End If
            Return this
        End Function
        <Extension>
        Public Sub Rotate(ByRef this As Vector2, gAngle As Single)
            Dim x1, y1 As Single
            x1 = this.X * Math.Cos(gAngle) - this.Y * Math.Sin(gAngle)
            y1 = this.Y * Math.Cos(gAngle) + this.X * Math.Sin(gAngle)
            this.X = x1
            this.Y = y1
        End Sub
        <Extension>
        Public Function RotateNew(this As Vector2, gAngle As Single) As Vector2
            Dim R As New Vector2
            Dim x1, y1 As Single
            x1 = this.X * Math.Cos(gAngle) - this.Y * Math.Sin(gAngle)
            y1 = this.Y * Math.Cos(gAngle) + this.X * Math.Sin(gAngle)
            R.X = x1
            R.Y = y1
            Return R
        End Function
        ''' <summary>
        ''' 进行 3x2 矩阵变换, 返回变换后的点。
        ''' </summary>
        ''' <param name="this">要变换的点</param>
        ''' <param name="matrix">二维变换矩阵</param>
        <Extension>
        Public Function ApplyTransform(this As Vector2, matrix As Matrix3x2) As Vector2
            this.X = this.X * matrix.M11 + this.Y * matrix.M21 + matrix.M31
            this.Y = this.X * matrix.M12 + this.Y * matrix.M22 + matrix.M32
            Return this
        End Function
    End Module

End Namespace
