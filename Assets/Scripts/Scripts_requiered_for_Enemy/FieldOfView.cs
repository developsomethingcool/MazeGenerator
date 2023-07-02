using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius; // The radius of the field of view
    public float distanceToTarget; // The distance to the target within the field of view
    [Range(0, 360)]
    public float angle; // The angle of the field of view

    public GameObject playerRef; // Reference to the player object

    public LayerMask targetMask; // The layer mask for the targets
    public LayerMask obstructunMask; // The layer mask for obstructions

    public bool canSeePlayer = false; // Flag to indicate if the player is within the field of view

    private void Start()
    {
        playerRef = GameObject.Find("Player"); // Find the player object
        StartCoroutine(FOVRoutine()); // Start the field of view routine
    }

    //Seting method for setting variables like the radius of targetmask
    public void SetVariable(float radius, LayerMask targetMask)
    {
        this.radius = radius; //radius represents the viewrange go the enemy
        this.targetMask = targetMask; // represent the layer on which the enemy is on
    }

    //IEnumerator used to not check for the player every frame but only 5 times oer second;
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f; //delay representin how many seconds this IEnumerator should wait
        WaitForSeconds wait = new WaitForSeconds(delay); //formulating the delay in terms of WaitforSeconds

        while (true)
        {
            yield return wait; //telling the IEnumerator to wait for our delay
            FieldofViewCheck(); // Perform field of view check
        }
    }

    /**
     * This funktionis used to check if the player is in sight range of the enemy
     * + the player is not obstructed by a wall
     * + the player is inside a given angle of the forward direction of the enemy
     */
    private void FieldofViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask); // Perform overlap sphere check

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform; // Get the first target within the field of view
            Vector3 directionToTarget = (target.position - transform.position).normalized; // Calculate direction to the target

            if (Vector3.Angle(transform.position, directionToTarget) < (angle / 2)) // Check if the target is within the angle range
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position); // Calculate the distance to the target

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructunMask)) // Check for obstructions
                {
                    canSeePlayer = true; // Set the flag to indicate that the player is within the field of view
                }
                else
                {
                    canSeePlayer = false; // Reset the flag as there is an obstruction between the object and the player
                }
            }
            else
            {
                canSeePlayer = false; // Reset the flag as the target is not within the angle range
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false; // Reset the flag as there are no targets within the field of view
        }
    }
}