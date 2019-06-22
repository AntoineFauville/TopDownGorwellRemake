using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class RoomBuilder : MonoBehaviour
{
    [Inject] private TileFactory _tileFactory;

    [Space(5)]
    [Header("Map Data")]
    public bool FalseIfYouWantEmpty;
    public bool AddingRandomOnEmptyTemplate;
    public RoomData CurrentLoadedReadingMap;

    [Space(5)]
    [Header("Editor Mode")]
    public bool EditorMode;

    [Space(5)]
    [Header("Manual References")]
    public TileManager TileManager;
    public TrashController TrashController;

    [Space(5)]
    [Header("Debug Options - Don't Modify")]
    public List<Vector2> WallTiles = new List<Vector2>();
    public List<Vector2> DoorTiles = new List<Vector2>();
    public List<Vector2> WalckableTiles = new List<Vector2>();
    public List<Vector2> EnemyTiles = new List<Vector2>();
    public List<Vector2> RoomSwitcherTiles = new List<Vector2>();
    
    public int roomSizeX;
    public int roomSizeY;
    public int offset;

    //when creating an empty room, it places on these tiles doors as debug.
    public int[] doorX;

    //this represent the index that the edge tile needs to figure out if they are walls or not for eg
    private int _RoomEdgeSizeX;
    private int _RoomEdgeSizeY;

    void Start()
    {
        EditorMode = false;
    }

    // Update is called once per frame
    public void CreateRoom(RoomData roomToCreate)
    {
        _RoomEdgeSizeX = ((roomSizeX) * offset) - offset;
        _RoomEdgeSizeY = ((roomSizeY) * offset) - offset;

        Vector3 position = new Vector3(0,0,0);

        for (int x = 0; x < roomSizeX; x++)
        {
            for (int y = 0; y < roomSizeY; y++)
            {
                //if we are not reading from a map, we don't load it and there for it creates the template
                if (!FalseIfYouWantEmpty)
                {
                    if (!AddingRandomOnEmptyTemplate)
                        CreateTileFromAnEmptyTemplate(position);
                    else
                        CreateTileFromAnEmptyTemplateWithRandom(position);
                }
                else
                    CreateFromRoomDataTile(position, roomToCreate);

                position.y += offset;
            }
            position.y = 0;

            position.x += offset;
        }
    }

    void CreateFromRoomDataTile(Vector3 position, RoomData roomToCreate)
    {
        TileType tileTypeLocal;

        Vector2 currentPosition = new Vector2(position.x, position.y);

        //reads from roomdata
        if (roomToCreate.WallTiles.Contains(currentPosition))
            tileTypeLocal = TileType.wall;
        else if (roomToCreate.DoorTiles.Contains(currentPosition))
            tileTypeLocal = TileType.door;
        else if (roomToCreate.EnemyTiles.Contains(currentPosition))
            tileTypeLocal = TileType.enemySpawner;
        else if (roomToCreate.RoomSwitcherTiles.Contains(currentPosition))
            tileTypeLocal = TileType.roomSwitcher;
        else
            tileTypeLocal = TileType.walkable;

        Tile tile = _tileFactory.CreateTile(tileTypeLocal, position, this.transform, this, TileManager);

        TrashController.ObjInTheRoom.Add(tile.gameObject);

        AddInMap(tileTypeLocal, tile);
    }

    void CreateTileFromAnEmptyTemplate(Vector3 position)
    {
        TileType tileTypeLocal;

        //walls
        if (position.x == 0 ||
            position.y == 0 ||
            position.x == _RoomEdgeSizeX ||
            position.y == _RoomEdgeSizeY)
        {
            tileTypeLocal = TileType.wall;
        }
        else
        {
            tileTypeLocal = TileType.walkable;
        }

        //Overwrite with Doors
        for (int i = 0; i < doorX.Length; i++)
        {
            if (position.x == doorX[i] && position.y == 0 ||
               (position.x == doorX[i] && position.y == (roomSizeY - 1)))
            {
                tileTypeLocal = TileType.door;
            }
        }

        Tile tile = _tileFactory.CreateTile(tileTypeLocal, position, this.transform, this, TileManager);

        AddInMap(tileTypeLocal, tile);
    }

    void CreateTileFromAnEmptyTemplateWithRandom(Vector3 position)
    {
        TileType tileTypeLocal;

        //walls
        if (position.x == 0 ||
            position.y == 0 ||
            position.x == _RoomEdgeSizeX ||
            position.y == _RoomEdgeSizeY)
        {
            tileTypeLocal = TileType.wall;
        }
        //randomTileInRoom
        else if (position.x == Random.Range(0, roomSizeX) ||
            position.y == Random.Range(0, roomSizeY))
        {
            tileTypeLocal = TileType.wall;
        }
        else
        {
            tileTypeLocal = TileType.walkable;
        }

        //Overwrite with Doors
        for (int i = 0; i < doorX.Length; i++)
        {
            if (position.x == doorX[i] && position.y == 0 ||
               (position.x == doorX[i] && position.y == (roomSizeY - 1)))
            {
                tileTypeLocal = TileType.door;
            }
        }

        Tile tile = _tileFactory.CreateTile(tileTypeLocal, position, this.transform, this, TileManager);

        AddInMap(tileTypeLocal, tile);
    }

    void AddInMap(TileType tileTypeLocal, Tile tile)
    {
        if (tileTypeLocal == TileType.wall)
            WallTiles.Add(tile.PositionInMap);
        if (tileTypeLocal == TileType.door)
            DoorTiles.Add(tile.PositionInMap);
        if (tileTypeLocal == TileType.walkable)
            WalckableTiles.Add(tile.PositionInMap);
        if (tileTypeLocal == TileType.enemySpawner)
            EnemyTiles.Add(tile.PositionInMap);
        if (tileTypeLocal == TileType.roomSwitcher)
            RoomSwitcherTiles.Add(tile.PositionInMap);
    }
    
    public void CreateNewRoom()
    {
        for (int i = 0; i < TrashController.ObjInTheRoom.Count; i++)
        {
            DestroyImmediate(TrashController.ObjInTheRoom[i]);
        }
        TrashController.ObjInTheRoom.Clear();

        CleanMap();

        DebugDeleteEnemiesOnMap();

        CreateRoom(CurrentLoadedReadingMap);
    }

    void CleanMap()
    {
        WallTiles.Clear();
        DoorTiles.Clear();
        WalckableTiles.Clear();
        EnemyTiles.Clear();
        RoomSwitcherTiles.Clear();
    }

    public void DebugDeleteEnemiesOnMap()
    {
        foreach (GameObject enemy in TrashController.EnemiesInTheRoom)
        {
            enemy.GetComponent<Enemy>().AskToDieDebug();
        }
    }

    public void DeleteSpecificEnemy(GameObject enemyGameObject)
    {
        if (TrashController.EnemiesInTheRoom.Contains(enemyGameObject))
        {
            TrashController.EnemiesInTheRoom.Remove(enemyGameObject);
            DestroyImmediate(GameObject.Find(enemyGameObject.name));
        }
    }
}   
