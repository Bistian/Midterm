using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLiquid : MonoBehaviour
{
    public float size = 1;
    public int grid = 16;

    private MeshFilter filter = null;
    // Start is called before the first frame update
    void Start()
    {
        if (filter)
        {
            filter.GetComponent<MeshFilter>();
            filter.mesh = GenerateMesh();
        }
    }

    private Mesh GenerateMesh()
    {
        Mesh m = new Mesh();

        var verticies = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();

        for (int i = 0; i < grid + 1; i++)
        {
            for (int j = 0; j < grid; j++)
            {
                verticies.Add(new Vector3(-size * 5.0f + size * (i / ((float)grid)), 0, -size * 5.0f + size * (j / ((float)grid))));
                normals.Add(new Vector2(i / (float)grid, j / (float)grid));
            }
        }
        var triangles = new List<int>();
        var Verts = grid + 1;
        for (int i = 0; i < Verts * Verts - Verts; i++)
        {
            if ((i + 1) % Verts == 0)
            {
                continue;
            }
            triangles.AddRange(new List<int>() { i + 1 + Verts, i + Verts, i, i, i + 1, i + Verts + 1 });

        }

        m.SetVertices(verticies);
        m.SetNormals(normals);
        m.SetUVs(0, uvs);
        m.SetTriangles(triangles, 0);

        return m;
    }
}
