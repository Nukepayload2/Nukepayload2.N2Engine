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
### 模板方法模式
### 发布者和订阅者模式
### 单实例模式
### 适配器/门面模式
### 装饰器模式
### 依赖注入
### 使用 VB 进行函数式编程
