using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
public class TutorialReceiver : MonoBehaviour
{


    public GameObject snail;

    public GameObject canvas;

    private int nextTrick;

    public int[] trickOrder;

    private int trickSlot = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTrick();
    }

    // Update is called once per frame
    void Update()
    {

        if (GetTrick() == nextTrick)
        {


            gameObject.GetComponent<DialogueStructure>().NextTutorialSegment();
            UpdateTrick();
            
        }

        if (snail.GetComponent<ReadTutorialButtons>().receivedInput == 1)
        {



            SceneLoader.instance.LoadScene(0);

        }

        if (snail.GetComponent<ReadTutorialButtons>().receivedInput == 2)
        {

            gameObject.SetActive(false);

           // SceneLoader.instance.LoadScene(0);

        }

    }

    private void UpdateTrick()
    {
        nextTrick = trickOrder[trickSlot];
        trickSlot++;
    }

    private int GetTrick()
    {
        int temp;

        temp = (int)snail.GetComponent<TricksController>().currentTrick.mName;

        print("Get trick int: " + temp);


        return temp;
    }



}
