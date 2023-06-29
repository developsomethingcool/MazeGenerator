using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam: MonoBehaviour

{
    [Header("ToBeDisplayed")]
    public Camerastyle currentStyle;  // The currently selected camera style

    [Header("Reference")]
    public Transform orientation;  // Reference to the orientation transform
    public Transform player;  // Reference to the player transform
    public Transform playerObj;  // Reference to the player object transform
    public Rigidbody rb;  // Reference to the Rigidbody component

    [Header("Cinemachines")]
    public GameObject basicCam;  // Reference to the basic camera object
    public GameObject combatCam;  // Reference to the combat camera object
    public GameObject fpsCam;  // Reference to the first-person camera object

    [Header("Values")]
    public float rotationSpeed;  // The rotation speed for the camera

    public Transform combatLookAt;  // The transform to look at during combat

    public enum Camerastyle
    {
        Basic = 0,
        Combat = 1,
        firstPerson = 2
    }

    private void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Disable unnecessary cameras and set the initial camera style to combat
        fpsCam.SetActive(false);
        basicCam.SetActive(false);
        combatCam.SetActive(true);
        currentStyle = Camerastyle.Combat;
    }

    private void Update()
    {
        // Switch CameraStyle when "O" key is pressed
        if (Input.GetKeyUp(KeyCode.O))
        {
            SwitchToNextCameraStyle();
        }

        // Rotate orientation to face the player's position
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection;

        // Rotate PlayerObject based on the selected camera style
        if (currentStyle == Camerastyle.Basic)
        {
            // Rotate the player object based on the input from the player
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDirection != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (currentStyle == Camerastyle.Combat)
        {
            // Rotate the orientation and player object to face the combat look-at position
            Vector3 directionToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = directionToCombatLookAt.normalized;
            playerObj.forward = directionToCombatLookAt.normalized;
        }
        else if (currentStyle == Camerastyle.firstPerson)
        {
            // Rotate the orientation based on the camera's forward direction in first-person mode
            orientation.forward = transform.forward * Time.deltaTime;
        }
    }

    private void SwitchToNextCameraStyle()
    {
        if (currentStyle == Camerastyle.Combat)
        {
            // Switch to first-person camera style
            combatCam.SetActive(false);
            fpsCam.SetActive(true);
            currentStyle = Camerastyle.firstPerson;
        }
        else if (currentStyle == Camerastyle.firstPerson)
        {
            // Switch to combat camera style
            fpsCam.SetActive(false);
            combatCam.SetActive(true);
            currentStyle = Camerastyle.Combat;
        }


        //Disabeling Basic cam as this camera Style seems not to be working as intended
        /*
        if (currentStyle == Camerastyle.Basic)
        {
            // Switch to combat camera style
            basicCam.SetActive(false);
            combatCam.SetActive(true);
            currentStyle = Camerastyle.Combat;
        }
        else if (currentStyle == Camerastyle.Combat)
        {
            // Switch to first-person camera style
            combatCam.SetActive(false);
            fpsCam.SetActive(true);
            currentStyle = Camerastyle.firstPerson;
        }
        else if (currentStyle == Camerastyle.firstPerson)
        {
            // Switch to basic camera style
            fpsCam.SetActive(false);
            basicCam.SetActive(true);
            currentStyle = Camerastyle.Basic;
        }

        */
    }

    public String getCurrentSytle()
    {
        return currentStyle.ToString();
    }
}
