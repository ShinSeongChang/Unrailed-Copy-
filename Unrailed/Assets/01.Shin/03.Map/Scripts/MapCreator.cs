using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] Vector2Int boardSize = new Vector2Int(11, 11);
    [SerializeField] MapBoard mapBoard = default;

    private void Awake()
    {
        mapBoard.Initialize(boardSize);
    }

    void OnValidate()
    {
        if(boardSize.x < 2) { boardSize.x = 2; }
        if(boardSize.y < 2) { boardSize.y = 2; }
    }

}
