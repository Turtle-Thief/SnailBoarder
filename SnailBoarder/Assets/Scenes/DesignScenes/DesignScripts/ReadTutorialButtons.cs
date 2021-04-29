using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadTutorialButtons : MonoBehaviour
{

    public int receivedInput;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTutorialExit()
    {

        if (receivedInput == 0)
        {
            receivedInput = 1;
        }

    }

    private void OnTutorialEnter()
    {

        if (receivedInput == 0)
        {
            receivedInput = 2;
        }

    }


}
