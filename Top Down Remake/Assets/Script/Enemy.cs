using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyType _enemyType;

    private GameManager _gameManager;
    private GameSettings _gameSettings;
    private BossController _bossController;

    public HealthSystem HealthSystem;

    public void Setup(GameManager gameManager, EnemyType enemyType, GameSettings gameSettings, BossController bossController)
    {
        _gameManager = gameManager;
        _enemyType = enemyType;
        _gameSettings = gameSettings;
        _bossController = bossController;
        
        HealthSystem = this.gameObject.AddComponent<HealthSystem>();
    }

    void OnCollisionStay2D(Collision2D Collision)
    {
        // projectile damages
        if (Collision.gameObject.tag == Tags.Projectile.ToString())
        {
            TakeDamage(_gameSettings.ProjectileDamage); 
        }
        //example to show that we can do different type of damages
        // playercollision damages
        if (Collision.gameObject.tag == Tags.Player.ToString())
        {
            TakeDamage(_gameSettings.PlayerToEnemyCollisionDamage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (!HealthSystem.CanTakeDamageAgain)
        {
            HealthSystem.ModifyHealth(damageAmount);

            if (HealthSystem.GetCurrentHealth() == 0)
            {
                StartCoroutine(waitToDie());
            }

            if (_enemyType == EnemyType.Boss)
            {
                _bossController.ModifyLifeView(damageAmount);
                _bossController.UpdateView();
            }
        }
        else
        {
            Debug.Log("I'm InvincibleForNowMortal");
        }
    }

    public void AskToDieDebug()
    {
        StartCoroutine(waitToDie());
    }

    void Die()
    {
        _gameManager.EnemyAmountInCurrentRoomLeft--;
        _gameManager.RoomBuilder.DeleteSpecificEnemy(this.gameObject);
    }

    IEnumerator waitToDie()
    {
        yield return new WaitForSeconds(0.0001f);
        Die();
    }
}
