using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private EnemyFactory _enemyFactory;
    
    void Start()
    {
        //_enemyFactory.CreateEnemy();
    }
}
