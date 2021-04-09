using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    // should be accessible to any class, using a static instance is also a potential way to implement this
    public bool gameIsPaused = false, onPausableScene = false, completedLastLevel = false;

    public int score, neededPoints;
    public InputActionAsset secondaryInputs;

    private UIManager UM;

    private GameObject debugPanel, UICanvas;
    private TextMeshProUGUI scoreText;

    private InputActionMap cheats;

    #region Cheats
    /*** TESTING PURPOSES ONLY, SHOULD BE DELETED FOR ALPHA/BETA ***/
    private void OnResetLevel()
    {
        SceneLoader.instance.ResetScene();
    }

    // This is a test function for testing anything you need with input.
    //  Not sure if some logic will pan out? Want to see if button presses are
    //  working? Need something to Debug? You can put it here
    //
    //  Press 'J' to activate this function during runtime
    private void OnTest()
    {
        //Debug.Log("pre coroutine");

        scoreText.CrossFadeAlpha(0, 2f, false);
        //StartCoroutine(TestCoroutine());
        //Debug.Log("past coroutine");
    }

    // For use with OnTest
    IEnumerator TestCoroutine()
    {
        //Debug.Log("Made it");
        // Testing alpha values
        scoreText.CrossFadeAlpha(0, 2f, false);
        yield return new WaitForSeconds(2f);
        //scoreText.CrossFadeAlpha(1, 5f, false);
    }
    #endregion
    #region Secondary Input Methods
    private void OnPause()
    {
        if (onPausableScene)
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
        if (UM.previousExists)
        {
            UM.ClosePanel(UM.currentPanel, true);
        }
        else
        {
            if (!onPausableScene)
            {
                UM.CloseAllPanels(); // Can be changed to ClosePanel()
            }
        }
    }
    #endregion

    public void GetScoreDifference()
    {
        neededPoints = UIManager.instance.SM.ScoreDifference();
    }

    public void OnNextLevel()
    {
        UIManager.instance.ClearAfterTrickHUD(); // reset
        score = neededPoints = 0; // reset

        onPausableScene = true;
        // reset score
        UIManager.instance.OpenPanel(UIManager.instance.HUDPanel);
        // Move this line to Park Manager, call Park Manager function here
        UIManager.instance.timer.GetComponent<Timer>().SetAndStartTimer(30);
    }

    public void OnFinishedLevel()
    {
        onPausableScene = false;
        gameIsPaused = false; // this may be redundant
        GetScoreDifference();

        UIManager.instance.ClosePanel(UM.HUDPanel, false);
        UIManager.instance.timer.GetComponent<Timer>().timerIsRunning = false;
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
    private void FindAndSetInputs()
    {
        // Enables all of our maps, included our cheats
        foreach (InputActionMap map in secondaryInputs.actionMaps)
        {
            map.Enable();
        }

        // Enable cheats -- use this to disable them later
        cheats = secondaryInputs.FindActionMap("Cheats");
        cheats.Enable();

        secondaryInputs.FindAction("Pause").performed += ctx => OnPause();
        secondaryInputs.FindAction("Previous").performed += ctx => OnPrevious();
        secondaryInputs.FindAction("Test").performed += ctx => OnTest();
        secondaryInputs.FindAction("ResetLevel").performed += ctx => OnResetLevel();
    }

    private void Awake()
    {
        // Older way to setup singletons
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        //if (!SceneLoader.instance.gameObject || !UIManager.instance.gameObject)
        //{
        //    Debug.Log("SceneLoader or UIManager is missing!!!");
        //}
    }


    // Start is called before the first frame update
    void Start()
    {
        FindAndSetInputs(); // This just enables cheats right now

        UICanvas = GameObject.Find("UI_Main"); // Might be more efficient to search for object on UI layer
        UM = UIManager.instance;

        if(UICanvas)
        {
            debugPanel = UICanvas.transform.GetChild(0).gameObject; // Gets the debug panel
            scoreText = debugPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(); // this is terrible please don't replicate this
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
