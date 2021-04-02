using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CosmeticMenu : MonoBehaviour
{

    public GameObject[] hats;
    public GameObject hatPoint;
    private GameObject newHat;

    private int hatNum = 0;

    public PlayerInput maps;

    public GameObject CamPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCycle()
    {
        Destroy(newHat);
        hatNum++;
        if(hatNum == hats.Length)
        {
            hatNum = 0;
        }
        newHat = Instantiate(hats[hatNum]);
        newHat.transform.position = hatPoint.transform.position;
        newHat.transform.parent = hatPoint.transform.parent;

    }


    private void OnAccept()
    {

        maps.actions.FindActionMap("CosmeticSelect").Disable();
        maps.actions.FindActionMap("Player").Enable();
        CamPoint.SetActive(false);
        
    }

}
