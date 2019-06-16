using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
	public PlayerController PlayerController;
	public float PlayerDiagonalSpeedLimiter = 0.7f;
	public float PlayerRunSpeedHorizontal = 8f;
	public float PlayerRunSpeedVertical = 8f;

    public GameObject ProjectilePrefab;
    public ShootingTemplate WarriorShooting;
    public float ShootingSpeed;
    public float ShootingCooldown;
    public float DeathProjectorTime = 8f;

    public GameObject Enemy;
    public float EnemySpawnOffsetX = 0.5f;
    public float EnemySpawnOffsetY = 0.8f;

    public GameObject Tile;

    public GameObject Camera;

    public Sprite WallTexture;
    public Sprite WalkableTexture;
    public Sprite DoorTexture;
    public Sprite RoomSwitcherTexture;
}
