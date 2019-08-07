using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System.Linq;

public class MovementPaternManager : MonoBehaviour
{
    private PlayerController _playerController;

    private Vector3 _agentDestination;
    [SerializeField] private NavMeshAgent _agent;
    private bool _needToMove;

    //need to move this is dirty
    [SerializeField] private Enemy _enemy;

    public Patern AgentState;

    public Vector3[] Destination;
    //23,0,3
    //23,0,9
    //3,0,3
    //3,0,9
    //13,0,6
    private int _randomDestination;

    private void Start()
    {
        _playerController = _enemy.PlayerController;

        //first take a random from the list from 0 to max of list
        _randomDestination = Random.Range(0, Destination.Length);
    }

    private void Update()
    {
        switch (AgentState)
        {
            case Patern.StandingStill:
                _needToMove = false;
                _agent.isStopped = true;
                break;

            case Patern.MoveToRandomLocation:
                //then check if the distance between destination and current enemy <= X
                //if smaller than X then switch to next destination in list
                float distance = Vector3.Distance(this.transform.position, Destination[_randomDestination]);
                if (distance < 2)
                {
                    _randomDestination++;

                    if (_randomDestination > Destination.Length - 1)
                        _randomDestination = 0;
                }
                _agentDestination = Destination[_randomDestination];
                
                _needToMove = true;
                _agent.isStopped = false;
                break;

            case Patern.MoveToPlayer:
                _needToMove = true;
                _agentDestination = _playerController.transform.position;
                _agent.isStopped = false;
                break;
        }

        if (_needToMove)
        {
            _agent.destination = _agentDestination;
        }
    }
}
