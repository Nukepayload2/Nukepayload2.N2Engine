Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.ParticleSystemViews

    Public MustInherit Class ParticleSystemView(Of T As IParticleSystem)
        Inherits GameElement

        Public ReadOnly Property Data As New PropertyBinder(Of T)

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