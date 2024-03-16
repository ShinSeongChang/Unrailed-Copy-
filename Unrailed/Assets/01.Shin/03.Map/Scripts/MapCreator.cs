using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] Vector2Int boardSize = new Vector2Int(11, 11);
    [SerializeField] MapBoard mapBoard = default;
    [SerializeField] MapTileContentFactory tileFactory = default;
    [SerializeField] EnemyFactory enemyFactory = default;
    [SerializeField] Train train = default;

    EnemyCollection enemies = new EnemyCollection();

    Ray Touch => Camera.main.ScreenPointToRay(Input.mousePosition);

    [SerializeField, Range(0.1f, 10f)]
    float spawnSpeed = 1f;
    float spawnProgress;

    private void Awake()
    {
        mapBoard.Initialize(boardSize, tileFactory);

        train.SpawnOn(mapBoard.GetStartPoint());
    }

    private void OnValidate()
    {
        if(boardSize.x < 2) { boardSize.x = 2; }
        if(boardSize.y < 2) { boardSize.y = 2; }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HandleAlternativeTouch();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            HandleTouch();
        }

        // 화살표 경로 활성, 비활성
        if (Input.GetKeyDown(KeyCode.V))
        {
            mapBoard.ShowPaths = !mapBoard.ShowPaths;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            mapBoard.ShowGrid = !mapBoard.ShowGrid;
        }

        //enemies.GameUpdate();
        train.GameUpdate();
    }

    private void FixedUpdate()
    {
        spawnProgress += spawnSpeed * Time.deltaTime;
        while (spawnProgress >= 1f)
        {
            spawnProgress -= 1f;

            // 적군 지속생성 막음
            //SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        MapTile spawnPoint =
            mapBoard.GetSpawnPoint(Random.Range(0, mapBoard.SpawnPointCount));
        Enemy enemy = enemyFactory.Get();
        enemy.SpawnOn(spawnPoint);
        enemies.Add(enemy);
    }

    private void HandleTouch()
    {
        MapTile tile = mapBoard.GetTile(Touch);

        if(tile != null)
        {
            mapBoard.ToggleWall(tile);
        }
    }

    private void HandleAlternativeTouch()
    {
        MapTile tile = mapBoard.GetTile(Touch);

        if(tile != null)
        {
            if (Input.GetKey(KeyCode.LeftShift)) { mapBoard.ToggleDestination(tile); }
            else {  mapBoard.ToggleRail(tile); }                            
        }
    }

}
