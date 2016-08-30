#Nukepayload2.N2Engine
升级版的 Nukepayload2.Graphics.N2Engine，能够同时适应 Win2D 和 MonoGame 两种图形框架，并且添加了制作游戏必备的助手库。
##支持的编程语言
<li>Microsoft Visual Basic, 版本 >= 14.0</li>
<li>Microsoft Visual C#, 版本 >= 6.0</li>
<li>Microsoft Visual F#, 版本 >= 4.0</li>
##支持的平台
<li>Universal Windows 10, 版本 >= 5.2.2</li>
<li>Windows 桌面, 版本 >= 6.1.7.7601</li>
<li>Windows Phone 8.1</li>
<li>Android, 版本 >= 4.0.3</li>
<li>iOS, 版本 >= 7</li>
<li>Linux 和 Mac, 需要 Gtk3 和 OpenGL</li>
##目前正在迁移上一个版本的N2引擎的功能
###已完成迁移的
<li>火花粒子系统</li>
<li>VB版的Box2D</li>
<li>角色扮演游戏帮助库的基础</li>
<li>战斗卡牌游戏帮助库的基础</li>
###新增功能
<li>Demo的代码更加平台无关</li>
<li>基础的Mono平台支持 (绘制几何图形的代码将使用 zhongzf 在LGPL下开源的类库 RaisingStudio.Xna.Graphics, 别名 xnagraphics。代码地址： http://xnagraphics.codeplex.com/ )</li>
##注意
<li>现在我还没有完成版本控制的迁移，所以在我完成之前，直接按修改日期判别版本。</li>
<li>此解决方案中所有 Nukepayload2.N2Engine 开头的项目使用 GPLv3 协议</li>
<li>此解决方案中所有 N2Demo 开头的项目使用 LGPLv3 协议</li>