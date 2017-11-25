# 贡献说明
## 贡献步骤
1. 新建一个 Issue 描述你需要进行怎样的更改
2. Fork 这个仓库
3. 在你 Fork 的方块选择恰当的分支 Clone 到本地
4. 更改想要改的文件
5. 检查你的代码或文档，更正错误
6. 选择恰当的分支对本仓库发起 Pull Request
7. 统一代码贡献协议
8. 等待代码被整合，或者修改代码以便整合。

## 项目结构
### 核心逻辑
核心逻辑是指平台无关的游戏引擎逻辑。在包含所有项目的 sln 文件中的 CoreLibrary 文件夹下。

#### 核心共享项目
核心逻辑实际有价值的代码都在核心共享项目里面。目前有:
- Nukepayload2.N2Engine.AnimationsApiContract
- Nukepayload2.N2Engine.BehaviorsApiContract
- Nukepayload2.N2Engine.DesignerApiContract
- Nukepayload2.N2Engine.GenericAIApiContract
- Nukepayload2.N2Engine.InputApiContract
- Nukepayload2.N2Engine.MediaApiContract
- Nukepayload2.N2Engine.ParticleSystemsApiContract
- Nukepayload2.N2Engine.PhysicsIntegrationApiContract
- Nukepayload2.N2Engine.PortableApiContract
- Nukepayload2.N2Engine.StorageApiContract
- Nukepayload2.N2Engine.UIApiContract

它们分别被对应的 .net standard 类库引用，并且被整合的核心逻辑 Nukepayload2.N2Engine 引用。

#### 核心分部类库
核心分部类库引用对应的核心共享项目，并且引用其它的核心分部类库。用于测试各个功能之间的依赖情况是否健康。属于特殊的测试项目。
- Nukepayload2.N2Engine.Animations
- Nukepayload2.N2Engine.Behaviors
- Nukepayload2.N2Engine.Designer
- Nukepayload2.N2Engine.GenericAI
- Nukepayload2.N2Engine.Input
- Nukepayload2.N2Engine.Media
- Nukepayload2.N2Engine.ParticleSystems
- Nukepayload2.N2Engine.PhysicsIntegration
- Nukepayload2.N2Engine.Portable
- Nukepayload2.N2Engine.Storage
- Nukepayload2.N2Engine.UI

#### 核心总类库
指的是 Nukepayload2.N2Engine 项目。它引用全部的核心共享项目，发布时会发布这个类库而不是核心分部类库。
- Nukepayload2.N2Engine

#### 核心单元测试
包含核心项目的单元测试。
- Nukepayload2.N2Engine.Core.Tests

### 演示项目
这些项目用于集成测试。
- N2Demo.Android
- N2Demo.Core
- N2Demo.DesktopGL
- N2Demo.iOS
- N2Demo.MonoOnUWP
- N2Demo.UWP
- N2Demo.Win32

### 随这个项目发布的基础逻辑
它们迟早会被清理出这个项目，分到别的地方。

- Nukepayload2.Collections
- Nukepayload2.UI.SpriteFonts
- Nukepayload2.UI.SpriteFonts.MonoGameShared

### 各个游戏类型的逻辑和设计器核心
包含已经成型的游戏类型的逻辑类库和设计器核心逻辑。
- Nukepayload2.N2Engine.ActionGames.Core
- Nukepayload2.N2Engine.ActionGames.UWP.Designer
- Nukepayload2.N2Engine.RolePlayGames.Core
- Nukepayload2.N2Engine.RolePlayGames.UWP.Designer
- Nukepayload2.N2Engine.TableGames.Core
- Nukepayload2.N2Engine.TableGames.UWP.Designer
- Nukepayload2.N2Engine.UWP.Designer

### 平台实现
为了将不同平台统一成平台无关的类库和共享项目

#### 平台实现类库
- Nukepayload2.N2Engine.Android
- Nukepayload2.N2Engine.DesktopGL
- Nukepayload2.N2Engine.iOS
- Nukepayload2.N2Engine.MonoOnUWP
- Nukepayload2.N2Engine.Win32
- Nukepayload2.N2Engine.UWP

#### 平台实现共享项目
- Nukepayload2.N2Engine.Platform
- Nukepayload2.N2Engine.Desktop
- Nukepayload2.N2Engine.Mono
- Nukepayload2.N2Engine.WinRT
- Nukepayload2.N2Engine.Xamarin

### 外来项目
#### 被我们改造过的物理引擎 
- FarseerPhysics.Portable

#### 被我们改造过的 MonoGame 矢量图绘制组件
- RaisingStudio.Xna.Graphics.Android
- RaisingStudio.Xna.Graphics.DesktopGL
- RaisingStudio.Xna.Graphics.iOS
- RaisingStudio.Xna.Graphics.Shared
- RaisingStudio.Xna.Graphics.UWP
- RaisingStudio.Xna.Graphics.Win32

### 在这个仓库不可见的项目
要使用这些项目的二进制文件，从发行版复制。
- Nukepayload2.N2Engine.Shell
