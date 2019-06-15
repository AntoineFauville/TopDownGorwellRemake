using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    private float _moveSpeed;
    public Rigidbody2D rigidBody;

    public void Setup(float moveSpeed, float deathTime)
    {
        _moveSpeed = moveSpeed;
        StartCoroutine(waitToDie(deathTime));
    }

    void FixedUpdate()
    {
        this.transform.Translate(Vector3.right * _moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            StartCoroutine(waitToDie(0.001f));
        }
    }

    IEnumerator waitToDie(float deathTime)
    {
        yield return new WaitForSeconds(deathTime);
        DestroyImmediate(this.gameObject);
    }
}
