from __future__ import annotations

import argparse
from pathlib import Path
import json

from .assets import build_manifest, convert_textures, inspect_file, write_json
from .smd import parse_smd, write_json as write_smd_json, write_obj


def main() -> int:
    parser = argparse.ArgumentParser(prog="mu-asset-tool", description="MuOnline Main 5.2 offline asset tools")
    sub = parser.add_subparsers(dest="command", required=True)

    inspect = sub.add_parser("inspect", help="inspect a file without modifying it")
    inspect.add_argument("input")

    smd = sub.add_parser("convert-smd", help="convert a text SMD to JSON and optional OBJ")
    smd.add_argument("input")
    smd.add_argument("--out", required=True)
    smd.add_argument("--obj", action="store_true", help="also write an OBJ preview")

    manifest = sub.add_parser("manifest", help="create an asset manifest")
    manifest.add_argument("input")
    manifest.add_argument("--out", required=True)

    textures = sub.add_parser("convert-textures", help="convert common textures to PNG")
    textures.add_argument("input")
    textures.add_argument("--out", required=True)

    args = parser.parse_args()
    if args.command == "inspect":
        print(json.dumps(inspect_file(args.input), ensure_ascii=False, indent=2))
    elif args.command == "convert-smd":
        data = parse_smd(args.input)
        output = Path(args.out)
        output.mkdir(parents=True, exist_ok=True)
        json_path = output / (Path(args.input).stem + ".mumodel.json")
        write_smd_json(data, json_path)
        if args.obj:
            write_obj(data, output / (Path(args.input).stem + ".obj"))
        print(json.dumps({"json": str(json_path), "triangles": len(data["triangles"]), "diagnostics": data["diagnostics"]}, ensure_ascii=False, indent=2))
    elif args.command == "manifest":
        write_json(build_manifest(args.input), args.out)
        print(args.out)
    elif args.command == "convert-textures":
        print(json.dumps({"converted": convert_textures(args.input, args.out)}, ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
