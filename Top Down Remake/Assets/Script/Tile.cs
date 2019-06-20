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

    public void Setup(TileType tileType,Vector2 positionInMap, RoomBuilder roomBuilder, TileManager tileManager)
    {
        TileType = tileType;
        _roomBuilder = roomBuilder;
        PositionInMap = positionInMap;
        _tileManager = tileManager;
    }

    void OnMouseDown()
    {
        if (_roomBuilder.EditorMode)
        {
            if (Input.GetKey("w"))
                _tileManager.ChangeTileType(this, TileType.wall);

            if (Input.GetKey("x"))
                _tileManager.ChangeTileType(this, TileType.door);

            if (Input.GetKey("c"))
                _tileManager.ChangeTileType(this, TileType.walkable);

            if (Input.GetKey("v"))
                _tileManager.ChangeTileType(this, TileType.enemySpawner);

            if (Input.GetKey("b"))
                _tileManager.ChangeTileType(this, TileType.roomSwitcher);
        }
        Debug.Log("TileType = " + TileType + " | PositionInMap = " + PositionInMap);
    }
}
