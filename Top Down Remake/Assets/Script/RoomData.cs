using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Antoine/RoomData")]
public class RoomData : ScriptableObject
{
    public List<Tile> tileList = new List<Tile>();
}
