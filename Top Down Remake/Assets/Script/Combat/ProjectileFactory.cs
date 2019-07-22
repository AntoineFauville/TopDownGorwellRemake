using Zenject;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProjectileFactory
{
    [Inject] private GameSettings _gameSettings;

    public Projectile CreateProjectile(Transform parent)
    {
        GameObject obj = Object.Instantiate(_gameSettings.ProjectilePrefab);
        obj.transform.position = parent.transform.position;
        obj.transform.rotation = parent.transform.rotation;

        obj.tag = Tags.Projectile.ToString();

        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.Setup(_gameSettings);
        return projectile;
    }
}
