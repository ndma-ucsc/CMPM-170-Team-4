using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLinear : Projectile
{
    void Update()
    {
        Vector3 movement = direction * speed * Time.deltaTime;

        rig.MovePosition(transform.position + movement);
    }
}
