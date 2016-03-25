using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CreateMeshFromWorldData : MonoBehaviour {

    public Texture2D heightmapTexture;
    public Texture2D diffuseTexture;

    public float maximumHeight;
    public Vector2 tileDimensionsInMetres;

    private Mesh tileMesh;
    private Vector3[] vertexData;

	void Start () {
        Debug.Log(heightmapTexture.width + " by " + heightmapTexture.height);
        GenerateMesh();
	}

    void GenerateMesh()
    {
        GetComponent<MeshFilter>().mesh = tileMesh = new Mesh();
        tileMesh.name = "Fake Terrain";

        float[,] heightmapData = GetHeightDataFromTexture(heightmapTexture);
        vertexData = new Vector3[(heightmapTexture.width + 1) * (heightmapTexture.height + 1)];
        Vector2[] uvData = new Vector2[vertexData.Length];

        Vector2 tileDimensionsInMetresPerPixel = tileDimensionsInMetres;
        tileDimensionsInMetresPerPixel.x /= heightmapTexture.width;
        tileDimensionsInMetresPerPixel.y /= heightmapTexture.height;

        Vector3[] vertices = new Vector3[(heightmapTexture.width + 1) * (heightmapTexture.height + 1)];
        for (int i = 0, y = 0; y <= heightmapTexture.height; y++)
        {
            for (int x = 0; x <= heightmapTexture.width; x++, i++)
            {
                vertices[i] = new Vector3(x * tileDimensionsInMetresPerPixel.x, heightmapData[x, y] * maximumHeight, y * tileDimensionsInMetresPerPixel.y);
            }
        }
        tileMesh.vertices = vertices;

        int[] triangles = new int[heightmapTexture.width * heightmapTexture.height * 6];
        for (int ti = 0, vi = 0, y = 0; y < heightmapTexture.height; y++, vi++)
        {
            for (int x = 0; x < heightmapTexture.width; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + heightmapTexture.width + 1;
                triangles[ti + 5] = vi + heightmapTexture.width + 2;
            }
        }
        tileMesh.triangles = triangles;

        tileMesh.RecalculateNormals();
    }

    float[,] GetHeightDataFromTexture(Texture2D heightmap)
    {
        float[,] heightData = new float[heightmap.width + 1, heightmap.height + 1];
        Color[] heightmapColors = heightmap.GetPixels();

        for (int x = 0; x < heightmap.width; x++)
        {
            for (int y = 0; y < heightmap.height; y++)
            {
                heightData[x, y] = heightmapColors[(x * heightmap.width) + y].grayscale;
            }
        }

        for (int x = 0; x <= heightmap.width; x++)
        {
            heightData[x, heightmap.height] = heightData[x, heightmap.height - 1];
        }
        for (int y = 0; y <= heightmap.height; y++)
        {
            heightData[heightmap.width, y] = heightData[heightmap.width - 1, y];
        }

        
        return heightData;
    }

    Material GenerateMaterialFromDiffuse(Texture2D diffuse)
    {
        Material mat = new Material(Shader.Find("Standard"));
        mat.SetTexture("_MainTex", diffuse);
        return mat;
    }

    
}
