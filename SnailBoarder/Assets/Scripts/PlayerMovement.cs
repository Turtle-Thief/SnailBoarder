﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerMaxSpeed = 50.0f, speedPerFrame = 1.0f, playerAcel = 100f, playerBrake = 1.9f, playerFric = 0.1f, playerTurn = .5f, currentSpeed;

    private Rigidbody playerRigidbody;
    Vector3 moveX, moveZ;

    // Trick triggers stuff
    public float distToGround = 1f;
    public Text debugText;

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
    }

    public void OnMoveForward()
    {
        //add to player speed
        currentSpeed += speedPerFrame;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
        //add acceleration force here?
        //playerRigidbody.AddForce(gameObject.transform.forward * currentSpeed, ForceMode.Acceleration);
    }

    public void OnBrake()
    {
        //subtract from player speed
        currentSpeed -= playerBrake;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, playerMaxSpeed);
        //Add decceleration force here?
        //playerRigidbody.AddForce(-gameObject.transform.forward * playerBrake, ForceMode.VelocityChange);
    }

    public void OnTurn(InputValue value)
    {
        Vector2 val = value.Get<Vector2>();

        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * currentSpeed * playerTurn * val.y, Space.World);
    }

    public void OnJump()
    {

    }


    public bool isGrounded = false;

    void GroundCheck()
    {
        // Tricks triggers stuff
        if (Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f))
        {
            debugText.text = "Grounded";
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