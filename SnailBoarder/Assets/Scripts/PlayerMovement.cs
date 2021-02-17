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
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * currentSpeed * playerTurn, Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * currentSpeed * playerTurn, Space.World);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //add to player speed
            currentSpeed += speedPerFrame;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
            //add acceleration force here?
            //playerRigidbody.AddForce(gameObject.transform.forward * currentSpeed, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //subtract from player speed
            currentSpeed -= playerBrake;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
            //Add decceleration force here?
            //playerRigidbody.AddForce(-gameObject.transform.forward * playerBrake, ForceMode.VelocityChange);
        }
        playerRigidbody.AddForce(gameObject.transform.forward * currentSpeed, ForceMode.Acceleration);
        currentSpeed -= playerFric; //friction
        currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
        //add friction force here?
    }

    //rotation
}
