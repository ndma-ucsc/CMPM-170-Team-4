﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    /*** Player references ***/
    public GameObject player; // Player whose recording is used
    public GameObject opponent; // Object to fire attacks at
    
    /*** Attack references ***/
    public Attack attackType1; // Prefab of attack 1
    public Attack attackType2; // Prefab of attack 2

    /*** Projectile references ***/
    public Projectile linearProjectile; // Prefab of linear movement projectile
    public Projectile trackingProjectile; // Prefab of tracking movement projectile

    /*** Attack parameters ***/
    bool attacking; // If player is currently attacking (sending out projectiles, not recording)
    private List<(float,int)> result; // Copy of previous recorded notes for assigned player

    void Awake()
    {
        attacking = false;
    }

    void Start()
    {
        if(player.name == "Player 1")
        {
            RythmSystem.instance.p1StartDeploy.AddListener(this.Attack);
        }
        else if (player.name == "Player 2")
        {
            RythmSystem.instance.p2StartDeploy.AddListener(this.Attack);
        }
        else
        {
            Debug.Log("No player assigned to player attack system");
        }
    }

    void Update()
    {
        // temp test using space for immediate attack spawn
        // if(Input.GetKeyDown("space"))
        // {
        //      Projectile attack = Instantiate(attackType1) as Projectile;
        //      attack.init(opponent.transform.position, 50);
        //      attack.transform.parent = this.transform;
        // }

        if(attacking)
        {
            // If passed time of earliest note, send out attack and pop note from list
            if(result.Count > 0 && result[0].Item1 < RythmSystem.instance.songPosInBeats)
            {
                Attack attack;
                if(result[0].Item2 == 1)
                {
                    attack = Instantiate(attackType1) as Attack;
                }
                else if(result[0].Item2 == 2)
                {
                    attack = Instantiate(attackType2) as Attack;
                }
                else
                {
                    attack = Instantiate(attackType1) as Attack;
                }
                attack.transform.position = this.transform.position;
                attack.init(opponent, 50);
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
        attacking = true; // Begin attacking logic
        result = RythmSystem.instance.getResult(); // Save copy of recorded notes
    }
}
