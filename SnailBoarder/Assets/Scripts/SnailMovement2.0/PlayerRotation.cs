using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerRotation : MonoBehaviour
{

    //X Axis Rays
    public GameObject pointRayFront;
    public GameObject pointRayBack;


    public GameObject visualPointFront;
    public GameObject visualPointBack;


    //Y Axis Rays

    public GameObject pointRayLeft;
    public GameObject pointRayRight;


    public GameObject visualPointLeft;
    public GameObject visualPointRight;


    public GameObject snailBody;


    public float rotSpeed;

    public float yRot;

    public LayerMask mask;

    public float timeStart;


    public bool airCheck;
    public bool airRecovered;
    public Vector3 preAirPosition;

    public float refreshTime;



    // Start is called before the first frame update
    void Start()
    {
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {


        refreshTime -= Time.deltaTime;
    }


    private void FixedUpdate()
    {


        //Debug Visual of Where Rays hit
        visualPointFront.transform.position = CastRayDown(pointRayFront.transform);
        visualPointBack.transform.position = CastRayDown(pointRayBack.transform);
        visualPointLeft.transform.position = CastRayDown(pointRayLeft.transform);
        visualPointRight.transform.position = CastRayDown(pointRayRight.transform);



        //Apply XZ angles to snail
        Vector3 groundAngle = ApplyAngle(GetAngle(CastRayDown(pointRayFront.transform), CastRayDown(pointRayBack.transform)), snailBody.transform.position.y, GetAngle(CastRayDown(pointRayLeft.transform), CastRayDown(pointRayRight.transform)));
        Vector3 airAngle = new Vector3(0, snailBody.transform.rotation.y, snailBody.transform.rotation.z);
        ApplyAirRotation(airCheck, GetComponent<GroundCheck>().isGrounded, groundAngle, airAngle);
        //Applying Y rotation to overall snail

        ApplyTurn(yRot);


    }



    public void OnTurn(InputValue value)
    {

        Vector2 val = value.Get<Vector2>();


            yRot = yRot + (val.x * Time.deltaTime * rotSpeed);

       // yRot = val;



    }


    //Determining if ground rotation or air rotation is used and applying it
    public void ApplyAirRotation(bool airCheck, bool groundCheck, Vector3 groundRotation, Vector3 airRotation)
    {
        Vector3 destination;
        if (airCheck && refreshTime<0)
        {
            
            airRecovered = true;
            preAirPosition = transform.position;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            destination = groundRotation;
            GetComponent<TricksController>().OnMcTwist();
         //   destination = new Vector3(0, yRot, 0);
        }
        else
        {
            if (airRecovered)
            {
                refreshTime = 2f;
                airRecovered = false;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                transform.position = preAirPosition;

                if (Mathf.Abs(yRot % 360) > 180)
                {
                    yRot -= 180f;
                }
                else
                {
                    yRot -= 90f;
                }
                snailBody.transform.localEulerAngles = new Vector3(-snailBody.transform.localEulerAngles.x, 0, -snailBody.transform.localEulerAngles.z);
            }

            transform.localEulerAngles = new Vector3(0, yRot, 0);
            if (groundCheck)
            {
                destination = groundRotation;
            }
            else
            {
                destination = snailBody.transform.localEulerAngles;

            }
        }
        


        snailBody.transform.localEulerAngles = Vector3.Lerp(snailBody.transform.localEulerAngles,destination,(Time.time - timeStart));

    }



    //Creating Ground Angle Rotation
    public void ApplyTurn(float rotation)
    {
        Vector3 originRot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(originRot.x, rotation, originRot.z);


    }




    //Applys all gathered angle data
    public Vector3 ApplyAngle(float xAngle,float yAngle, float zAngle)
    {

        Vector3 angle = new Vector3(xAngle * 45f, yAngle, zAngle * 45f);

        return angle;
    }


    //3D pythagoras calculation
    public float GetAngle(Vector3 pointA, Vector3 pointB)
    {



        //Getting Line lengths
        float rise = pointB.y - pointA.y;
        float run = pointB.x - pointA.x;
        float rose = pointB.z - pointA.z;

        // c^2 = a^2 * b^2 * d^2
        float lineC = Mathf.Sqrt(rise * rise + run * run + rose * rose);
        float theta = Mathf.Asin(rise / lineC);
  //      print("Rise(" + rise + ") Run(" + run + ") Theta(" + theta + ")");
        return theta;
    }


    //Casts a ray straight down
    public Vector3 CastRayDown(Transform point)
    {
        RaycastHit hit;
        Physics.Raycast(point.position, Vector3.down, out hit, 200f,mask);
        return hit.point;
    }






}
