using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Factory/Tile", order = 0)]
public class MapTileContentFactory : GameObjectFactory
{
    [SerializeField] TileContent destinationTile = default;
    [SerializeField] TileContent emptyTile = default;
    [SerializeField] TileContent wallTile = default;
    [SerializeField] TileContent spawnTile = default;
    [SerializeField] TileContent railTile = default;

    private TileContent Get(TileContent prefab)
    {
        TileContent instance = CreateGameObjectInstance(prefab);
        instance.OriginFactory = this;
        return instance;
    }

    public void Reclame(TileContent content)
    {
        Debug.Assert(content.OriginFactory == this, "Wrong facotory reclaimed!");
        Destroy(content.gameObject);    
    }


    public TileContent Get(TileType type)
    {
        switch (type)
        {
            case TileType.Destination:
                return Get(destinationTile);
            case TileType.Empty:
                return Get(emptyTile);
            case TileType.Wall:
                return Get(wallTile);
            case TileType.SpawnPoint:
                return Get(spawnTile);
            case TileType.Rail:
                return Get(railTile);
        }

        Debug.Assert(false, "Unsupported Type : " + type);
        return null;
    }
}
