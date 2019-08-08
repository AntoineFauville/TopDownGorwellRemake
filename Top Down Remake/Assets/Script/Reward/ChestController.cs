using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChestController : MonoBehaviour
{
    [Inject] private TimerController _timerController;
    [Inject] private GameSettings _gameSettings;
    [Inject] private SavingController _savingController;
    [Inject] private PlayerController _playerController;

    private ChestType _chestRecieved;

    private int _goldRecieved;

    public void SetupChestContent()
    {
        if (CalculateTheScore() < _gameSettings.ScoreCap)
        {
            _chestRecieved = ChestType.Small;
        }
        else
        {
            _chestRecieved = ChestType.Medium;
        }

        switch (_chestRecieved)
        {
            case ChestType.Small:
                _goldRecieved = Random.Range(5, 15);
                break;

            case ChestType.Medium:
                _goldRecieved = Random.Range(10, 20);
                break;
        }

        Debug.Log("Player will recieve " + _goldRecieved + " gold");

        int totalPlayerGold = _savingController.GetPlayerPrefInt(_gameSettings.GoldSaving) + _goldRecieved;

        _savingController.SetPlayerPrefInt(_gameSettings.GoldSaving, totalPlayerGold);
    }

    public void OpenChest()
    {
        Debug.Log("Player recieves " + _goldRecieved + " gold");
    }

    public int CalculateTheScore()
    {
        int Score;

        Score = (int)((_gameSettings.MaxTimingInDungeon - _timerController.Timer) / 10 ) + _playerController.GetPlayerLife() + _gameSettings.MinimumScore;

        return Score;
    }
}
