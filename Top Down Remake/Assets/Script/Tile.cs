using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType TileType;
    private TileManager _tileManager;

    public SpriteRenderer SpriteRenderer;
    public BoxCollider2D BoxCollider2D;

    public Vector2 PositionInMap;

    private RoomBuilder _roomBuilder;
    private DebugSettings _debugSettings;

    public void Setup(TileType tileType,Vector2 positionInMap, RoomBuilder roomBuilder, TileManager tileManager, DebugSettings debugSettings)
    {
        TileType = tileType;
        _roomBuilder = roomBuilder;
        PositionInMap = positionInMap;
        _tileManager = tileManager;
        _debugSettings = debugSettings;
    }

    void OnMouseDown()
    {
        if (_roomBuilder.EditorMode)
        {
            if (Input.GetKey(_debugSettings.WallKey))
                _tileManager.ChangeTileType(this, TileType.wall);

            if (Input.GetKey(_debugSettings.DoorKey))
                _tileManager.ChangeTileType(this, TileType.door);

            if (Input.GetKey(_debugSettings.WalkableKey))
                _tileManager.ChangeTileType(this, TileType.walkable);

            if (Input.GetKey(_debugSettings.EnemyKey))
                _tileManager.ChangeTileType(this, TileType.enemySpawner);

            if (Input.GetKey(_debugSettings.RoomSwitcherKey))
                _tileManager.ChangeTileType(this, TileType.roomSwitcher);
        }
        Debug.Log("TileType = " + TileType + " | PositionInMap = " + PositionInMap);
    }
}
