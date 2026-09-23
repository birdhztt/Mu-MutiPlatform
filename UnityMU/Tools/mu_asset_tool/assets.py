from __future__ import annotations

from pathlib import Path
import hashlib
import json
import shutil

SUPPORTED = {".smd", ".bmd", ".map", ".att", ".obj", ".tga", ".jpg", ".jpeg", ".png", ".bmp", ".wav"}


def inspect_file(path: str | Path) -> dict:
    file = Path(path)
    data = file.read_bytes()
    result = {
        "path": str(file),
        "name": file.name,
        "extension": file.suffix.lower(),
        "size": len(data),
        "sha256": hashlib.sha256(data).hexdigest(),
        "supported": file.suffix.lower() in SUPPORTED,
        "diagnostics": [],
    }
    if file.suffix.lower() == ".bmd":
        result["diagnostics"].append("BMD binary layout is version-dependent; provide a sample and schema before enabling conversion")
    elif file.suffix.lower() in {".map", ".att"}:
        result["diagnostics"].append("map parsing is not enabled until map version and coordinate layout are confirmed")
    return result


def build_manifest(root: str | Path) -> dict:
    base = Path(root)
    files = []
    for file in sorted(p for p in base.rglob("*") if p.is_file()):
        item = inspect_file(file)
        item["relativePath"] = file.relative_to(base).as_posix()
        files.append(item)
    return {"format": "MuAssetManifest", "version": 1, "root": str(base), "files": files}


def convert_texture(path: str | Path, output_dir: str | Path) -> Path:
    try:
        from PIL import Image
    except ImportError as exc:
        raise RuntimeError("texture conversion requires Pillow; run: python -m pip install -r requirements.txt") from exc
    source = Path(path)
    target = Path(output_dir) / (source.stem + ".png")
    target.parent.mkdir(parents=True, exist_ok=True)
    with Image.open(source) as image:
        image.convert("RGBA").save(target, "PNG")
    return target


def convert_textures(root: str | Path, output_dir: str | Path) -> list[str]:
    base = Path(root)
    converted = []
    for path in sorted(base.rglob("*")):
        if path.is_file() and path.suffix.lower() in {".tga", ".jpg", ".jpeg", ".bmp", ".png"}:
            converted.append(str(convert_texture(path, Path(output_dir) / path.relative_to(base).parent)))
    return converted


def write_json(data: dict, output: str | Path) -> None:
    target = Path(output)
    target.parent.mkdir(parents=True, exist_ok=True)
    target.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
