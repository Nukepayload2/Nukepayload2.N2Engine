Imports Nukepayload2.N2Engine.Foundation
Imports Nukepayload2.N2Engine.ParticleSystems
Imports Nukepayload2.N2Engine.UI.Elements

Namespace UI.ParticleSystemViews

    Public MustInherit Class ParticleSystemView(Of T As IParticleSystem)
        Inherits GameElement

        Public ReadOnly Property Data As New PropertyBinder(Of T)

        Public Overrides Property UpdateAction As Action(Of UpdatingEventArgs)
            Get
                Return Sub(args)
                           MyBase.UpdateAction.Invoke(args)
                           Data.Value.Update()
                       End Sub
            End Get
            Set(value As Action(Of UpdatingEventArgs))
                MyBase.UpdateAction = value
            End Set
        End Property
    End Class
End Namespace