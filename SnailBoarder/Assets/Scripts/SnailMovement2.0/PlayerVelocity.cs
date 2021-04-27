using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVelocity : MonoBehaviour
{
    public float acceleration = 50.0f,
                     braking = 10.0f,
                     friction = 5.0f,
                     maxForwardSpeed = 100.0f,
                     maxReverseSpeed = -30.0f,
                     maxAirSpeed,
                     gravity = 0.25f,
                     rotationSpeed = 100.0f,
                     jumpForce,
                     currentSpeed;

    [HideInInspector]
    public GroundCheck groundCheck;
    [HideInInspector]
    public Rigidbody rigidbody;

    public Vector2 rotation;

    [HideInInspector] public Quaternion PhysicsRotation;
    [HideInInspector] public Quaternion VelocityRotation;
    [HideInInspector] public Quaternion InputRotation;
    [HideInInspector] public Quaternion ComputedRotation;

    public GameObject frontWheels, backWheels;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = gameObject.GetComponent<GroundCheck>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        currentSpeed = 0;
    }

    private void Update()
    {
        if (!GameManager.instance.gameIsPaused)
        {

        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.gameIsPaused)
        {
            //CheckPhysics();
            //rigidbody.velocity = transform.forward.normalized * currentSpeed;
            //SkaterMove(rotation);
            //Debug.DrawRay(transform.position, transform.up, Color.cyan);


            rigidbody.velocity += transform.forward * currentSpeed * Time.deltaTime;
            rigidbody.velocity = UpdateVelocityDirection();
            rigidbody.velocity += Vector3.down * gravity;
            Friction();
        }
    }
    
    // calculate velocity
    Vector3 UpdateVelocityDirection()
    {
        Vector3 updatedVel = rigidbody.velocity;
        if (GetComponent<PlayerRotation>().airCheck)
        {
            if (currentSpeed >= 0)
            {
                updatedVel = transform.forward.normalized * rigidbody.velocity.magnitude;
                updatedVel = Vector3.ClampMagnitude(updatedVel, maxAirSpeed);
            }
            else
            {
                updatedVel = transform.forward.normalized * -1 * rigidbody.velocity.magnitude;
                updatedVel = Vector3.ClampMagnitude(updatedVel, maxAirSpeed);
            }
        }
        else
        {
            if (currentSpeed >= 0)
            {
                updatedVel = transform.forward.normalized * rigidbody.velocity.magnitude;
                updatedVel = Vector3.ClampMagnitude(updatedVel, maxForwardSpeed);
            }
            else
            {
                updatedVel = transform.forward.normalized * -1 * rigidbody.velocity.magnitude;
                updatedVel = Vector3.ClampMagnitude(updatedVel, maxReverseSpeed);
            }
        }
        return new Vector3(updatedVel.x, rigidbody.velocity.y, updatedVel.z);
    }

    // player is accelerating
    void OnMoveForward()
    {
        currentSpeed += acceleration;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxReverseSpeed, maxForwardSpeed);
        //needs to add force
        //Vector3 Direction = transform.forward.normalized * currentSpeed;
        //rigidbody.AddForce(Direction, ForceMode.VelocityChange);
    }

    // player is braking
    void OnBrake()
    {
        currentSpeed -= braking;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxReverseSpeed, maxForwardSpeed);
        //needs to add force
        //Vector3 Direction = InputRotation * transform.forward.normalized * currentSpeed;
        //rigidbody.AddForce(-Direction, ForceMode.VelocityChange);
        //rigidbody.velocity += Direction * Time.deltaTime;
    }

    // player friction
    void Friction()
    {
        if (currentSpeed > 0)
        {
            currentSpeed -= friction;
            Mathf.Clamp(currentSpeed, 0, maxForwardSpeed);
        }
        else if (currentSpeed < 0)
        {
            currentSpeed += friction;
            Mathf.Clamp(currentSpeed, -maxReverseSpeed, 0);
        }
    }



    public void Jump(float forceMultiplier)
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce * forceMultiplier, 0), ForceMode.Acceleration);

    }



}
