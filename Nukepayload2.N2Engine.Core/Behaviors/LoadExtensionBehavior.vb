Imports Nukepayload2.N2Engine.Extensibility
Imports Nukepayload2.N2Engine.UI.Elements

Namespace Behaviors
    Public Class LoadExtensionBehavior(Of TExtendableVisual As {GameVisual, IExtendable}, TExtension As IGameExtensionInfo)
        Inherits BehaviorBase(Of TExtendableVisual)

        Sub New(extension As TExtension)
            Me.Extension = extension
        End Sub

        Public Property Extension As TExtension

        Protected Overrides Sub OnAttached(visual As TExtendableVisual)
            MyBase.OnAttached(visual)
            visual.Extensions.Add(Extension)
        End Sub
    End Class

End Namespace
