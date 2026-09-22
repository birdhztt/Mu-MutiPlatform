# Mu-MutiPlatform

<div align="center">

**MuOnline Main 5.2 · Cross-platform development foundation**

[English](#english) · [简体中文](#简体中文)

</div>

---

<a name="english"></a>

## English

### About

**Mu-MutiPlatform** is a source-code project for MuOnline Main 5.2 and MuServer Update 15. It provides a cleaned and organized foundation for developers who want to study, build, customize, and extend a classic MuOnline client/server codebase.

The project is based on the original Webzen Main 5.2 source and the Louis Emulator Update 15 source tree. The included projects have been prepared for Visual Studio 2019 and support both Debug and Release configurations.

> This repository contains source code for development and research. It is not a ready-to-deploy, plug-and-play server package.

### Highlights

- Visual Studio 2019 project compatibility
- Debug and Release build configurations
- MuOnline Main 5.2 source code
- MuServer Update 15 source code
- MuServer/client-compatible project structure
- ASIO protocol support with an available enable/disable option
- CashShop integrated into the source without an external library dependency
- Main scene updates inspired by Season 6 systems, including FPS-related improvements
- Reduced legacy build definitions and unnecessary code
- English-focused source text and comments
- Initial groundwork for MapManager, ItemManager, and Loaddata components
- A cleaner foundation for continued feature development

### Planned or prepared extension areas

The codebase contains groundwork for future development in areas including:

- Rage Fighter support
- Right-click equipment interaction
- Lucky Items
- Karutan map support

These areas may require additional implementation and integration before they are production-ready.

### Repository layout

```text
Mu-MutiPlatform/
├── MuServer_Season_5_Update_15/   # MuServer package and related resources
├── Source Main 5.2/               # Main 5.2 source and dependencies
├── Source MuServer Update 15/      # MuServer Update 15 source
├── .gitignore
└── README.md
```

Directory contents may evolve as the project is cleaned up and extended.

### Requirements

- Windows
- Visual Studio 2019
- Desktop C++ development tools for Visual Studio
- The dependencies and project-specific files included in the repository

### Build

1. Clone the repository:

   ```bash
   git clone https://github.com/birdhztt/Mu-MutiPlatform.git
   cd Mu-MutiPlatform
   ```

2. Open the relevant Visual Studio solution or project files under the source directories.
3. Select the required platform and configuration, such as **Debug** or **Release**.
4. Restore or configure any local dependencies required by your environment.
5. Build the selected project with Visual Studio 2019.

Because this is a source project rather than a packaged server distribution, local configuration and dependency adjustments may be necessary.

### Season 6 systems not included

The following systems are not included in the current source package:

- New Master Skill Tree
- Mu Helper
- Extended Chest
- Extended Inventory

### Project status

This project is intended as a development foundation. It includes cleanup, compatibility updates, and several fixes, but additional testing and integration work may be required for a specific server environment.

### Contributing

Contributions are welcome. When submitting changes:

- Keep changes focused and clearly documented.
- Preserve the existing source organization where possible.
- Describe build or runtime impacts in the commit or pull request.
- Test both Debug and Release configurations when applicable.

### License and third-party notices

This repository is provided as-is for learning, research, and development purposes. Please review and comply with the original licensing terms of the included source materials and all third-party dependencies before redistributing or using the project.

---

<a name="简体中文"></a>

## 简体中文

### 项目简介

**Mu-MutiPlatform** 是一个面向 MuOnline Main 5.2 与 MuServer Update 15 的源代码项目，为希望研究、编译、定制和扩展经典 MuOnline 客户端/服务端代码的开发者提供基础框架。

本项目基于 Webzen Main 5.2 原始源码以及 Louis Emulator Update 15 源码整理而成，项目文件主要面向 Visual Studio 2019，并支持 Debug 与 Release 两种编译配置。

> 本仓库提供的是用于开发和研究的源代码，并不是开箱即用的完整服务器安装包。

### 主要特性

- 支持 Visual Studio 2019 项目环境
- 支持 Debug 和 Release 编译配置
- 包含 MuOnline Main 5.2 源码
- 包含 MuServer Update 15 源码
- 保持 MuServer/客户端兼容的项目结构
- 支持 ASIO 协议，并提供启用/关闭选项
- CashShop 直接集成到源码中，不依赖外部 LIB
- 主场景引入部分 Season 6 风格系统，包括 FPS 相关改进
- 精简旧的编译宏和不必要代码
- 统一以英文源码文本和注释为主
- 初步整理 MapManager、ItemManager 和 Loaddata 等组件
- 为后续功能开发提供更清晰的代码基础

### 已准备的扩展方向

当前代码库为以下方向保留或准备了基础代码：

- Rage Fighter 支持
- 右键装备物品交互
- Lucky Items
- Karutan 地图支持

以上功能可能仍需要进一步开发、测试和整合，不能直接视为完整的生产功能。

### 仓库结构

```text
Mu-MutiPlatform/
├── MuServer_Season_5_Update_15/   # MuServer 程序包及相关资源
├── Source Main 5.2/               # Main 5.2 源码及依赖
├── Source MuServer Update 15/      # MuServer Update 15 源码
├── .gitignore
└── README.md
```

随着项目持续整理和扩展，目录内容可能会发生变化。

### 环境要求

- Windows 操作系统
- Visual Studio 2019
- Visual Studio 的“使用 C++ 的桌面开发”工具集
- 仓库中包含的依赖项及项目专用文件

### 编译步骤

1. 克隆仓库：

   ```bash
   git clone https://github.com/birdhztt/Mu-MutiPlatform.git
   cd Mu-MutiPlatform
   ```

2. 使用 Visual Studio 2019 打开源码目录下对应的解决方案或项目文件。
3. 选择目标平台以及 **Debug** 或 **Release** 配置。
4. 根据本地环境恢复或配置所需依赖。
5. 使用 Visual Studio 2019 编译目标项目。

由于本项目是源码工程而不是完整的服务器发行包，因此在不同开发环境中可能需要额外调整配置和依赖项。

### 未包含的 Season 6 系统

当前源码包不包含以下系统：

- 新 Master Skill Tree
- Mu Helper
- 扩展宝箱
- 扩展背包

### 项目状态

本项目定位为持续开发的基础工程，包含代码清理、兼容性调整以及多项修复。针对特定服务器环境使用时，仍可能需要进一步测试、配置和整合。

### 参与贡献

欢迎提交贡献。提交代码时建议：

- 保持修改范围明确，并补充必要说明。
- 尽量保留现有源码目录结构。
- 在提交信息或 Pull Request 中说明对编译和运行行为的影响。
- 在适用情况下同时验证 Debug 和 Release 配置。

### 许可证与第三方声明

本仓库按现状提供，用于学习、研究和开发。重新分发或使用本项目之前，请确认并遵守所包含源码及所有第三方依赖的原始许可证和使用条款。

---

<div align="center">

如果本项目对你有帮助，欢迎 Star、提出 Issue 或提交改进建议。

If this project is useful to you, consider starring the repository, opening an issue, or submitting an improvement.

</div>
