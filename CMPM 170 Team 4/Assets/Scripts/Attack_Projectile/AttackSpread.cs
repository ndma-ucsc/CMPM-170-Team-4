using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Simple spread attack, sends set number of projectiles in fan shape toward target
public class AttackSpread : Attack
{
    private float angleRange = Mathf.PI / 2;
    private int numProjectiles = 4;
    private float projectileWidth = 1;
    private float distFromSpawn;
    private float diffAngle;
    public override void init(GameObject endTarget, float movementSpeed)
    {
        // Basic parameters
        speed = movementSpeed;
        target = endTarget;

        // Calculate spawn relations to avoid initial collision
        diffAngle = angleRange / (numProjectiles - 1);
        distFromSpawn = Mathf.Sin((Mathf.PI) - diffAngle) / (Mathf.Sin(diffAngle) / projectileWidth) + 0.0001f;
        Vector3 targetVector = Vector3.Normalize(target.transform.position - this.transform.position) * distFromSpawn;
        Vector3 fanStart = new Vector3(targetVector.x * Mathf.Cos(-angleRange / 2) - targetVector.y * Mathf.Sin(-angleRange / 2),
                                       targetVector.x * Mathf.Sin(-angleRange / 2) + targetVector.y * Mathf.Cos(-angleRange / 2), 1);

        // Spawn initial projectile
        Projectile attack = Instantiate(projectileType) as Projectile;
        projectiles.Add(attack);

        // Initialize parameters
        attack.init(fanStart, fanStart, target, 50);

        // Assign parent
        attack.transform.parent = this.transform;

        // Setup projectile listen for collision
        attack.hit.AddListener(removeProjectile);

        // Spawn projectiles
        for(int i = 1; i < numProjectiles; ++i)
        {
            // Create projectile
            attack = Instantiate(projectileType) as Projectile;
            projectiles.Add(attack);

            // Calculate location and direction
            Vector3 location = new Vector3(fanStart.x * Mathf.Cos(diffAngle * i) - fanStart.y * Mathf.Sin(diffAngle * i),
                                           fanStart.x * Mathf.Sin(diffAngle * i) + fanStart.y * Mathf.Cos(diffAngle * i), 1);

            // Initialize parameters
            attack.init(location, location, target, 50);

            // Assign parent
            attack.transform.parent = this.transform;

            // Setup projectile listen for collision
            attack.hit.AddListener(removeProjectile);
        }
    }
}
