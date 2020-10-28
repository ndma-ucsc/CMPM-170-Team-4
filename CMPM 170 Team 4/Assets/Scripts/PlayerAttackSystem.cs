using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public int playerNumber;
    public GameObject opponent;
    public Projectile attackType1;

    public bool attacking;
    private List<float> result;

    void Awake()
    {
        attacking = false;
    }

    void Start()
    {
        if(playerNumber == 1)
        {
            RythmSystem.instance.p1StartDeploy.AddListener(this.Attack);
        }
        else
        {
            RythmSystem.instance.p2StartDeploy.AddListener(this.Attack);
        }
    }

    void Update()
    {
        // temp test using e for immediate attack spawn
        if(Input.GetKeyDown("space"))
        {
             Projectile attack = Instantiate(attackType1) as Projectile;
             attack.init(transform.position, opponent.transform.position, 50);
             attack.transform.parent = this.transform;
        }

        if(attacking)
        {
            if(result.Count > 0 && result[0] < RythmSystem.instance.songPosInBeats)
            {
                Projectile attack = Instantiate(attackType1) as Projectile;
                attack.init(transform.position, opponent.transform.position, 50);
                attack.transform.parent = this.transform;
                result.RemoveAt(0);
            }
            else if(result.Count == 0)
            {
                attacking = false;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
        attacking = true;
        result = RythmSystem.instance.getResult();
    }
}
