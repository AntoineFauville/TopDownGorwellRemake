using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootingController : MonoBehaviour
{
    private ProjectileFactory _projectileFactory;
    private PlayerController _playerController;

    [SerializeField] private GameObject _projectileSpawnerHolder;
    [SerializeField] private GameObject[] _spawners;
    
    void Start()
    {
        Enemy enemy = this.GetComponent<Enemy>();

        _projectileFactory = enemy.ProjectileFactory;
        _playerController = enemy.PlayerController;
    }
    
    void Update()
    {
        Vector3 playerPos = new Vector3(_playerController.gameObject.transform.position.x, 0 , _playerController.gameObject.transform.position.z);

        _projectileSpawnerHolder.transform.LookAt(playerPos);
    }

    public void ShootStraight()
    {
        _projectileFactory.CreateProjectile(_spawners[0].transform);
    }
}
