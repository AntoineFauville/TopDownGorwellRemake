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

    [Space(5)]
    [Header("Manual References")]
    public RoomBuilder RoomBuilder;

    [Space(5)]
    [Header("Player Controls")]
    public bool BlockPlayerControls;
    private bool _waitForPossibleRoomSwitch;

    void Start()
    {
        _playerController.SetupGameManager(this);

        //activate the boss life
        _bossManager.TurnOnOffBossLife(true);

        RoomBuilder.CurrentLoadedReadingMap = _gameSettings.roomDatas[_roomIndex];
        RoomBuilder.CreateNewRoom();
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

            RoomBuilder.CurrentLoadedReadingMap = _gameSettings.roomDatas[_roomIndex];
            RoomBuilder.CreateNewRoom();

            RoomBossCheck();
        }

        StartCoroutine(waitForPossibleRoomSwitch());
    }

    void RoomBossCheck()
    {
        if (_roomIndex == _gameSettings.BossRoomIndex)
        {
            //turn it off
            _bossManager.TurnOnOffBossLife(false);
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
}
