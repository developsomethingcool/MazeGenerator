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

    public void SetVariable(float radius, LayerMask targetMask)
    {
        this.radius = radius;
        this.targetMask = targetMask;
    }

    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldofViewCheck(); // Perform field of view check
        }
    }

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