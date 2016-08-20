Imports Nukepayload2.N2Engine.Core.ParticleSystem

Public Class SparkParticleSystemView
    Inherits ParticleSystemView(Of SparkParticleSystem)
    Public Overrides Property UpdateCommand As IGameCommand
        Get
            Return New SimpleCommand(Sub()
                                         MyBase.UpdateCommand.Execute()
                                         Data.Value.Update()
                                     End Sub)
        End Get
        Set(value As IGameCommand)
            MyBase.UpdateCommand = value
        End Set
    End Property
End Class