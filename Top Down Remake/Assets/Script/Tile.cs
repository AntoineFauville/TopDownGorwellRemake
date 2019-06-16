using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType TileType;
    private GameSettings _gameSettings;

    public SpriteRenderer SpriteRenderer;
    public BoxCollider2D BoxCollider2D;

    public Vector2 PositionInMap;

    private RoomBuilder _roomBuilder;
    private EnemyFactory _enemyFactory;

    public void Setup(TileType tileType,GameSettings gameSettings, Vector2 positionInMap, RoomBuilder roomBuilder, EnemyFactory enemyFactory)
    {
        TileType = tileType;
        _gameSettings = gameSettings;
        _roomBuilder = roomBuilder;
        _enemyFactory = enemyFactory;
        PositionInMap = positionInMap;

        SetupVisuals();

        //if it's a tile enemyspawner then it's just the same as a walkable. but spawns an enemy on top
        if(tileType == TileType.enemySpawner)
            SpawnEnemies();
    }

    void SpawnEnemies()
    {
        Vector3 position = new Vector3(PositionInMap.x,PositionInMap.y,0);

        Enemy enemy = _enemyFactory.CreateEnemy(position);
        _roomBuilder.ObjInTheRoom.Add(enemy.gameObject);
    }

    public void SetupVisuals()
    {
        if (TileType == TileType.wall)
        {
            SpriteRenderer.sprite = _gameSettings.WallTexture;
            BoxCollider2D.isTrigger = false;
            gameObject.tag = "Wall";
        }
        else if (TileType == TileType.door)
        {
            SpriteRenderer.sprite = _gameSettings.DoorTexture;
            BoxCollider2D.isTrigger = false;
            gameObject.tag = "Wall";
        }
        else
        {
            SpriteRenderer.sprite = _gameSettings.WalkableTexture;
            BoxCollider2D.isTrigger = true;
        }
    }

    void OnMouseDown()
    {
        if (_roomBuilder.EditorMode)
        {
            if (Input.GetKey("w"))
            {
                TileType = TileType.wall;
                SetupVisuals();
                SpawnEnemies();

                //remove if it was in any existing list && add in the correct new list
                AdjustInList(TileType);
            }

            if (Input.GetKey("x"))
            {
                TileType = TileType.door;
                SetupVisuals();
                SpawnEnemies();

                //remove if it was in any existing list && add in the correct new list
                AdjustInList(TileType);
            }

            if (Input.GetKey("c"))
            {
                TileType = TileType.walkable;
                SetupVisuals();
                SpawnEnemies();

                //remove if it was in any existing list && add in the correct new list
                AdjustInList(TileType);
            }

            if (Input.GetKey("v"))
            {
                TileType = TileType.enemySpawner;
                SetupVisuals();
                SpawnEnemies();

                //remove if it was in any existing list && add in the correct new list
                AdjustInList(TileType);
            }
        }
        Debug.Log("TileType = " + TileType + " | PositionInMap = " + PositionInMap);
    }

    void AdjustInList(TileType tileType)
    {
        if (_roomBuilder.WallTiles.Contains(PositionInMap))
            _roomBuilder.WallTiles.Remove(PositionInMap);
        else if (_roomBuilder.WalckableTiles.Contains(PositionInMap))
            _roomBuilder.WalckableTiles.Remove(PositionInMap);
        else if (_roomBuilder.DoorTiles.Contains(PositionInMap))
            _roomBuilder.DoorTiles.Remove(PositionInMap);
        else if (_roomBuilder.EnemyTiles.Contains(PositionInMap))
            _roomBuilder.EnemyTiles.Remove(PositionInMap);

        if (tileType == TileType.wall)
            _roomBuilder.WallTiles.Add(PositionInMap);
        else if (tileType == TileType.door)
            _roomBuilder.DoorTiles.Add(PositionInMap);
        else if (tileType == TileType.walkable)
            _roomBuilder.WalckableTiles.Add(PositionInMap);
        else if (tileType == TileType.enemySpawner)
            _roomBuilder.EnemyTiles.Add(PositionInMap);
    }
}
