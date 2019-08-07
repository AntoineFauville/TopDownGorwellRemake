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

        float projectileSpeed;
        float projectileLifeSpan;

        if (projectileType == ProjectileType.PlayerProjectile)
        {
            sprite = _gameSettings.PlayerProjectile;
            obj.tag = Tags.ProjectilePlayer.ToString();
            projectileSpeed = _playerArchetypeController.PlayerArchetype.ProjectileSpeed;
            projectileLifeSpan = _playerArchetypeController.PlayerArchetype.ProjectileLifeSpan;
        }
        else if (projectileType == ProjectileType.MageProjectile)
        {
            sprite = _gameSettings.EnemyProjectile;
            obj.tag = Tags.ProjectileEnemy.ToString();
            projectileSpeed = _gameSettings.MageProjectileSpeed;
            projectileLifeSpan = _gameSettings.MageProjectileLifeSpan;
        }
        else if (projectileType == ProjectileType.GhostProjectile)
        {
            sprite = _gameSettings.EnemyProjectile;
            obj.tag = Tags.ProjectileEnemy.ToString();
            projectileSpeed = _gameSettings.GhostProjectileSpeed;
            projectileLifeSpan = _gameSettings.GhostProjectileLifeSpan;
        }
        else //last case = owl
        {
            sprite = _gameSettings.EnemyProjectile;
            obj.tag = Tags.ProjectileEnemy.ToString();
            projectileSpeed = _gameSettings.OwlProjectileSpeed; 
            projectileLifeSpan = _gameSettings.OwlProjectileLifeSpan;
        }

            projectile.Setup(projectileSpeed, projectileLifeSpan, sprite);
        return projectile;
    }
}
