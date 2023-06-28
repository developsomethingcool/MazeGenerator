using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Axe_script : MonoBehaviour
{
    private string targetTag = "Player";  // The tag of the target object we want the axe to collide with

    public Weapon weaponcontroller;  // Reference to the weapon controller script

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);  // Output the tag of the collided object to the console for debugging purposes

        // Check if the tag of the collided object matches the target tag we specified
        if (targetTag.Equals(other.gameObject.tag))
        {
            weaponcontroller.AxeHit();  // Call the AxeHit() method in the weapon controller script
        }
    }
}

