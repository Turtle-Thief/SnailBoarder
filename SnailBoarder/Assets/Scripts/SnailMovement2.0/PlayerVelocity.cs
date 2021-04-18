using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVelocity : MonoBehaviour
{
    public GroundCheck groundCheck;
    public Rigidbody   rigidbody;

    public float acceleration = 50.0f, braking = 10.0f, currentSpeed, friction = 5.0f, maxForwardSpeed = 100.0f, maxReverseSpeed = -30.0f;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = gameObject.GetComponent<GroundCheck>();
        rigidbody   = gameObject.GetComponent<Rigidbody>();
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
            if (groundCheck.isGrounded)
            {
                VelocityUpdate();
            }
        }
    }

    // calculate velocity
    void VelocityUpdate()
    {
        //Mathf.Clamp(currentSpeed, -maxReverseSpeed, maxForwardSpeed);
        Friction();
        //Debug.Log("speed: " + currentSpeed + ", velocity: " + rigidbody.velocity);
        Vector3 changeInSpeed;
        changeInSpeed = (gameObject.transform.forward * currentSpeed) - rigidbody.velocity;
        changeInSpeed.y = 0;
        if (changeInSpeed.magnitude >= 0.2f)
            rigidbody.velocity += changeInSpeed;
        Mathf.Clamp(rigidbody.velocity.magnitude, -maxReverseSpeed, maxForwardSpeed);
    }

    // player is accelerating
    void OnMoveForward()
    {
        currentSpeed += acceleration;
    }

    // player is braking
    void OnBrake()
    {
        currentSpeed -= braking;
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
}
