using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject sceneLoader;
    public int score;

    private Canvas currentScreen;
    private GameObject debugPanel;
    private Text scoreText;


    public void AddToScore(int value)
    {
        score += value;
        if(debugPanel)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void Awake()
    {
        // Older way to setup singletons
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if(!sceneLoader)
        {
            sceneLoader = SceneLoader.instance.gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScreen = GameObject.FindObjectOfType<Canvas>();
        if(currentScreen)
        {
            debugPanel = currentScreen.transform.GetChild(0).gameObject; // Gets the debug panel
            scoreText = debugPanel.transform.GetChild(1).gameObject.GetComponent<Text>(); // this is terrible please don't replicate this
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!currentScreen)
        {
            // If / when we have a Scene Manager, the 
            //  current Canvas object should be reassigned
            //  in our scene change method
            currentScreen = FindObjectOfType<Canvas>();
        }
    }
}
