using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple linear projectile, moves at constant speed in constant direction toward target
public class ProjectileLinear : Projectile
{
    void Update()
    {
        Vector3 movement = direction * speed * Time.deltaTime;

        rig.MovePosition(transform.position + movement);
    }
}
