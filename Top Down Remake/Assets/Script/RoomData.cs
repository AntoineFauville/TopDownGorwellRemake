using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Antoine/RoomData")]
public class RoomData : ScriptableObject
{
    public RoomType RoomType;
    public List<Vector2> WallTiles = new List<Vector2>();
    public List<Vector2> DoorTiles = new List<Vector2>();
    public List<Vector2> WalckableTiles = new List<Vector2>();
    public List<Vector2> EnemyTiles = new List<Vector2>();
    public List<Vector2> RoomSwitcherTiles = new List<Vector2>(); 
    public List<Vector2> DungeonEnterTiles = new List<Vector2>();
}
