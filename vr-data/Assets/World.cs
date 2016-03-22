using Assets;
using UnityEngine;
using System.Collections;

public class World : MonoBehaviour 
{
    public Vector2 tileCoordinate;
    public int zoomLevel = 14;

	void Start ()
    {
        var tileObject = new GameObject("Tile (" + tileCoordinate.x.ToString() + ", " + tileCoordinate.y.ToString() + ", " + zoomLevel.ToString() + ")");
        var tile = tileObject.AddComponent<Tile>();
        tileObject.transform.parent = transform;
        StartCoroutine(tile.CreateTile(this, new Vector2(tileCoordinate.x, tileCoordinate.y), new Vector2(), zoomLevel));
    }
}
