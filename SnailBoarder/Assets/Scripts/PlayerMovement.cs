using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMaxSpeed = 50.0f, speedPerFrame = 1.0f, playerAcel = 100f, playerBrake = 1.9f, playerFric = 0.1f, playerTurn = .5f, currentSpeed;

    private Rigidbody playerRigidbody;
    Vector3 moveX, moveZ;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        currentSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //add to player speed
            currentSpeed += speedPerFrame;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //subtract from player speed
            currentSpeed -= playerBrake;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
        }
        playerRigidbody.AddForce(gameObject.transform.forward * currentSpeed, ForceMode.Acceleration);
        currentSpeed -= playerFric;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);

        //Friction();
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Move();
        //}
        //else if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    Break();
        //}
        //currentSpeed = playerRigidbody.velocity.magnitude;
    }

    void Move()
    {
        if (currentSpeed < playerMaxSpeed)
        {
            Vector3 force = gameObject.transform.forward * playerAcel;
            playerRigidbody.AddForce(force);
        }
    }

    void Break()
    {
        if (currentSpeed > 0)
        {
            playerRigidbody.AddForce(gameObject.transform.forward * -playerBrake * Time.deltaTime);
        }
    }

    void Friction()
    {
        playerRigidbody.AddForce(gameObject.transform.forward * -playerFric * Time.deltaTime);
    }
}
