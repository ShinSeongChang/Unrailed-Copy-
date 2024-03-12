using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Emtpy, Destination, Wall
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
