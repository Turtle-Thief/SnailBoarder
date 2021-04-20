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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  print("Front " + CastRayDown(pointRayFront.transform));
      //  print("Back " + CastRayDown(pointRayBack.transform));

        //Converts Positions into usable Vector 2s
        //X Axis
        Vector2 frontPoint = new Vector2(CastRayDown(pointRayFront.transform).z, CastRayDown(pointRayFront.transform).y);
        Vector2 backPoint = new Vector2(CastRayDown(pointRayBack.transform).z, CastRayDown(pointRayBack.transform).y);

        //Y Axis
        Vector2 leftPoint = new Vector2(CastRayDown(pointRayLeft.transform).x, CastRayDown(pointRayLeft.transform).y);
        Vector2 rightPoint = new Vector2(CastRayDown(pointRayRight.transform).x, CastRayDown(pointRayRight.transform).y);




        //Debug Visual of Where Rays hit
        visualPointFront.transform.position = CastRayDown(pointRayFront.transform);
        visualPointBack.transform.position = CastRayDown(pointRayBack.transform);
        visualPointLeft.transform.position = CastRayDown(pointRayLeft.transform);
        visualPointRight.transform.position = CastRayDown(pointRayRight.transform);

     //   print("Left " + leftPoint);
      //  print("Right " + rightPoint);
      //  print("Front " + frontPoint);
    //    print("Back " + backPoint);
     //   print(GetAngle(leftPoint, rightPoint));

        //Applying Angles to snail model
        ApplyAngle(GetAngle(frontPoint, backPoint), 0, GetAngle(leftPoint, rightPoint));

        //Applying Y rotation to overall snail
        ApplyTurn(yRot);

    }


    public void OnTurn(InputValue value)
    {

        Vector2 val = value.Get<Vector2>();


            yRot = yRot + (val.x * Time.deltaTime * rotSpeed);

       // yRot = val;



    }


    public void ApplyTurn(float rotation)
    {
        Vector3 originRot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(originRot.x, rotation, originRot.z);


    }




    //Applys all gathered angle data
    public void ApplyAngle(float xAngle,float yAngle, float zAngle)
    {
        snailBody.transform.localRotation = Quaternion.Euler(xAngle *-90f , yAngle, zAngle *90f);
    }


    //Math to get slope of 2 points, if theres already a function dont tell me
    public float GetAngle(Vector2 pointA, Vector2 pointB)
    {

        float rise = pointB.y - pointA.y;
        float run = pointB.x - pointA.x;
        if (rise == run)
        {
            print(rise);
            print(run);
            return 0;
        }
        float slope = rise / run;

        slope = (Mathf.Atan(slope)) / (Mathf.PI / 2);

        return slope;
    }


    //Casts a ray straight down
    public Vector3 CastRayDown(Transform point)
    {
        RaycastHit hit;
        Physics.Raycast(point.position, Vector3.down, out hit, 200f);
        return hit.point;
    }






}
