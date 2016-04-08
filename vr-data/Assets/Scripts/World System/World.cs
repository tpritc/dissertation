using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public Texture2D worldHeightmap;
    public Texture2D worldDiffuse;
    public Vector3 worldDimensions;

    private Texture2D rotatedWorldDiffuse;

    private Tile[] tiles;
    private static int tileSize = 128;
    private Vector3 tileDimensions;

	void Start () {
        
        tiles = new Tile[(worldHeightmap.width / tileSize) * (worldHeightmap.height * tileSize)];
        tileDimensions = new Vector3(worldDimensions.x / (worldHeightmap.width / tileSize), worldDimensions.y, worldDimensions.z / (worldHeightmap.height / tileSize));

        Vector3 tileAsProportionOfWorld = new Vector3(0.25f, 1.0f, 0.25f);
        //new Vector2(1 / (worldHeightmap.width / tileSize), 1 / (worldHeightmap.height / tileSize));

        Debug.Log("Tile as a Proportion of the World: " + tileAsProportionOfWorld);

        for (int y = 0, i = 0; y < worldHeightmap.width / tileSize; y++)
        {
            for (int x = 0; x < worldHeightmap.height / tileSize; x++, i++)
            {
                // Create GameObject and Tile
                GameObject tileGameObject = new GameObject("Tile (" + x + ", " + y + ")");
                tiles[i] = tileGameObject.AddComponent<Tile>();

                // Create Heightmap for Tile
                Texture2D tileHeightmap = new Texture2D(tileSize, tileSize);
                tileHeightmap.SetPixels(worldHeightmap.GetPixels(x * tileSize, y * tileSize, tileSize, tileSize));

                // Create UV Map for Tile
                Vector2 tileUVBottomLeft = new Vector2(x * tileAsProportionOfWorld.x, y * tileAsProportionOfWorld.z);
                Vector2 tileUVTopRight = new Vector2((x + 1) * tileAsProportionOfWorld.x, (y + 1) * tileAsProportionOfWorld.z);
                tiles[i].uvBottomLeft = tileUVBottomLeft;
                tiles[i].uvTopRight = tileUVTopRight;


                // Set Heightmap, Diffuse, and the Dimensions of the Tile
                tiles[i].Heightmap = tileHeightmap;
                tiles[i].Diffuse = worldDiffuse;
                tiles[i].Dimensions = tileDimensions;

                // Place it nicely in the world
                tileGameObject.transform.parent = transform;
                Vector3 tileOffset = new Vector3(x * tileDimensions.x, 0.0f, y * tileDimensions.z); // multiply z by -1f to make it work idk
                tileGameObject.transform.position = tileOffset;
                //tileGameObject.transform.localScale -= new Vector3(tileGameObject.transform.localScale.x * 2, 0.0f, 0.0f);
                //tileGameObject.transform.Rotate(Vector3.up, 90);
                Debug.Log(tileGameObject.GetComponent<MeshFilter>().mesh.uv[673]);
            }
        }
	}
}
