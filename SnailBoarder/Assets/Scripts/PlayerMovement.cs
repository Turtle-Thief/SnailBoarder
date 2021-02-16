using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerMaxSpeed = 5.0f, playerAcel = 1.2f, playerBrake = 0.9f, playerFric = 0.5f, playerTurn = .5f, currentSpeed;
    Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Friction();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift");
            Break();
        }
        currentSpeed = playerRigidbody.
    }

    void Move()
    {
        if (currentSpeed < playerMaxSpeed)
        {
            playerRigidbody.AddForce(gameObject.transform.forward * playerAcel * Time.deltaTime);
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
