using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    // should be accessible to any class, using a static instance is also a potential way to implement this
    public bool gameIsPaused = false, onPausableScene = false; 

    public GameObject sceneLoader;
    public int score;

    public UIManager UM;
    
    private GameObject debugPanel, UICanvas;
    private Text scoreText;

    #region Secondary Input Methods
    /*** TESTING PURPOSES ONLY, SHOULD BE DELETED FOR ALPHA/BETA ***/
    private void OnResetLevel()
    {
        SceneLoader.instance.ResetScene();
    }


    private void OnPause()
    {
        if(onPausableScene)
        {
            // This function is called everytime we press the pause button,
            //  so we reverse the bool value each time 
            //  ****** If we don't like Esc being a shortcut to unpause, set this value to true
            gameIsPaused = !gameIsPaused;

            // Only pause the game if isPaused is true
            if (gameIsPaused)
                PauseGame();
            else
                ResumeGame();

            ////////////////////////////////Debug.Log(gameIsPaused);
        }
    }
    private void OnPrevious()
    {
        // Go to previous UI menus / close menus
        //  may be used for other functionality later
        if(UM.previousExists)
        {
            UM.ClosePanel(UM.currentPanel, true);
        }
        else
        {
            if(!onPausableScene)
            {
                UM.CloseAllPanels(); // Can be changed to ClosePanel()
            }
        }
    }
    #endregion

    public void AddToScore(int value)
    {
        score += value;
        if(debugPanel)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void OnNextLevel()
    {
        onPausableScene = true;
    }

    public void OnFinishedLevel()
    {
        onPausableScene = false;
        gameIsPaused = false; // this may be redundant
    }

    public void PauseGame()
    {
        // We want the Pause Screen to appear
        //  but we also want:
        //  - the player not to move,
        //  - any time functions to stop,
        //  - Audio to pause

        // This stops all time-related functions
        //  Update still runs, FixedUpdate does not
        //  (Time.time is stopped so needed functions for time should use Time.realtimeSinceStartup or any "unscaled" Time function)
        //  (For Coroutines use:  WaitForSecondsRealTime)
        //  (For Audio, use AudioSource.ignoreListenerPause = true;)

        Time.timeScale = 0;
        AudioListener.pause = true;

        UM.OpenPanel(UM.pausePanel);
    }

    public void ResumeGame()
    {
        // First check to make sure our game registers as unpaused
        if (gameIsPaused)
            gameIsPaused = false;

        // Then just reverse what we do in PauseGame!
        UM.CloseAllPanels();
        
        Time.timeScale = 1;
        AudioListener.pause = false;
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
        UICanvas = GameObject.Find("UI_Main"); // Might be more efficient to search for object on UI layer
        UM = UICanvas.GetComponent<UIManager>();

        if(UICanvas)
        {
            debugPanel = UICanvas.transform.GetChild(0).gameObject; // Gets the debug panel
            scoreText = debugPanel.transform.GetChild(1).gameObject.GetComponent<Text>(); // this is terrible please don't replicate this
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!

        }
    }
}
