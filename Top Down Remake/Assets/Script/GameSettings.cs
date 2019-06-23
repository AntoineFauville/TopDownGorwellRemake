using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    [Space()]
    [Header("Player")]
    public PlayerController PlayerController;
    public PlayerView PlayerView;
    public float PlayerDiagonalSpeedLimiter = 0.7f;
	public float PlayerRunSpeedHorizontal = 8f;
	public float PlayerRunSpeedVertical = 8f;
    public string InputAxisHorizontalName = "Horizontal";
    public string InputAxisVerticalName = "Vertical";
    public string InputAxisShootHorizontal = "Fire1";
    public string InputAxisShootVertical = "Fire2";
    public int PlayerToEnemyCollisionDamage = 1;
    public int PlayerMaxHealth = 3;

    [Space()]
    [Header("Projectiles")]
    public GameObject ProjectilePrefab;
    public ShootingTemplate WarriorShooting;
    public float ProjectileSpeed;
    public float ProjectileShootingCooldown;
    public float DeathProjectorTime = 8f;
    public int ProjectileDamage = 1;

    [Space()]
    [Header("Enemies")]
    public GameObject Enemy;
    public float EnemySpawnOffsetX = 0.5f;
    public float EnemySpawnOffsetY = 0.8f;
    public int EnemyToPlayerCollisionDamage = 1;
    public int EnemyMaxHealth = 1;

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
    public BossFillingLifeView BossFillingLifeView ;
    public int BossToPlayerCollisionDamage = 2;
    public int BossMaxLife = 10;

    [Space()]
    [Header("Dungeon")]
    public int BossRoomIndex = 4;
    public RoomData[] roomDatas;

    [Space()]
    [Header("Gameplay")]
    public float InvincibilityFrameTime = 0.5f;
    public float bossLifeFillingSpeed = 0.01f;
}
