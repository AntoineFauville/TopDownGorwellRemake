using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootingController : MonoBehaviour
{
    private ProjectileFactory _projectileFactory;
    private PlayerController _playerController;

    [SerializeField] private GameObject _projectileSpawnerHolder;
    [SerializeField] private GameObject[] _spawners;

    public AIShootingPatern State;

    [SerializeField] private float _rotateSpeed;
    
    void Start()
    {
        Enemy enemy = this.GetComponent<Enemy>();

        _projectileFactory = enemy.ProjectileFactory;
        _playerController = enemy.PlayerController;
    }
    
    void Update()
    {
        switch (State)
        {
            case AIShootingPatern.FollowTarget:
                Vector3 playerPos = new Vector3(_playerController.gameObject.transform.position.x, 0, _playerController.gameObject.transform.position.z);
                _projectileSpawnerHolder.transform.LookAt(playerPos);
                break;

            case AIShootingPatern.StayStill:
                _projectileSpawnerHolder.transform.rotation = new Quaternion();
                break;

            case AIShootingPatern.RotateFreely:
                if (_rotateSpeed < 3)
                {
                    _rotateSpeed += 0.1f;
                }
                _projectileSpawnerHolder.transform.Rotate(new Vector3(0, _rotateSpeed, 0));
                break;
        }
       
    }

    public void ShootStraight()
    {
        _projectileFactory.CreateProjectile(_spawners[0].transform);
    }
}
