Imports Nukepayload2.N2Engine.Core

Public Class SparksView
    Inherits GameCanvas

    Dim sparks As New SparkParticleSystemView
    Public Property SparksData As New SparksViewModel

    Sub New()
        sparks.Data.Bind(Function() SparksData.SparkSys)
        SparksData.RemoveFromVisualTree.Bind(Function() AddressOf sparks.RemoveFromGameCanvas)

        Children.Add(sparks)
    End Sub
End Class