using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 18;
    private Rigidbody rig;
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    
    private void Start() {
        rig = GetComponent<Rigidbody>();
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.y; //extents = size of height / 2
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