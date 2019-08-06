using Zenject;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

public class ProjectileFactory
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private PlayerArchetypeController _playerArchetypeController;

    public Projectile CreateProjectile(Transform parent, ProjectileType projectileType)
    {
        GameObject obj = Object.Instantiate(_gameSettings.ProjectilePrefab);
        obj.transform.position = parent.transform.position;
        obj.transform.rotation = parent.transform.rotation;

        
        
        Projectile projectile = obj.GetComponent<Projectile>();

        Sprite sprite;
        sprite = _gameSettings.PlayerProjectile;
        if (projectileType == ProjectileType.PlayerProjectile)
        {
            sprite = _gameSettings.PlayerProjectile;
            obj.tag = Tags.ProjectilePlayer.ToString();
        }
        else if (projectileType == ProjectileType.EnemyProjectile)
        {
            sprite = _gameSettings.EnemyProjectile;
            obj.tag = Tags.ProjectileEnemy.ToString();
        }

        projectile.Setup(_playerArchetypeController.PlayerArchetype.ProjectileSpeed, _playerArchetypeController.PlayerArchetype.ProjectileLifeSpan, sprite);
        return projectile;
    }
}
