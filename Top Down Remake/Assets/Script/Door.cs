using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameSettings _gameSettings;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    public void Setup(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

    public void OpenDoorSwitchLocalVisuals()
    {
        _spriteRenderer.sprite = _gameSettings.WalkableTexture;
        _boxCollider2D.isTrigger = true;
    }
}
