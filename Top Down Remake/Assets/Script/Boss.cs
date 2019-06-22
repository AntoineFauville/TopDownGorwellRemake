using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    void Start()
    {
        StartCoroutine(SlowUpdate());
    }

    IEnumerator SlowUpdate()
    {
        yield return new WaitForSeconds(0.1f);

        Vector3 direction = new Vector3(Random.Range(-0.03f, 0.03f), Random.Range(-0.03f, 0.03f), 0);

        this.transform.Translate(direction);

        StartCoroutine(SlowUpdate());
    }
}
