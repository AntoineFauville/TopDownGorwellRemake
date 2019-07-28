using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Antoine/PlayerArchetype")]
public class PlayerArchetype : ScriptableObject
{
    public string Name;
    public ShootingTemplate ShootingTemplate;
    public Sprite PlayerSkin;
    public float ProjectileSpeed;
    public float ProjectileLifeSpan;

    //projectile skin
    //life
    //damage
}
