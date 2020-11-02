using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 18;
    private Rigidbody rig;
    
    private void Start() {
        rig = GetComponent<Rigidbody>();
    }
    
    private void Update() {
        float hAxis, vAxis;
        
        if (gameObject.name == "Player 1"){
            hAxis = Input.GetAxis("Horizontal1");
            vAxis = Input.GetAxis("Vertical1");
        }
        else{
            hAxis = Input.GetAxis("Horizontal2");
            vAxis = Input.GetAxis("Vertical2");
        }

        Vector3 movement = new Vector3(hAxis, vAxis, 0) * speed * Time.deltaTime;
        rig.MovePosition(transform.position + movement);
    }
}