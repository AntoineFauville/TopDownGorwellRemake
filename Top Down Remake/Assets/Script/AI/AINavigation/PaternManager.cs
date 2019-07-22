﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PaternManager : MonoBehaviour
{
    private PlayerController _playerController;

    private Vector3 _agentDestination;
    [SerializeField] private NavMeshAgent _agent;
    private bool _needToMove;

    //need to move this is dirty
    [SerializeField] private Enemy _enemy;

    public Patern AgentState;

    private void Start()
    {
        _playerController = _enemy.PlayerController;
    }

    private void Update()
    {
        switch (AgentState) {
            case Patern.StandingStill:
                _needToMove = false;
                break;
            case Patern.MoveToRandomLocation:
                _needToMove = true;
                break;
            case Patern.MoveToPlayer:
                _needToMove = true;
                _agentDestination = _playerController.transform.position;
                break;
        }

        if (_needToMove)
        {
            _agent.destination = _agentDestination;
        }
    }
}
