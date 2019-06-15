using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileFactory
{
    [Inject] private GameSettings _gameSettings;

    private GameObject obj;

    public Tile CreateTile(TileType tileType, Vector3 position, Transform parent)
    {
        GameObject obj;
        obj = Object.Instantiate(_gameSettings.Tile);

        obj.transform.SetParent(parent);
        obj.transform.localPosition = position;
        
        Tile tile = obj.GetComponent<Tile>();
        tile.Setup(tileType, _gameSettings);

        return tile;
    }
}
