using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //this class will take whatever boss template you throw at him and handles all the action that this boss should be doing
    //will spawns the projectile that the boss should create
    //will spawns whatever extra mobbs the boss will wnat to have
    //should handle the different paterns of shooting

    private int BossShootingType;

    private PaternManager _paternManager;

    void Start()
    {
        _paternManager = this.GetComponent<PaternManager>();

        _paternManager.AgentState = 0;

        StartCoroutine(SlowUpdate());

        BossShootingType = Random.Range(0, 2);
    }

    void Update()
    {
        switch (BossShootingType)
        {
            case 0: // owl
                break;

            case 1: // chtulhu
                break;

            case 2: // eye
                break;
        }
    }

    IEnumerator SlowUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        

        StartCoroutine(SlowUpdate());
    }
}
