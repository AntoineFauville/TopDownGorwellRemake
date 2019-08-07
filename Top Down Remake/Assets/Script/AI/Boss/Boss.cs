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

    private float OwlTimingBetweenAttackCooldown = 0.8f;

    void Start()
    {
        _movementPaternManager = this.GetComponent<MovementPaternManager>();
        _aIShootingController = this.GetComponent<AIShootingController>();

        switch (BossTypeStyle)
        {
            case 0: // owl
                int rotationLogic = Random.Range(0,2);
                StartCoroutine(Owl(rotationLogic));
                break;

            case 1: // chtulhu
                break;

            case 2: // eye
                break;
        }
    }

    void OwlIncrementing()
    {
        OwlTimingBetweenAttackCooldown -= 0.05f;
        if (OwlTimingBetweenAttackCooldown <= 0.2f)
        {
            OwlTimingBetweenAttackCooldown = 0.2f;
        }
    }

    IEnumerator Owl(int rotationLogic)
    {
        _movementPaternManager.AgentState = Patern.MoveToPlayer;
        _aIShootingController.State = AIShootingPatern.StayStill;
        //waiting time for player to move around a bit for fairness

        if (rotationLogic == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                yield return new WaitForSeconds(OwlTimingBetweenAttackCooldown);
                _aIShootingController.ShootStraight(i, ProjectileType.OwlProjectile);

                OwlIncrementing();
            }
        }
        else
        {
            for (int i = 7; i > -1; i--)
            {
                yield return new WaitForSeconds(OwlTimingBetweenAttackCooldown);
                _aIShootingController.ShootStraight(i, ProjectileType.OwlProjectile);

                OwlIncrementing();
            }
        }

        StartCoroutine(Owl(rotationLogic));
    }
}
