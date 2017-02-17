Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Effects
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.ParticleSystemViews
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class SparkParticleSystemRenderer

    Sub New(view As SparkParticleSystemView)
        MyBase.New(view)
    End Sub

    Friend Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        Debug.WriteLine("准备火花资源")
        DirectCast(View, SparkParticleSystemView).Data.Value.RemoveFromGameCanvasCallback = AddressOf View.RemoveFromGameCanvas
    End Sub

    Friend Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim drawingSession = args.DrawingSession
        Dim SparkSys = DirectCast(View, SparkParticleSystemView).Data.Value
        ' 同时绘制多组对象时需要注意：
        ' 如果要绘制的对象没有共用的形状，
        ' 则必须先绘制到 CanvasCommandList 上面，然后绘制到 args.DrawingSession。
        ' 否则会遇到平面变换 Bug。
        Using cl = New CanvasCommandList(drawingSession)
            Using ds = cl.CreateDrawingSession
                For Each part In SparkSys.Particles
                    ds.FillRectangle(New Rect(part.Location.ToPoint, New Size(part.SparkSize, part.SparkSize)), part.SparkColor.AsWindowsColor)
                Next
            End Using
            drawingSession.DrawImage(cl)
        End Using
    End Sub

    Public Overrides Sub DisposeResources()
        Debug.WriteLine("释放全部火花资源")
    End Sub
End Class