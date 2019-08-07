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

            obj.tag = Tags.Enemy.ToString();

            Enemy enemy = obj.GetComponent<Enemy>();
            enemy.name = "Enemy" + position;
            enemy.transform.position = new Vector3(position.x + _gameSettings.EnemySpawnOffsetX, 0, position.y + _gameSettings.EnemySpawnOffsetY);
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);

            enemy.Setup(_playerController, gameManager, enemyType, _gameSettings, _bossController, _projectileFactory);

            if (enemyType == EnemyType.Boss)
            {
                obj.AddComponent<Boss>();
                enemy._spriteRenderer.sprite = _gameSettings.OwlSprite;
            }
            else
            {
                EnemyArchetypes enemyArchetypes;

                obj.AddComponent<EnemyArchetypes>();

                enemyArchetypes = obj.GetComponent<EnemyArchetypes>();

                int rand = Random.Range(0, 100);

                if (rand >= _gameSettings.percentOfZombie)
                {
                    enemyArchetypes.EnemyTypeStyle = EnemyType.Zombie;
                    enemy._spriteRenderer.sprite = _gameSettings.ZombieSprite;
                }
                else if (rand >= _gameSettings.percentOfMage && rand < _gameSettings.percentOfZombie)
                {
                    enemyArchetypes.EnemyTypeStyle = EnemyType.Mage;
                    enemy._spriteRenderer.sprite = _gameSettings.MageSprite;
                }
                else
                {
                    enemyArchetypes.EnemyTypeStyle = EnemyType.Ghost;
                    enemy._spriteRenderer.sprite = _gameSettings.GhostSprite;
                }
            }

        int chanceOfSpawn = Random.Range(0, 100);

        //use the health system to instantly kill the enemy with 0 life when they spawn to generate randomness
        if (enemyType == EnemyType.Boss)
        {
            enemy.HealthSystem.Setup(SetupBossLifeFromBossController(), _gameSettings);
        }
        //enemy
        else if(chanceOfSpawn < _gameSettings.ChanceOfEnemySpawningOnTile && enemyType != EnemyType.Boss)
        {
            enemy.HealthSystem.Setup(_gameSettings.EnemyMaxHealth, _gameSettings);
        } 
        else
        {
            enemy.HealthSystem.Setup(_gameSettings.EnemyMaxHealth, _gameSettings);
            enemy.StartCoroutine(enemy.waitToDie());
        }

        return enemy;
        
    }

    int SetupBossLifeFromBossController()
    {
        return (int)_bossController.GetLife();
    }
}
