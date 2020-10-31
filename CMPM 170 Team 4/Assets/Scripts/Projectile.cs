using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Basic energy ball called to hit target, linear movement
public class Projectile : MonoBehaviour
{
    /*** Parameters ***/
    protected GameObject target;
    protected Vector3 direction;
    protected float speed;
    protected Rigidbody rig;

    /*** Events ***/
    public UnityEvent<Projectile> hit = new UnityEvent<Projectile>();

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Initializes start parameters based on origin(spawn location), target location, and movement speed
    public virtual void init(Vector3 origin, Vector3 dir, GameObject targetObj, float movementSpeed)
    {
        target = targetObj;
        direction = Vector3.Normalize(dir);
        rig.transform.position = origin + direction * 1.1f;
        speed = movementSpeed;
    }

    // For collision with any object, check if target or not
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == transform.parent.GetComponent<Attack>().target) // Hit target
        {
            // TODO: Animation
            // TODO: Health
            Debug.Log("Hit target");
        }
        else // Hit obstacle or projectile
        {
            // TODO: Animation
            Debug.Log("Hit obstacle/projectile");
        }
        hit.Invoke(this);
        Destroy(gameObject);
    }
}
