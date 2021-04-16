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
            Debug.Log("Rail Trigger");
            if (other.gameObject.tag.Equals("Player"))
            {
                other.gameObject.GetComponent<SnailPathFollower>().pathCreator = gameObject.GetComponent<PathCreator>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Rail Un-Trigger");
            if (other.gameObject.tag.Equals("Player"))
            {
                if (other.gameObject.GetComponent<SnailPathFollower>().pathCreator != null)
                {
                    other.gameObject.GetComponent<SnailPathFollower>().pathCreator = null;
                }
            }
        }
    }
}
