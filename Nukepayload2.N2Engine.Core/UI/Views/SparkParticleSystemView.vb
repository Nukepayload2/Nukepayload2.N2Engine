Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.ParticleSystem

Namespace UI.Views

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
End Namespace