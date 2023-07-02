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
    private bool addedToDeathcount = false; //Variable used to count the enemy ONETIME to the gaolstate

    public int numberKilledEnemies = 0; //number of killed enemies

    //Awakefunktion used to set variables
    private void Awake()
    {
        player = GameObject.Find("Player").transform; // Find and assign the player's transform
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        sightCheck.SetVariable(sightRange, whatisPlayer); // Set the sight range and target mask for the FieldOfView component
        StartCoroutine(AttackRangeChecker()); // Start the coroutine to check the attack range
        attackCooldown = attributeManager.attackcooldown; // Get the attack cooldown from the AttributeManager component
        gameObject.GetComponent<Animator>().enabled = false; //Setting animator false it is only used for the death animation right now
    }

 
    //Enumerator is basicly a thread which runns parallel to the other code
    private IEnumerator AttackRangeChecker()
    {
        float delay = 0.2f; //delay used for waiting
        WaitForSeconds wait = new WaitForSeconds(delay);    //creat a new wait for seconds which represents 1/5 second

        //infinit while loop
        while (true)
        {
            yield return wait; //The enumerator whatits the specified time befor contiuing
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer); //Checks if the player is in attackrange
        }
    }

    private void Update()
    {
        //retirvig Player death status from the attribute manager
        if (attributeManager.dead)
        {
            //If the enemy didn't yet count towords the endgoal this is changed
            if (!addedToDeathcount)
            {
                addedToDeathcount=true;
                FindObjectOfType<GoalAreaRender>().EnemyKilled();

            }

            //Starting to play the death animation 
            if (!playingDeathAnimation)
            {
                gameObject.GetComponent<Animator>().enabled = true;//first aktivating the animator again
                animator.SetTrigger("Death"); //Setting trigger to death so that the animator players the deth animation
                playingDeathAnimation = true; //Changing the boolean so this part is only called ones
            }

        }
        //If the enemy is not dead
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

    //Patroling the standart state for the enemy. Here the enemy walks toward a random point in its radius
    private void Patroling()
    {
        if (!walkpointSet) //When there is no current walkpoint
        {
            SearchWalkPoint(); // Find a new random walk point
        }

        if (walkpointSet) //Wehen thir is a walkpoint the enemy walks towrds it
        {
            agent.SetDestination(walkpoint); // Set the agent's destination to the walk point
        }

        //Checking for distance between enemy and its walkpoint
        Vector3 distanceToWalkpoint = transform.position - walkpoint;

        //When the enemy is close to the walkpoint the walkpoint is deactivated and therefor will creat a new one the next time the funktion is called 
        if (distanceToWalkpoint.magnitude < 1f)
        {
            walkpointSet = false; // If the distance to the walk point is close, mark walkpointSet as false to find a new walk point
        }
    }

    //Funktion used to find a new walkpoint for patroling
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
