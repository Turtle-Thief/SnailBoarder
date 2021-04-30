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



    //Determines the Amount of Time per Level
    //public float levelLength;


    //Determines the Score Needed for Victory
    //public float scoreNeeded;



    // This the Snail
    public GameObject snail;








  //  public GameObject hatPoint;
  //  public GameManager[] hats;

    private void Awake()
    {
       

    }

    // Start is called before the first frame update
    void Start()
    {

        //EnterCosmetics();

        SetLevelParameter(0);
        try
        {

            


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
        }
        catch
        {

        }
        
    }




    private void EnterCosmetics()
    {
        //maps.actions.FindActionMap("Player").Disable();
        //GameManager.instance.PauseGame(false);
        maps.actions.FindActionMap("CosmeticSelect").Enable();
        //snail.gameObject.transform.GetChild(2).gameObject.SetActive(true);   // FIX PLS
    }










    private void CallScene()
    {

        //if statement to check score

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
          //  print(tempArray[i]);





        }
        
     //   parkLayoutHighway[0][] = tempArray;
        publicTest = tempArray;
    }





    public void SetLevelParameter(int type)
    {



    }



}
