Imports System.Numerics
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Nukepayload2.N2Engine.UI.Views
Imports Nukepayload2.N2Engine.UWP.Marshal

Friend Class SparkParticleSystemRenderer

    Sub New(view As SparkParticleSystemView)
        MyBase.New(view)
    End Sub

    Protected Overrides Sub OnCreateResources(sender As CanvasAnimatedControl, args As CanvasCreateResourcesEventArgs)
        Debug.WriteLine("准备火花资源")
    End Sub

    Protected Overrides Sub OnDraw(sender As ICanvasAnimatedControl, args As CanvasAnimatedDrawEventArgs)
        Dim ds = args.DrawingSession
        Dim SparkSys = View.Data.Value
        For Each part In SparkSys.Particles
            ds.FillRectangle(New Rect(part.Location.ToPoint, New Size(part.SparkSize, part.SparkSize)), part.SparkColor.AsWindowsColor)
        Next
    End Sub

    Public Overrides Sub DisposeResources()
        MyBase.DisposeResources()
        Debug.WriteLine("释放全部火花资源")
    End Sub
End Class