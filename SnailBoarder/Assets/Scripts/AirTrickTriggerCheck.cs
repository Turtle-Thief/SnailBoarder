using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTrickTriggerCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);


        if (other.gameObject.tag == "AirTrickTrigger")
        {
            //Debug.Log("HERE!!!");
            transform.parent.parent.parent.GetComponentInParent<TricksController>().AirTriggerEnter();
        }
    }
}
