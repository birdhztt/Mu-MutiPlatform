#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityMU.Tools
{
    [Serializable] public class MuModel { public string format; public string source; public MuTriangle[] triangles; }
    [Serializable] public class MuTriangle { public string texture; public MuVertex[] vertices; }
    [Serializable] public class MuVertex { public int bone; public float[] position; public float[] normal; public float[] uv; }

    public sealed class MuModelImporter : AssetPostprocessor
    {
        static bool IsModel(string path) => path.EndsWith(".mumodel.json", StringComparison.OrdinalIgnoreCase);

        static void OnPostprocessAllAssets(string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            foreach (var path in imported)
                if (IsModel(path)) CreatePrefab(path);
        }

        static void CreatePrefab(string jsonPath)
        {
            var model = JsonUtility.FromJson<MuModel>(File.ReadAllText(jsonPath));
            if (model == null || model.triangles == null || model.triangles.Length == 0) return;
            var root = new GameObject(Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(jsonPath)));
            var mesh = new Mesh { name = root.name + "_Mesh" };
            var vertices = new List<Vector3>(); var normals = new List<Vector3>();
            var uv = new List<Vector2>(); var indices = new List<int>();
            foreach (var triangle in model.triangles)
            {
                foreach (var v in triangle.vertices)
                {
                    vertices.Add(ToVector(v.position));
                    normals.Add(v.normal != null && v.normal.Length >= 3 ? ToVector(v.normal) : Vector3.up);
                    uv.Add(v.uv != null && v.uv.Length >= 2 ? new Vector2(v.uv[0], 1f - v.uv[1]) : Vector2.zero);
                    indices.Add(indices.Count);
                }
            }
            mesh.SetVertices(vertices); mesh.SetNormals(normals); mesh.SetUVs(0, uv); mesh.SetTriangles(indices, 0); mesh.RecalculateBounds();
            var child = new GameObject("Mesh"); child.transform.SetParent(root.transform, false);
            var filter = child.AddComponent<MeshFilter>(); filter.sharedMesh = mesh; child.AddComponent<MeshRenderer>();
            var prefabPath = Path.ChangeExtension(jsonPath, ".prefab").Replace("\\", "/");
            PrefabUtility.SaveAsPrefabAsset(root, prefabPath);
            UnityEngine.Object.DestroyImmediate(root);
            AssetDatabase.ImportAsset(prefabPath);
        }

        static Vector3 ToVector(float[] value) => value != null && value.Length >= 3 ? new Vector3(value[0], value[1], value[2]) : Vector3.zero;
    }
}
#endif
