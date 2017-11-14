## VB �������� v2
����鲻��ѭҲ����Ӱ�� PR ��ͨ�������������Ҿ������ء�

ע��: [C# ������ �� .NET Core �ı���һ��](https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/coding-style.md)�����ﲻ��׸����

1. ���Ƕ��� `New With`, `New <TypeName>(<Args>) With` �� `New From` ���, �Լ��ϳ��ļ��� ʹ�� K&R ���Ĵ����š�������λ��һ�еĽ�β��������ռһ�С�
2. ����ʹ���� 4 Ϊ�����Ŀո����������
3. ����ʹ�� `_camelCase` �Է� `Public` �ֶ�������������ԣ���� `ReadOnly` ���η��� ʵ���ֶ��� `_` ��ͷ, `Shared` �ֶ��� `s_` ��ͷ, `<ThreadStatic> Shared` �ֶ��� `t_` ��ͷ�����ʹ�� `Shared` �� `ReadOnly`, `Shared` ��ǰ, `ReadOnly` �ں�
4. ���Ǿ�����ʡ�� `Me.` �� 
5. ����������ʽָ�����ʼ��� (��:
   `Private _foo As String` ������ `Dim _foo As String`)���ɼ���Ҫ����ǰ��λ�� (��: 
   `Public MustOverride` ������ `MustOverride Public`)��
6. �����ļ��е������ռ�����͵���Ӧ�ð���ĸ����
7. ��Ҫ���������У������ڶ����ַ��������С�
8. ��Ҫ����ʹ�ÿհ��ַ������� `If someVar = 0 Then...` ��, ... ��λ�ñ�ʾ����Ŀհ��ַ���
   �� Visual Studio ��ʹ�� "�鿴�հ��ַ� (Ctrl+E, S)" �����ҳ��������⡣
9. �������������鲻ͬ�ĵط� (����˽�г�Ա����Ϊ `m_member`
   ������ `_member`), �Ѿ����ڵķ�����ȡ�
   �� `Property` ��װ���ֶ����Զ����ɵĴ����񱣳�һ�¡����磺`_PascalCase`�����ǲ���������ȻӦ���� `camelCase` ���
10. ����ֻ�����ͺ����Ե�ʱ��ͨ�� `Option Infer` �� `Dim` �ƶϱ������͡���ʹ�ñ��������ƶϵ�ʱ����������� `As` ��ʹ�� `As` ������ `=`�� (��: `Dim stream As New FileStream(...)` ������ `Dim stream = OpenStandardInput()` ���� `Dim stream = New FileStream(...)`)��
11. ����ʹ�ùؼ�ֵ������ BCL ���� (i.e. `Integer, Date, Byte` ������ `Int32, DateTime, [Byte]` ������)��
12. �����ô��շ����������������������ڱ�д���������롣
13. �����ں��ʵ������ʹ�� `NameOf` �������ַ���������
14. �ֶξ������������͵Ķ��������԰�װ���ֶ����⡣
15. �����к��з� ASCII �ַ����ַ�������ʱ��ʹ�� UTF-8 ���뱣����������ļ� (���䲻Ҫ�� GB2312)��
