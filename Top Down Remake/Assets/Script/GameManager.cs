using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private PlayerController _playerController;
    [Inject] private BossController _bossManager;
    [Inject] private GameSettings _gameSettings;

    private int _roomIndex = 0;
    public bool RoomUnlockedState; // lock the room to make sure some checks are done only once, relock the room onces player switched
    
    [Space(5)]
    [Header("Manual References")]
    public RoomBuilder RoomBuilder;

    [Space(5)]
    [Header("Player Controls")]
    public bool BlockPlayerControls;
    private bool _waitForPossibleRoomSwitch;

    [Space(5)]
    [Header("Enemy Controls")]
    private int _currentRoomEnemyAmount;
    public int EnemyAmountInCurrentRoomLeft;

    void Start()
    {
        StartCoroutine(SlowerUpdate());

        _playerController.SetupGameManager(this);

        //activate the boss life
        _bossManager.TurnOnOffBossLife(true);
        
        SetupRoom();
    }

    public void SetupRoom()
    {
        _currentRoomEnemyAmount = 0;
        EnemyAmountInCurrentRoomLeft = 0;

        RoomBuilder.CurrentLoadedReadingMap = _gameSettings.roomDatas[_roomIndex];
        _currentRoomEnemyAmount = _gameSettings.roomDatas[_roomIndex].EnemyTiles.Count;
        EnemyAmountInCurrentRoomLeft = _currentRoomEnemyAmount;
        RoomBuilder.CreateNewRoom();
        RoomBossCheck();
        RoomUnlockedState = false;
    }

    public void SwitchRoom()
    {
        if (!_waitForPossibleRoomSwitch)
        {
            _waitForPossibleRoomSwitch = true;

            if (_roomIndex >= _gameSettings.roomDatas.Length - 1)
                _roomIndex = _gameSettings.roomDatas.Length - 1;
            else
                _roomIndex++;

            SetupRoom();
        }

        StartCoroutine(waitForPossibleRoomSwitch());
    }

    void RoomBossCheck()
    {
        if (RoomBuilder.CurrentLoadedReadingMap.RoomType == RoomType.Boss)
        {
            //turn it off
            _bossManager.TurnOnOffBossLife(false);
        }
        else
            _bossManager.TurnOnOffBossLife(true);
    }

    void CheckToOpenDoors()
    {
        if (!RoomUnlockedState && _currentRoomEnemyAmount - EnemyAmountInCurrentRoomLeft == _currentRoomEnemyAmount)
        {
            OpenDoors();
            RoomUnlockedState = true;
        }
    }

    void OpenDoors()
    {
        Debug.Log("Room" + _gameSettings.roomDatas[_roomIndex] + "is now open");
        for (int i = 0; i < RoomBuilder.DoorTiles.Count; i++)
        {
            GameObject.Find(RoomBuilder.DoorTiles[i].ToString()).GetComponent<Door>().OpenDoorSwitchLocalVisuals();
        }
    }
    
    public IEnumerator waitToSwitch(float deathTime)
    {
        yield return new WaitForSeconds(deathTime);
        _playerController.RePositionPlayer();
        SwitchRoom();
    }

    IEnumerator waitForPossibleRoomSwitch()
    {
        yield return new WaitForSeconds(1f);
        _waitForPossibleRoomSwitch = false;
    }

    public IEnumerator SlowerUpdate()
    {
        if (EnemyAmountInCurrentRoomLeft < 0)
            EnemyAmountInCurrentRoomLeft = 0;

        if (EnemyAmountInCurrentRoomLeft > _currentRoomEnemyAmount)
            EnemyAmountInCurrentRoomLeft = _currentRoomEnemyAmount;

        if (_currentRoomEnemyAmount < 0)
            _currentRoomEnemyAmount = 0;

        yield return new WaitForSeconds(0.05f);
        
        CheckToOpenDoors();

        StartCoroutine(SlowerUpdate());
    }
}
