Imports Nukepayload2.N2Engine.ParticleSystem

Namespace UI.Views

    Public Class SparkParticleSystemView
        Inherits ParticleSystemView(Of SparkParticleSystem)
        Public Overrides Property UpdateAction As Action
            Get
                Return Sub()
                           MyBase.UpdateAction.Invoke()
                           Data.Value.Update()
                       End Sub
            End Get
            Set(value As Action)
                MyBase.UpdateAction = value
            End Set
        End Property
    End Class
End Namespace