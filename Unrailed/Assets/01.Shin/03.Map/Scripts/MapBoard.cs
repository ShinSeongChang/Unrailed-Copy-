using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard : MonoBehaviour
{
    [SerializeField] Transform ground = default;
    [SerializeField] MapTile mapTile = default;

    MapTile[] mapTiles = null;

    Vector2Int size = default;

    Queue<MapTile> searchFrontier = new Queue<MapTile>();

    MapTileContentFactory contentFactory = default;

    // 지형 생성 메서드
    public void Initialize(Vector2Int size, MapTileContentFactory contentFactory)
    {
        this.size = size;
        this.contentFactory = contentFactory;
        ground.localScale = new Vector3(size.x, size.y, 1);
            
        Vector2 offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);

        mapTiles = new MapTile[size.x * size.y];

        for(int i = 0, y = 0; y < size.y; y++)
        {
            for(int x = 0; x < size.x; x++, i++)
            {
                MapTile tile = Instantiate(mapTile);
                mapTiles[i] = tile;

                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);


                // { 맵 생성시 이웃들 정의해주기
                if(x > 0)
                {
                    MapTile.MakeEastWestNeighbors(tile, mapTiles[i - 1]);
                }

                if(y > 0)
                {
                    MapTile.MakeNorthSouthNeighbors(tile, mapTiles[i - size.x]);
                }
                // } 맵 생성시 이웃들 정의해주기

                tile.isAlternative = (x & 1) == 0;

                if((y & 1) == 0)
                {
                    tile.isAlternative = !tile.isAlternative;
                }

                tile.Content = contentFactory.Get(TileType.Emtpy);
            }
        }

        ToggleDestination(mapTiles[mapTiles.Length / 2]);
    }

    private bool FindPaths()
    {
        // 모든 타일 경로 초기화
        foreach(MapTile tile in mapTiles)
        {
            if(tile.Content.Type == TileType.Destination)
            {
                tile.BecomDestination();
                searchFrontier.Enqueue(tile);
            }
            else
            {
                tile.ClearPath();
            }
        }

        if(searchFrontier.Count == 0)
        {
            return false;
        }

        // 목표가 될 타일 지정
        //mapTiles[mapTiles.Length / 2].BecomDestination();
        //searchFrontier.Enqueue(mapTiles[mapTiles.Length / 2]);


        // 03.11 시점에서 봤을 때 경로 찾는 것은 목표지점에서 부터 역순으로 자신의 이웃들에게 경로를 부여하는 듯 함
        while (searchFrontier.Count > 0)
        {
            MapTile tile = searchFrontier.Dequeue();

            if(tile != null)
            {                
                // if문이 서로 반대의 순서, 지그재그 방식으로 길찾기 진행
                if(tile.isAlternative)
                {
                    searchFrontier.Enqueue(tile.GrowPathNorth());
                    searchFrontier.Enqueue(tile.GrowPathSouth());
                    searchFrontier.Enqueue(tile.GrowPathEast());
                    searchFrontier.Enqueue(tile.GrowPathWest());
                }
                else
                {
                    searchFrontier.Enqueue(tile.GrowPathWest());
                    searchFrontier.Enqueue(tile.GrowPathEast());
                    searchFrontier.Enqueue(tile.GrowPathSouth());
                    searchFrontier.Enqueue(tile.GrowPathNorth());
                }

            }

        }

        foreach (MapTile tile in mapTiles)
        {
            tile.ShowPath();
        }

        return true;
    }



    // 몇번째 타일을 클릭하였는지 반환해주는 메서드
    public MapTile GetTile(Ray ray)
    {        
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            int x = (int)(hit.point.x + size.x * 0.5f);
            int y = (int)(hit.point.z + size.y * 0.5f);

            // 지정한 크기 안의 범위일때만 
            if (x >= 0 && x < size.x && y >= 0 && y < size.y)
            {
                Debug.Log(x + y * size.x + "번째 타일");
                return mapTiles[x + y * size.x];
            }
        }

        return null;
    }

    public void ToggleDestination(MapTile tile)
    {
        if(tile.Content.Type == TileType.Destination)
        {
            tile.Content = contentFactory.Get(TileType.Emtpy);

            if(!FindPaths())
            {
                tile.Content = contentFactory.Get(TileType.Destination);
                FindPaths();
            }

        }
        else
        {
            tile.Content = contentFactory.Get(TileType.Destination);
            FindPaths();
        }
    }
}
