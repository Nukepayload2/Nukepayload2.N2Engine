Imports System.Reflection

Namespace Battle
    Public MustInherit Class CardManger
        Implements ICardManger
        Protected MustOverride Function LoadModAssemblies() As IEnumerable(Of Assembly)
        Public Shared ReadOnly Property Current() As ICardManger
        Protected Sub SetCardTypesFromAssembly(asm As IEnumerable(Of Assembly))
            For Each mem In asm
                For Each htp In mem.ExportedTypes
                    Dim tp = htp.GetTypeInfo
                    If tp.IsClass AndAlso Not tp.IsAbstract Then
                        If GetType(IHandCard).GetTypeInfo.IsAssignableFrom(tp) Then
                            Dim o = Activator.CreateInstance(htp)
                            AvailableHandCardTypes.Add(htp)
                        End If
                    End If
                Next
            Next
        End Sub
        Sub New()
            SetCardTypesFromAssembly(LoadModAssemblies().Concat({[GetType].GetTypeInfo.Assembly}))
            _Current = Me
        End Sub
        ''' <summary>
        ''' 创建Available的HandCard的Default实例
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property AvailableHandCard As IEnumerable(Of IHandCard) Implements ICardManger.AvailableHandCard
            Get
                Return From c In AvailableHandCardTypes Select CreateHandCard(c)
            End Get
        End Property
        Dim m_AvailableHandCardTypes As New List(Of Type)
        Public ReadOnly Property AvailableHandCardTypes As IList(Of Type) Implements ICardManger.AvailableHandCardTypes
            Get
                Return m_AvailableHandCardTypes
            End Get
        End Property
        Public Function RandomHandCard() As IHandCard Implements ICardManger.RandomHandCard
            If AvailableHandCardTypes.Count > 0 Then
                Return CreateHandCard(AvailableHandCardTypes(RndEx(0, AvailableHandCardTypes.Count - 1)))
            Else
                Return Nothing
            End If
        End Function
        Public Function CreateHandCard(Card As Type) As IHandCard Implements ICardManger.CreateHandCard
            Return DirectCast(Activator.CreateInstance(Card), IHandCard)
        End Function
    End Class
End Namespace
