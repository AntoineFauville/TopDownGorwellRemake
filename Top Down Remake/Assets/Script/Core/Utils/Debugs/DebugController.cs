using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class DebugController : MonoBehaviour
{
    [Inject] private DebugSettings _debugSettings;
    [Inject] private SceneController _sceneController;

    public RoomBuilder RoomBuilder;
    public GameManager GameManager;

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
            RoomBuilder.CurrentLoadedReadingMap.DungeonEnterTiles = RoomBuilder.DungeonEnterTiles;
            RoomBuilder.CurrentLoadedReadingMap.ChestTiles = RoomBuilder.ChestTiles;
            RoomBuilder.CurrentLoadedReadingMap.ArchetypeSwitcherTiles = RoomBuilder.ArchetypeSwitcherTiles;

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
            if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Village)
            {
                Debug.Log("recreate room isn't a featured made for the village");
            }
            else if(_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Dungeon)
            {
                StartCoroutine(waitForAllToDie());
                Debug.Log("Pressed " + _debugSettings.RecreateRoomKey + " and recreated the last saved " + RoomBuilder.CurrentLoadedReadingMap);
            }
        }

        //KillEnemies
        if (Input.GetKeyDown(_debugSettings.KillAllEnemiesKey))
        {
            if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Village)
            {
                Debug.Log("Looks like enemies spawning is not supported yet on the village map");
            }
            else if(_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Dungeon)
            {
                RoomBuilder.DebugDeleteEnemiesOnMap();
                Debug.Log("Deleted Enemies");
            }
        }

        //ResetMapToDraw
        if (Input.GetKeyDown(_debugSettings.ResetMapWhenEditingKey))
        {
            if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Village)
            {
                Debug.Log("Resetting the map isn't allowed in the village map");
            }
            else if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Dungeon)
            {
                RoomBuilder.FalseIfYouWantEmpty = false;
                RoomBuilder.CreateRoom(RoomBuilder.CurrentLoadedReadingMap);
                GameManager.SetupRoom();
                RoomBuilder.FalseIfYouWantEmpty = true;
                Debug.Log("Map Reset");
            }
        }

        //PressToGetInfo
        if (Input.GetKeyDown(_debugSettings.GetInfoKey))
        {
            Debug.Log("Current Map: " + RoomBuilder.CurrentLoadedReadingMap
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.WallTiles.Count + " Walls"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.WalckableTiles.Count + " Walckable"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.EnemyTiles.Count + " Enemies"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.DoorTiles.Count + " Doors"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.RoomSwitcherTiles.Count + " RoomSwitchs"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.DungeonEnterTiles.Count + " DungeonEnter"
                 + " / " + RoomBuilder.CurrentLoadedReadingMap.ArchetypeSwitcherTiles.Count + " ArchetypeSwitcher");
        }

        if (Input.GetKeyDown(_debugSettings.LoadOtherMap))
        {
            if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Village)
            {
                _sceneController.LoadScene((int)SceneIndex.Dungeon);
            }
            else if (_sceneController.GetActiveSceneIndex() == (int)SceneIndex.Dungeon)
            {
                _sceneController.LoadScene((int)SceneIndex.Village);
            }
        }
    }

    IEnumerator waitForAllToDie()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.SetupRoom();
    }
}
