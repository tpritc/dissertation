using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public Texture2D worldHeightmap;
    public Texture2D worldDiffuse;
    public Vector3 worldDimensions;

    private Tile[] tiles;
    private static int tileSize = 128;
    private Vector3 tileDimensions;

	void Start () {
        
        tiles = new Tile[(worldHeightmap.width / tileSize) * (worldHeightmap.height * tileSize)];
        tileDimensions = new Vector3(worldDimensions.x / (worldHeightmap.width / tileSize), worldDimensions.y, worldDimensions.z / (worldHeightmap.height / tileSize));

        float diffuseAsProportionOfHeightmap = worldDiffuse.width / worldHeightmap.width;

        Debug.Log("Heightmap: " + worldHeightmap.width + " by " + worldHeightmap.height);

        for (int x = 0, i = 0; x < worldHeightmap.width / tileSize; x++)
        {
            for (int y = 0; y < worldHeightmap.height / tileSize; y++, i++)
            {
                Texture2D tileHeightmap = new Texture2D(tileSize, tileSize);
                tileHeightmap.SetPixels(worldHeightmap.GetPixels(x * tileSize, y * tileSize, tileSize, tileSize));

                float proportions = (float)tileSize / (float)worldHeightmap.width;
                Vector2 tileUVTopLeft = new Vector2(x * proportions, y * proportions);
                Vector2 tileUVBottomRight = new Vector2((x + 1) * proportions, (y + 1) * proportions);

                Vector3 tileOffset = new Vector3(x * tileDimensions.x, 0.0f, y * tileDimensions.z);

                GameObject tileGameObject = new GameObject("Tile (" + x + ", " + y + ")");

                tiles[i] = tileGameObject.AddComponent<Tile>();
                tiles[i].Heightmap = tileHeightmap;
                tiles[i].Diffuse = worldDiffuse;
                tiles[i].Dimensions = tileDimensions;
                tiles[i].uvTopLeft = tileUVTopLeft;
                tiles[i].uvBottomRight = tileUVBottomRight;

                tileGameObject.transform.parent = transform;
                tileGameObject.transform.position = tileOffset;
            }
        }
	}
}
