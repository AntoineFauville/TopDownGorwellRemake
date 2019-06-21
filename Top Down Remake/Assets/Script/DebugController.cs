﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class DebugController : MonoBehaviour
{
    [Inject] private DebugSettings _debugSettings;

    public RoomBuilder RoomBuilder;

    void Update()
    {
        //save current map
        if (Input.GetButtonDown(_debugSettings.SaveMapInput))
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
        if (Input.GetKeyDown(_debugSettings.EditModeKey))
        {
            RoomBuilder.EditorMode = !RoomBuilder.EditorMode;
            Debug.Log("Editor Mode now: " + RoomBuilder.EditorMode);
        }

        //recreate room
        if (Input.GetKeyDown(_debugSettings.RecreateRoomKey))
        {
            RoomBuilder.CreateNewRoom();
            Debug.Log("Pressed 0 and recreated the last saved " + RoomBuilder.CurrentLoadedReadingMap);
        }

        //KillEnemies
        if (Input.GetKeyDown(_debugSettings.KillAllEnemiesKey))
        {
            RoomBuilder.DeleteEnemiesOnMap();
            Debug.Log("Deleted Enemies");
        }

        //ResetMapToDraw
        if (Input.GetKeyDown(_debugSettings.ResetMapWhenEditingKey))
        {
            RoomBuilder.FalseIfYouWantEmpty = false;
            RoomBuilder.CreateRoom(RoomBuilder.CurrentLoadedReadingMap);
            RoomBuilder.FalseIfYouWantEmpty = true;
            Debug.Log("Map Reset");
        }

        //PressToGetInfo
        if (Input.GetKeyDown(_debugSettings.GetInfoKey))
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
