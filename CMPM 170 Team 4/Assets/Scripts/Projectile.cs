using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    protected float speed;
    private Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == transform.parent.GetComponent<PlayerAttackSystem>().opponent && transform.parent.GetComponent<PlayerAttackSystem>().opponent.GetComponent<PlayerController>().parry == false) // Hit target
        {
            // TODO: Animation
            // TODO: Health
            Debug.Log("Hit target");
        }
        if (collision.gameObject == transform.parent.GetComponent<PlayerAttackSystem>().opponent) // Hit target
        {
            // TODO: Animation
            // TODO: Health
            Debug.Log("Parried");
        }
        else // Hit obstacle or projectile
        {
            // TODO: Animation
            Debug.Log("Hit obstacle/projectile");
        }
        
        Destroy(gameObject);
    }
}
