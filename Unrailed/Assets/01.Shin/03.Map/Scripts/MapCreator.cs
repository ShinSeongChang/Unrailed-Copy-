using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] Vector2Int boardSize = new Vector2Int(11, 11);
    [SerializeField] MapBoard mapBoard = default;
    [SerializeField] MapTileContentFactory factory = default;
    Ray Touch => Camera.main.ScreenPointToRay(Input.mousePosition);

    private void Awake()
    {
        mapBoard.Initialize(boardSize, factory);
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
            else {  mapBoard.ToggleSpawnPoint(tile); }                            
        }
    }

}
