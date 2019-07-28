using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerArchetypeController : MonoBehaviour
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private PlayerController _playerController;

    public PlayerArchetype PlayerArchetype;

    void Awake()
    {
        //assign a default class for debug
        PlayerArchetype = _gameSettings.PlayerArchetypes[1];
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
    }
}
