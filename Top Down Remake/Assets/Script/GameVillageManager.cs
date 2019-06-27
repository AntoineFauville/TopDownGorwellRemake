using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameVillageManager : MonoBehaviour
{
    [Inject] private PlayerController _playerController;
    [Inject] private SceneController _sceneController;

    [Header("Manual References")]
    public RoomBuilder RoomBuilder;

    public RoomData VillageRoomData;

    void Start()
    {
        SetupRoom();
        _playerController.SetupGameVillageManager(this);
    }

    public void SetupRoom()
    {
        RoomBuilder.CurrentLoadedReadingMap = VillageRoomData;
        RoomBuilder.CreateNewRoom();
    }

    public IEnumerator WaitToSwitch(float time)
    {
        yield return new WaitForSeconds(time);
        _sceneController.LoadScene((int)SceneIndex.Dungeon);
    }
}
