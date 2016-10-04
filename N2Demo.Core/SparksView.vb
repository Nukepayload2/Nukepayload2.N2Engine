Imports System.Numerics
Imports Nukepayload2.N2Engine.UI.Elements
Imports Nukepayload2.N2Engine.UI.Views

Public Class SparksView
    Inherits GameCanvas

    Dim sparks As New SparkParticleSystemView
    Public Property SparksData As New SparksViewModel

    Sub New()
        sparks.Data.Bind(Function() SparksData.SparkSys)
        SparksData.RemoveFromVisualTree.Bind(Function() AddressOf sparks.RemoveFromGameCanvas)

        Add(sparks)
    End Sub
    ''' <summary>
    ''' 平台特定实现点击此视图时，调用此方法
    ''' </summary>
    Public Sub OnTapped(pos As Vector2)
        SparksData.SparkSys.Location = pos
    End Sub
End Class