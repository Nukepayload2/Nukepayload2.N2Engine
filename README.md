# Nukepayload2.N2Engine
升级版的 Nukepayload2.Graphics.N2Engine。</br>
用于支持 __任意__ 能够生成 __可移植类库 或 .NET Standard 类库__ 的 __.NET 语言__ 制作基于 Win2D 和 MonoGame 两种图形框架的跨平台游戏。

## 支持编程语言的优先级
* Microsoft Visual Basic, 版本 >= 14.0
* Microsoft Visual C#, 版本 >= 6.0
* (未完善) Microsoft Visual F#, 版本 >= 4.0
* (未完善) 其它语言

## 支持的平台
* Universal Windows 10, 版本 >= 1511
* Windows 桌面, 版本 >= 6.1.7.7601 (Windows 7 sp1)
* (未完善) Windows Phone 8.1
* Android, 版本 >= 4.0.3
* (未完善) iOS, 版本 >= 7
* (未完善) Linux 和 Mac, 需要 Gtk3 和 OpenGL

## 功能
* 使用 MonoGame 显示 2D 图形
* 使用 Win2D 显示 2D 图形
* 处理鼠标和触摸输入
* 可移植的物体对象树
* 绘制几何图形 (使用 zhongzf 在LGPL下开源的类库 RaisingStudio.Xna.Graphics, 别名 xnagraphics。代码地址： http://xnagraphics.codeplex.com/ )
* 播放音乐和音效

## 注意
* 此解决方案中所有 Nukepayload2.N2Engine 开头的项目使用 GPLv3 协议
* 此解决方案中所有 N2Demo 开头的项目使用 LGPLv3 协议
* FarseerPhysics 项目使用 Ms-PL 1.1 协议
