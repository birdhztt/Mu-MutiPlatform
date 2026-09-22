# Mu-MutiPlatform

MuOnline Main 5.2 for cross platform.

This repository contains a cleaned and organized version of the MuOnline Main 5.2 source code together with the MuServer Update 15 sources, intended for development and compilation with Visual Studio 2019. It is based on the original Webzen Main 5.2 and the Louis emulator update, with many cleanup changes and compatibility adjustments for modern builds.

## Overview

This project is a custom server-side codebase for MuOnline, including:

- Source Main 5.2 clean
- Source MuServer Update 15
- MuServer/client compatible structure
- Visual Studio 2019 compatible project setup
- Modernized build options for Debug and Release mode

## Included in this repository

- Source Main 5.2 clean
- Source MuServer Update 15 Louis emulator
- MuServer/Client compatible code
- Updated project files for Visual Studio 2019

## Main features

- All files updated for Visual Studio 2019
- New ASIO protocol support (on/off option included)
- CashShop integrated directly into the source without external LIB dependency
- Main scene updated to Season 6 style systems (FPS-related improvements)
- Compilation works in both Debug and Release modes
- Removed unnecessary language variants, leaving English as default
- Removed most unused build definitions and unnecessary global settings
- Cleaned out Chinese text/comments from the source tree
- Simplified and reduced a large number of defines to essential options only
- Prepared foundation for future coding work such as:
  - Rage Fighter (S6)
  - Right-click equipped item system (S6)
  - Lucky Items (S6)
  - Karutan Map (S6)
- Added and improved new classes such as MapManager, ItemManager, and Loaddata groundwork
- Removed a large amount of unused and obsolete code
- Includes several stability and compatibility fixes

## Repository structure

```text
Mu-MutiPlatform/
├── MuServer_Season_5_Update_15/
├── Source Main 5.2/
├── Source MuServer Update 15/
├── .gitignore
├── README.md
└── ...
```

The repository is organized around the original MuOnline sources, with separate source folders for the main client/server logic and the MuServer update branch.

## Build requirements

- Windows environment
- Visual Studio 2019
- C++ build tools compatible with the project configuration
- Project-specific dependencies as included in the source tree

## Build notes

- The project targets a Windows/MSVC build environment
- Debug and Release build paths are both supported
- The source tree is intended for customization and extension
- Some systems from later MuOnline seasons are not included in this package

## Season 6 systems not included

- New Master Skill tree
- Mu Helper
- Extended chest
- Extended inventory

## Notes

This project is a source-based emulator foundation and is best suited for developers who want to study, modify, or extend an older MuOnline codebase. It is not a plug-and-play packaged server, and compilation may require project configuration adjustments depending on your local environment.

## License and usage

This repository is provided as-is for learning, research, and development purposes. Please respect the original licensing terms of the included source materials and any third-party dependencies present in the project tree.

## Getting started

1. Clone the repository.
2. Open the relevant Visual Studio solution/project files in the source folders.
3. Select the desired configuration (Debug or Release).
4. Build the project.
5. Review the source structure and adjust the project as needed for your environment.

## Contribution

Contributions, cleanup, and compatibility fixes are welcome. If you are working on this project, keep the original structure and clearly document any changes that affect build behavior or server logic.

---

This README is intended to provide a clean overview of the project and the included source packages without changing the original intent of the repository.
