using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyFactory originFactory;
    MapTile tileFrom, tileTo;
    Vector3 positionFrom, positionTo;
    float progress;

    public EnemyFactory OriginFactory
    {
        get => originFactory;
        set
        {
            Debug.Assert(originFactory == null, "Redefined origin factory!");
            originFactory = value;
        }
    }

    public void SpawnOn(MapTile tile)
    {
        Debug.Assert(tile.NextOnPath != null, "Nowhere to go!", this);

        tileFrom = tile;
        tileTo = tile.NextOnPath;
        positionFrom = tileFrom.transform.localPosition;
        positionTo = tileTo.transform.localPosition;
        positionTo = tileFrom.ExitPoint;
        transform.localRotation = tileFrom.PathDirection.GetRotation();
        progress = 0f;
    }

    public bool GameUpdate()
    {
        progress += Time.deltaTime;
        while (progress >= 1f)
        {
            tileFrom = tileTo;
            tileTo = tileTo.NextOnPath;

            if (tileTo == null)
            {
                OriginFactory.Reclaim(this);
                return false;
            }

            positionFrom = positionTo;
            positionTo = tileTo.transform.localPosition;
            positionTo = tileFrom.ExitPoint;
            transform.localRotation = tileFrom.PathDirection.GetRotation();
            progress -= 1f;
        }

        transform.localPosition = Vector3.LerpUnclamped(positionFrom, positionTo, progress);
        return true;
    }
}
