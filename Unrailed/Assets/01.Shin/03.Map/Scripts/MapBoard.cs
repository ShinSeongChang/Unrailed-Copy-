using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard : MonoBehaviour
{
    [SerializeField] Transform ground = default;
    [SerializeField] MapTile mapTile = default;

    MapTile[] mapTiles = null;

    Vector2Int size = default;

    public void Initialize(Vector2Int size)
    {
        mapTiles = new MapTile[size.x * size * y];

        this.size = size;
        ground.localScale = new Vector3(size.x, size.y, 1);
            
        Vector2 offset = new Vector2(size.x - 1 * 0.5f, size.y - 1 * 0.5f);

        for(int i = 0, y = 0; y < size.y; y++)
        {
            for(int x = 0; x < size.x; x++, i++)
            {
                MapTile tile = Instantiate(mapTile);
                mapTiles[i] = tile;

                tile.transform.SetParent(transform, false);
                tile.transform.localRotation = Quaternion.Euler(90, 0, 0);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);
            }
        }
    }

}
