using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SkateTimer : MonoBehaviour
{

    public float skateStart;
    public TMP_Text timeDisplay;
    public bool activeState;

    public bool shown;

    // Start is called before the first frame update
    void Start()
    {
        skateStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (shown)
        {
            DisplayTime();
        }
        else
        {
            HideTime();
        }
    }



    public void ActivateTime()
    {
        activeState = true;
        skateStart = Time.time;

    }

    private void DisplayTime()
    {
        if (activeState)
        {
            timeDisplay.text = "" + (Time.time - skateStart);
        }
    }
    
    private void HideTime()
    {


        timeDisplay.text = "";
    }
}
