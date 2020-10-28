using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public GameObject opponent;
    public Projectile attackType1;

    void Update()
    {
        // temp test using e for immediate attack spawn
        if(Input.GetKeyDown("space"))
        {
             Projectile attack = Instantiate(attackType1) as Projectile;
             attack.init(transform.position, opponent.transform.position, 50);
             attack.transform.parent = this.transform;
        }
    }
}
