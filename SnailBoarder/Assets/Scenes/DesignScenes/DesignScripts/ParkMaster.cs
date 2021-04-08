using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperiorArrays;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class ParkMaster : MonoBehaviour
{
    public static GameObject seedInfo;
    public GameObject highway;
    private GameObject[][] parkLayoutHighway;

    public GameObject[] publicTest;



    private GameObject timerObject;


    public string nextScene;

    public PlayerInput maps;

    public float levelLength;

    public GameObject snail;

  //  public GameObject hatPoint;
  //  public GameManager[] hats;

    private void Awake()
    {
       

    }

    // Start is called before the first frame update
    void Start()
    {

        EnterCosmetics();

        SetLevelParameter(0);
        try
        {
            timerObject = GameObject.Find("UI_Main");
            timerObject.GetComponent<SkateTimer>().ActivateTime();
            timerObject.GetComponent<SkateTimer>().shown = true;

        }
        catch
        {
        }

        AssignSlots(highway);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            CheckTimer();
        }
        catch
        {

        }
        ForceTransition();
    }




    private void EnterCosmetics()
    {
        maps.actions.FindActionMap("Player").Disable();
        maps.actions.FindActionMap("CosmeticSelect").Enable();
        snail.gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }







    private void ForceTransition()
    {



    }


    private void CheckTimer()
    {

        if (Time.time - timerObject.GetComponent<SkateTimer>().skateStart > levelLength)
        {
            timerObject = GameObject.Find("UI_Main");
            timerObject.GetComponent<SkateTimer>().activeState=false;
            timerObject.GetComponent<SkateTimer>().shown = false;
            CallScene();


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
                tempArray = Sray.AppendGameObjectArray(tempArray, section.transform.GetChild(i).gameObject);
            //}
            print(tempArray[i]);





        }
        
     //   parkLayoutHighway[0][] = tempArray;
        publicTest = tempArray;
    }





    public void SetLevelParameter(int type)
    {



    }



}
