using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    [Space()]
    [Header("Player")]
    public PlayerController PlayerController;
    public PlayerCornerLifeView PlayerCornerLifeView;
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
    [Header("Player Classes")]
    public PlayerArchetypeController PlayerArchetypeController;
    public PlayerArchetype[] PlayerArchetypes;
    public string ArchetypeSavingString = "ArchetypeSavingString";

    [Space()]
    [Header("Projectiles")]
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
    public float ProjectileShootingCooldown;
    public float DeathProjectorTime = 8f;
    public int ProjectileDamage = 1;
    public Sprite PlayerProjectile;
    public Sprite EnemyProjectile;

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
    public Sprite DungeonEnterTexture;
    public Sprite ChestTileTexture;
    public Sprite ArchetypeSwitcherTexture;

    [Space()]
    [Header("Camera")]
    public GameObject Camera;
    public int CameraSmoothSpeedInFreeMode = 2;

    [Space()]
    [Header("Boss Life")]
    public BossController BossManager;
    public BossFillingLifeView BossFillingLifeView ;
    public int BossToPlayerCollisionDamage = 2;
    public int BossMaxLife = 10;

    [Space()]
    [Header("Dungeon")]
    public int RoomSizeX = 28;
    public int RoomSizeY = 13;
    public int Offset = 1;
    public int BossRoomIndex = 4;
    public int AmountOfRoomBeforeBoss = 4;
    public int AmountOfCycleDungeonHave = 3;
    public RoomData[] roomDatas;
    public RoomData[] bossRoomDatas;
    public RoomData RewardRoom;

    [Space()]
    [Header("DungeonEnemyStats")]
    public int percentOfRunner = 80;
    public Sprite RunnerSprite;
    public int percentOfDistance = 20;
    public Sprite DistanceSprite;
    public Sprite OwlSprite;

    [Space()]
    [Header("ChestSystem")]
    public ChestController ChestController;

    [Space()]
    [Header("Gameplay")]
    public float InvincibilityFrameTime = 0.5f;
    public float bossLifeFillingSpeed = 0.01f;
    public TimerController TimerController;

    [Space()]
    [Header("Other Settings")]
    public int VillageSizeX = 100;
    public int VillageSizeY = 100;

    [Space()]
    [Header("Other Settings")]
    public SceneController SceneController;
    public LoadingScreen LoadingScreen;
    public TimerVillagerView TimerVillagerView;
    public NavMeshController NavMeshController;

    [Space()]
    [Header("Saving System")]
    public SavingController SavingController;
    public string BestTime = "bestTime";
    public string PreviousTime = "previousTime";

}
