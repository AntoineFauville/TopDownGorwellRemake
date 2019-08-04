using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArchetypes : MonoBehaviour
{
    //this class will take whatever boss template you throw at him and handles all the action that this boss should be doing
    //will spawns the projectile that the boss should create
    //will spawns whatever extra mobbs the boss will wnat to have
    //should handle the different paterns of shooting

    public EnemyType EnemyTypeStyle;

    private MovementPaternManager _movementPaternManager;
    private AIShootingController _aIShootingController;

    // Start is called before the first frame update
    void Start()
    {
        _movementPaternManager = this.GetComponent<MovementPaternManager>();
        _aIShootingController = this.GetComponent<AIShootingController>();

        switch (EnemyTypeStyle)
        {
            case EnemyType.Distance: // Distance
                _movementPaternManager.AgentState = Patern.MoveToPlayer;
                break;

            case EnemyType.Runner: // chtulhu
                StartCoroutine(Runner());
                break;
        }
    }

    IEnumerator Runner()
    {
        _movementPaternManager.AgentState = Patern.MoveToPlayer;
        _aIShootingController.State = AIShootingPatern.FollowTarget;
        yield return new WaitForSeconds(1.5f);
        _movementPaternManager.AgentState = Patern.StandingStill;
        yield return new WaitForSeconds(0.6f);
        _aIShootingController.ShootStraight();
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(Runner());
    }
}
