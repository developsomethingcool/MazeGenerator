using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billbord : MonoBehaviour
{
    private Transform cam;  // Reference to the camera transform

    private void Start()
    {
        cam = GameObject.Find("Camera").transform;//find the emty object attached to the player calle camera
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Make the object's transform look at a point in front of it based on the camera's forward direction
        transform.LookAt(transform.position + cam.forward);
    }
}
