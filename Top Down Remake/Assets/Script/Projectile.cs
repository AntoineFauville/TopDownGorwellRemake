using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    public Rigidbody rigidBody;

    private GameSettings _gameSettings;

    public void Setup(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
        StartCoroutine(waitToDie(_gameSettings.DeathProjectorTime));
    }

    void FixedUpdate()
    {
        this.transform.Translate(Vector3.right * _gameSettings.ProjectileSpeed);
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == Tags.Wall.ToString() 
            || collider.gameObject.tag == Tags.RoomSwitch.ToString()
            || collider.gameObject.tag == Tags.Enemy.ToString())
        {
            StartCoroutine(waitToDie(0.05f));
        }
    }

    IEnumerator waitToDie(float deathTime)
    {
        yield return new WaitForSeconds(deathTime);
        DestroyImmediate(this.gameObject);
    }
}
