using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoomBuilder : MonoBehaviour
{
    [Inject] private TileFactory _tileFactory;
    
    public List<Vector2> WallTiles = new List<Vector2>();
    public List<Vector2> DoorTiles = new List<Vector2>();
    public List<Vector2> WalckableTiles = new List<Vector2>();
    public List<Vector2> EnemyTiles = new List<Vector2>();
    public List<Vector2> RoomSwitcherTiles = new List<Vector2>();

    public List<GameObject> ObjInTheRoom = new List<GameObject>();
    public List<GameObject> EnemiesInTheRoom = new List<GameObject>();

    public int roomSizeX;
    public int roomSizeY;
    public int offset;

    public int[] doorX;

    private int _edgeX;
    private int _edgeY;

    public bool EditorMode;
    
    public bool LoadFromReadingMap;
    public RoomData CurrentLoadedReadingMap;

    void Start()
    {
        EditorMode = false;
    }

    // Update is called once per frame
    public void CreateRoom(RoomData roomToCreate)
    {
        _edgeX = ((roomSizeX) * offset) - offset;
        _edgeY = ((roomSizeY) * offset) - offset;

        Vector3 position = new Vector3(0,0,0);

        for (int x = 0; x < roomSizeX; x++)
        {
            for (int y = 0; y < roomSizeY; y++)
            {
                //if we are not reading from a map, we don't load it and there for it creates the template
                if (!LoadFromReadingMap)
                    CreateTile(position);
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

        Tile tile = _tileFactory.CreateTile(tileTypeLocal, position, this.transform, this);

        ObjInTheRoom.Add(tile.gameObject);

        AddInMap(tileTypeLocal, tile);
    }

    void CreateTile(Vector3 position)
    {
        TileType tileTypeLocal;

        //walls
        if (position.x == 0 ||
            position.y == 0 ||
            position.x == _edgeX ||
            position.y == _edgeY)
        {
            tileTypeLocal = TileType.wall;
        }
        
        ////randomTileInRoom
        //else if (position.x == Random.Range(0,roomSizeX) ||
        //    position.y == Random.Range(0, roomSizeY))
        //{
        //    tileTypeLocal = TileType.wall;
        //}
        //walkable
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

        Tile tile = _tileFactory.CreateTile(tileTypeLocal, position, this.transform, this);

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

    void Update()
    {
        //save current map
        if (Input.GetButtonDown("Jump"))
        {
            CurrentLoadedReadingMap.WallTiles = WallTiles;
            CurrentLoadedReadingMap.DoorTiles = DoorTiles;
            CurrentLoadedReadingMap.WalckableTiles = WalckableTiles;
            CurrentLoadedReadingMap.EnemyTiles = EnemyTiles;
            CurrentLoadedReadingMap.RoomSwitcherTiles = RoomSwitcherTiles;

            Debug.Log("Saved " + CurrentLoadedReadingMap.name);
        }

        //edit mode
        if (Input.GetKeyDown("p"))
        {
            EditorMode = !EditorMode;
            Debug.Log("Editor Mode now: " + EditorMode);
        }

        //recreate room
        if (Input.GetKeyDown("o"))
        {
            CreateNewRoom();
            Debug.Log("Pressed 0 and now created a new room");
        }

        //KillEnemies
        if (Input.GetKeyDown("i"))
        {
            DeleteEnemies();
            Debug.Log("Deleted Enemies");
        }
    }

    public void CreateNewRoom()
    {
        for (int i = 0; i < ObjInTheRoom.Count; i++)
        {
            DestroyImmediate(ObjInTheRoom[i]);
        }
        ObjInTheRoom.Clear();

        DeleteEnemies();

        CreateRoom(CurrentLoadedReadingMap);
    }

    void DeleteEnemies()
    {
        for (int i = 0; i < EnemiesInTheRoom.Count; i++)
        {
            DestroyImmediate(EnemiesInTheRoom[i]);
        }
        EnemiesInTheRoom.Clear();
    }
}   
