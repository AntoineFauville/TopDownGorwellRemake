using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public RoomData[] roomDatas;
    public int RoomIndex = 0;

    public RoomBuilder RoomBuilder;

    [Inject] private PlayerController _playerController;

    void Start()
    {
        _playerController.SetupGameManager(this);

        RoomBuilder.CurrentLoadedReadingMap = roomDatas[RoomIndex];
        RoomBuilder.CreateNewRoom();
    }

    public void SwitchRoom()
    {
        if (RoomIndex >= roomDatas.Length-1)
            RoomIndex = roomDatas.Length-1;
        else
            RoomIndex++;


        RoomBuilder.CurrentLoadedReadingMap = roomDatas[RoomIndex];
        RoomBuilder.CreateNewRoom();
    }
}
