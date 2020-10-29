using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    /*** Player references ***/
    public int playerNumber;
    public GameObject opponent;
    
    /*** Attack references ***/
    public Projectile attackType1;

    /*** Attack parameters ***/
    bool attacking; // If player is currently attacking (sending out projectiles, not recording)
    private List<float> result; // Copy of previous recorded notes for assigned player

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
        // temp test using space for immediate attack spawn
        // if(Input.GetKeyDown("space"))
        // {
        //      Projectile attack = Instantiate(attackType1) as Projectile;
        //      attack.init(transform.position, opponent.transform.position, 50);
        //      attack.transform.parent = this.transform;
        // }

        if(attacking)
        {
            // If passed time of earliest note, send out attack and pop note from list
            if(result.Count > 0 && result[0] < RythmSystem.instance.songPosInBeats)
            {
                Projectile attack = Instantiate(attackType1) as Projectile;
                attack.init(transform.position, opponent.transform.position, 50);
                attack.transform.parent = this.transform;
                result.RemoveAt(0);
            }
            else if(result.Count == 0) // All notes played
            {
                attacking = false;
            }
        }
    }

    // Callback for start deploy listener, called when recording ended.
    void Attack()
    {
        Debug.Log("Attack");
        attacking = true; // Begin attacking logic
        result = RythmSystem.instance.getResult(); // Save copy of recorded notes
    }
}
