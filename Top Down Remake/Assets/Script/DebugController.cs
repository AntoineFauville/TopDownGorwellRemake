using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public RoomBuilder RoomBuilder;

    void Update()
    {
        //save current map
        if (Input.GetButtonDown("Jump"))
        {
            RoomBuilder.CurrentLoadedReadingMap.WallTiles = RoomBuilder.WallTiles;
            RoomBuilder.CurrentLoadedReadingMap.DoorTiles = RoomBuilder.DoorTiles;
            RoomBuilder.CurrentLoadedReadingMap.WalckableTiles = RoomBuilder.WalckableTiles;
            RoomBuilder.CurrentLoadedReadingMap.EnemyTiles = RoomBuilder.EnemyTiles;
            RoomBuilder.CurrentLoadedReadingMap.RoomSwitcherTiles = RoomBuilder.RoomSwitcherTiles;

            Debug.Log("Saved " + RoomBuilder.CurrentLoadedReadingMap.name);

            AssetDatabase.Refresh();
            EditorUtility.SetDirty(RoomBuilder.CurrentLoadedReadingMap);
            AssetDatabase.SaveAssets();
        }

        //edit mode
        if (Input.GetKeyDown("p"))
        {
            RoomBuilder.EditorMode = !RoomBuilder.EditorMode;
            Debug.Log("Editor Mode now: " + RoomBuilder.EditorMode);
        }

        //recreate room
        if (Input.GetKeyDown("o"))
        {
            RoomBuilder.CreateNewRoom();
            Debug.Log("Pressed 0 and recreated the last saved " + RoomBuilder.CurrentLoadedReadingMap);
        }

        //KillEnemies
        if (Input.GetKeyDown("i"))
        {
            RoomBuilder.DeleteEnemiesOnMap();
            Debug.Log("Deleted Enemies");
        }

        //ResetMapToDraw
        if (Input.GetKeyDown("n"))
        {
            RoomBuilder.FalseIfYouWantEmpty = false;
            RoomBuilder.CreateRoom(RoomBuilder.CurrentLoadedReadingMap);
            RoomBuilder.FalseIfYouWantEmpty = true;
            Debug.Log("Map Reset");
        }

        //PressToGetInfo
        if (Input.GetKeyDown("u"))
        {
            Debug.Log("Current Map: " + RoomBuilder.CurrentLoadedReadingMap
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.WallTiles.Count + " Walls"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.WalckableTiles.Count + " Walckable"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.EnemyTiles.Count + " Enemies"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.DoorTiles.Count + " Doors"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.RoomSwitcherTiles.Count + " RoomSwitchs");
        }
    }
}
