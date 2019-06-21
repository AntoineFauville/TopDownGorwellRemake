using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileFactory
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private DebugSettings _debugSettings;

    private GameObject obj;

    public Tile CreateTile(TileType tileType, Vector3 position, Transform parent, RoomBuilder roomBuilder, TileManager tileManager)
    {
        GameObject obj;
        obj = Object.Instantiate(_gameSettings.Tile);
        
        obj.transform.SetParent(parent);
        obj.transform.localPosition = position;

        Vector2 Pos = new Vector2(position.x, position.y);

        obj.name = Pos.ToString();

        Tile tile = obj.GetComponent<Tile>();
        tile.Setup(tileType, Pos, roomBuilder, tileManager, _debugSettings);

        tileManager.SetupTileVisuals(tile);
        tileManager.SpawnEnemiesOnTileLocation(tile, tile.TileType);

        return tile;
    }
}
