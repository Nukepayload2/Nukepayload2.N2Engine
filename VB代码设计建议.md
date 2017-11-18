## VB ������ƽ���
���ĵ���ƽ���������ģʽ�ͱ��ģ�͡�����ѭ��Щ����Ҳ����Ӱ�� PR ��ͨ�������������Ҿ�����������Щ�����˼���ٱ�д���롣

ע��: C# ������ƽ��鿴 Channel 9 ����Ƶ�Σ����ﲻ��׸����
- [Command/Memento patterns](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-CommandMemento)
- [Strategy pattern](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-Strategy)
- [Template Method pattern](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-Template-Method)
- [Observer/Publish-Subscribe patterns](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-Observer-and-Publish-Subscribe)
- [Singleton pattern](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-Singleton)
- [Factory patterns](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-Factories)
- [Adapter/Facade patterns](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-AdapterFaade)
- [Decorator pattern](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Design-Patterns-Decorator)
- [Dependency-Injection](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Dependency-Injection)
- [Functional Programming in C#](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Functional-Programming-in-CSharp)

### ����/������Ƭģʽ
#### ����
���������㴴��һ����Ϸ����Ҫ���Խ�ɫ�����Ĺ��ܡ������Ľ������һ���ı��������������������һ����ť���ڳ��������ı����һ����ť����������������д�������ɶ��Ĵ��봦���������⣿
#### ����
�ܶ���Ϸ��ƽ̳��������̵ģ�

- ��ȷ��������ť����¼�����ʱ���ı�������ݶ�ȡ���������浽��Ϸ�浵���ݿ⡣���һ��������¼����Ϸ�浵���ݿ⡣
- �ڳ�����ť����¼�����ʱ�����ݿ��ѯ������¼���ҵ�֮��ȡ��ֵ�������ݿ��Ƴ���Ȼ��ֵ���ı���� `Text` ���ԡ�

__VB__
```vb.net
Private Async Sub BtnChangeName_Click(sender As Object, e As EventArgs) Handles BtnChangeName.Click
    Dim name = TxtName.Text
    With _gameSaveService.Character
        .Name = name
        .NameHistory.Add(name)
    End With
    Await _gameSaveService.SaveAsync()
End Sub

Private Async Sub BtnUndoName_Click(sender As Object, e As EventArgs) Handles BtnUndoName.Click
    With _gameSaveService.Character
        If .NameHistory.Any Then
            TxtName.Text = .NameHistory.Last
            .NameHistory.RemoveAt(.NameHistory.Count - 1)
            Await _gameSaveService.SaveAsync()
        End If
    End With
End Sub
```

#### ����
������ʹ���˼�����Ƭģʽ����ĳһ��ʱ���ĳ���������Ϣ�����������ڻָ�������û��������ģʽ�Դ����һ����װ�������������⣺

- �߼��������û�������ϡ������û������ܵ��´��뼸���������á�
- ���벻�ܵ�Ԫ���ԡ�
- ��ʷ��¼�߼������״̬ά��������ϡ�

#### �������
- �������װҵ���߼�����Ҫ�߼��ŵ� `Execute` �����ڡ�
- ����ʷ��¼��������Ƭģʽ�������ƶ��������е� `Undo` �����ڡ�

����Ĵ���������ģʽ��װ�˸����ͳ��������߼����Լ����������߼���

__VB__
```vb.net
Public Class ChangeNameCommand
    Private _dataContext As Character

    Public Sub New(dataContext As Character)
        _dataContext = dataContext
    End Sub

    Public Sub Execute(param As String)
        _dataContext.Name = param
        _dataContext.NameHistory.Add(name)
    End Sub

    Public Function Undo(ByRef name As String) As Boolean
        If _mem.Any Then
            _dataContext.Name = _dataContext.NameHistory.Last
            name = _dataContext.Name 
            _dataContext.NameHistory.RemoveAt(_dataContext.NameHistory.Count - 1)
        End If
    End Function
End Class

Public Class SaveGameCommand
    Public Async Function ExecuteAsync(parameter As GameSaveService) As Task
        Await parameter.SaveAsync()
    End Function
End Class
```

__VB__
```vb.net
Private ReadOnly Property ChangeNameCommand As New ChangeNameCommand(_gameSaveService.Character)
Private ReadOnly Property SaveGameCommand As New SaveGameCommand(_gameSaveService)

Private Async Sub BtnChangeName_Click(sender As Object, e As EventArgs) Handles BtnChangeName.Click
    ChangeNameCommand.Execute(TxtName.Text)
    Await SaveGameCommand.ExecuteAsync(_gameSaveService)
End Sub

Private Async Sub BtnUndoName_Click(sender As Object, e As EventArgs) Handles BtnUndoName.Click
    If ChangeNameCommand.Undo(TxtName.Text) Then
        Await SaveGameCommand.ExecuteAsync(_gameSaveService)
    End If
End Sub
```

�����޸�֮����ͼ����������ˣ�ҵ���߼�������ȫ�����ˡ�
��Ҫ���в���ʱ��ֻҪ�ȶ�������е�Ԫ�����ٱ�д�ٵ�������� UI �����ҳ������Ǳ�ڵ����⡣

#### �������
�� Mvvm ģʽ�� Mvc ģʽ�У�����ͨ��ʵ��һ������������ӿڽ�һ���������ݺ���ͼ��
���磬�� UWP, WPF �� Xamarin.Forms ��ʵ�� `System.Windows.Input.ICommand`�� 
���� N2Engine ��ʵ�� `Nukepayload2.N2Engine.Input.IGameCommand`��
������ʼ������Ĵ����Լ���ʼ���������Ĵ��뽫����ȡ�� ViewModel �� Controller �С�

### ����ģʽ
���ģʽ����һ�ֹ��ܵĲ�ͬʵ�֡�����ʱ���Զ�̬�滻�����ʵ�֡�
ͨ����������Ϊ��ͬ���ܵĲ�ͬʵ�ֱַ𴴽��࣬Ȼ����ȡ�����Ľӿڡ�
ʹ��ʱʹ����ȡ�����Ĺ����ӿڣ�����ֱ�ӵ���ʵ��ʵ�ֹ��ܵ��ࡣ

����Ĵ��붩���������������������������������ھ�����ı���

__VB__
```vb.net
Public Interface ISuperWeapon
    Function Warn() As String
End Interface

Public Class NuclearMissile
    Implements ISuperWeapon

    Public Function Warn() As String Implements ISuperWeapon.Warn
        Return "Warning: Nuclear Missile launched!"
    End Function
End Class

Public Class IronCurtain
    Implements ISuperWeapon

    Public Function Warn() As String Implements ISuperWeapon.Warn
        Return "Warning: Iron Curtain activated!"
    End Function
End Class
```

����Ĵ��붨����һ����Ӫ����Ӫ�ܹ�������������������֪ͨ���ٲ������������ľ����ı���ʹ�ò���ģʽ�����Լ򻯸��ٲ���������������Ĵ��롣�Ժ�����Ҫ��Ӷ��ٸ�����������ʹ�ó��������Ĵ��붼����Ҫ���ġ�

__VB__
```vb.net
Public Class SovietSide
    Inherits Side

    Public Property StrongestSuperWeapon As ISuperWeapon
    
    Public Sub WarnSuperWeapon()
        Adjutant.BeginSpeak(PrimarySuperWeapon?.Warn)
    End Sub

    Public Sub UpgradeSuperWeapon()
       If StrongestSuperWeapon Is Nothing Then
           StrongestSuperWeapon = New IronCurtain
       Else
           StrongestSuperWeapon = New NuclearMissile
       End If
    End Sub
End Class
```
�����޸ĵ�ǰ��ǿ�ĳ��������Ͳ���Ҫ�ı���ó��������Ĵ����ˡ���Ϊ���ó��������Ĵ���ͨ�� `StrongestSuperWeapon` ���á�
Adjutant ��ʹ������������ģ�巽��ģʽʵ�֣���Ϊ������Ҫ������ʱ��̬�任��

### ģ�巽��ģʽ
һ����������һϵ�в����ĹǼܣ���һ���ֲ����Ƴٵ����ࡣʹ�� "��ȡ����" �ع����Կ����������ģʽ��

����Ĵ����ʾһ������ָ���ط�����˵��ĺ˵����侮������˵��Ĳ������£�
- �ж��ܲ��ܷ���˵�
- ������ԣ���׼Ŀ�������
- ����������Ϣ
- ����˵�
- ������䲻�ˣ�������Ҳ�����ָ��λ�÷���˵�

�����ڸ��ඨ���������̣��þ��������ʵ��ÿһ����ϸ�ڡ�

__VB__
```vb.net
Public MustInherit Class NuclearSilo
    Implements ISuperWeaponLauncher

    Public Function TryLaunchANuke(coordinate As Vector2) As Boolean Implements ISuperWeaponLauncher.TryLaunch
        If CanLaunchNuke Then
            SelectTarget(coordinate)
            WarnSuperWeapon()
            DoLaunch()
            Return True
        Else
            NotifyLaunchFailed(coordinate)
            Return False
        End If
    End Function

    Protected MustOverride Function CanLaunchNuke() As Boolean
    Protected MustOverride Sub SelectTarget(coordinate As Vector2) 
    Protected MustOverride Sub WarnSuperWeapon() 
    Protected MustOverride Sub DoLaunch()
    Protected MustOverride Sub NotifyLaunchFailed(coordinate As Vector2) 
End Class
```
�������ô��벻�����ظ������ҽṹ�������ô�������ά����

### �����ߺͶ�����ģʽ
����һ�ַ��ಢ�㲥��Ϣ�����ģʽ������Ϸ�����зǳ����������������û������Ĵ����С�
#### ����
* ������������߲�ֱ��֪���Է�
* ����һ�Σ������߶����յ���Ϣ
* ��ת������˺�·��

#### ʾ������
ʹ�� .NET �¼�ʵ��

����Ĵ����ʾ����ܹ�֪ͨ�����������ڷ��䳬��������

__VB__
```vb.net
Public Class Player
    Public Event LaunchingSuperWeapon As TypedEventHandler(Of Player, SuperWeaponLaunchingEventArgs)

End Class
```

#### Ǳ������: �ڴ�й©
���һ�������ڶ�����ĳ���¼�֮������Ҫ��ʹ�ã������ڴ治�ᱻ���ա���Ϊ����������Ȼ��ʹ���еĶ�����¼������߶����� `Shared` �¼���

����Ĵ�����������ʹ�� `RemoveHandler` ���±��¼�����������������ڲ�����Ҫ��ʹ�õ�������ڴ治�����ա�

```vb.net
Public Class EventLogger
    Private _logs As New ObservableCollection(Of LogItem)

    Public Sub ListenPlayer(target As Player)
        AddHandler target.LaunchingSuperWeapon,
            Sub(sender, args) _logs.Add(New LogItem(sender.Name, args.Description))
    End Sub

End Class
```

#### ���Ǳ������
��Ҫ ʡ�� `RemoveHandler`, ���� `Shared` �¼� ���� ������ʵ��������¼�ʱ����Ҫ�� .NET ���¼���Ҫʵ��һ������ `WeakReference` ���¼���

����Ĵ�����һ��ʹ�������õ��¼���ʵ�֡���Ҫע����ǣ��������̰߳�ȫ�ġ�һ�ּ򵥵��̰߳�ȫ���췽ʽ��ʹ��װ����ģʽ�����ģʽ�ں������͡�

__VB__
```vb.net
Public Class WeakEvent(Of TSender, TEventArgs)
    Private ReadOnly _observers As New List(Of (Observer As WeakReference, Handler As WeakReference(Of TypedEventHandler(Of TSender, TEventArgs))))

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Sub [RemoveHandler](observer As Object, eventHandler As TypedEventHandler(Of TSender, TEventArgs))
        If observer Is Nothing Then
            Throw New ArgumentNullException(NameOf(observer))
        End If

        If eventHandler Is Nothing Then
            Throw New ArgumentNullException(NameOf(eventHandler))
        End If

        For i = _observers.Count - 1 To 0 Step -1
            Dim item = _observers(i)
            If item.Observer.IsAlive Then
                If item.Observer.Target Is observer Then
                    Dim handler As TypedEventHandler(Of TSender, TEventArgs) = Nothing
                    If item.Handler.TryGetTarget(handler) Then
                        If handler Is eventHandler Then
                            _observers.RemoveAt(i)
                        End If
                    Else
                        _observers.RemoveAt(i)
                    End If
                End If
            Else
                _observers.RemoveAt(i)
            End If
        Next
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Sub [AddHandler](observer As Object, eventHandler As TypedEventHandler(Of TSender, TEventArgs))
        If observer Is Nothing Then
            Throw New ArgumentNullException(NameOf(observer))
        End If

        If eventHandler Is Nothing Then
            Throw New ArgumentNullException(NameOf(eventHandler))
        End If

        _observers.Add((New WeakReference(observer), New WeakReference(Of TypedEventHandler(Of TSender, TEventArgs))(eventHandler)))
    End Sub

    ' ע�⣺��������̰߳�ȫ�ġ�RaiseEvent ��ʱ���� AddHandler ���� RemoveHandler��
    Public Sub [RaiseEvent](sender As TSender, e As TEventArgs)
        For i = _observers.Count - 1 To 0 Step -1
            Dim item = _observers(i)
            If item.Observer.IsAlive Then
                Dim handler As TypedEventHandler(Of TSender, TEventArgs) = Nothing
                If item.Handler.TryGetTarget(handler) Then
                    handler(sender, e)
                Else
                    _observers.RemoveAt(i)
                End If
            Else
                _observers.RemoveAt(i)
            End If
        Next
    End Sub

    Public ReadOnly Property ObserverCount As Integer
        <MethodImpl(MethodImplOptions.Synchronized)>
        Get
            Return _observers.Count
        End Get
    End Property
End Class
```

ʹ�������¼�ʱ����ʹ����ʹ�� `RemoveHandler`, Ҳ���ᵼ���ڴ�й©�������ǶԱ� .NET �¼����������¼�����ʹ�� `RemoveHandler` �����������

__VB__
```vb.net
Public Class Player
    Public Event SuperWeaponLaunching As TypedEventHandler(Of Player, EventArgs)
    Public ReadOnly Property SuperWeaponLaunching2 As New WeakEvent(Of Player, EventArgs)

    Public Sub RaiseSuperWeaponLaunching()
        RaiseEvent SuperWeaponLaunching(Me, EventArgs.Empty)
        SuperWeaponLaunching2.RaiseEvent(Me, EventArgs.Empty)
    End Sub
End Class

Public Class SuperWeaponEventLogger
    Sub AddSuperWeaponLaunchingHandler(player As Player)
        AddHandler player.SuperWeaponLaunching, AddressOf Player_SuperWeaponLaunching
    End Sub

    Sub AddSuperWeaponLaunchingHandler2(player As Player)
        player.SuperWeaponLaunching2.AddHandler(Me, AddressOf Player_SuperWeaponLaunching)
    End Sub

    Private Sub Player_SuperWeaponLaunching(p As Player, e As EventArgs)
        Console.WriteLine("Event Fired.")
    End Sub
End Class

Public Module LoggerMemoryLeakTest
    Public Sub TestNetEvent()
        Dim log As New SuperWeaponEventLogger
        Dim player As New Player
        log.AddSuperWeaponLaunchingHandler(player)
        player.RaiseSuperWeaponLaunching()
        log = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
        player.RaiseSuperWeaponLaunching() '���� log Ϊ���ˣ���δ�����Ȼ�� log �����
    End Sub

    Public Sub TestWeakEvent()
        Dim log As New SuperWeaponEventLogger
        Dim player As New Player
        log.AddSuperWeaponLaunchingHandler2(player)
        player.RaiseSuperWeaponLaunching()
        log = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
        player.RaiseSuperWeaponLaunching() '��δ���ʲôҲ�����������Ϊ log �Ѿ���Ϊ���ˡ�
        Console.WriteLine(player.SuperWeaponLaunching2.ObserverCount) '����� 0��
    End Sub
End Module
```

#### �Աȹ۲���ģʽ (���� INotifyPropertyChanged)
- ������֪��������
- ������֪��������
- һ���շ�һ��
- ֱ��ͨ��

### ��ʵ��ģʽ
#### ѡ�� Module �� Shared ���� ���� ��ʵ��ģʽ
#### ʾ������
ʵ�ֵ�ʵ��ģʽ�ж���д����Ŀǰ�Ƚ����е�����Ϊ����ʵ�ֺ�ʵ��ʵ����������

���´�����ʾ��Ϊ����ĵ�ʵ��ģʽʵ�֡�

__VB__
```vb.net
Imports System.Threading

Public MustInherit Class SingletonThreadSafe(Of T As {New, SingletonThreadSafe(Of T)})
    Private Shared _instance As T
    Private Shared ReadOnly _lock As New Object

    Public Shared ReadOnly Property Instance As T
        Get
            If Volatile.Read(_instance) IsNot Nothing Then
                Return Volatile.Read(_instance)
            End If

            SyncLock _lock
                If Volatile.Read(_instance) Is Nothing Then
                    Volatile.Write(_instance, New T)
                End If
            End SyncLock
            Return Volatile.Read(_instance)
        End Get
    End Property

End Class
```

### ����ģʽ
����ģʽ���������Ĵ���ӹ��캯������ȡ�������������״����ӵĶ��󴴽����̡�


### ������/����ģʽ
#### ������
������ģʽͳһ���ڲ�ͬ�������ƵĽӿڵĵ��á�

#### ����ģʽ
�����µĽӿڰ�װһЩ�Ѿ����ڵĽӿڣ�ͨ��������߿ɶ��Ժ�ͨ���ԡ�

����Ĵ��뽫һ�� VB6 ���� API ��װΪ .NET ͨ�÷��

__VB__
```vb.net
Function StrPtr(Str As String) As IntPtr
```

��װ��:

__VB__
```vb.net
Function AddressOfFirstChar(str As String) As IntPtr
```

����� C# ������ VB ����Ҳ���ƹ� `SafeBuffer` ֱ�Ӵ��� `UnmanagedMemoryStream`��

__C#__
```csharp
public static unsafe UnmanagedMemoryStream CreateUnmanagedMemoryStream(IntPtr pointer, long length)
{
    return new UnmanagedMemoryStream((byte*)(pointer.ToPointer()), length)
}
```

### װ����ģʽ
### ����ע��
### ʹ�� VB ���к���ʽ���
