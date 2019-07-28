using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private PlayerController _playerController;
    [Inject] private BossController _bossManager;
    [Inject] private GameSettings _gameSettings;
    [Inject] private SceneController _sceneController;
    [Inject] private TimerController _timerController;

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

    [Space(5)]
    [Header("Random Generation")]
    [SerializeField] private List<RoomData> _dungeon = new List<RoomData>();

    void Start()
    {
        GenerateLayout();

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

        RoomBuilder.CurrentLoadedReadingMap = _dungeon[_roomIndex];
        _currentRoomEnemyAmount = _dungeon[_roomIndex].EnemyTiles.Count;
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

            if (_roomIndex >= _dungeon.Count - 1)
                StartCoroutine(WaitToGetBackToVillage());
            else
                _roomIndex++;

            SetupRoom();
        }

        StartCoroutine(WaitForPossibleRoomSwitch());
    }

    void RoomBossCheck()
    {
        if (RoomBuilder.CurrentLoadedReadingMap.RoomType == RoomType.Boss || RoomBuilder.CurrentLoadedReadingMap.RoomType == RoomType.RewardRoom)
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
            if (_roomIndex >= _dungeon.Count - 2)
            {
                _timerController.Finish();
            }

            StartCoroutine(SlowlyOpenDoor());
            RoomUnlockedState = true;
        }
    }

    void OpenDoors()
    {
        Debug.Log("Room" + _dungeon[_roomIndex] + "is now open");
        for (int i = 0; i < RoomBuilder.DoorTiles.Count; i++)
        {
            GameObject.Find(RoomBuilder.DoorTiles[i].ToString()).GetComponent<Door>().OpenDoorSwitchLocalVisuals();
        }
    }
    
    void GenerateLayout()
    {
        for (int y = 0; y < _gameSettings.AmountOfCycleDungeonHave; y++)
        {
            // add all the room before the boss
            for (int i = 0; i < _gameSettings.AmountOfRoomBeforeBoss; i++)
            {
                _dungeon.Add(_gameSettings.roomDatas[Random.Range(0,_gameSettings.roomDatas.Length)]);
            }
            //add the boss room
            _dungeon.Add(_gameSettings.bossRoomDatas[Random.Range(0, _gameSettings.bossRoomDatas.Length)]);
        }
        _dungeon.Add(_gameSettings.RewardRoom);
    }

    public IEnumerator WaitToSwitch(float deathTime)
    {
        yield return new WaitForSeconds(deathTime);
        _playerController.RePositionPlayer();
        SwitchRoom();
    }

    IEnumerator WaitForPossibleRoomSwitch()
    {
        yield return new WaitForSeconds(1f);
        _waitForPossibleRoomSwitch = false;
    }

    IEnumerator WaitToGetBackToVillage()
    {
        yield return new WaitForSeconds(0.00001f);
        _sceneController.LoadScene((int)SceneIndex.Village);
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

    //added this because in some cases the doors didn't open proprelly... cause the game to soft block itself
    public IEnumerator SlowlyOpenDoor()
    {
        yield return new WaitForSeconds(0.2f);
        OpenDoors();
    }
}
