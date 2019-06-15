using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    [Inject] private GameSettings _gameSettings;

    public Enemy CreateEnemy()
    {
        GameObject obj = Object.Instantiate(_gameSettings.Enemy);
        
        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.Setup(_gameSettings.PlayerController);

        return enemy;
    }
}
