using UnityEngine;

public enum TileType
{
    Empty, Destination, Wall, SpawnPoint, Rail
}
public class TileContent : MonoBehaviour
{
    MapTileContentFactory originFactory;
    [SerializeField] TileType type = default;

    public TileType Type => type;

    public MapTileContentFactory OriginFactory
    {
        get => originFactory;
        set
        {
            Debug.Assert(originFactory == null, "Redfined Origin Factory!");
            originFactory = value;
        }
    }

    public void Recycle()
    {
        originFactory.Reclame(this);
    }

}
