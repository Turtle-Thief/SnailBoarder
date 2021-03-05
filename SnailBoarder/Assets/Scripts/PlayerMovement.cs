using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerMaxSpeed = 50.0f, speedPerFrame = 1.0f, playerAcel = 100f, playerBrake = 1.9f, playerFric = 0.1f, playerRotSpeed = 300f, jumpForce = 10f, currentSpeed;

    // Rotation stuff
    Quaternion targetRotation;
    [HideInInspector]
    public float rotationY = 0f;

    private Rigidbody playerRigidbody;
    Vector3 moveX, moveZ;

    // Trick triggers stuff
    public Text debugText;

    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isDoingTrick = false;

    public float distToGround = 3f;

    float slopeAngle;
    // We can use normalised slope to multiply it by position change
    // to make it harder to go up the hill/ramp and easier on the way down
    //float normalisedSlope;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        debugText = GameObject.Find("DebugText").GetComponent<Text>();  //Find Debug Text on Scene
        currentSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        playerRigidbody.AddForce(gameObject.transform.forward * currentSpeed, ForceMode.Acceleration);
        currentSpeed -= playerFric; //friction
        currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
        //add friction force here?

        GroundCheck();
        Rotate();
    }

    public void OnMoveForward()
    {
        //add to player speed
        currentSpeed += speedPerFrame;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
        //add acceleration force here?
        playerRigidbody.AddForce(gameObject.transform.forward * currentSpeed, ForceMode.Acceleration);
    }

    public void OnBrake()
    {
        //subtract from player speed
        currentSpeed -= playerBrake;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
        //Add decceleration force here?
        playerRigidbody.AddForce(-gameObject.transform.forward * playerBrake, ForceMode.VelocityChange);
    }

    public void OnTurn(InputValue value)
    {
        if (isGrounded && !isDoingTrick)
        {
            Vector2 val = value.Get<Vector2>();
            rotationY = val.x * Time.deltaTime * playerRotSpeed;
            //transform.Rotate(new Vector3(0, val.x, 0) * Time.deltaTime * playerTurn, Space.Self);
            //Debug.Log("rotate");
        }
    }

    void Rotate()
    {
        if (isGrounded && !isDoingTrick)
        {
            transform.Rotate(0.0f, rotationY, 0.0f);
        }
    }

    public void OnJump()
    {
        if (isGrounded && !isDoingTrick)
        {
            //jump
            playerRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Acceleration);
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
    //rotation
}