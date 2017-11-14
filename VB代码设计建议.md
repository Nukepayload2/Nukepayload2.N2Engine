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
### ģ�巽��ģʽ
### �����ߺͶ�����ģʽ
### ��ʵ��ģʽ
### ������/����ģʽ
### װ����ģʽ
### ����ע��
### ʹ�� VB ���к���ʽ���
