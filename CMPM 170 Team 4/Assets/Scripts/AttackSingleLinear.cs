using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple linear attack, spawns one projectile that travels linearly toward target's position at spawn time
public class AttackSingleLinear : Attack
{
    public override void init(Projectile projectileType, GameObject endTarget, float movementSpeed)
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
