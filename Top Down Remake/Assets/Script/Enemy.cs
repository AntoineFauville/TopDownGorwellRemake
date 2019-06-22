using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager _gameManager;

    public void Setup(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    void OnCollisionEnter2D(Collision2D Collision)
    {
        if (Collision.gameObject.tag == Tags.Projectile.ToString())
        {
            StartCoroutine(waitToDie());
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
