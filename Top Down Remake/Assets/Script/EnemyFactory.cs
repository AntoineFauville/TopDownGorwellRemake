using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    [Inject] private GameSettings _gameSettings;

    public Enemy CreateEnemy(Vector3 position, GameManager gameManager, EnemyType enemyType)
    {
        GameObject obj;

        if (enemyType == EnemyType.Boss)
        {
            obj = Object.Instantiate(_gameSettings.Enemy);
            obj.AddComponent<Boss>();
        }
        else
            obj = Object.Instantiate(_gameSettings.Enemy);

        obj.tag = Tags.Enemy.ToString();

        Enemy enemy = obj.GetComponent<Enemy>();

        enemy.Setup(gameManager, enemyType);

        enemy.name = "Enemy" + position;

        enemy.transform.position = new Vector3(position.x + _gameSettings.EnemySpawnOffsetX, position.y + _gameSettings.EnemySpawnOffsetY);

        return enemy;
    }
}
