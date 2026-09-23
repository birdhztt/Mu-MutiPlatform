# UnityMU Tools

离线资源转换工具的第一版，目标是把 MuOnline 客户端资源转换为 Unity 可导入的中间数据。

## 当前能力

- 解析文本 SMD 的 `nodes`、`skeleton`、`triangles` 段；
- 导出 `.mumodel.json` 中间格式；
- 导出可快速检查的 Wavefront `.obj`；
- 生成资源目录清单 `.muassets.json`；
- 转换纹理为 PNG（需要 Pillow）；
- 对 BMD 和地图文件执行安全的格式探测并输出诊断，不会假装支持未知二进制格式；
- 提供 Unity Editor 导入器，将 `.mumodel.json` 生成为 Mesh、Material 和 Prefab。

## 使用方式

在 `UnityMU/Tools` 目录执行：

```bash
python -m mu_asset_tool inspect path/to/file.smd
python -m mu_asset_tool convert-smd path/to/file.smd --out output
python -m mu_asset_tool manifest path/to/assets --out output/assets.muassets.json
python -m mu_asset_tool convert-textures path/to/assets --out output/textures
```

安装可选纹理依赖：

```bash
python -m pip install -r requirements.txt
```

## 设计约束

- 不直接复用旧客户端的 OpenGL、Win32 和裸指针运行时；
- 转换结果使用显式 JSON，便于审计、版本控制和 Unity 导入；
- 未确认格式前，BMD/地图只做探测和清单，不进行破坏性转换；
- 资源转换必须在拥有合法使用权限的资源上进行。

## 后续实现顺序

1. 根据实际样本补全 BMD 二进制解析器；
2. 根据地图文件样本补全地形、阻挡、对象和传送门解析；
3. 增加骨骼动画到 Unity AnimationClip 的导出；
4. 增加纹理脚本、材质类型和 Alpha/Blend 映射；
5. 增加转换回归测试和字节/顶点数量校验。
