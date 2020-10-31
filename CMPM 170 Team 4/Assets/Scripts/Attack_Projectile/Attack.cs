using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Attack parent class
public abstract class Attack : MonoBehaviour
{
    /*** Parameters ***/
    public Projectile projectileType;
    protected float speed;
    public GameObject target;
    protected List<Projectile> projectiles = new List<Projectile>();

    // Update is called once per frame
    void Update()
    {
        if(projectiles.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    // Initializes start parameters based on target location, and movement speed
    public abstract void init(GameObject endTarget, float movementSpeed);

    // Removes projectile from list
    protected void removeProjectile(Projectile obj)
    {
        projectiles.Remove(obj);
    }
}
