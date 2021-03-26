using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    TricksController tricksController;

    public float playerMaxSpeed = 50.0f, speedPerFrame = 1.0f, playerAcel = 100f, playerBrake = 1.9f, playerFric = 0.1f, playerRotSpeed = 300f, jumpForce = 10f, currentSpeed;

    // Rotation stuff
    Quaternion targetRotation;
    [HideInInspector]
    public float rotationY = 0f;

    private Rigidbody playerRigidbody;
    public Transform centerOfMass;
    public Collider selfRighting;
    Vector3 moveX, moveZ;

    // Trick triggers stuff
    public Text debugText;

    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isBraking = false;

    public float distToGround = 3f;

    float slopeAngle;
    // We can use normalised slope to multiply it by position change
    // to make it harder to go up the hill/ramp and easier on the way down
    //float normalisedSlope;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        tricksController = this.GetComponent<TricksController>();
        debugText = GameObject.Find("DebugText").GetComponent<Text>();  //Find Debug Text on Scene
        currentSpeed = 0.0f;
        if (centerOfMass != null)
            playerRigidbody.centerOfMass =  centerOfMass.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is NOT paused...
        if(!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!

        }
    }

    private void FixedUpdate()
    {
        //Brake();
        Friction();
        GroundCheck();
        Rotate();
    }

    // Input Event Functions
    public void OnMoveForward()
    {
        if (isGrounded)
        {
            //add to player speed
            currentSpeed += speedPerFrame;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
            //add acceleration force here?
            if (playerRigidbody.velocity.magnitude < playerMaxSpeed)
            {
                playerRigidbody.AddForce(gameObject.transform.forward.normalized * playerAcel, ForceMode.VelocityChange);
            }
        }
    }

    public void OnBrake()
    {
        //isBraking = !isBraking;
        if (isGrounded)
        {
            //subtract from player speed
            currentSpeed -= playerBrake;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
            //Add decceleration force here?
            if (playerRigidbody.velocity.sqrMagnitude > 0.2)
            {
                playerRigidbody.AddForce(gameObject.transform.forward.normalized * -playerBrake, ForceMode.VelocityChange);
            }
        }
    }

    public void OnTurn(InputValue value)
    {
        if (isGrounded && tricksController.currentTrick.mName == tricksController.Tricks[(int)TricksController.TrickName.NullTrick].mName)
        {
            Vector2 val = value.Get<Vector2>();
            rotationY = val.x * Time.deltaTime * playerRotSpeed;
            //transform.Rotate(new Vector3(0, val.x, 0) * Time.deltaTime * playerTurn, Space.Self);
            //Debug.Log("rotate");
        }
    }

    public void Jump(float forceMultiplier)
    {
        playerRigidbody.AddForce(new Vector3(0, jumpForce * forceMultiplier, 0), ForceMode.Acceleration);
    }

    // Physics functions
    void Brake()
    {
        if (isGrounded && isBraking)
        {
            //subtract from player speed
            currentSpeed -= playerBrake;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
            //Add decceleration force here?
            if (playerRigidbody.velocity.magnitude > playerBrake)
            {
                playerRigidbody.AddForce(gameObject.transform.forward.normalized * -playerBrake, ForceMode.VelocityChange);
            }
        }
    }
    void Rotate()
    {
        if (isGrounded && tricksController.currentTrick.mName == tricksController.Tricks[(int)TricksController.TrickName.NullTrick].mName)
        {
            transform.Rotate(0.0f, rotationY, 0.0f);
        }
    }

    void GroundCheck()
    {
        // Tricks triggers stuff
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + 0.1f))
        {
            slopeAngle = Vector3.Angle(hit.normal, transform.forward) - 90;
            // normalisedSlope = (slopeAngle / 90f) * -1f;
            debugText.text = "Grounded on " + hit.transform.name;
            debugText.text += "\nSlope Angle: " + slopeAngle.ToString("N0") + "°";
            isGrounded = true;
        }
        else
        {
            debugText.text = "Not Grounded";
            isGrounded = false;
        }
    }
    
    void Friction()
    {
        if (isGrounded)
        {
            currentSpeed -= playerFric; //friction
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
            //add friction force here?
            if (playerRigidbody.velocity.magnitude > playerFric)
            {
                playerRigidbody.AddForce(gameObject.transform.forward.normalized * -playerFric, ForceMode.Acceleration);
            }
        }
    }
}