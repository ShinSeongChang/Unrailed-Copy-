using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Net.Mime;

[CreateAssetMenu(menuName = "Map", fileName = "TileFactory")]
public class MapTileContentFactory : ScriptableObject
{
    Scene contentScene;

    [SerializeField] TileContent destinationTile = default;
    [SerializeField] TileContent emptyTile = default;
    [SerializeField] TileContent wallTile = default;

    private TileContent Get(TileContent prefab)
    {
        TileContent instance = Instantiate(prefab);
        instance.OriginFactory = this;
        MoveToFactoryScene(instance.gameObject);
        return instance;
    }


    // 타일 생성시 핫 리로드 타임때 방어기제??
    private void MoveToFactoryScene(GameObject gameObject)
    {
        if(!contentScene.isLoaded)
        {
            if (Application.isEditor)
            {
                contentScene = SceneManager.GetSceneByName(name);

                if(!contentScene.isLoaded)
                {
                    contentScene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                contentScene = SceneManager.CreateScene(name);
            }
        }

        SceneManager.MoveGameObjectToScene(gameObject, contentScene);
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
            case TileType.Emtpy:
                return Get(emptyTile);
            case TileType.Wall:
                return Get(wallTile);
        }

        Debug.Assert(false, "Unsupported Type : " + type);
        return null;
    }
}
