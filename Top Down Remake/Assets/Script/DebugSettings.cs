using System;
using UnityEngine;

[Serializable]
public class DebugSettings
{
    public string WalkableKey = "w";
    public string WallKey = "x";
    public string RoomSwitcherKey = "c";
    public string DoorKey = "v";
    public string EnemyKey = "b";

    public string SaveMapInput = "Jump";
    public string EditModeKey = "p";
    public string RecreateRoomKey = "o";
    public string KillAllEnemiesKey = "i";
    public string ResetMapWhenEditingKey = "u";
    public string GetInfoKey = "n";
}
