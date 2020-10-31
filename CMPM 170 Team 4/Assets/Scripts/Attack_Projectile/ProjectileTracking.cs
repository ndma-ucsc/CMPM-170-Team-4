using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracking projectile, curves toward target at constant speed and with restrained turn force
public class ProjectileTracking : Projectile
{
    private float maxForce = 1f;
    void Update()
    {
        Vector3 desiredDirection = Vector3.Normalize(target.transform.position - this.transform.position) * speed;
        Vector3 force = desiredDirection - direction;
        if(force.magnitude > (maxForce * Time.deltaTime))
        {
            force = Vector3.Normalize(force) * (maxForce * Time.deltaTime);
        }
        direction = Vector3.Normalize(direction + force);

        rig.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}
