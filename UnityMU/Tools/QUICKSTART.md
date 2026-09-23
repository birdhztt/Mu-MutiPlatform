# MuAssetTool quick start

```bash
cd UnityMU/Tools
python -m mu_asset_tool inspect ../../path/to/model.smd
python -m mu_asset_tool convert-smd ../../path/to/model.smd --out ../../converted --obj
python -m mu_asset_tool manifest ../../path/to/assets --out ../../converted/assets.muassets.json
python -m mu_asset_tool convert-textures ../../path/to/assets --out ../../converted/textures
```

`convert-smd` 只处理当前已确认的文本 SMD 网格部分。骨骼动画、BMD 二进制和地图格式仍会输出诊断信息，避免在未知格式上生成错误数据。
