Imports Nukepayload2.N2Engine.Core.ParticleSystem

Public Class SparkParticleSystemView
    Inherits ParticleSystemView(Of SparkParticleSystem)
    Public Overrides Property UpdateCommand As IGameCommand
        Get
            Return New SimpleCommand(AddressOf Data.Value.Update)
        End Get
        Set(value As IGameCommand)
            Throw New NotSupportedException("粒子系统的更新是固定的")
        End Set
    End Property
End Class