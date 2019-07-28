using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    public Rigidbody rigidBody;

    private float _projectileSpeed;
    private float _projectileLifeSpan;

    public void Setup(float projectileSpeed, float projectileLifeSpan)
    {
        _projectileLifeSpan = projectileLifeSpan;
        _projectileSpeed = projectileSpeed;

        StartCoroutine(waitToDie(_projectileLifeSpan));
    }

    void FixedUpdate()
    {
        this.transform.Translate(Vector3.right * _projectileSpeed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == Tags.Wall.ToString() || collider.gameObject.tag == Tags.Enemy.ToString())
        {
            Die();
        }

        if (collider.gameObject.tag == Tags.RoomSwitch.ToString())
        {
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(waitToDie(0.05f));
    }

    public IEnumerator waitToDie(float deathTime)
    {
        yield return new WaitForSeconds(deathTime);
        DestroyImmediate(this.gameObject);
    }
}
