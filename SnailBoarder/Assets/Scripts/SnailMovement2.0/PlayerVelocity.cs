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
                     gravity = 0.5f,
                     rotationSpeed = 100.0f,
                     currentSpeed;

    [HideInInspector]
    public GroundCheck groundCheck;
    [HideInInspector]
    public Rigidbody rigidbody;

    public Vector2 inputRotation;

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
            CheckPhysics();
            SkaterMove(inputRotation);
            Debug.DrawRay(transform.position, transform.up, Color.cyan);
        }
    }

    // player is accelerating
    void OnMoveForward()
    {
        currentSpeed += acceleration;
        //needs to add force
        Vector3 Direction = transform.forward.normalized * currentSpeed;
        rigidbody.AddForce(Direction, ForceMode.VelocityChange);
    }

    // player is braking
    void OnBrake()
    {
        currentSpeed -= braking;
        //needs to add force
        Vector3 Direction = InputRotation * transform.forward.normalized * currentSpeed;
        rigidbody.AddForce(-Direction, ForceMode.VelocityChange);
    }

    void OnTurn(InputValue rot)
    {
        inputRotation = rot.Get<Vector2>();
    }

    void CheckPhysics()
    {
        if (!groundCheck.isGrounded)
        {
            rigidbody.velocity += Vector3.down * gravity;
        }
    }

    void SkaterMove(Vector2 inputs)
    {

        PhysicsRotation = !groundCheck.isGrounded ? Quaternion.identity : GetPhysicsRotation(); // Rotation according to ground normal 
        VelocityRotation = GetVelocityRot();
        InputRotation = Quaternion.identity;
        ComputedRotation = Quaternion.identity;


        if (inputs.magnitude > 0.1f)
        {
            Vector3 adapted_direction = ProcessInput(inputs);
            Vector3 planar_direction = transform.forward;
            planar_direction.y = 0;
            InputRotation = Quaternion.FromToRotation(planar_direction, adapted_direction);

            if (groundCheck.isGrounded)
            {
                Vector3 Direction = InputRotation * transform.forward.normalized * currentSpeed;
                //rigidbody.AddForce(Direction, ForceMode.VelocityChange);
                /* NEXT STEPS:
                    - Make this rotate the snail
                    - Add a buffer to get velocity rot
                    - profit and/or cry             */
            }
        }

        ComputedRotation = PhysicsRotation * VelocityRotation * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, ComputedRotation, rotationSpeed * Time.deltaTime);
    }

    Quaternion GetVelocityRot()
    {
        Vector3 vel = rigidbody.velocity;
        if (vel.magnitude > 0.2f)
        {
            vel.y = 0;
            Vector3 dir = transform.forward;
            dir.y = 0;
            Quaternion vel_rot = Quaternion.FromToRotation(dir.normalized, vel.normalized);
            return vel_rot;
        }
        else
            return Quaternion.identity;
    }

    Vector3 ProcessInput(Vector2 d)
    {
        inputRotation = Vector2.zero;
        Vector3 player = transform.position;
        player.y = 0;

        Vector3 player_right = Quaternion.AngleAxis(90, Vector3.up) * player;

        Vector3 direction = player * d.y + player_right * d.x;
        return direction.normalized;
    }

    Quaternion GetPhysicsRotation()
    {
        Vector3 target_vec = Vector3.up;
        Ray ray = new Ray(transform.position, Vector3.down);
        Ray rayFront = new Ray(frontWheels.transform.position, Vector3.down);
        Ray rayBack = new Ray(backWheels.transform.position, Vector3.down);
        RaycastHit hit, hitFront, hitBack;

        if (Physics.Raycast(rayFront, out hitFront, 1.05f) && Physics.Raycast(rayBack, out hitBack, 1.05f))
        {
            target_vec = hitFront.normal + hitBack.normal;
            //target_vec.y = 0;
            // instead I need to get the normals at each wheel

            if ((hitFront.normal - hitBack.normal).magnitude <= 0.5f)
            {
                if (Physics.Raycast(ray, out hit, 1.05f))
                    target_vec = hit.normal;
                else
                    target_vec = Vector3.up;
            }
        }

        return Quaternion.FromToRotation(transform.up, target_vec);
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
