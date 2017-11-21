## VB 代码风格建议 v2
风格建议不遵循也不会影响 PR 的通过。但还是请大家尽量遵守。

注意: [C# 代码风格 与 .NET Core 的保持一致](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md)，这里不再赘述。

1. 我们对于 `New With`, `New <TypeName>(<Args>) With` 和 `New From` 语句, 以及较长的集合 使用 K&R 风格的大括号。左括号位于一行的结尾，右括号占一行。
2. 我们使用以 4 为倍数的空格进行缩进。
3. 我们使用 `_camelCase` 对非 `Public` 字段命名。如果可以，添加 `ReadOnly` 修饰符。 实例字段以 `_` 开头, `Shared` 字段以 `s_` 开头, `<ThreadStatic> Shared` 字段以 `t_` 开头。如果使用 `Shared` 和 `ReadOnly`, `Shared` 在前, `ReadOnly` 在后。
4. 我们尽可能省略 `Me.` 。 
5. 我们总是显式指定访问级别 (即:
   `Private _foo As String` 而不是 `Dim _foo As String`)。可见性要在最前的位置 (即: 
   `Public MustOverride` 而不是 `MustOverride Public`)。
6. 代码文件中的命名空间和类型导入应该按字母排序。
7. 不要连续空两行，除非在定义字符串常量中。
8. 不要错误使用空白字符。例如 `If someVar = 0 Then...` 中, ... 的位置表示错误的空白字符。
   在 Visual Studio 中使用 "查看空白字符 (Ctrl+E, S)" 可以找出这种问题。
9. 代码中有与风格建议不同的地方 (例如私有成员命名为 `m_member`
   而不是 `_member`), 已经存在的风格优先。
   由 `Property` 包装的字段与自动生成的代码风格保持一致。例如：`_PascalCase`。但是参数名称仍然应该用 `camelCase` 风格。
10. 我们只在类型很明显的时候通过 `Option Infer` 和 `Dim` 推断变量类型。能使用变量类型推断的时候如果可以用 `As` 则使用 `As` 而不是 `=`。 (即: `Dim stream As New FileStream(...)` 而不是 `Dim stream = OpenStandardInput()` 或者 `Dim stream = New FileStream(...)`)。
11. 我们使用关键值而不是 BCL 类型 (i.e. `Integer, Date, Byte` 而不是 `Int32, DateTime, [Byte]` 这样的)。
12. 我们用大驼峰命名法命名常量。除非在编写互操作代码。
13. 我们在合适的情况下使用 `NameOf` 而不是字符串常量。
14. 字段尽量定义在类型的顶部。属性包装的字段例外。
15. 代码中含有非 ASCII 字符或字符串常量时，使用 UTF-8 编码保存这个代码文件 (尤其不要用 GB2312)。
