Imports Nukepayload2.N2Engine.Core.ParticleSystem

Public MustInherit Class ParticleSystemView(Of T As IParticleSystem)
    Inherits GameElement

    Public ReadOnly Property Data As New PropertyBinder(Of T)

End Class