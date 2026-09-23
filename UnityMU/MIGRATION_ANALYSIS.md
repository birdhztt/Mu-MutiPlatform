# MuOnline Main 5.2 客户端迁移 Unity 分析

> 仓库：[`birdhztt/Mu-MutiPlatform`](https://github.com/birdhztt/Mu-MutiPlatform)
>
> 分析范围：`Source Main 5.2` 客户端源码
>
> 目标：评估将现有 Windows/OpenGL C++ 客户端迁移到 Unity 所需的准备工作、技术路线和实施阶段。

## 1. 结论

`Source Main 5.2` 不适合直接转换为 Unity 项目。推荐采用以下迁移方式：

```text
协议与服务器兼容
        +
数据格式与业务规则复用
        +
资源离线转换
        +
Unity 客户端逻辑、渲染和 UI 重写
```

可以复用或参考协议字段、枚举、数据结构、配置解析规则、地图编号、角色/技能/物品规则以及旧客户端行为；Win32 窗口、OpenGL 渲染、旧 UI、Windows 音频、平台保护代码和大量全局状态不应直接搬到 Unity。

## 2. 源码结构概览

### 2.1 平台与程序入口

主要文件：

- `Winmain.cpp`
- `Win.cpp`
- `WinEx.cpp`
- `Input.cpp`
- `w_WindowMessageHandler.h`
- `SysMenuWin.cpp`
- `ProtectSysKey.cpp`
- `ExternalObject/*`

依赖 Win32 窗口消息、Windows 句柄、线程、计时器、WinSock、DirectSound、WinMM、OpenGL/GLEW、Windows 资源文件和可能的反外挂/完整性检查模块。

这部分由 Unity 运行时替代，不建议移植。

### 2.2 OpenGL 渲染

主要文件：

- `ZzzOpenglUtil.cpp`
- `ZzzScene.cpp`
- `ZzzObject.cpp`
- `ZzzCharacter.cpp`
- `ZzzTexture.cpp`
- `ZzzEffect*.cpp`
- `ShadowVolume.cpp`
- `ZzzLodTerrain.cpp`
- `Sprite.cpp`
- `NewUI3DRenderMng.cpp`

现有代码采用较强的自定义 OpenGL 渲染模型，包含手动纹理、矩阵、光照、透明度、阴影、粒子、动画帧和地形处理。Unity 中应改为 `GameObject`、`Transform`、`Renderer`、`Material`、`Animator`、`ParticleSystem`、Terrain/Mesh 和 Unity Camera 等组件。

不要按函数逐一翻译 OpenGL 代码，应先设计 Unity 的场景、实体、材质、动画和特效模型。

### 2.3 模型、动画和资源

主要文件：

- `SMD.cpp`
- `SMD2BMD.cpp`
- `ZzzBMD.cpp`
- `ZzzOpenData.cpp`
- `LoadData.cpp`
- `TextureScript.cpp`
- `ZzzTexture.cpp`
- `BoneManager.cpp`
- `SideHair.cpp`

需要重点确认 `.BMD`、`.SMD`、地图、纹理、特效脚本和配置文件的版本、压缩/加密方式以及再利用权限。

建议建立独立的离线资源转换工具：

```text
BMD/SMD/纹理/地图
        ↓
离线解析与转换工具
        ↓
FBX/GLB/PNG/WAV/JSON/Unity 二进制数据
        ↓
Unity Asset Import Pipeline
        ↓
Prefab/Material/AnimationClip/ScriptableObject
```

第一阶段只需验证一个角色、一个武器、一个地图和一个技能特效的完整转换链路。

### 2.4 角色、对象和战斗逻辑

主要文件：

- `CharacterManager.cpp`
- `ZzzCharacter.cpp`
- `ZzzObject.cpp`
- `ZzzAI.cpp`
- `SkillManager.cpp`
- `SkillInfo.cpp`
- `SkillEffectMgr.cpp`
- `SummonSystem.cpp`
- `GIPetManager.cpp`
- `PartyManager.cpp`
- `GuildManager.cpp`
- `DuelMgr.cpp`
- `ItemManager.cpp`
- `QuestMng.cpp`

这些模块涵盖角色、怪物、NPC、目标、移动、攻击、技能、召唤物、宠物、Buff、任务、队伍、公会、决斗和物品。

Unity 中建议拆分为：

```text
GameBootstrap
 ├── NetworkClient
 ├── WorldManager
 ├── EntityManager
 ├── PlayerController
 ├── CharacterState
 ├── CombatSystem
 ├── SkillSystem
 ├── BuffSystem
 ├── ItemSystem
 ├── QuestSystem
 ├── PartySystem
 ├── GuildSystem
 └── UIManager
```

旧代码中存在全局对象、单例、裸指针和对象间指针引用。Unity 版本应优先使用实体 ID、组件引用、事件和网络对象引用，避免继续扩大这些耦合。

### 2.5 UI

项目包含大量自定义窗口和控件，例如：

- `UIManager.cpp`
- `UIMng.cpp`
- `UIWindows.cpp`
- `NewUIManager.cpp`
- `NewUIMainFrameWindow.cpp`
- `NewUIMyInventory.cpp`
- `NewUICharacterInfoWindow.cpp`
- `NewUIChatLogWindow.cpp`
- `NewUINPCDialogue.cpp`
- `NewUINPCShop.cpp`
- `NewUIQuestProgress.cpp`
- `NewUIPartyInfoWindow.cpp`
- `NewUIGuildInfoWindow.cpp`
- `NewUITrade.cpp`
- `NewUIOptionWindow.cpp`
- `NewUIMiniMap.cpp`

Unity 版本应使用 UGUI 或 UI Toolkit，将固定像素坐标改为锚点、布局和响应式适配；窗口状态应与业务数据分离，并统一适配不同分辨率、宽高比、触控设备和刘海屏。

### 2.6 网络和协议

主要文件：

- `WSclient.cpp/h`
- `WSctlc.cpp`
- `ProtocolSend.cpp/h`
- `ProtocolAsio.h`
- `SocketSystem.cpp/h`
- `StreamPacketEngine.h`

`ProtocolSend.h` 中包含登录、位置、移动以及新协议头定义，包括：

- `PMSG_CONNECT_ACCOUNT_SEND`
- `PMSG_POSITION_SEND`
- `PMSG_MOVE_SEND`
- `PMSG_CONNECT_CLIENT_RECV`
- `PMSG_SIMPLE_RESULT_RECV`

重点风险：

1. 使用 `#pragma pack(1)`，字段布局必须严格一致；
2. 使用 Windows 类型 `BYTE`、`WORD`、`DWORD`；
3. 可能同时存在 Classic Protocol 和 New Protocol；
4. 旧代码可能直接发送结构体内存；
5. 必须确认字节序、字符编码、长度字段、校验和加密；
6. 客户端版本、序列号和心跳可能参与服务器校验。

Unity 中建议实现显式序列化器，而不是依赖 C# 结构体内存布局：

```text
MuProtocol
 ├── PacketReader
 ├── PacketWriter
 ├── PacketHeader
 ├── LoginPackets
 ├── CharacterPackets
 ├── MovementPackets
 ├── CombatPackets
 └── ProtocolTests
```

所有字段需要明确字节序、长度、编码、压缩、加密、校验和对齐方式。

## 3. 可复用与不应复用的内容

### 3.1 可复用或参考

- 协议头、包字段和消息编号；
- 职业、技能、物品、地图等枚举；
- 任务、Buff、装备数据结构；
- 配置文件解析规则；
- 地图编号、网关编号和传送规则；
- 移动路径数据；
- 资源格式说明；
- 旧客户端行为和表现作为回归基准。

### 3.2 不建议直接复用

- `Winmain.cpp`、Win32 消息循环和窗口代码；
- OpenGL 状态机渲染代码；
- `stdafx.h` 的平台依赖体系；
- Windows 线程、句柄、DirectSound 和 WinMM；
- 旧的自绘 UI；
- 裸指针对象管理和大量全局变量；
- x86、Windows SDK、DirectX SDK 依赖；
- 反外挂、进程保护和平台专用模块。

只有资源解析器、加密算法或高性能寻路等独立模块，在确认跨平台和授权条件后，才考虑通过 native plugin 局部复用。

## 4. 迁移前必须准备的资料

### 4.1 服务器和协议

需要确认：

- 登录服务器、角色服务器和游戏服务器连接流程；
- IP/端口切换和服务器选择规则；
- 协议版本、客户端版本和序列号校验；
- 登录、选角、进入地图、移动、攻击、聊天、交易和传送协议；
- 断线重连、心跳、包体压缩/加密/校验；
- 服务器是否允许新的 Unity 客户端接入。

应保存旧客户端的原始协议样本：

- 登录请求和服务器响应；
- 角色列表；
- 进入地图；
- 移动；
- 攻击；
- 聊天；
- 错误和断线响应。

### 4.2 资源清单

建立以下资源表：

| 类别 | 需整理内容 |
|---|---|
| 角色 | 职业、骨骼、动作、装备挂点 |
| 怪物 | 模型、动画、属性、AI |
| 装备 | 模型、纹理、套装、特效 |
| 地图 | 地形、阻挡、区域、传送门 |
| 技能 | 动作、特效、音效、飞行物 |
| UI | 图片、字体、图标、窗口 |
| 音频 | 背景音乐、技能音效、环境音 |
| 文本 | 多语言、任务、NPC 对话 |
| 数据 | 物品、技能、地图、怪物、任务配置 |

同时统计文件数量、容量、纹理分辨率、重复纹理、移动端压缩需求以及运行时解密/解压机制。

### 4.3 坐标和数学规则

必须明确：

- X/Y/Z 轴定义；
- 地面坐标与世界坐标转换；
- 角度单位和角色朝向；
- 地图格子大小；
- 路径坐标范围；
- 高度和地形采样；
- 相机旋转方向；
- 动画帧率；
- 服务器位置更新频率。

Unity 的坐标、旋转和动画约定与旧客户端不同，应集中建立转换层，不要在各模块中重复处理。

## 5. 推荐迁移阶段

### 阶段一：建立旧客户端基线

1. 在 Windows 上成功编译和运行旧客户端；
2. 记录登录、选角、进入地图、移动、攻击、拾取、聊天和传送；
3. 抓取并保存关键网络包；
4. 记录资源加载日志和错误；
5. 保存关键画面、行为视频和测试账号；
6. 整理枚举、结构体、全局变量、协议和资源清单。

### 阶段二：制作独立协议 SDK

先实现不依赖 Unity 的 C# 类库，完成：

- 连接和断开；
- 登录；
- 角色列表；
- 进入地图；
- 心跳；
- 移动；
- 断线和重连；
- 字节级协议回归测试。

### 阶段三：Unity 最小可运行版本

只实现：

1. 登录界面；
2. 服务器连接；
3. 角色列表；
4. 进入一个地图；
5. 显示一个角色；
6. 点击移动；
7. 接收服务器位置；
8. 显示名称和基础血条；
9. 断线重连。

暂时不要实现完整 UI、所有活动地图、商城、公会、复杂粒子和全部技能。

### 阶段四：资源管线

完成以下最小闭环：

```text
一个角色 + 一个怪物 + 一套装备 + 一个地图 + 一个技能 + 一个 UI 图标
```

验证原始资源、转换工具、Unity 导入、Prefab、动画和特效播放全部可用。

### 阶段五：逐模块迁移

推荐顺序：

1. 网络连接；
2. 登录和选角；
3. 地图和角色显示；
4. 移动与寻路；
5. 怪物/NPC；
6. 基本攻击；
7. 物品和背包；
8. 技能和 Buff；
9. 任务和对话；
10. 组队和公会；
11. 商店、交易、个人商店；
12. 活动地图；
13. 性能优化；
14. 移动端适配。

## 6. 推荐 Unity 项目结构

```text
Assets/
├── Runtime/
│   ├── Bootstrap/
│   ├── Network/
│   ├── Protocol/
│   ├── World/
│   ├── Entity/
│   ├── Character/
│   ├── Combat/
│   ├── Skill/
│   ├── Buff/
│   ├── Item/
│   ├── Quest/
│   ├── Party/
│   ├── Guild/
│   ├── UI/
│   └── Audio/
├── Editor/
│   ├── AssetImporter/
│   ├── BmdImporter/
│   ├── SmdImporter/
│   └── MapImporter/
├── Data/
│   ├── Items/
│   ├── Skills/
│   ├── Monsters/
│   ├── Maps/
│   └── Quests/
├── Prefabs/
├── Scenes/
├── Materials/
├── Textures/
├── Animations/
└── VFX/
```

静态配置可以使用 `ScriptableObject`；需要热更新的数据使用 JSON 或明确版本的二进制格式；网络层不直接操作场景对象；UI 使用事件或 ViewModel 与业务数据连接。

## 7. 主要风险

### 7.1 资源格式风险

如果 BMD/SMD、地图和纹理格式无法完整解析，Unity 可能只能先完成网络和业务验证，无法直接显示原始内容。

### 7.2 协议兼容风险

`#pragma pack(1)` 和 Windows 类型会导致 C# 端字段偏移、长度和编码不一致，必须使用十六进制样本逐字段验证。

### 7.3 全局状态风险

旧客户端大量依赖全局状态和单例。Unity 场景切换、断线重连和异步加载时容易产生生命周期问题，应逐步改为模块化状态管理。

### 7.4 坐标系风险

地图、路径、角度、技能方向和相机方向可能在迁移后出现镜像、旋转或高度错误，必须建立统一坐标转换层。

### 7.5 性能风险

移动端需要提前设计对象池、GPU Instancing、LOD、合批、纹理压缩、粒子限制、地图分块、异步加载和网络实体裁剪。

### 7.6 安全与合规风险

迁移前需要确认游戏资源、协议和第三方库的使用与再发布权限；旧版保护系统通常不能原样迁移，移动端需要重新设计客户端完整性和反篡改策略。

## 8. 立即执行清单

### P0：技术可行性验证

- [ ] 在 Windows 上编译并运行旧客户端；
- [ ] 确认测试服务器和测试账号；
- [ ] 抓取登录、选角、进入地图和移动数据；
- [ ] 确认服务器是否允许自定义客户端；
- [ ] 确认 BMD/SMD 和地图资源是否可解析。

### P1：迁移资产整理

- [ ] 协议清单；
- [ ] 枚举清单；
- [ ] 结构体清单；
- [ ] 资源格式清单；
- [ ] 地图清单；
- [ ] 角色、怪物、技能清单；
- [ ] UI 窗口清单；
- [ ] 全局变量和单例清单。

### P2：两个最小原型

- [ ] C# 控制台协议客户端完成登录；
- [ ] Unity 显示一个转换后的角色和地图；
- [ ] 协议包通过字节级回归测试；
- [ ] 资源转换流程可以重复执行。

### P3：确定最终路线

```text
服务器：尽量保持兼容
协议：C# 重新实现
资源：离线转换
渲染：Unity 重做
UI：Unity 重做
游戏逻辑：按模块重写
旧 C++：作为行为、协议和资源格式参考
```

## 9. 建议的首个里程碑

首个里程碑不要以“完成全部客户端”为目标，而应以以下结果为完成标准：

```text
Unity 客户端
  → 连接现有服务器
  → 完成登录
  → 获取角色列表
  → 进入一个地图
  → 显示一个角色
  → 点击移动
  → 正确收发位置包
  → 断线后可以重连
```

完成这个闭环后，再扩展模型、技能、背包、任务和 UI，能够显著降低一次性重写整个客户端的风险。
