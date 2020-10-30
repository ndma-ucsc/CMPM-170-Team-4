using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic energy ball called to hit target, linear movement
public class Projectile : MonoBehaviour
{
    /*** Parameters ***/
    private Vector3 direction;
    private float speed;
    private Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Initializes start parameters based on origin(spawn location), target location, and movement speed
    public void init(Vector3 origin, Vector3 target, float movementSpeed)
    {
        direction = Vector3.Normalize(target - origin);
        rig.transform.position = origin + direction * 1.1f;
        speed = movementSpeed;
    }

    void Update()
    {
        Vector3 movement = direction * speed * Time.deltaTime;

        rig.MovePosition(transform.position + movement);
    }

    // For collision with any object, check if target or not
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == transform.parent.GetComponent<PlayerAttackSystem>().opponent) // Hit target
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
        
        Destroy(gameObject);
    }
}
