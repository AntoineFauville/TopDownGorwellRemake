using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    [Inject] private GameSettings _gameSettings;

    public Enemy CreateEnemy(Vector3 position)
    {
        GameObject obj = Object.Instantiate(_gameSettings.Enemy);
        
        Enemy enemy = obj.GetComponent<Enemy>();

        enemy.Setup(_gameSettings.PlayerController);

        enemy.transform.position = new Vector3(position.x + _gameSettings.EnemySpawnOffsetX, position.y + _gameSettings.EnemySpawnOffsetY);

        return enemy;
    }
}
