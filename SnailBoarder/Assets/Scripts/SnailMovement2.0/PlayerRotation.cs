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
        //   print("Right " + rightPoint);
        //   print("Front " + frontPoint);
        //  print("Back " + backPoint);
        //    print("Normalized Angle " + GetAngle(leftPoint, rightPoint));
        //     print("Normalized Angle * 90 " + GetAngle(leftPoint, rightPoint)*90f);

        //Applying Angles to snail model
       // ApplyAngle(GetAngle(frontPoint, backPoint), 0, GetAngle(leftPoint, rightPoint));
      //  ApplyAngle(0, 0, GetAngle(leftPoint, rightPoint));
        ApplyAngle(GetAngle(CastRayDown(pointRayFront.transform), CastRayDown(pointRayBack.transform)), snailBody.transform.position.y ,GetAngle(CastRayDown(pointRayLeft.transform), CastRayDown(pointRayRight.transform)));

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
        snailBody.transform.localEulerAngles = new Vector3(xAngle *45f, yAngle, zAngle * 45f);
    }


    //Math to get slope of 2 points, if theres already a function dont tell me
    public float GetAngle(Vector3 pointA, Vector3 pointB)
    {

      //  Vector3 pointC = new Vector3(pointB.x, pointA.y, pointB.z);


        float rise = pointB.y - pointA.y;
        float run = pointB.x - pointA.x;
        float rose = pointB.z - pointA.z;
        if (rise == run)
        {
            print(rise);
            print(run);
            return 0;
        }



        float lineC = Mathf.Sqrt(rise * rise + run * run + rose * rose);
        float slope = rise / run;
        //   slope = Mathf.Abs(slope);
        
       // print(slope);
        float theta = Mathf.Asin(rise / lineC);
       // Mathf.sin
        slope = (Mathf.Atan(slope)) / (Mathf.PI / 2);
        print("Rise(" + rise + ") Run(" + run + ") Theta(" + theta + ")");
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
