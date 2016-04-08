using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class Tile : MonoBehaviour {

    private Vector3 tileDimensions;
    private Texture2D heightmapTexture;
    private Texture2D diffuseTexture;

    public Vector2 uvBottomLeft;
    public Vector2 uvTopRight;

    private Mesh mesh;

    private void BuildMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();

        float[,] heightmapData = GetHeightDataFromTexture(heightmapTexture);

        Vector3[] vertices = new Vector3[(heightmapTexture.width + 1) * (heightmapTexture.height + 1)];
        Vector2[] uv = new Vector2[vertices.Length];

        float uvWidth = uvBottomLeft.x - uvTopRight.x;
        float uvHeight = uvBottomLeft.y - uvTopRight.y;

        for (int i = 0, z = 0; z <= heightmapTexture.height; z++)
        {
            for (int x = 0; x <= heightmapTexture.width; x++, i++)
            {
                vertices[i] = new Vector3(x * (tileDimensions.x / heightmapTexture.width), heightmapData[z, x] * tileDimensions.y, z * (tileDimensions.z / heightmapTexture.height));
                //vertices[i].x -= tileDimensions.x / 2;
                //vertices[i].z -= tileDimensions.y / 2;
                uv[i] = new Vector2((float)(x) / (float)(heightmapTexture.width), (float)(z) / (float)(heightmapTexture.height));
                uv[i].x = (uv[i].x * 0.25f) + uvBottomLeft.x;
                uv[i].y = (uv[i].y * 0.25f) + uvBottomLeft.y;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[heightmapTexture.width * heightmapTexture.height * 6];
        for (int ti = 0, vi = 0, y = 0; y < heightmapTexture.height; y++, vi++)
        {
            for (int x = 0; x < heightmapTexture.width; x++, ti += 6, vi++)
            {
                /*triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + heightmapTexture.width + 1;
                triangles[ti + 5] = vi + heightmapTexture.width + 2;*/
                triangles[ti] = vi + heightmapTexture.width + 1;
                triangles[ti + 2] = vi;
                triangles[ti + 1] = vi + 1;
                triangles[ti + 3] = vi + heightmapTexture.width + 1;
                triangles[ti + 5] = vi + 1;
                triangles[ti + 4] = vi + heightmapTexture.width + 2;

            }
        }
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void UpdateTexture(Texture2D newTexture)
    {
        Material mat = GetComponent<MeshRenderer>().material;
        mat.SetTexture("_MainTex", newTexture);
    }

    private float[,] GetHeightDataFromTexture(Texture2D heightmap)
    {
        // Setup arrays and get the color data from the heightmap
        float[,] heights = new float[heightmap.width + 1, heightmap.height + 1];
        Color[] colors = heightmap.GetPixels();

        // Create the height info from the grayscale component of the pixel
        for (int x = 0; x < heightmap.width; x++)
        {
            for (int y = 0; y < heightmap.height; y++)
            {
                heights[x, y] = colors[(x * heightmap.width) + y].grayscale;
            }
        }

        // Pad the outside of the heightmap by one unit in each direction
        for (int x = 0; x <= heightmap.width; x++)
        {
            heights[x, heightmap.height] = heights[x, heightmap.height - 1];
        }
        for (int y = 0; y <= heightmap.height; y++)
        {
            heights[heightmap.width, y] = heights[heightmap.width - 1, y];
        }

        return heights;
    }

    private Material GenerateMaterialFromDiffuse(Texture2D diffuse)
    {
        Material mat = new Material(Shader.Find("Unlit/Texture"));
        mat.mainTexture = diffuse;
        mat.mainTexture.wrapMode = TextureWrapMode.Clamp;
        return mat;
    }

    // ------------------------------------------------------------------------------------

    public void RebuildMesh()
    {
        BuildMesh();
    }

    public Vector3 Dimensions
    {
        get
        {
            return tileDimensions;
        }
        set
        {
            tileDimensions = value;
            BuildMesh();
        }
    }

    public Texture2D Heightmap
    {
        get
        {
            return heightmapTexture;
        }
        set
        {
            heightmapTexture = value;
            BuildMesh();
        }
    }

    public Texture2D Diffuse
    {
        get
        {
            return diffuseTexture;
        }
        set
        {
            diffuseTexture = value;
            UpdateTexture(value);
        }
    }
}
