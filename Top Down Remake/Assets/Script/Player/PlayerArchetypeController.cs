using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerArchetypeController : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private PlayerController _playerController;
    [Inject] private SavingController _savingController;

    public PlayerArchetype PlayerArchetype;

    void Start()
    {
        //assign a default class for debug
        PlayerArchetype = _gameSettings.PlayerArchetypes[_savingController.GetPlayerPrefInt(_gameSettings.ArchetypeSavingString)];
        SwitchClass(PlayerArchetype);

        //load from previous game
        //TODO
    }

    public void SwitchClassRandom()
    {
        PlayerArchetype = _gameSettings.PlayerArchetypes[Random.Range(0, _gameSettings.PlayerArchetypes.Length)];

        SwitchClass(PlayerArchetype);
    }

    public void SwitchClass(PlayerArchetype playerArchetype)
    {
        _playerController.UpdateView(playerArchetype.PlayerSkin, playerArchetype.ShootingTemplate);

        _savingController.SetPlayerPrefInt(_gameSettings.ArchetypeSavingString, (int)playerArchetype.PlayerArchetypes);
    }
}
