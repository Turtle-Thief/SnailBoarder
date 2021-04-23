using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isOnRamp = false;

    public LayerMask IgnoreGroundCheckLayer;
    public float distToGround = 3f;
    float slopeAngle;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GroundCheckFunction();
    }

    void GroundCheckFunction()
    {
        Debug.DrawRay(transform.position + new Vector3(0.0f, 1.4f, 0.0f), (Vector3.down * 1.5f - transform.up).normalized * distToGround, Color.blue);

        // Tricks triggers stuff
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position + new Vector3(0.0f, 1.4f, 0.0f), (Vector3.down.normalized * 2 - transform.up.normalized).normalized, out hit, distToGround, ~IgnoreGroundCheckLayer))
        {
            //Debug.Log("Grounded on " + hit.transform.name);

            slopeAngle = Vector3.Angle(hit.normal, transform.forward) - 90;
            // normalisedSlope = (slopeAngle / 90f) * -1f;
            //if (debugText)
            //{
            //    debugText.text = "Grounded on " + hit.transform.name;
            //    debugText.text += "\nSlope Angle: " + slopeAngle.ToString("N0") + "°";
            //}
            if (hit.transform.gameObject.layer == 10) // Is ramp??
            {
                isOnRamp = true;
                //Debug.Log("ramp");
            }
            else
            {
                //Debug.Log("Grounded on " + hit.transform.name);
                isOnRamp = false;
            }
            isGrounded = true;
        }
        else
        {
            //if (debugText)
            //    debugText.text = "Not Grounded";
            isGrounded = false;
        }

        if (GetComponent<PlayerRotation>().airCheck)
        {
            isGrounded = false;
        }


    }
}
