using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Current Values")]
    public float speed;  // Current speed of the player
    public bool enableWallRuning = false;  // Flag indicating if wall running is enabled
    public bool wallrunning;  // Flag indicating if the player is wall running
    public bool grounded = false;  // Flag indicating if the player is grounded

    [Header("Walk-Variables")]
    private float moveSpeed = 5f;  // Base move speed
    public float walkSpeed;  // Speed when walking
    public float sprintSpeed;  // Speed when sprinting
    public float wallruningSpeed;  // Speed when wall running
    public float groundDrag;  // Drag applied when the player is grounded

    [Header("Jumping Variables")]
    public float jumpHight = 5f;  // Jump height
    public float airMultipyer = 1.1f;  // Multiplier for air speed
    private int jumpsperformed = 0;  // Number of jumps performed
    public int jumpsallowed = 1;  // Number of jumps allowed
    bool readyToJump = true;  // Flag indicating if the player is ready to jump
    public float jumpCooldown = 0.1f;  // Cooldown between jumps

  
    [Header("Shrinking Variables")]
    public float shrinkSpeed = 5;  // Speed when shrinking
    public float shrinkScale = 1;  // Scale factor when shrinking
    private float startXScale;  // Initial x scale of the player
    private float startYScale;  // Initial y scale of the player
    private float startZScale;  // Initial z scale of the player
    private bool alreadyshrinking = false;  // Flag indicating if the player is already shrinking

    [Header("Groundcheck")]
    private float playerHight;  // Current height of the player
    public LayerMask whatIsGround;  // Layer mask for detecting ground

    [Header("Walldetecteion")]
    private float wallCkeckDistance; //distance the player has to be from the wall for it to be recognised
    private RaycastHit leftWallhit; //Raycast hit left wall
    private RaycastHit rightWallhit;//Raycast hit right wall
    public bool wallLeft; //bool representing if a wall was detected on the left
    public bool wallRight; //bool representing if a wall was detected on the right
    public LayerMask whatIsWall; //layermask which represent the layser all walls are saved on

    [Header("WallRunning")] 
    public float wallPushBackAfterTimer; //force pushing the player form the wall after player leaves wall
    public float maxWallrunningTime; //maximum time the player can wallrunn
    public float wallrunnningHight; //the hight which is added to the players y position when wallrunning
    private bool stopcalled = false; //bool to stop the wallrunn
    [SerializeField] private float yPositionWallRunning = 4; //concrete y-position  

    [Header("Other Variabels")]
    public Rigidbody pB = null;  // Reference to the Rigidbody component of the player
    public Transform orientation;  // Reference to the orientation transform
    float hInput; //horisontal input
    float vInput; //vertical imput
    private AttributeManager at; //attribute manager
    private Data_Percistence dp; //data percistance
    public TimerSelf timer; //timer


    public MovmementState state;  // Current movement state of the player

    //player has 5 movmentstates with diffrent behaviours
    public enum MovmementState
    {
        walking,
        sprinting,
        wallruning,
        shrinking,
        air
    }

    [Header("Key Binds")]
    public KeyCode jumpKey = KeyCode.Space;  // Key bind for jumping
    public KeyCode sprintKey = KeyCode.LeftShift;  // Key bind for sprinting
    public KeyCode shrinkKey = KeyCode.C;  // Key bind for shrinking
    public KeyCode wallrunningKey = KeyCode.Q;//Key binf for wallrunning 

    Vector3 moveDirection;  // Direction of player movement

    //positions of goal are
    private float GoalAreaPositionX;
    private float GoalAreaPositionY;
    private float GoalAreaPositionZ;


    // Start is called before the first frame update
    void Start()
    {
        //getting the ridgied body of the player and freezing its rotation
        pB = GetComponent<Rigidbody>();
        pB.freezeRotation = true;

        //getting the standart scale of the player befor shrinking
        startXScale = transform.localScale.x;
        startYScale = transform.localScale.y;
        startZScale = transform.localScale.z;
        playerHight = transform.localScale.y;

        //setting wallcheckDistacne to the plyers girth + 1.25
        wallCkeckDistance = transform.localScale.z * 1.25f;
        //defining the concrete wallrunning position based on the players character
        yPositionWallRunning = transform.position.y + wallrunnningHight;

        //getting the players attribute manager
        at = gameObject.GetComponent<AttributeManager>();

        //getting the position of the goal area
        GoalAreaPositionX = FindObjectOfType<GoalAreaRender>().getGoalAreaPosition().x;
        GoalAreaPositionY = FindObjectOfType<GoalAreaRender>().getGoalAreaPosition().y;
        GoalAreaPositionZ = FindObjectOfType<GoalAreaRender>().getGoalAreaPosition().z;

        //istantiating data_persitance
        dp = new Data_Percistence();

        //locking the corses and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /******************************************************
     * Methods which are used by Unity while running
     ******************************************************/

    //fixed update to make the playermovment even for all frame rates
    private void FixedUpdate()
    {
        //funktion that moves the player
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //Groundcheck
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHight * 0.5f + 1f, whatIsGround);
        //Checking for walls next to the player
        CheckForWall();

        //Input
        MyInput();
        SpeedControl();
        StateHandler();

        //ADD Drag
        if (grounded)
        {
            pB.drag = groundDrag;
            jumpsperformed = 0;
        }
        else //NO Drag while Player is in the air
        {
            pB.drag = 0;
        }

        //killing the player if it's y scale goes to low, used to kill it in lavatraps
        if (pB.position.y - (startYScale/0.5f) + 0.1f < -1.8f || at.dead)
        {
            Invoke("DelayedEndGame", 0.1f);
        }

        //Checks that the player reached the goal area and that it is unlocket
        if( Mathf.Abs(GoalAreaPositionX- pB.position.x) < 1f && Mathf.Abs(GoalAreaPositionZ - pB.position.z) < 1f && Mathf.Abs(GoalAreaPositionZ - pB.position.z) < 3f && FindObjectOfType<GoalAreaRender>().goalReached())
        {
            //sets the end time
            dp.SetEndTime(timer.EndTimer());
            DelayedVictory();
        }

        //Saving the current velocity so it can be viewed in the editor while running
        speed = pB.velocity.magnitude;
            
    }

    /******************************************************
     * Methods used for normal walking
     ******************************************************/

    /*
     * This Method sets the direction in which the player wants to travel and handles the request to jump and shrink
     */
    private void MyInput()
    {
        // Calculate the Movement direction out of the player input
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        /*
         * If-else Statement which first checks if the Player wants to
         * 1. Jump
         * 2. Shrink
         */

        // Jump
        if (Input.GetKey(jumpKey) && (jumpsallowed > jumpsperformed) && readyToJump)
        {
            // If this method is called the player can't jump till the timer is ready again (Set in the Jump Function)
            readyToJump = false;

            // Game adds 1 to the amount of jumps performed, this is reset every time the player is grounded
            jumpsperformed++;

            // Calls the Jump Function
            Jump();
        }

        // Shrinking
        if (Input.GetKey(shrinkKey) && !alreadyshrinking)
        {
            // If the player is not yet shrunk
            if (!alreadyshrinking)
            {
                pB.AddForce(Vector3.down * 1f, ForceMode.Impulse);
            }

            // Sets the player nearer to the ground
            transform.localPosition = new Vector3(transform.localPosition.x, shrinkScale + 1f, transform.localPosition.z);

            // Make the player smaller
            transform.localScale = new Vector3(startXScale * shrinkScale, startYScale * shrinkScale, startZScale * shrinkScale);

            // Change Player height
            playerHight = shrinkScale;

            // Changes bool to reflect that the player is now shrunk
            alreadyshrinking = true;
        }

        // Stop shrinking
        if (Input.GetKeyUp(shrinkKey))
        {
            // Returns the player to normal size
            transform.localScale = new Vector3(startXScale, startYScale, startZScale);

            // Sets the player height to its original height
            playerHight = startYScale;

            // Changes bool to reflect that the player isn't shrunk anymore
            alreadyshrinking = false;
        }

        //Wallrunnig
        //if the player hits the wall running key  and is next to a wall and is not already wallrunnig and wallrunnig is enabled or the player is already wallrunning
        if ((Input.GetKeyUp(wallrunningKey) && (wallRight || wallLeft) && !wallrunning && enableWallRuning) || wallrunning)
        {
            //if the player is not yet wallrunning
            if (!wallrunning)
            {
                //we set that it is wallrunning
                wallrunning = true;

                //we set the player position to the wallrunning position
                yPositionWallRunning = pB.position.y + wallrunnningHight;
                pB.position = new Vector3(pB.position.x, yPositionWallRunning, pB.position.z);
                //We stop the player form beeing affacted by gravity
                pB.useGravity = false;  

                //if ther is not yet a stop called
                if (!stopcalled)
                {
                    //we let the wallrunning stop after the ,aximuim wallrunning time
                    Invoke("StopWallRun", maxWallrunningTime);
                    stopcalled = true;
                }
            }

            //whenever the player is wallrunning we tell it to run with the wall
            RunningWithWall();


        }

        //Stopwallrunnig
        //if the player is wallrunning but there is no wall next to it we stop the wallrunning
        if(( wallrunning && !(wallRight || wallLeft)))
        {
            //stoping wallrunning
            StopWallRun();
        }

    }

    /*
     * This Method calculates the movement direction and after that applies force to the player based on two factors:
     * 1. When the player is grounded, the force is applied normally
     * 2. If the player is in the air, the movement speed is modified with a multiplier for air speed
     */
    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * vInput + orientation.right * hInput;
        transform.forward = orientation.forward;

        // Normal Movement
        if (grounded)
        {
            pB.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded) // If in the air, the movement speed gets multiplied with airMultiplier
        {
            pB.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultipyer, ForceMode.Force);
        }
    }

    /*
     * This Method controls the speed of the player. As we apply force to the player every time the player should move, we run the risk of the player reaching infinite speed or, on a more normal level, a speed greater than wanted.
     * This Method regulates this phenomenon and caps the speed at the current moveSpeed.
     */
    private void SpeedControl()
    {
        // Determine velocity
        Vector3 flatVel = new Vector3(pB.velocity.x, 0f, pB.velocity.z);

        // Limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            pB.velocity = new Vector3(limitedVel.x, pB.velocity.y, limitedVel.z);
        }
    }

    /*
     * This Method is used to adjust the movement speed based on the task currently performed by the player.
     */
    private void StateHandler()
    {
        // Mode Wallrunning
        if (wallrunning)
        {
            state = MovmementState.wallruning;
            moveSpeed = wallruningSpeed;
        }
        else if (alreadyshrinking)
        {
            state = MovmementState.shrinking;
            moveSpeed = shrinkSpeed;
        }
        else if (grounded && Input.GetKey(sprintKey)) // Mode Sprinting
        {
            state = MovmementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded) // Mode Walking
        {
            state = MovmementState.walking;
            moveSpeed = walkSpeed;
        }
        else // Mode Air
        {
            state = MovmementState.air;
        }
    }

    /******************************************************
     * Methods used for Jumping
     ******************************************************/

    private void Jump()
    {
        // Reset y velocity
        pB.velocity = new Vector3(pB.velocity.x, 0f, pB.velocity.z);

        //adding force to the player in the upwords position
        pB.AddForce(transform.up * jumpHight, ForceMode.Impulse);

        //calling resetJump funktion(Player can not jump till this funktion is called
        Invoke("ResetJump", jumpCooldown);
    }

    //reste jump funktion
    private void ResetJump()
    {
        //allows the player to jump again
        readyToJump = true;
    }

    //getter and setter methods
    public int getJumpsAllowed()
    {
        return this.jumpsallowed;
    }

    public void setJumpsAllowed(int value)
    {
        this.jumpsallowed = value;
    }

    /******************************************************
     * Methods used for Wallrunning
     ******************************************************/

    //Checks if a wall is near
    private void CheckForWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out rightWallhit, wallCkeckDistance, whatIsWall);
        wallRight = Physics.Raycast(transform.position, orientation.right, out leftWallhit, wallCkeckDistance, whatIsWall);
    }

    //ending wallrunnig
    private void StopWallRun()
    {
        //Set the variable indicating that the playert is wallrunning false
        wallrunning = false;

        //turn on gravity
        pB.useGravity = true;

        //no stop is called any more as we are stopping right now
        stopcalled = false;

        //get the direction orthoganal to the fall from  the players posiion
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        //push the player form the wall
        pB.AddForce(wallNormal * wallPushBackAfterTimer, ForceMode.Impulse);

    }

    private void RunningWithWall()
    {
        //get the direction orthoganal to the fall from  the players posiion
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        float pushForceMagnitude = 100f; // Adjust the desired magnitude of the force

        // Apply the force in the direction of the wall's normal vector
        pB.AddForce(wallNormal * pushForceMagnitude, ForceMode.Force);

    }

    //end the game -> instructing the gamemanager to stop the game eitheras death ir victory
    private void DelayedEndGame()
    {
        FindObjectOfType<GameManage>().EndGame();
    }

    private void DelayedVictory()
    {
        FindObjectOfType<GameManage>().Victory();
    }

}