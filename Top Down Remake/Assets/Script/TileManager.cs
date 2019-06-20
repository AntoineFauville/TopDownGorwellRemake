using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TileManager : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private EnemyFactory _enemyFactory;

    public RoomBuilder RoomBuilder;

    public void SetupTileVisuals(Tile tile)
    {
        if (tile.TileType == TileType.wall)
        {
            tile.SpriteRenderer.sprite = _gameSettings.WallTexture;
            tile.BoxCollider2D.isTrigger = false;
            tile.gameObject.tag = "Wall";
        }
        else if (tile.TileType == TileType.door)
        {
            tile.SpriteRenderer.sprite = _gameSettings.DoorTexture;
            tile.BoxCollider2D.isTrigger = false;
            tile.gameObject.tag = "Wall";
        }
        else if (tile.TileType == TileType.roomSwitcher)
        {
            tile.SpriteRenderer.sprite = _gameSettings.RoomSwitcherTexture;
            tile.BoxCollider2D.isTrigger = true;
            tile.gameObject.tag = "RoomSwitch";
        }
        else
        {
            tile.SpriteRenderer.sprite = _gameSettings.WalkableTexture;
            tile.BoxCollider2D.isTrigger = true;
        }
    }

    public void ChangeTileType(Tile tile, TileType tileType)
    {
        tile.TileType = tileType;
        SetupTileVisuals(tile);
        SpawnEnemiesOnTileLocation(tile, tile.TileType);

        //remove if it was in any existing list && add in the correct new list
        AdjustInListAfterChangingTileType(tile, tile.TileType);
    }

    void AdjustInListAfterChangingTileType(Tile tile, TileType tileType)
    {
        if (RoomBuilder.WallTiles.Contains(tile.PositionInMap))
            RoomBuilder.WallTiles.Remove(tile.PositionInMap);
        else if (RoomBuilder.WalckableTiles.Contains(tile.PositionInMap))
            RoomBuilder.WalckableTiles.Remove(tile.PositionInMap);
        else if (RoomBuilder.DoorTiles.Contains(tile.PositionInMap))
            RoomBuilder.DoorTiles.Remove(tile.PositionInMap);
        else if (RoomBuilder.EnemyTiles.Contains(tile.PositionInMap))
            RoomBuilder.EnemyTiles.Remove(tile.PositionInMap);
        else if (RoomBuilder.RoomSwitcherTiles.Contains(tile.PositionInMap))
            RoomBuilder.RoomSwitcherTiles.Remove(tile.PositionInMap);

        if (tileType == TileType.wall)
            RoomBuilder.WallTiles.Add(tile.PositionInMap);
        else if (tileType == TileType.door)
            RoomBuilder.DoorTiles.Add(tile.PositionInMap);
        else if (tileType == TileType.walkable)
            RoomBuilder.WalckableTiles.Add(tile.PositionInMap);
        else if (tileType == TileType.enemySpawner)
            RoomBuilder.EnemyTiles.Add(tile.PositionInMap);
        else if (tileType == TileType.roomSwitcher)
            RoomBuilder.RoomSwitcherTiles.Add(tile.PositionInMap);
    }

    public void SpawnEnemiesOnTileLocation(Tile tile, TileType tileType)
    {
        if (tileType == TileType.enemySpawner)
        {
            Vector3 position = new Vector3(tile.PositionInMap.x, tile.PositionInMap.y, 0);

            Enemy enemy = _enemyFactory.CreateEnemy(position);
            RoomBuilder.EnemiesInTheRoom.Add(enemy.gameObject);
        }
    }
}
