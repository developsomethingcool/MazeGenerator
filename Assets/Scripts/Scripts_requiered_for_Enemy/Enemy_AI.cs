using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public NavMeshAgent agent; // Reference to the NavMeshAgent component for navigation
    public Transform player; // Reference to the player's transform
    public AttributeManager attributeManager; // Reference to the AttributeManager component
    public LayerMask whatisGroud, whatisPlayer; // Layer masks for ground and player detection
    public FieldOfView sightCheck; // Reference to the FieldOfView component for sight detection
    public Animator animator; // Reference to the Animator component for animation control
    private bool playingDeathAnimation = false; // Flag to track if the death animation is already playing
    public Weapon weapon; // Reference to the Weapon component for attacking

    // Patrolling
    public Vector3 walkpoint; // The position to patrol towards
    private bool walkpointSet = false; // Flag to track if a walkpoint is set
    public float walkPointRange; // The range within which to search for a walkpoint

    // Attacking
    private float attackCooldown; // The cooldown between attacks
    private bool alreadyAttacked; // Flag to track if an attack has already been performed

    // States
    public float sightRange, attackRange; // The range for sight and attack detection
    public bool playerInSight, playerInAttackRange; // Flags to track if the player is in sight and attack range

    public int numberKilledEnemies = 0; //number of killed enemies
    private void Awake()
    {
        player = GameObject.Find("Player").transform; // Find and assign the player's transform
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        sightCheck.SetVariable(sightRange, whatisPlayer); // Set the sight range and target mask for the FieldOfView component
        StartCoroutine(AttackRangeChecker()); // Start the coroutine to check the attack range
        attackCooldown = attributeManager.attackcooldown; // Get the attack cooldown from the AttributeManager component
    }

    private void Start()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    private IEnumerator AttackRangeChecker()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);
        }
    }

    private void Update()
    {
        //retirvig Player death status from the attribute manager
        if (attributeManager.dead)
        {
            if (!playingDeathAnimation)
            {
                playingDeathAnimation = true;

                //if enemy is killed, increase number of killed enemies
                FindObjectOfType<GoalAreaRender>().enemyKilled();

                gameObject.GetComponent<Animator>().enabled = true;
                animator.SetTrigger("Death");
                Debug.Log("Playing death animation!!");
               
            }
            FindObjectOfType<GoalAreaRender>().enemyKilled();
           
               

        }
        else
        {
            // Check for sight and attack range
            if (playerInSight && sightCheck.distanceToTarget < sightRange)
            {
                playerInSight = true; // Player is within sight range
            }
            else
            {
                playerInSight = sightCheck.canSeePlayer; // Use FieldOfView component to determine if player is in sight
            }

            if (!playerInSight && !playerInAttackRange)
            {
                Patroling(); // Player is neither in sight nor in attack range, patrol the area
            }
            else if (playerInSight && !playerInAttackRange)
            {
                ChasePlayer(); // Player is in sight but not in attack range, chase the player
            }
            else if (playerInSight && playerInAttackRange)
            {
                AttackPlayer(); // Player is both in sight and in attack range, attack the player
            }
            else
            {
                Patroling(); // Default behavior, patrol the area
            }
        }
    }

    private void Patroling()
    {
        if (!walkpointSet)
        {
            SearchWalkPoint(); // Find a new random walk point
        }

        if (walkpointSet)
        {
            agent.SetDestination(walkpoint); // Set the agent's destination to the walk point
        }

        Vector3 distanceToWalkpoint = transform.position - walkpoint;

        if (distanceToWalkpoint.magnitude < 1f)
        {
            walkpointSet = false; // If the distance to the walk point is close, mark walkpointSet as false to find a new walk point
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point within range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check that the walkpoint is on the ground
        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatisGroud))
        {
            walkpointSet = true; // Mark walkpointSet as true if the walkpoint is on the ground
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); // Set the agent's destination to the player's position
        transform.LookAt(player); // Rotate to face the player
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position); // Stop the agent's movement
        transform.LookAt(player); // Rotate to face the player

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Debug.Log("Attacking Player");
            weapon.Attack(); // Perform the attack using the weapon
            Invoke(nameof(ResetAttack), attackCooldown); // Reset the attack after the cooldown time
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false; // Reset the alreadyAttacked flag to allow for the next attack
    }


}
