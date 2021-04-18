using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    TricksController tricksController;

    public float playerMaxSpeed = 50.0f, speedPerFrame = 1.0f, playerAcel = 100f, playerBrake = 1.9f,
                   playerFric = 0.1f, playerRotSpeed = 300f, playerAirRotSpeed = 600f, jumpForce = 10f, currentSpeed, currentRotSpeed;

    // Rotation stuff
    Quaternion targetRotation;
    [HideInInspector]
    public float rotationVal = 0f;
    public Vector2 rotationVec = Vector2.zero;

    private Rigidbody playerRigidbody;
    public Transform centerOfMass;
    public Collider selfRighting;
    Vector3 moveX, moveZ;

    // Trick triggers stuff
    //public Text debugText;

    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isOnRamp = false;
    [HideInInspector]
    public bool isBraking = false;

    public LayerMask IgnoreGroundCheckLayer;
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
        //debugText = GameObject.Find("DebugText").GetComponent<Text>();  //Find Debug Text on Scene
        currentSpeed = 0.0f;
        currentRotSpeed = playerRotSpeed;
        if (centerOfMass != null)
            playerRigidbody.centerOfMass = centerOfMass.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!

        }
    }

    private void FixedUpdate()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            //Brake();
            Friction();
            GroundCheck();
            Rotate();
            SnailRotate(rotationVec);
            VelocityDirectionUpdate();
        }

    }

    // Input Event Functions
    public void OnMoveForward()
    {
        if (isGrounded)
        {
            // how fast is the player currently
            currentSpeed = playerRigidbody.velocity.magnitude;

            if (currentSpeed < playerMaxSpeed)
            {
                // add to player speed
                currentSpeed += playerAcel;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
                playerRigidbody.AddForce(gameObject.transform.forward.normalized * currentSpeed, ForceMode.VelocityChange);
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
        Vector2 val = value.Get<Vector2>();
        //Debug.Log("TURN" + val);
        //rotationVec = val;
        if (isGrounded)
        {
            rotationVal = val.x * Time.deltaTime * playerRotSpeed;
        }
        else
            rotationVal = val.x * Time.deltaTime * playerAirRotSpeed;

        rotationVec = val;

        //transform.Rotate(new Vector3(0, val.x, 0) * Time.deltaTime * playerTurn, Space.Self);
        //Debug.Log("rotate");

    }

    public void OnJump()
    {
        if (isGrounded)
        {
            //jump
            Jump(1.0f);
        }
    }

    // Physics functions
    public void Jump(float forceMultiplier)
    {
        playerRigidbody.AddForce(new Vector3(0, jumpForce * forceMultiplier, 0), ForceMode.Acceleration);
        if (gameObject.GetComponent<PathCreation.Examples.SnailPathFollower>().pathCreator != null)
        {
            gameObject.GetComponent<PathCreation.Examples.SnailPathFollower>().StartRailGrind();
        }
    }

    void Brake()
    {
        if (isGrounded && isBraking)
        {

            //Add decceleration force here?
            if (playerRigidbody.velocity.magnitude > playerBrake)
            {
                //subtract from player speed
                currentSpeed -= playerBrake;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
                playerRigidbody.AddForce(gameObject.transform.forward.normalized * -playerBrake, ForceMode.VelocityChange);
            }
        }
    }
    void Rotate()
    {
        if (tricksController.currentTrick.mName == tricksController.Tricks[(int)TricksController.TrickName.NullTrick].mName)
        {
            transform.Rotate(0.0f, rotationVal, 0.0f);
            //playerRigidbody.AddForce(transform.forward.normalized * playerRigidbody.velocity.magnitude - playerRigidbody.velocity, ForceMode.VelocityChange);
            //playerRigidbody.rotation = transform.rotation;

        }
    }

    public void VelocityDirectionUpdate()
    {
        if (tricksController.currentTrick.mName == tricksController.Tricks[(int)TricksController.TrickName.NullTrick].mName
            && playerRigidbody.velocity.magnitude > 0.5f)
        {
            if (isGrounded)
            {
                //currentSpeed = playerRigidbody.velocity.magnitude;
                currentRotSpeed = playerRotSpeed - currentSpeed;
                if (Mathf.Abs(Vector3.Dot(transform.forward.normalized, playerRigidbody.velocity.normalized)) <= 0.85f)
                {
                    playerRigidbody.velocity = new Vector3(transform.forward.normalized.x * playerRigidbody.velocity.magnitude
                                                           , playerRigidbody.velocity.y
                                                           , transform.forward.normalized.z * playerRigidbody.velocity.magnitude);
                }
            }
            else
            {
                currentRotSpeed = playerAirRotSpeed - currentSpeed;
            }
        }
    }

    // Trying some other rotation stuff

    // A function that should return a quaternion with the rotation difference between the normal of the ground and the snails current up vector
    Quaternion GetPhysicsRotation()
    {
        Vector3 targetVec = Vector3.up;
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (Vector3.down * 2 - transform.up).normalized, out hit, distToGround, ~IgnoreGroundCheckLayer))
        {
            targetVec = hit.normal;
        }

        return Quaternion.FromToRotation(transform.up, targetVec);
    }

    // A function to help the player go the direction theyre facing, constrained to a plane
    Quaternion GetVelocityRot()
    {
        Vector3 vel = playerRigidbody.velocity;
        if (vel.magnitude > 0.2f)
        {
            vel.y = 0;
            Vector3 dir = transform.forward;
            dir.y = 0;
            Quaternion velRot = Quaternion.FromToRotation(dir.normalized, vel.normalized);
            return velRot;
        }
        else
            return Quaternion.identity;
    }

    // Function that moves the snail and rotates if needed!
    void SnailRotate(Vector2 input)
    {
        //if (tricksController.currentTrick.mName == tricksController.Tricks[(int)TricksController.TrickName.NullTrick].mName)
        {
            Quaternion physicsRotation = !isGrounded ? Quaternion.identity : GetPhysicsRotation();
            Quaternion velocityRotation = GetVelocityRot();
            Quaternion inputRotation = Quaternion.identity;
            Quaternion computedRotation = Quaternion.identity;

            if (input.magnitude > 0.1f)
            {
                Vector3 adaptedDirection = RotateToPlayer(input);
                Vector3 planarDirection = transform.forward.normalized;
                planarDirection.y = 0;
                inputRotation = Quaternion.FromToRotation(planarDirection, adaptedDirection);

                if (isGrounded)
                {
                    Vector3 direction = inputRotation * transform.forward.normalized * currentSpeed * Time.deltaTime;
                    //Vector3 direction = planarDirection  * currentSpeed;
                    //Debug.DrawRay(transform.position, direction, Color.yellow);
                    playerRigidbody.AddForce(direction, ForceMode.VelocityChange);

                }
            }
            float rotationSpeed;
            if (isGrounded)
                rotationSpeed = playerRotSpeed;
            else
                rotationSpeed = playerAirRotSpeed;

            computedRotation = physicsRotation * transform.rotation;
            //Debug.Log("Physics: " + physicsRotation.eulerAngles);
            //Debug.Log("Velocity: " + velocityRotation.eulerAngles);
            Quaternion rotation = Quaternion.Lerp(transform.rotation, computedRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, computedRotation, rotationSpeed * 0.01f * Time.deltaTime);
            //playerRigidbody.MoveRotation(rotation);
        }
    }

    Vector3 RotateToPlayer(Vector2 direction)
    {
        Vector3 forward = Vector3.ProjectOnPlane(transform.position - (-2.0f * transform.forward), Vector3.up);
        forward.y = 0;
        Vector3 right = Quaternion.AngleAxis(90, Vector3.up) * forward;

        return (forward * direction.y + right * direction.x).normalized;
    }

    void GroundCheck()
    {
        Debug.DrawRay(transform.position + new Vector3(0.0f, 1.4f, 0.0f), (Vector3.down * 1.5f - transform.up).normalized * distToGround, Color.blue);
        
        // Tricks triggers stuff
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + new Vector3(0.0f, 1.4f, 0.0f), (Vector3.down.normalized * 2 - transform.up.normalized).normalized, out hit, distToGround, ~IgnoreGroundCheckLayer))
        {
            //Debug.Log("Grounded on " + hit.transform.name);

            slopeAngle = Vector3.Angle(hit.normal, transform.forward) - 90;
            // normalisedSlope = (slopeAngle / 90f) * -1f;
            //if (debugText)
            //{
            //    debugText.text = "Grounded on " + hit.transform.name;
            //    debugText.text += "\nSlope Angle: " + slopeAngle.ToString("N0") + "°";
            //}
            if (hit.transform.gameObject.layer == 10) // Is ramp??
            {
                isOnRamp = true;
                //Debug.Log("ramp");
            }
            else
            {
                //Debug.Log("Grounded on " + hit.transform.name);
                isOnRamp = false;
            }
            isGrounded = true;
        }
        else
        {
            //if (debugText)
            //    debugText.text = "Not Grounded";
            isGrounded = false;
        }
    }

    void Friction()
    {
        if (isGrounded)
        {
            if (playerRigidbody.velocity.magnitude > playerFric)
            {
                currentSpeed -= playerFric; //friction
                currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
                playerRigidbody.AddForce(gameObject.transform.forward.normalized * -playerFric, ForceMode.Acceleration);
            }
        }
    }

    void SelfRight()
    {

    }
}