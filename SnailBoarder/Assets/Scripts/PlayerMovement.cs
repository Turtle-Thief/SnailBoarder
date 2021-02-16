using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMaxSpeed = 50.0f, playerSpeedPerFreame = 1.0f, playerAcel = 100f, playerBrake = 0.9f, playerFric = 0.5f, playerTurn = .5f, currentSpeed;

    private Rigidbody playerRigidbody;
    Vector3 moveX, moveZ;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space))
        {
            //add to player speed
        }
         if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //subtract from player speed
        }
    }

    private void FixedUpdate()
    {
        //Friction();
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Break();
        }
        currentSpeed = playerRigidbody.velocity.magnitude;*/
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
