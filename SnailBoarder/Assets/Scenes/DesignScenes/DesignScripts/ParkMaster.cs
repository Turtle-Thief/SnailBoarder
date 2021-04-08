using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperiorArrays;
using UnityEngine.SceneManagement;
public class ParkMaster : MonoBehaviour
{
    public static GameObject seedInfo;
    public GameObject highway;
    private GameObject[][] parkLayoutHighway;


    private GameObject timerObject;


    public string nextScene;



    public float levelLength;


    private void Awake()
    {
       

    }

    // Start is called before the first frame update
    void Start()
    {

        SetLevelParameter(0);

        timerObject = GameObject.Find("UI_Main");
        timerObject.GetComponent<SkateTimer>().ActivateTime();
        AssignSlots(highway);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimer();
        ForceTransition();
    }


    private void ForceTransition()
    {



    }


    private void CheckTimer()
    {

        if (Time.time - timerObject.GetComponent<SkateTimer>().skateStart > levelLength)
        {

            //CallScene();


        }
    }

    private void CallScene()
    {
        SceneManager.LoadScene(nextScene);

    }

    public void AssignSlots(GameObject section)
    {
        GameObject[] tempArray = new GameObject[0];
        for (int i = 0; i < section.transform.childCount; i++)
        {


          //  for (int j = 0; j < length; j++)
          //  {
            //    Sray.AppendGameObjectArray(tempArray, section.transform.GetChild(i).gameObject);
            //}

            

            


        }

    }





    public void SetLevelParameter(int type)
    {



    }



}
