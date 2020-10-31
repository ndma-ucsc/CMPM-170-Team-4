using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple attack, spawns one projectile
public class AttackSingle : Attack
{
    public override void init(GameObject endTarget, float movementSpeed)
    {
        // Basic parameters
        speed = movementSpeed;
        target = endTarget;

        // Create projectile
        Projectile attack = Instantiate(projectileType) as Projectile;
        projectiles.Add(attack);
        attack.init(this.transform.position, target.transform.position - this.transform.position, target, 50);
        attack.transform.parent = this.transform;

        // Setup projectile listen for collision, called when object collides
        attack.hit.AddListener(removeProjectile);
    }
}
