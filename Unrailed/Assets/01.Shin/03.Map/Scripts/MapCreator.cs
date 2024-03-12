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
            mapBoard.ToggleDestination(tile);
        }
    }

}
