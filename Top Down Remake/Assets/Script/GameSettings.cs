using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    [Space()]
    [Header("Player")]
    public PlayerController PlayerController;
	public float PlayerDiagonalSpeedLimiter = 0.7f;
	public float PlayerRunSpeedHorizontal = 8f;
	public float PlayerRunSpeedVertical = 8f;
    public string InputAxisHorizontalName = "Horizontal";
    public string InputAxisVerticalName = "Vertical";
    public string InputAxisShootHorizontal = "Fire1";
    public string InputAxisShootVertical = "Fire2";

    [Space()]
    [Header("Projectiles")]
    public GameObject ProjectilePrefab;
    public ShootingTemplate WarriorShooting;
    public float ProjectileSpeed;
    public float ProjectileShootingCooldown;
    public float DeathProjectorTime = 8f;

    [Space()]
    [Header("Enemies")]
    public GameObject Enemy;
    public float EnemySpawnOffsetX = 0.5f;
    public float EnemySpawnOffsetY = 0.8f;

    [Space()]
    [Header("Tiles")]
    public GameObject Tile;
    public Sprite WallTexture;
    public Sprite WalkableTexture;
    public Sprite DoorTexture;
    public Sprite RoomSwitcherTexture;

    [Space()]
    [Header("Camera")]
    public GameObject Camera;

    [Space()]
    [Header("Boss Life")]
    public BossController BossManager;
    public BossFillingLifeView BossFillingLifeView;
    public float bossLifeFillingSpeed;

    [Space()]
    [Header("Dungeon")]
    public int BossRoomIndex = 4;
    public RoomData[] roomDatas;
}
