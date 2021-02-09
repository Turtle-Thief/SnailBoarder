using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStructure : MonoBehaviour
{

    public Vector3 highPoint;
    public Vector3 lowPoint;
    public float height;

    public bool stateChange;
    private bool state;
    private float startTime;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        highPoint = new Vector3(transform.position.x, transform.position.y+height, transform.position.z);
        lowPoint = transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        CheckState();
        MovePlat();

        


    }



    void CheckState()
    {
        if (stateChange)
        {
            stateChange = false;
            state = !state;
            startTime = Time.time;
        }
    }


    void MovePlat()
    {
        if (state) {
            transform.position = Vector3.Lerp(transform.position, lowPoint, (Time.time - startTime) / speed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, highPoint, (Time.time - startTime) / speed);
        }


    }

}
