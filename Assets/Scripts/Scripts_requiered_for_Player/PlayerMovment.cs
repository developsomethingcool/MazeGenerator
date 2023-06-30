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
    private float wallCkeckDistance;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    public bool wallLeft;
    public bool wallRight;
    public LayerMask whatIsWall;

    [Header("WallRunning")] 
    public float wallPushBackAfterTimer;
    public float maxWallrunningTime;
    public float wallrunnningHight;
    private bool stopcalled = false;
    [SerializeField] private float yPositionWallRunning = 4;

    [Header("Other Variabels")]
    public Rigidbody pB = null;  // Reference to the Rigidbody component of the player
    public Transform orientation;  // Reference to the orientation transform
    float hInput;
    float vInput;
    private AttributeManager at;

    public MovmementState state;  // Current movement state of the player

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
        pB = GetComponent<Rigidbody>();
        pB.freezeRotation = true;

        startXScale = transform.localScale.x;
        startYScale = transform.localScale.y;
        startZScale = transform.localScale.z;
        playerHight = transform.localScale.y;

        wallCkeckDistance = transform.localScale.z * 1.25f;
        yPositionWallRunning = transform.position.y + wallrunnningHight;
        at = gameObject.GetComponent<AttributeManager>();

        GoalAreaPositionX = FindObjectOfType<GoalAreaRender>().getGoalAreaPosition().x;
        GoalAreaPositionY = FindObjectOfType<GoalAreaRender>().getGoalAreaPosition().y;
        GoalAreaPositionZ = FindObjectOfType<GoalAreaRender>().getGoalAreaPosition().z;
}

    /******************************************************
     * Methods which are used by Unity while running
     ******************************************************/

    private void FixedUpdate()
    {
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

        if (pB.position.y - (startYScale/0.5f) + 0.1f < -1.8f || at.dead)
        {
            Debug.Log(pB.position.y - (startYScale / 0.5f) + 0.1f);
            Invoke("DelayedEndGame", 0.1f);
            //FindObjectOfType<GameManage>().EndGame();
        }

        if( Mathf.Abs(GoalAreaPositionX- pB.position.x) < 1f && Mathf.Abs(GoalAreaPositionZ - pB.position.z) < 1f && Mathf.Abs(GoalAreaPositionZ - pB.position.z) < 3f)
        {
            Debug.Log("Final Space is reached!");
            Invoke("DelayedVictory", 0.3f);
        }

        /*if (Mathf.Abs(FindObjectOfType<GoalAreaRender>().getGoalAreaPosition().x - pB.position.x) < 1f && Mathf.Abs(FindObjectOfType<GoalAreaRender>().getGoalAreaPosition().z - pB.position.z) < 1f);
        {
            
        } */

        //Saving the current velocity so it can be viewed in the editor while running
        speed = pB.velocity.magnitude;

        if (Input.GetKey(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
            
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
        if ((Input.GetKeyUp(wallrunningKey) && (wallRight || wallLeft) && !wallrunning && enableWallRuning) || wallrunning)
        {
            if (!wallrunning)
            {
                wallrunning = true;
                yPositionWallRunning = pB.position.y + wallrunnningHight;
                pB.position = new Vector3(pB.position.x, yPositionWallRunning, pB.position.z);
                
                pB.useGravity = false;  
                if (!stopcalled)
                {
                    Invoke("StopWallRun", maxWallrunningTime);
                    stopcalled = true;
                }
            }

            
            RunningWithWall();


        }

        //Stopwallrunnig
    
        if(( wallrunning && !(wallRight || wallLeft)))
        {
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

        pB.AddForce(transform.up * jumpHight, ForceMode.Impulse);

        Invoke("ResetJump", jumpCooldown);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

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
       // if (!wallrunning)
        //{
            wallrunning = false;

            pB.useGravity = true;

            stopcalled = false;


            Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

            pB.AddForce(wallNormal * wallPushBackAfterTimer, ForceMode.Impulse);
      //  }

    }

    private void RunningWithWall()
    {
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        float pushForceMagnitude = 100f; // Adjust the desired magnitude of the force

        // Apply the force in the direction of the wall's normal vector
        pB.AddForce(wallNormal * pushForceMagnitude, ForceMode.Force);
        //Debug.Log("ForceAllyed");

    }
    //end the game
    private void DelayedEndGame()
    {
        FindObjectOfType<GameManage>().EndGame();
    }

    private void DelayedVictory()
    {
        FindObjectOfType<GameManage>().Victory();
    }

}