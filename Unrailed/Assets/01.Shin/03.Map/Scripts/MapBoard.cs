using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class MapBoard : MonoBehaviour
{
    [SerializeField] Transform ground = default;
    [SerializeField] MapTile mapTile = default;
    [SerializeField] Texture2D gridTexture = default;

    MapTile[] mapTiles = null;

    Vector2Int size = default;

    Queue<MapTile> searchFrontier = new Queue<MapTile>();

    MapTileContentFactory contentFactory = default;

    List<MapTile> spawnPoints = new List<MapTile>();

    bool showPaths, showGrid;

    public bool ShowPaths
    {
        get => showPaths;
        set
        {
            showPaths = value;

            if(showPaths)
            {
                foreach(MapTile tile in mapTiles)
                {
                    tile.ShowPath();
                }
            }
            else
            {
                foreach (MapTile tile in mapTiles)
                {
                    tile.HidePath();
                }
            }
        }
    }

    public bool ShowGrid
    {
        get => showGrid;
        set
        {
            showGrid = value;
            Material m = ground.GetComponent<MeshRenderer>().material;

            if (showGrid) 
            {
                m.mainTexture = gridTexture;
                m.SetTextureScale("_MainTex", size);
            }
            else { m.mainTexture = null; }
        }
    }

    public int SpawnPointCount => spawnPoints.Count;

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
                    // x축 이웃 정해주기
                    MapTile.MakeEastWestNeighbors(tile, mapTiles[i - 1]);
                }

                if(y > 0)
                {
                    // y축 이웃 정해주기
                    MapTile.MakeNorthSouthNeighbors(tile, mapTiles[i - size.x]);
                }
                // } 맵 생성시 이웃들 정의해주기


                // x값이 짝수인지 아닌지 비트연산
                tile.isAlternative = (x & 1) == 0;

                if((y & 1) == 0)
                {
                    tile.isAlternative = !tile.isAlternative;
                }

                tile.Content = contentFactory.Get(TileType.Empty);
            }
        }

        ToggleDestination(mapTiles[mapTiles.Length / 2]);
        ToggleSpawnPoint(mapTiles[0]);
    }

    private bool FindPaths()
    {
        // 모든 타일 경로 초기화
        foreach(MapTile tile in mapTiles)
        {
            // 최종 목적지 타일 찾아서 가장먼저 Queue에 넣기
            if(tile.Content.Type == TileType.Destination)
            {
                tile.BecomDestination();
                searchFrontier.Enqueue(tile);
            }
            // 나머지 타일은 경로 초기화
            else { tile.ClearPath(); }
        }

        // 최종 목적지가 없으면 경로갱신 false
        if(searchFrontier.Count == 0)
        {
            return false;
        }

        while (searchFrontier.Count > 0)
        {
            // 최종 목적지 타일 꺼내기
            MapTile tile = searchFrontier.Dequeue();

            // 갱신한 이웃이 없을 때 == null일 경우 방어
            if(tile != null)
            {                
                // 해당 타일의 4이웃 타일 방문하여 내 자신의 정보를 이용하여 거리 갱신, Queue 갱신
                // 이후 이웃이 모두 없어질때까지 해당 반복문 반복
                // 특정 불값을 이용해서 검색조건을 서로 대칭이 되게 하여 대각선 이동도 가능하게 함
                // 해당 부분은 Initialize() 메서드의 isAlternative를 판단하는 비트연산을 이해 해야함
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

        // 타일 모두 순회중 이동불가 타일 존재시 false
        //foreach(MapTile tile in mapTiles)
        //{
        //    if (!tile.isPath) { return false; }
        //}

        // 화살표 경로를 비출지 말지 정하는 bool값으로 판단
        if(ShowPaths)
        {
            foreach (MapTile tile in mapTiles)
            {
                tile.ShowPath();
            }
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

    // 목적지 타일을 설치, 경로 갱신하는 메서드
    public void ToggleDestination(MapTile tile)
    {
        if(tile.Content.Type == TileType.Destination)
        {
            tile.Content = contentFactory.Get(TileType.Empty);

            if(!FindPaths())
            {
                tile.Content = contentFactory.Get(TileType.Destination);
                FindPaths();
            }

        }
        else if(tile.Content.Type == TileType.Empty)
        {
            tile.Content = contentFactory.Get(TileType.Destination);
            FindPaths();
        }
    }

    public void ToggleWall(MapTile tile)
    {
        // 이미 벽이 설치되어 있다면
        if(tile.Content.Type == TileType.Wall)
        {
            // 벽 제거
            tile.Content = contentFactory.Get(TileType.Empty);

            // 경로 갱신
            FindPaths();
        }
        // 비어있는 타일이라면
        else if(tile.Content.Type == TileType.Empty)
        {
            // 벽타일 인스턴스화, 설치
            tile.Content = contentFactory.Get(TileType.Wall);

            // 해당 위치에 설치시 이동불가 경로라면
            if(!FindPaths())
            {
                tile.Content = contentFactory.Get(TileType.Empty);
                FindPaths();
            }
        }
    }

    public void ToggleSpawnPoint(MapTile tile)
    {
        if (tile.Content.Type == TileType.SpawnPoint)
        {
            if (spawnPoints.Count > 1)
            {
                spawnPoints.Remove(tile);
                tile.Content = contentFactory.Get(TileType.Empty);
            }
        }
        else if (tile.Content.Type == TileType.Empty)
        {
            tile.Content = contentFactory.Get(TileType.SpawnPoint);
            spawnPoints.Add(tile);
        }
    }

    // 열차 레일 까는 메서드 작업중
    public void ToggleRail(MapTile tile)
    {
        // 이미 깔려있다면
        if (tile.Content.Type == TileType.Rail)
        {
            tile.Content = contentFactory.Get(TileType.Empty);
            FindPaths();
        }
        // 설치가 안되어 있는 타일이라면
        else if (tile.Content.Type == TileType.Empty)
        {
            tile.Content = contentFactory.Get(TileType.Rail);

            //if (!FindPaths())
            //{
            //    tile.Content = contentFactory.Get(TileType.Empty);
            //    FindPaths();
            //}
        }
    }



    public MapTile GetSpawnPoint(int idx)
    {
        return spawnPoints[idx];
    }

    public MapTile GetStartPoint() => mapTiles[0];

}
