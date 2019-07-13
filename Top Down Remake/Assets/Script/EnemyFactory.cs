using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private BossController _bossController;

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
        enemy.name = "Enemy" + position;
        enemy.transform.position = new Vector3(position.x + _gameSettings.EnemySpawnOffsetX,0, position.y + _gameSettings.EnemySpawnOffsetY);
        enemy.transform.rotation = Quaternion.Euler(90,0,0);

        enemy.Setup(gameManager, enemyType, _gameSettings, _bossController);

        //specify that this is a boss for the life health and inject the boss controller life to the boss
        if(enemyType == EnemyType.Boss)
            enemy.HealthSystem.Setup(SetupBossLifeFromBossController(), _gameSettings);
        else
            enemy.HealthSystem.Setup(_gameSettings.EnemyMaxHealth, _gameSettings);

        return enemy;
    }

    int SetupBossLifeFromBossController()
    {
        return (int)_bossController.GetLife();
    }
}
