using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    Mesh mesh;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private Vector2[] uvs;

    private Texture2D heightMap;

    public int i2 = 0;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        heightMap = PerlinNoise.GenerateTexture(256, 256, Random.Range(0, 9999), Random.Range(0, 9999), 10);

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        int i = 0;

        for (int x = 0; x < 64; x++) 
        {
            for (int y = 0; y < 64; y++)
            {
                vertices.Add(new Vector3(x+0, heightMap.GetPixel(x+0, y+0).r*16, y+0));
                vertices.Add(new Vector3(x, heightMap.GetPixel(x + 0, y + 1).r*16, y+1));
                vertices.Add(new Vector3(x+1, heightMap.GetPixel(x + 1, y + 0).r*16, y+0));
                vertices.Add(new Vector3(x+1, heightMap.GetPixel(x + 1, y + 1).r*16, 1+y));

                triangles.Add(0 + i);
                triangles.Add(1 + i);
                triangles.Add(2 + i);
                triangles.Add(1 + i);
                triangles.Add(3 + i);
                triangles.Add(2 + i);

                i += 4;
            }
        }

        uvs = new Vector2[vertices.Count];

        for (int x = 0; x < 128; x++)
        {
            for (int y = 0; y < 128; y++)
            {
                uvs[i2] = new Vector2((float)x / 128, (float)y / 128);

                i2++;
            }
        }
    }

    void UpdateMesh() 
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }
}
