using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {
    public Camera MainCamera; //be sure to assign this in the inspector to your main camera
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Use this for initialization
    void Start(){
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1));
        // screenBounds = MainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.y; //extents = size of height / 2
        Debug.Log(screenBounds.x);
        Debug.Log(screenBounds.y);
    }

    // Update is called once per frame
    void LateUpdate(){
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.y * -1 + objectWidth, screenBounds.y - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.x * -2 + objectHeight, screenBounds.x*2 - objectHeight);
        transform.position = viewPos;
    }
}