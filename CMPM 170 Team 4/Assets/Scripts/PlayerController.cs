using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 18;
    public bool parry = false;
    public GameObject metronome;
    private Rigidbody rig;
    float parry_init;
    
    private void Start() {
        rig = GetComponent<Rigidbody>();
    }
    
    private void Update() {
        if (parry == false)
        {
            if (Input.GetKeyDown("p"))
            {
                parry_init = metronome.GetComponent<RythmSystem>().songPosInBeats;
                parry = true;
            }

            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(hAxis, vAxis, 0) * speed * Time.deltaTime;

            rig.MovePosition(transform.position + movement);
        }
        else
        {
            if (metronome.GetComponent<RythmSystem>().songPosInBeats - parry_init > 1)
            {
                parry = false;
            }
        }
    }
}
