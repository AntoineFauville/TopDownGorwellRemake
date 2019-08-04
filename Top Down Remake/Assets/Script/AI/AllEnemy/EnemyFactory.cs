using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private BossController _bossController;
    [Inject] private PlayerController _playerController;
    [Inject] private ProjectileFactory _projectileFactory;

    public Enemy CreateEnemy(Vector3 position, GameManager gameManager, EnemyType enemyType)
    {
        GameObject obj;

        obj = Object.Instantiate(_gameSettings.Enemy);

        if (enemyType == EnemyType.Boss)
        {
            obj.AddComponent<Boss>();
        }
        else
        {
            EnemyArchetypes enemyArchetypes;

            obj.AddComponent<EnemyArchetypes>();

            enemyArchetypes = obj.GetComponent<EnemyArchetypes>();

            int rand = Random.Range(0, 100);

            if (rand < _gameSettings.percentOfRunner)
            {
                enemyArchetypes.EnemyTypeStyle = EnemyType.Runner;
            }
            else
                enemyArchetypes.EnemyTypeStyle = EnemyType.Distance;


        }

        obj.tag = Tags.Enemy.ToString();

        Enemy enemy = obj.GetComponent<Enemy>();
        enemy.name = "Enemy" + position;
        enemy.transform.position = new Vector3(position.x + _gameSettings.EnemySpawnOffsetX,0, position.y + _gameSettings.EnemySpawnOffsetY);
        enemy.transform.rotation = Quaternion.Euler(0,0,0);

        enemy.Setup(_playerController, gameManager, enemyType, _gameSettings, _bossController, _projectileFactory);

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
