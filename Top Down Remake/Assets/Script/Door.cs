using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameSettings _gameSettings;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider _boxCollider;

    void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _boxCollider = this.GetComponent<BoxCollider>();
    }

    public void Setup(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

    public void OpenDoorSwitchLocalVisuals()
    {
        _spriteRenderer.sprite = _gameSettings.WalkableTexture;
        _boxCollider.isTrigger = true;
    }
}
