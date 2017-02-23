Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.ParticleSystemViews

    Public MustInherit Class ParticleSystemView(Of T As IParticleSystem)
        Inherits GameElement

        Public ReadOnly Property Data As New PropertyBinder(Of T)

    End Class
End Namespace