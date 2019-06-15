using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType TileType;
    private GameSettings _gameSettings;

    public SpriteRenderer SpriteRenderer;
    public BoxCollider2D BoxCollider2D;

    public void Setup(TileType tileType,GameSettings gameSettings)
    {
        TileType = tileType;
        _gameSettings = gameSettings;

        SetupVisuals();
    }

    public void SetupVisuals()
    {
        if (TileType == TileType.wall)
        {
            SpriteRenderer.sprite = _gameSettings.WallTexture;
            BoxCollider2D.enabled = true;
            gameObject.tag = "Wall";
        }
        else if (TileType == TileType.door)
        {
            SpriteRenderer.sprite = _gameSettings.DoorTexture;
            BoxCollider2D.enabled = true;
            gameObject.tag = "Wall";
        }
        else
        {
            SpriteRenderer.sprite = _gameSettings.WalkableTexture;
            BoxCollider2D.enabled = false;
        }
    }
}
