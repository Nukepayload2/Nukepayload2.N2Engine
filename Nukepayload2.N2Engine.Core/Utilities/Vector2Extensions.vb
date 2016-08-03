Option Strict Off

Namespace Numerics
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
    End Module

End Namespace
