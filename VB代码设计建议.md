## VB 代码设计建议
本文的设计建议包含设计模式和编程模型。不遵循这些建议也不会影响 PR 的通过。但还是请大家尽量先领悟这些建议的思想再编写代码。

注意: C# 代码设计建议看 Channel 9 的视频课，这里不再赘述。
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

### 命令/记忆碎片模式
#### 问题
假如您打算创建一个游戏，需要做对角色改名的功能。改名的界面包含一个文本框用于玩家输入姓名，一个按钮用于撤销姓名的变更，一个按钮保存姓名。怎样让写出清晰可读的代码处理这种问题？
#### 反例
很多游戏设计教程是这样教的：

- 在确定改名按钮点击事件发生时将文本框的内容读取出来，保存到游戏存档数据库。添加一条改名记录到游戏存档数据库。
- 在撤销按钮点击事件发生时从数据库查询改名记录。找到之后取出值，从数据库移除，然后赋值给文本框的 `Text` 属性。

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

#### 分析
反例中使用了记忆碎片模式，将某一个时间点某个对象的信息储存起来用于恢复。但是没有用命令模式对代码进一步封装。存在以下问题：

- 逻辑代码与用户界面耦合。更换用户界面框架导致代码几乎不能重用。
- 代码不能单元测试。
- 历史记录逻辑与服务状态维护代码耦合。

#### 解决方案
- 用命令封装业务逻辑，主要逻辑放到 `Execute` 方法内。
- 将历史记录（记忆碎片模式）代码移动到命令中的 `Undo` 方法内。

下面的代码用命令模式封装了改名和撤销改名逻辑，以及保存数据逻辑。

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

这样修改之后视图代码就缩短了，业务逻辑代码完全分离了。
需要进行测试时，只要先对命令进行单元测试再编写假的命令测试 UI 即可找出大多数潜在的问题。

#### 替代方案
在 Mvvm 模式或 Mvc 模式中，可以通过实现一个公共的命令接口进一步分离数据和视图。
例如，在 UWP, WPF 或 Xamarin.Forms 中实现 `System.Windows.Input.ICommand`， 
或在 N2Engine 中实现 `Nukepayload2.N2Engine.Input.IGameCommand`。
这样初始化命令的代码以及初始化服务对象的代码将被提取到 ViewModel 或 Controller 中。

### 策略模式
这个模式隐藏一种功能的不同实现。运行时可以动态替换具体的实现。
通常的做法是为相同功能的不同实现分别创建类，然后提取公共的接口。
使用时使用提取出来的公共接口，而不直接调用实际实现功能的类。

下面的代码订阅了两个超级武器。超级武器都有用于警告的文本。

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

下面的代码定义了一个阵营。阵营能够升级超级武器，还能通知副官播报超级武器的警告文本。使用策略模式，可以简化副官播报超级武器警告的代码。以后无论要添加多少个超级武器，使用超级武器的代码都不需要更改。

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
这样修改当前最强的超级武器就不需要改变调用超级武器的代码了。因为调用超级武器的代码通过 `StrongestSuperWeapon` 调用。
Adjutant 则使用下面描述的模板方法模式实现，因为它不需要在运行时动态变换。

### 模板方法模式
一个方法定义一系列操作的骨架，将一部分操作推迟到子类。使用 "提取方法" 重构可以快速用上这个模式。

下面的代码表示一个能向指定地方发射核弹的核弹发射井。发射核弹的步骤如下：
- 判断能不能发射核弹
- 如果可以，瞄准目标的坐标
- 播报警告信息
- 发射核弹
- 如果发射不了，告诉玩家不能向指定位置发射核弹

不妨在父类定义好这个过程，让具体的子类实现每一步的细节。

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
这样能让代码不容易重复，并且结构清晰，让代码容易维护。

### 发布者和订阅者模式
这是一种分类并广播消息的设计模式。在游戏开发中非常常见，尤其是与用户交互的代码中。
#### 特征
* 发送者与接收者不直接知道对方
* 发送一次，接收者都能收到消息
* 中转处理过滤和路由

#### 示例代码
使用 .NET 事件实现

下面的代码表示玩家能够通知其它对象，正在发射超级武器。

__VB__
```vb.net
Public Class Player
    Public Event LaunchingSuperWeapon As TypedEventHandler(Of Player, SuperWeaponLaunchingEventArgs)

End Class
```

#### 潜在问题: 内存泄漏
如果一个对象在订阅了某个事件之后不再需要被使用，它的内存不会被回收。因为它订阅了仍然被使用中的对象的事件，或者订阅了 `Shared` 事件。

下面的代码由于忘记使用 `RemoveHandler` 导致被事件监听器监听的玩家在不再需要被使用的情况下内存不被回收。

```vb.net
Public Class EventLogger
    Private _logs As New ObservableCollection(Of LogItem)

    Public Sub ListenPlayer(target As Player)
        AddHandler target.LaunchingSuperWeapon,
            Sub(sender, args) _logs.Add(New LogItem(sender.Name, args.Description))
    End Sub

End Class
```

#### 解决潜在问题
需要 省略 `RemoveHandler`, 监听 `Shared` 事件 或者 监听单实例对象的事件时，不要用 .NET 的事件。要实现一个基于 `WeakReference` 的事件。

下面的代码是一种使用弱引用的事件的实现。需要注意的是，它不是线程安全的。一种简单的线程安全改造方式是使用装饰器模式。这个模式在后面会解释。

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

    ' 注意：这个不是线程安全的。RaiseEvent 的时候不能 AddHandler 或者 RemoveHandler。
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

使用上述事件时，即使忘记使用 `RemoveHandler`, 也不会导致内存泄漏。下面是对比 .NET 事件与弱引用事件忘记使用 `RemoveHandler` 发生的情况。

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
        player.RaiseSuperWeaponLaunching() '明明 log 为空了，这段代码仍然让 log 输出。
    End Sub

    Public Sub TestWeakEvent()
        Dim log As New SuperWeaponEventLogger
        Dim player As New Player
        log.AddSuperWeaponLaunchingHandler2(player)
        player.RaiseSuperWeaponLaunching()
        log = Nothing
        GC.Collect()
        GC.WaitForPendingFinalizers()
        player.RaiseSuperWeaponLaunching() '这段代码什么也不会输出。因为 log 已经不为空了。
        Console.WriteLine(player.SuperWeaponLaunching2.ObserverCount) '输出是 0。
    End Sub
End Module
```

#### 对比观察者模式 (例如 INotifyPropertyChanged)
- 接收者知道发送者
- 发送者知道接收者
- 一次收发一次
- 直接通信

### 单实例模式
#### 选择 Module 或 Shared 方法 还是 单实例模式
#### 示例代码
实现单实例模式有多种写法。目前比较流行的是作为基类实现和实现实例管理器。

以下代码演示作为基类的单实例模式实现。

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

### 工厂模式
工厂模式将构造对象的代码从构造函数中提取出来。这样容易处理复杂的对象创建过程。


### 适配器/门面模式
#### 适配器
适配器模式统一对于不同但是相似的接口的调用。

#### 门面模式
创建新的接口包装一些已经存在的接口，通常用于提高可读性和通用性。

下面的代码将一个 VB6 风格的 API 包装为 .NET 通用风格。

__VB__
```vb.net
Function StrPtr(Str As String) As IntPtr
```

包装成:

__VB__
```vb.net
Function AddressOfFirstChar(str As String) As IntPtr
```

下面的 C# 代码让 VB 代码也能绕过 `SafeBuffer` 直接创建 `UnmanagedMemoryStream`。

__C#__
```csharp
public static unsafe UnmanagedMemoryStream CreateUnmanagedMemoryStream(IntPtr pointer, long length)
{
    return new UnmanagedMemoryStream((byte*)(pointer.ToPointer()), length)
}
```

### 装饰器模式
### 依赖注入
### 使用 VB 进行函数式编程
