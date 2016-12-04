Imports Windows.Media.Audio
Namespace Media
    Public Class AudioState
        Sub New()

        End Sub
        Sub New(submixNode As AudioSubmixNode, fileInputNode As AudioFileInputNode)
            Me.SubmixNode = submixNode
            Me.FileInputNode = fileInputNode
        End Sub

        Public Property SubmixNode As AudioSubmixNode
        Public Property FileInputNode As AudioFileInputNode

    End Class
End Namespace