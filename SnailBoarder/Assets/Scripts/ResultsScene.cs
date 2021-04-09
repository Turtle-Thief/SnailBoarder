using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsScene : MonoBehaviour
{
    public TextMeshProUGUI resultsText;

    public void OnRetry()
    {
        SceneLoader.instance.LoadScene("title");
    }

    public void OnContinue()
    {
        SceneLoader.instance.LoadScene("nextLevel");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.completedLastLevel)
        {
            resultsText.text = "You scored " + GameManager.instance.score + " points!";
        }
        else
        {
            resultsText.text = "You needed " + GameManager.instance.neededPoints + " more points";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
