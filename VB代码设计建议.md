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
    Private ReadOnly _strongHandlers As New List(Of TypedEventHandler(Of TSender, TEventArgs))

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Sub [RemoveHandler](eventHandler As TypedEventHandler(Of TSender, TEventArgs))
        If eventHandler Is Nothing Then
            Throw New ArgumentNullException(NameOf(eventHandler))
        End If

        Dim observer = eventHandler.Target
        If observer Is Nothing Then
            _strongHandlers.Remove(eventHandler)
        Else
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
        End If
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
    Public Sub [AddHandler](eventHandler As TypedEventHandler(Of TSender, TEventArgs))
        If eventHandler Is Nothing Then
            Throw New ArgumentNullException(NameOf(eventHandler))
        End If

        Dim observer = eventHandler.Target
        If observer Is Nothing Then
            _strongHandlers.Add(eventHandler)
        Else
            _observers.Add((New WeakReference(observer), New WeakReference(Of TypedEventHandler(Of TSender, TEventArgs))(eventHandler)))
        End If
    End Sub

    <MethodImpl(MethodImplOptions.Synchronized)>
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
        For i = 0 To _strongHandlers.Count - 1
            _strongHandlers(i)(sender, e)
        Next
    End Sub

    Public ReadOnly Property ObserverCount As Integer
        <MethodImpl(MethodImplOptions.Synchronized)>
        Get
            Return _observers.Count + _strongHandlers.Count
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

例如，一款即时战略游戏中的兵营可以生产各种各样类型的步兵。不同阵营对于各种类型的步兵都有自己的兵种。这时候要使用容易理解的代码创建步兵，就需要使用工厂模式实现。

__VB__
```vb.net
Public MustInherit Class Barracks
    Public MustOverride Function CreateScout() As IInfantry
    Public MustOverride Function CreateBasicAntiInfantry() As IInfantry
    Public MustOverride Function CreateBasicAntiArmor() As IInfantry
    Public MustOverride Function CreateEngineer() As IInfantry
    Public MustOverride Function CreateAdvancedSoldier() As IInfantry
    Public MustOverride Function CreateCommando() As IInfantry
End Class

Public Class SovietBarracks
    Inherits Barracks
    Public Overrides Function CreateBasicScout() As IInfantry
        Return New SovietAttackDog
    End Function
    Public Overrides Function CreateBasicAntiInfantry() As IInfantry
        Return New Conscript
    End Function
    Public Overrides Function CreateBasicAntiArmor() As IInfantry
        Return New FlakTrooper
    End Function
    Public Overrides Function CreateEngineer() As IInfantry
        Return New SovietEngineer
    End Function
    Public Overrides Function CreateAdvancedSoldier() As IInfantry
        Return New TeslaTrooper
    End Function
    Public Overrides Function CreateCommando() As IInfantry
        Return New Natasha
    End Function
End Class

Public Class AlliedBarracks
    Inherits Barracks
    Public Overrides Function CreateBasicScout() As IInfantry
        Return New AlliedAttackDog
    End Function
    Public Overrides Function CreateBasicAntiInfantry() As IInfantry
        Return New PeaceKeeper
    End Function
    Public Overrides Function CreateBasicAntiArmor() As IInfantry
        Return New JavelinRocketSoldier
    End Function
    Public Overrides Function CreateEngineer() As IInfantry
        Return New AlliedEngineer
    End Function
    Public Overrides Function CreateAdvancedSoldier() As IInfantry
        Return New Spy
    End Function
    Public Overrides Function CreateCommando() As IInfantry
        Return New Tanya
    End Function
End Class

Public Class Dojo
    Inherits Barracks
    Public Overrides Function CreateBasicScout() As IInfantry
        Return New Dragonfly
    End Function
    Public Overrides Function CreateBasicAntiInfantry() As IInfantry
        Return New Warrior
    End Function
    Public Overrides Function CreateBasicAntiArmor() As IInfantry
        Return New TankDestroyer
    End Function
    Public Overrides Function CreateEngineer() As IInfantry
        Return New EmpireEngineer
    End Function
    Public Overrides Function CreateAdvancedSoldier() As IInfantry
        Return New Ninja
    End Function
    Public Overrides Function CreateCommando() As IInfantry
        Return New Yoriko
    End Function
End Class
```

### 适配器/门面模式
#### 适配器
适配器模式统一对于不同但是相似的接口的调用。

下面的代码统一了两种音频播放 API。

__VB__
```vb.net
' 从元数据得到的声明
Public Class MediaElement
    Public Property MediaSource As Stream
    Public Property IsPlaying As Boolean
End Class

' 从元数据得到的声明
Public Class AudioPlayer
    Public Async Function SetMediaStreamAsync(strm As Stream) As Task
    Public Sub Play()
End Class

' 适配器
Public Interface IAudioPlayer
    Function SetMediaStreamAsync(strm As Stream) As Task
    Sub Play()
End Interface

Public Class MediaElementAdapter
    Implements IAudioPlayer

    Private _mediaElement As New MediaElement

    Public Function SetMediaStreamAsync(strm As Stream) As Task Implements IAudioPlayer.SetMediaStream
        _mediaElement.Stream = strm
        Return Task.CompletedTask
    End Function

    Public Sub Play() Implements IAudioPlayer.Play
        _mediaElement.IsPlaying = True
    End Sub
End Class

Public Class AudioPlayerAdapter
    Implements IAudioPlayer

    Private _audioPlayer As New AudioPlayer

    Public Function SetMediaStreamAsync(strm As Stream) As Task Implements IAudioPlayer.SetMediaStream
        Await _audioPlayer.SetMediaStreamAsync(strm)
    End Function

    Public Sub Play() Implements IAudioPlayer.Play
        _audioPlayer.Play
    End Sub
End Class
```


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
装饰器模式嵌套使用同基类的类型。这样可以在上一次计算的基础上做出调整。

下面的代码使用装饰器模式实现了伤害计算的嵌套。

__VB__
```vb.net
Public Interface IDamageCompositor
    Function GetDamage(value As Double) As Double
End Interface

Public Class WoodenArmor
    Implements IDamageCompositor

    Function GetDamage(value As Double) As Double Implements IDamageCompositor.GetDamage
        Return Math.Max(value * 0.9, value * 0.98 - 20)
    End Function
End Class

Public Class Pan
    Implements IDamageCompositor

    Public Property BaseArmor As IDamageCompositor

    Function GetDamage(value As Double) As Double Implements IDamageCompositor.GetDamage
        Return If(Rnd < 0.1, 0.0, BaseArmor.GetDamage(value))
    End Function
End Class
```

### 依赖注入
依赖注入让跨平台或者其它高度抽象的代码使用具体平台或具体设计的功能。

下面的代码封装了获取进程名称的功能给跨平台的类库使用。

.NET Standard 代码:

__VB__
```vb.net
Public Interface IMetaGame
    Function GetCurrentProcessName() As String
End Interface

Public Interface IProcessInfoProvider
    Function GetCurrentProcess() As Integer
    Function GetProcessName(pid As Integer) As String
End Interface
```

.NET Framework 平台具体实现代码(省略细节):

__VB__
```vb.net
<PlatformImpl(GetType(IMetaGame))>
Friend Class MetaGame
    Implements IMetaGame
    Sub New(processInfoProvider As IProcessInfoProvider)
        '...
    End Sub
    '...
End Class

<PlatformImpl(GetType(IProcessInfoProvider, SingleInstance:=True))>
Friend Class ProcessInfoProvider
    Implements IProcessInfoProvider
    '...
End Class
```

应用程序启动时，在平台相关的代码中注册平台具体实现的代码。

__VB__
```vb.net
PlatformActivator.Register(Of ProcessInfoProvider)
PlatformActivator.Register(Of MetaGame)
```

.NET Standard 代码创建刚才注册的 IMetaGame:

__VB__
```vb.net
Dim metaGame = PlatformActivator.CreateInstance(Of IMetaGame)
```

### 使用 VB 进行函数式编程
函数式编程是一种编程风格，专注于输入和输出而不是维护状态，分离数据和行为。对于数据处理代码可以让代码逻辑更加清晰。

#### 不可变类型
不可变类型的每一个需要变更数据的操作都产生新的对象。典型的例子是 `String`。

下面的代码定义了一个不可变的点，使用的时候代码更加简介和线程安全。

__VB__
```vb.net
Public Structure ImmutablePoint
    Public ReadOnly Property X As Single
    Public ReadOnly Property Y As Single

    Sub New(x As Single, y As Single)
        _X = x
        _Y = y
    End Sub

    Public Function Offset(x As Single, y As Single) As ImmutablePoint
        Return New ImmutablePoint(Me.X + x, Me.Y + y)
    End Function
End Structure
```

__VB__
```vb.net
Dim pt As New ImmutablePoint(123, 567)

pt = pt.Offset(-222, 172).Offset(9876, -761)
```

#### 委托

使用匿名委托，可以定义带有行为的变量。

__VB__
```vb.net
Dim addOne = Function(n%) n + 1

Console.WriteLine(addOne(1)) ' 输出 2 并换行
```

也可以用泛型委托做同样的事情。

__VB__
```vb.net
Dim addOne As Func(Of Integer, Integer) = Function(n) n + 1

Console.WriteLine(addOne(1)) ' 输出 2 并换行
```

使用委托作为参数, 可以查询或处理数据。下面的代码使用 Linq 查询苏军阵营的玩家的数量。

__VB__
```vb.net
Dim sides As ISide() = Players.Select(Sub(p) p.Side).ToArray

Dim isSoviet = Function(side As ISide) side.Name = "Soviet"
Dim sovietPlayerCount = sides.Count(isSoviet)
```

#### 使用表达式代替完整的声明
注意：这种风格在 VB 的实现可能会导致一些反模式。下面的代码用字段代替了方法，省略了一个 `End Function`。

__VB__
```vb.net
Public ReadOnly GetArmoredPlayers As Func(Of IPlayer(), IPlayer()) = 
    Function(Players) Players.Where(Sub(p) p.Armor IsNot Nothing)
```

#### 管线
这种写法省略了重复的变量名。使用 `With` 块或者返回正在操作的实例的方法或拓展方法能够实现这个写法。

__VB__
```vb.net
With player
    .TakeDamage(Function(p) p.Armor.GetDamage(1 + alcohol.Count))
    .AskForHelpIf(Function(p) p.HP <= 0 AndAlso Not p.SelfHelp)
    .SurrenderIf(Function(p) p.HP <= 0)
End With
```

或者像 C# 一样干脆让每个方法返回被最初的对象

__VB__
```vb.net
player.
    TakeDamage(Function(p) p.Armor.GetDamage(1 + alcohol.Count)).
    AskForHelpIf(Function(p) p.HP <= 0 AndAlso Not p.SelfHelp).
    SurrenderIf(Function(p) p.HP <= 0)
```

#### 拓展方法
查看 Linq 的源码，模仿它们的风格。能够帮助实现管线风格。

__VB__
```vb.net
Module PlayerExtensions
    <Extension>
    Sub SurrenderIf(player As Player, predicate As Predicate(Of Player))
        If predicate(player) Then player.Surrender
    End Sub
End Module
```

如果这个库要给 C# 用，返回最初操作的对象。

__VB__
```vb.net
Public Module PlayerExtensions
    <Extension>
    Public Function SurrenderIf(player As Player, predicate As Predicate(Of Player)) As Player
        If predicate(player) Then player.Surrender
        Return player
    End Function
End Module
```

#### Iterator Function
实现上面提到的拓展方法通常需要 `Yield` 语句。这样可以省略 List 的定义。

__VB__
```vb.net
Module MyLinqExtensions
    <Extension>
    Function Where(players As PlayerCollection, predicate As Predicate(Of Player)) As IEnumerable(Of Player)
        players.Lock
        For Each p In players
            If predicate(p) Then Yield p
        Next
        players.Unlock
    End Function
End Module
```

#### 值元组
使用这个功能可能要装 Nuget 包 `System.ValueTuple`。

下面的代码使用值元组写了一个简单的 RTS 游戏的 AI 的决策树。

__VB__
```vb.net
Private _decisionsToAttack As (Func(Of Player, Boolean), PlayerAction)() = 
{
    (Function(p) p.HasSuperWeapon, PlayerAction.UseSuperWeapon),
    (Function(p) p.HasAgreement, PlayerAction.UseAgreement),
    (Function(p) p.ThreatSense.CanDecideIf(
                     Function(t) t.Armor.ThreatLevel = ThreatLevel.Higher,
                     Function() p.Units.Where(Function(u) u.AntiArmor)),
                 PlayerAction.InfantryRush),
    (Function(p) p.ThreatSense.CanDecideIf(
                     Function(t) t.AirForces.ThreatLevel = ThreatLevel.Higher,
                     Function() p.Units.Count(Function(u) u.AntiAir)) < p.AADefenceMinCount,
                 PlayerAction.BuildMoreAA)
}
```
