using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoomBuilder : MonoBehaviour
{
    [Inject] private TileFactory _tileFactory;

    public RoomData RoomData;
    public int roomSizeX;
    public int roomSizeY;
    public int offset;

    public int[] doorX;

    int edgeX;
    int edgeY;

    void Start()
    {
        edgeX = ((roomSizeX) * offset) - offset;
        edgeY = ((roomSizeY) * offset) - offset;

        CreateRoom();
    }

    // Update is called once per frame
    void CreateRoom()
    {
        Vector3 position = new Vector3(0,0,0);

        for (int x = 0; x < roomSizeX; x++)
        {
            for (int y = 0; y < roomSizeY; y++)
            {
                CreateTile(position);

                position.y += offset;
            }
            position.y = 0;

            position.x += offset;
        }
    }

    void CreateTile(Vector3 position)
    {
        TileType tileTypeLocal;

        //walls
        if (position.x == 0 ||
            position.y == 0 ||
            position.x == edgeX ||
            position.y == edgeY)
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

        _tileFactory.CreateTile(tileTypeLocal, position, this.transform);
    }
}   
