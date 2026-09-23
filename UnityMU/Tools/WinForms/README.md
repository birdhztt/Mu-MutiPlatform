# UnityMU WinForms 资源工具

这是 UnityMU 资源迁移工具的第一步：使用 C# WinForms 扫描 `Source Main 5.2/bin/Data`，识别 `Player` 目录中的 BMD、OZJ、OZT 等资源，并导出资源清单。

## 已实现

- 选择资源根目录；
- 扫描全部子目录；
- 按扩展名分类统计；
- 识别 `Player` 目录中的玩家资源；
- 计算文件大小和 SHA-256；
- 对 BMD 输出安全诊断信息；
- 导出 `muassets.json`；
- 导出 CSV；
- 不修改、不覆盖原始资源。

## 运行

需要 .NET 8 SDK 和 Windows：

```powershell
cd UnityMU/Tools/WinForms
dotnet run
```

或发布独立程序：

```powershell
dotnet publish -c Release -r win-x64 --self-contained false
```

## 当前边界

第一步只做资源扫描和 BMD 诊断，不猜测未知 BMD 二进制布局，也不直接生成错误的 FBX/Prefab。下一步应使用真实 `player.bmd` 样本和旧客户端 `BMD::Open/Open2` 的实现继续完成解析器。
