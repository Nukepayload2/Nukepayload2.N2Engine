<TestClass>
Public Class PropertyBinderTest
    <TestMethod>
    Public Sub TestProperty_Basic()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32Value = 3
        Dim view As New TestView
        view.Int32Value.Bind(getter:=Function() TestViewModel.Instance.Model.TestInt32Value,
                             setter:=Sub(value) TestViewModel.Instance.Model.TestInt32Value = value)
        Assert.AreEqual(3, view.Int32Value.Value)
        view.Int32Value.Value = 4
        Assert.AreEqual(4, vm.Model.TestInt32Value)
    End Sub

    <TestMethod>
    Public Sub TestCachedProperty_Basic()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32ValueNotify = 3
        Dim view As New TestView
        view.CachedInt32Value.DataSource = vm.Model
        view.CachedInt32Value.Path = NameOf(vm.Model.TestInt32ValueNotify)
        Assert.AreEqual(3, view.CachedInt32Value.Value)
        view.CachedInt32Value.Value = 4
        Assert.AreEqual(4, vm.Model.TestInt32ValueNotify)
    End Sub

    <TestMethod>
    Public Sub TestCachedProperty_SetBinding()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32ValueNotify = 3
        Dim view As New TestView
        view.CachedInt32Value.SetBinding(vm.Model, NameOf(vm.Model.TestInt32ValueNotify))
        Assert.AreEqual(3, view.CachedInt32Value.Value)
        view.CachedInt32Value.Value = 4
        Assert.AreEqual(4, vm.Model.TestInt32ValueNotify)
    End Sub

End Class