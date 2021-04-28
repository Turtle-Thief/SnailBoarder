using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PathCreation.Examples
{
    public class RailGrindTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("Rail Trigger");
            if (other.gameObject.tag.Equals("RailgrindTrigger"))
            {
                transform.parent.parent.parent.GetComponentInParent<SnailPathFollower>().pathCreator = gameObject.GetComponent<PathCreator>();
                transform.parent.parent.parent.GetComponentInParent<TricksController>().doRailGrind = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Debug.Log("Rail Un-Trigger");
            if (other.gameObject.tag.Equals("RailgrindTrigger"))
            {
                if (other.gameObject.GetComponent<SnailPathFollower>().pathCreator != null)
                {
                    transform.parent.parent.parent.GetComponentInParent<SnailPathFollower>().pathCreator = null;
                }
                transform.parent.parent.parent.GetComponentInParent<TricksController>().doRailGrind = true;
            }
        }
    }
}
