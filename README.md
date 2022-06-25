# Nukepayload2.N2Engine
升级版的 Nukepayload2.Graphics.N2Engine。

用于支持 __任意__ 能够生成 __.NET Standard 类库__ 的 __.NET 语言__ 制作基于 Win2D 和 MonoGame 两种图形框架的跨平台游戏。

这个游戏引擎主要用于 2D 游戏创作和游戏程序框架建模。追求代码可维护性的极致，而不是性能的极致。

__已暂停开发__，因为近几年国内游戏出版审核延迟长达两年甚至更长，会导致客户的投入远大于产出，进而大幅影响到对此项目的潜在捐赠。

## 支持编程语言的优先级
* Microsoft Visual Basic, 版本 >= 15.0
* Microsoft Visual C#, 版本 >= 7.0
* (未完善) Microsoft Visual F#, 版本 >= 4.1
* (未完善) 其它语言

## 支持的平台
* Universal Windows 10, 版本 1607
* Windows 桌面, 版本 >= 6.1.7.7601 (Windows 7 sp1)
* Android, 版本 >= 4.0.3
* (未完善) iOS, 版本 >= 7
* (未完善) Linux 和 Mac, 需要 Gtk3 和 OpenGL

## 最近放弃的平台
* Windows Phone 8.1
* Windows 10 Mobile, 版本 <= 1511

## 警告
本计算机程序源代码受著作权法和国际条约保护。如未经授权而擅自违背许可协议，将受到严厉的民事和刑事制裁，并将在法律许可的最大限度内受到起诉。

## 协议说明
* 设计器 Nukepayload2.N2Engine.Shell 闭源，别找我们要代码。
* 此仓库中所有 Nukepayload2.N2Engine 开头的项目使用 GPLv3 协议
* 此仓库中所有 Nukepayload2.UI 开头的项目使用 LGPLv3 协议
* 此仓库中所有 Nukepayload2.Collections 开头的项目使用 LGPLv3 协议
* 此仓库中所有 N2Demo 开头的项目使用 LGPLv3 协议
* (外部项目 http://farseerphysics.codeplex.com/) FarseerPhysics 使用 Ms-PL 1.1 协议
* (外部项目 http://xnagraphics.codeplex.com/) RaisingStudio.Xna.Graphics 使用 LGPL 协议

## 附加许可条款
* 在 2018年7月10日 之前，此游戏引擎不得被用于制作商业游戏。
* 未经授权，此项目源代码使用 GPLv3 和 LGPLv3 协议的部分不得用于 比赛，展示，作业，课程设计 和 毕业设计。
* 项目的合作人要申请的授权最多可以延迟到行动后 24 小时。

## 功能
* 使用 MonoGame 显示 2D 图形
* 使用 Win2D 显示 2D 图形
* 处理鼠标和触摸输入
* 可移植的物体对象树
* 绘制几何图形 (使用 zhongzf 在LGPL下开源的类库 RaisingStudio.Xna.Graphics, 别名 xnagraphics。)
* 播放音乐和音效
