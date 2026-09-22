# Mu-MutiPlatform

<div align="center">

  <h3>MuOnline Main 5.2 • Cross-platform development foundation</h3>
  <p>
    <a href="https://github.com/birdhztt/Mu-MutiPlatform">Repository</a>
  </p>

</div>

---

<a name="english"></a>

## English

### 1. Project Overview

`Mu-MutiPlatform` is a source-based development repository for MuOnline Main 5.2 and MuServer Update 15. It brings together a cleaned and structured foundation based on the original Webzen Main 5.2 source code and the Louis Emulator Update 15 implementation, with compatibility and modernization work for Visual Studio 2019.

This repository is intended for developers who want to study, build, maintain, and extend a classic MuOnline server codebase. It is not a packaged, ready-to-run server distribution; rather, it is a technical codebase meant to be compiled and adapted in a local development environment.

### 2. Intended Scope

The repository includes:

- MuOnline Main 5.2 source tree
- MuServer Update 15 source tree
- source organization and cleanup for maintainability
- updated project compatibility for Visual Studio 2019
- compatibility-oriented adjustments for Debug and Release builds
- a foundation for feature extension and custom development

### 3. Key Technical Characteristics

This project aims to provide a more workable and readable codebase for continued development. The main characteristics include:

- Visual Studio 2019 compatibility
- Debug and Release build support
- English-centric source text and comments
- removal of a large amount of unnecessary language-specific content
- reduction of legacy build defines and noise
- simplified project structure to support easier maintenance
- integrated CashShop source support without relying on external LIB packaging
- ASIO protocol support with toggling capability
- improved project cleanliness for future custom features
- starter-level groundwork for future systems such as:
  - Rage Fighter
  - Right-click equipped item behavior
  - Lucky Items
  - Karutan Map

### 4. Repository Structure

```text
Mu-MutiPlatform/
├── MuServer_Season_5_Update_15/
│   └── MuServer-related source and resources
├── Source Main 5.2/
│   └── Main 5.2 source tree
├── Source MuServer Update 15/
│   └── MuServer update source tree
├── .gitignore
├── README.md
└── additional project files and dependencies
```

The exact directory layout may vary depending on the included source packages and project organization.

### 5. Build Requirements

The project is primarily aimed at a Windows-based development workflow.

Required environment:

- Windows operating system
- Visual Studio 2019
- Desktop development with C++ workload
- repository-included dependencies or project-specific components
- local environment compatibility adjustments when needed

### 6. Build Instructions

1. Clone the repository:

   ```bash
   git clone https://github.com/birdhztt/Mu-MutiPlatform.git
   cd Mu-MutiPlatform
   ```

2. Open the relevant solution or project files using Visual Studio 2019.
3. Select the desired platform and configuration, such as `Debug` or `Release`.
4. Restore required project dependencies if your local environment requires them.
5. Build the project.

Note: This project is a source-level engine and emulator codebase, so some local configuration changes may be required depending on the build environment, toolchain, and dependency setup.

### 7. Included vs. Not Included

#### Included in the project

- source tree for MuOnline Main 5.2
- source tree for MuServer Update 15
- compatibility cleanup for Visual Studio 2019
- ASIO protocol option support
- CashShop source integration
- improvements for build stability and maintainability
- selected Season 6-style foundation work

#### Not included in the current source package

- New Master Skill Tree
- Mu Helper
- Extended Chest
- Extended Inventory

These systems are explicitly not included in the repository as delivered and would require additional implementation work.

### 8. Development Notes

This codebase is suitable for:

- source-level study and reverse engineering
- custom feature development
- debugging and modernization efforts
- project cleanup and compatibility work
- new system prototyping for later MuOnline-era features

Because it is based on a historical source tree, some code may be legacy, partially commented, or require adaptation to modern toolchains. This repository is best treated as a starting point for a custom server or research project rather than a turnkey production deployment.

### 9. Maintenance and Contribution

Contributions are welcome, especially for:

- build fixes
- compatibility improvements
- code cleanup and refactoring
- documentation improvements
- bug fixes and stability improvements

When contributing:

- keep the changes focused and well documented
- preserve the existing source organization where possible
- clearly note any build or runtime impact
- validate both Debug and Release behavior when relevant

### 10. License and Third-Party Notice

This repository is provided as-is for education, research, development, and customization. Before redistribution or production usage, ensure that you comply with the licensing requirements and constraints of the original source materials and any third-party dependencies included within this project.

---

<a name="简体中文"></a>

## 简体中文

### 1. 项目概述

`Mu-MutiPlatform` 是一个面向 MuOnline Main 5.2 和 MuServer Update 15 的源码开发仓库。它整合了基于 Webzen 原始 Main 5.2 源码与 Louis Emulator Update 15 的清理版与结构化版本，并补充了适配 Visual Studio 2019 的兼容性与现代化改造。

本仓库适合开发者用于学习、编译、维护和扩展经典 MuOnline 服务端代码。它不是一个开箱即用的完整服务器发行包，而是一个用于本地开发环境编译和二次开发的技术源码仓库。

### 2. 项目用途

本仓库包含以下内容：

- MuOnline Main 5.2 源代码树
- MuServer Update 15 源代码树
- 清理和整理后的代码结构
- 面向 Visual Studio 2019 的兼容性更新
- Debug / Release 双配置的构建支持
- 后续功能扩展与自定义开发的基础框架

### 3. 核心技术特点

该项目旨在提供一个更适合持续开发和维护的代码基础，主要特点包括：

- Visual Studio 2019 兼容性
- 支持 Debug 和 Release 编译
- 以英文文本和注释为主
- 清除了大量不必要的语言特定内容
- 精简旧版编译宏及冗余内容
- 简化项目结构，便于维护
- CashShop 直接集成到源码中，无需依赖外部 LIB 包装
- 支持 ASIO 协议，并提供可控开关
- 提升项目整洁度，便于未来功能扩展
- 为后续系统准备了初步基础：
  - Rage Fighter
  - 右键装备物品逻辑
  - Lucky Items
  - Karutan Map

### 4. 仓库结构

```text
Mu-MutiPlatform/
├── MuServer_Season_5_Update_15/
│   └── MuServer 相关源码与资源
├── Source Main 5.2/
│   └── Main 5.2 源码树
├── Source MuServer Update 15/
│   └── MuServer 更新源码树
├── .gitignore
├── README.md
└── 其它项目文件及依赖
```

实际目录结构可能因源码包内容和整理方式而有所不同。

### 5. 构建环境要求

本项目主要面向 Windows 开发环境。

所需条件：

- Windows 操作系统
- Visual Studio 2019
- C++ 桌面开发工作负载
- 仓库自带的依赖项或项目专用组件
- 根据本地环境可能需要额外配置

### 6. 编译步骤

1. 克隆仓库：

   ```bash
   git clone https://github.com/birdhztt/Mu-MutiPlatform.git
   cd Mu-MutiPlatform
   ```

2. 使用 Visual Studio 2019 打开对应的解决方案或项目文件。
3. 选择目标平台与构建配置，例如 `Debug` 或 `Release`。
4. 如本地环境要求，恢复并配置所需依赖。
5. 编译项目。

需要说明的是：这是一个源码级的服务器/引擎代码库，因此不同环境下可能需要额外调整配置、依赖和构建参数。

### 7. 包含内容与未包含内容

#### 已包含内容

- MuOnline Main 5.2 源代码
- MuServer Update 15 源码
- Visual Studio 2019 兼容性整理
- ASIO 协议支持
- CashShop 源码集成
- 构建稳定性与维护性优化
- 部分 Season 6 风格基础功能代码

#### 当前未包含内容

- New Master Skill Tree
- Mu Helper
- Extended Chest
- Extended Inventory

这些功能未出现在当前交付版本中，若需要使用，需继续实现和集成。

### 8. 开发说明

本代码库适合用于：

- 源码学习与研究
- 自定义功能开发
- 调试与现代化改造
- 清理和兼容性修复
- 后续 MuOnline 时代功能的原型开发

由于它基于历史源码树，部分代码可能存在老旧结构、注释残留，或者需要针对当前编译工具链进行适配。因此，它更适合作为开发起点，而不是直接部署的生产环境包。

### 9. 维护与贡献

欢迎参与贡献，尤其适合以下方向：

- 构建修复
- 兼容性增强
- 代码清理与重构
- 文档更新
- 错误修复与稳定性优化

参与贡献时建议：

- 保持改动聚焦且清晰说明
- 尽量保留现有源码组织结构
- 明确说明修改对编译或运行的影响
- 在适用情况下验证 Debug 与 Release 构建

### 10. 许可证与第三方声明

本仓库按现状提供，供学习、研究、开发和二次定制使用。在重新分发或用于生产环境前，请确认遵守所包含原始源码及第三方依赖的许可证要求和使用条款。

---

<div align="center">

  <p><strong>MuOnline Main 5.2 · Source Development Repository</strong></p>
  <p>For learning, development, building, and custom feature work.</p>

</div>
