using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //this class will take whatever boss template you throw at him and handles all the action that this boss should be doing
    //will spawns the projectile that the boss should create
    //will spawns whatever extra mobbs the boss will wnat to have
    //should handle the different paterns of shooting

    private int BossTypeStyle;

    private MovementPaternManager _movementPaternManager;
    private AIShootingController _aIShootingController;

    void Start()
    {
        _movementPaternManager = this.GetComponent<MovementPaternManager>();
        _aIShootingController = this.GetComponent<AIShootingController>();

        switch (BossTypeStyle)
        {
            case 0: // owl
                StartCoroutine(Owl());
                break;

            case 1: // chtulhu
                break;

            case 2: // eye
                break;
        }
    }

    IEnumerator Owl()
    {
        _movementPaternManager.AgentState = Patern.MoveToPlayer;
        _aIShootingController.State = AIShootingPatern.FollowTarget;
        yield return new WaitForSeconds(2f);
        _movementPaternManager.AgentState = Patern.StandingStill;
        yield return new WaitForSeconds(0.6f);
        _aIShootingController.ShootStraight(ProjectileType.OwlProjectile);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(Owl());
    }
}
