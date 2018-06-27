<TestClass>
Public Class PropertyBinderBenchmark
    Const LoopCount = 1_0000_0000

    <TestMethod>
    Public Sub TestCachedProperty_GetBenchmark()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32ValueNotify = 3
        Dim view As New TestView
        view.CachedInt32Value.SetBinding(vm.Model, NameOf(vm.Model.TestInt32ValueNotify))
        For i = 1 To LoopCount
            Dim value = view.CachedInt32Value.Value
        Next
    End Sub

    <TestMethod>
    Public Sub TestCachedProperty_SetSameValueBenchmark()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32ValueNotify = 3
        Dim view As New TestView
        view.CachedInt32Value.SetBinding(vm.Model, NameOf(vm.Model.TestInt32ValueNotify))
        For i = 1 To LoopCount
            view.CachedInt32Value.Value = 4
        Next
    End Sub

    <TestMethod>
    Public Sub TestCachedProperty_SetDifferentValueBenchmark()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32ValueNotify = 3
        Dim view As New TestView
        view.CachedInt32Value.SetBinding(vm.Model, NameOf(vm.Model.TestInt32ValueNotify))
        For i = 1 To LoopCount
            view.CachedInt32Value.Value = i
        Next
    End Sub

    <TestMethod>
    Public Sub TestProperty_GetBenchmark()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32Value = 3
        Dim view As New TestView
        view.Int32Value.Bind(getter:=Function() TestViewModel.Instance.Model.TestInt32Value,
                             setter:=Sub(value) TestViewModel.Instance.Model.TestInt32Value = value)
        For i = 1 To LoopCount
            Dim value = view.Int32Value.Value
        Next
    End Sub

    <TestMethod>
    Public Sub TestProperty_SetBenchmark()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32Value = 3
        Dim view As New TestView
        view.Int32Value.Bind(getter:=Function() TestViewModel.Instance.Model.TestInt32Value,
                             setter:=Sub(value) TestViewModel.Instance.Model.TestInt32Value = value)
        For i = 1 To LoopCount
            view.Int32Value.Value = 4
        Next
    End Sub

    <TestMethod>
    Public Sub TestCodeBehind_GetBenchmark()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32Value = 3
        For i = 1 To LoopCount
            Dim value = vm.Model.TestInt32Value
        Next
    End Sub

    <TestMethod>
    Public Sub TestCodeBehind_SetBenchmark()
        Dim vm = TestViewModel.Instance
        vm.Model.TestInt32Value = 3
        For i = 1 To LoopCount
            vm.Model.TestInt32Value = 4
        Next
    End Sub

End Class
