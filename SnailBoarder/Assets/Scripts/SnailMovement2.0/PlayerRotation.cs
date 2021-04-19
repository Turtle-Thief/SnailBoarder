using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerRotation : MonoBehaviour
{


    public GameObject pointRayFront;
    public GameObject pointRayBack;


    public GameObject visualPointFront;
    public GameObject visualPointBack;



    public GameObject snailBody;


    public float rotSpeed;

    private float yRot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     //   print("Front " + CastRayDown(pointRayFront.transform));
     //   print("Back " + CastRayDown(pointRayBack.transform));


        Vector2 frontPoint = new Vector2(CastRayDown(pointRayFront.transform).z, CastRayDown(pointRayFront.transform).y);
        Vector2 backPoint = new Vector2(CastRayDown(pointRayBack.transform).z, CastRayDown(pointRayBack.transform).y);

        //print("Angle" + GetAngle(frontPoint, backPoint));

        visualPointFront.transform.position = CastRayDown(pointRayFront.transform);
        visualPointBack.transform.position = CastRayDown(pointRayBack.transform);




        ApplyAngle(GetAngle(frontPoint, backPoint), yRot, 0);
    }


    public void OnTurn(InputValue value)
    {

        Vector2 val = value.Get<Vector2>();


            yRot = yRot + (val.x * Time.deltaTime * rotSpeed);

       // yRot = val;



    }


    //Applys all gathered angle data
    public void ApplyAngle(float xAngle,float yAngle, float zAngle)
    {
        snailBody.transform.eulerAngles = new Vector3(xAngle *-90f , yAngle, zAngle);
    }


    //Math to get slope of 2 points, if theres already a function dont tell me
    public float GetAngle(Vector2 pointA, Vector2 pointB)
    {
        float rise = pointB.y - pointA.y;
        float run = pointB.x - pointA.x;
        float slope = rise / run;

        slope = (Mathf.Atan(slope)) / (Mathf.PI / 2);

        return slope;
    }


    //Casts a ray straight down
    public Vector3 CastRayDown(Transform point)
    {
        RaycastHit hit;
        Physics.Raycast(point.position, Vector3.down, out hit, 100f);
        return hit.point;
    }






}
