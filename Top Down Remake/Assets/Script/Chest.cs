using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private GameSettings _gameSettings;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private ChestController _chestController;

    private bool thisChestHasBeenOpenned;

    void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    public void Setup(GameSettings gameSettings, ChestController chestController)
    {
        _gameSettings = gameSettings;
        _chestController = chestController;
        _chestController.SetupChestContent();
    }

    public void OpenChestSwitchLocalVisuals()
    {
        _spriteRenderer.sprite = _gameSettings.WalkableTexture;
        _boxCollider2D.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !thisChestHasBeenOpenned)
        {
            thisChestHasBeenOpenned = true;

            _chestController.OpenChest();
            OpenChestSwitchLocalVisuals();
        }
    }
}
