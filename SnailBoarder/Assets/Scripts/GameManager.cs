using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public InputActionAsset playerInputs, secondaryInputs;

    #region Hidden From Inspector Public Variables
    [HideInInspector]
    // should be accessible to any class, using a static instance is also a potential way to implement this
    public bool gameIsPaused = false, onPausableScene = false, completedLastLevel = false, hatted = false;
    //[HideInInspector]
    public int score, neededPoints, hatIndex;
    [HideInInspector]
    public ZoneStyle[] judgesPreferences = new ZoneStyle[3];
    [HideInInspector]
    public ZoneStyle currentStyle;
    #endregion
    #region Private Variables
    [SerializeField]
    private Difficulty[] difficulties = new Difficulty[numOfDifficulties];

    private const int numOfDifficulties = 3;
    [SerializeField]
    private int selDif;
    [SerializeField]
    private Difficulty selectedDifficulty;
    private bool testBool, sanic = false;

    private UIManager UM;
    private AudioManager AM;

    private GameObject debugPanel, UICanvas;
    private TextMeshProUGUI scoreText;

    private InputActionMap cheats;
    #endregion

    public enum ZoneStyle // your custom enumeration
    {
        Metal,
        Punk,
        Cute,
        Cool,
        Snail,
        Classic,
        None
    };

    [Serializable]
    public struct Difficulty
    {
        [SerializeField] string name;
        [SerializeField] int scoreNeeded;
        [SerializeField] int timeGiven;

        public Difficulty(string name, int score, int time)
        {
            this.name = name;
            this.scoreNeeded = score;
            this.timeGiven = time;
        }

        //public static implicit operator Difficulty(string name, int score, int time)
        //{
        //    return new Difficulty(name, score, time);
        //s}

        public int getScore() { return scoreNeeded; }
        public int getTime() { return timeGiven; }
    }

    #region Cheats
    /*** TESTING PURPOSES ONLY, SHOULD BE DELETED / DISABLED PERMANENTLY FOR ALPHA/BETA ***/

    private void OnEnableCheats()
    {
        // Enable cheats
        cheats.Enable();

        secondaryInputs.FindAction("Test").performed += ctx => OnTest();
        secondaryInputs.FindAction("SpeedUpTime").performed += ctx => OnSpeedUpTime();
    }

    private void OnSpeedUpTime()
    {
        sanic = true;
    }

    // This is a test function for testing anything you need with input.
    //  Not sure if some logic will pan out? Want to see if button presses are
    //  working? Need something to Debug? You can put it here
    //
    //  Press 'J' to activate this function during runtime
    private void OnTest()
    {
        testBool = !testBool;
        Debug.Log("Testbool in function = " + testBool);
        //Debug.Log("pre coroutine");

        //scoreText.CrossFadeAlpha(0, 2f, false);
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
                PauseGame(true);
            else
                ResumeGame();

            ////////////////////////////////Debug.Log(gameIsPaused);
        }
    }
    private void OnPrevious()
    {
        // Go to previous UI menus / close menus
        //  may be used for other functionality later
        if(!onPausableScene) // If we're on Title Screen or anywhere but game
        {
            if (UM.previousExists) // And there's a previous panel
            {
                UM.ClosePanel(UM.currentPanel, true); // Close that panel and return to the original panel
            }
            else // If there is no previous panel
            {
                UM.CloseAllPanels();
            }
        }
        else // We're in the game, and can pause
        {
            if(gameIsPaused && UM.currentPanel != UM.pausePanel)
            {
                if (UM.previousExists) // And there's a previous panel
                {
                    UM.ClosePanel(UM.currentPanel, true); // Close that panel and return to the original panel
                }
                else // If there is no previous panel
                {
                    UM.CloseAllPanels();
                }
            }
            else
            {

            }
        }
    }
    #endregion
    
    private void FindAndSetInputs()
    {
        // Enables all of our maps, included our cheats
        foreach (InputActionMap map in secondaryInputs.actionMaps)
        {
            map.Enable();
        }

        cheats = secondaryInputs.FindActionMap("Cheats");
        cheats.Disable();


        secondaryInputs.FindAction("Pause").performed += ctx => OnPause();
        secondaryInputs.FindAction("Previous").performed += ctx => OnPrevious();
        secondaryInputs.FindAction("ResetLevel").performed += ctx => OnResetLevel();

        secondaryInputs.FindAction("EnableCheats").performed += ctx => OnEnableCheats();
    }

    private void OnResetLevel()
    {
        if (onPausableScene)
        {
            SceneLoader.instance.ResetScene();
        }
    }

    public bool IsMultipliedByJudges()
    {
        if (currentStyle == judgesPreferences[0] || currentStyle == judgesPreferences[1] || currentStyle == judgesPreferences[2])
        {
            //Debug.Log("Multiplied");
            return true;
        }

        
        return false;
    }

    public void RandomizePreferences()
    {
        List<ZoneStyle> chooseList = Enum.GetValues(typeof(ZoneStyle)).Cast<ZoneStyle>().ToList();
        chooseList.RemoveAt(chooseList.Count-1); // Remove "none", judges will always have a preference

        for (int i = 0; i < judgesPreferences.Length; i++)
        {
            int index = UnityEngine.Random.Range(0, chooseList.Count);
            ZoneStyle choice = chooseList[index];
            judgesPreferences[i] = choice;
            chooseList.RemoveAt(index);
        }
        chooseList.Clear();
    }

    public void SetDifficultToCurrent()
    {
        UM.UpdateScoreDifficulty(selectedDifficulty);
    }

    public Difficulty GetDifficulty()
    {
        Debug.Log("Testing the retrieve: " + difficulties[selDif].getScore());
        return difficulties[selDif];
    }

    public void SetDifficulty(int difficultyChoice)
    {
        //selDif = difficultyChoice;
        //Debug.Log("Dif choice = " + difficultyChoice + "; SelDif = " + selDif);
        selectedDifficulty = difficulties[difficultyChoice];
        neededPoints = selectedDifficulty.getScore();

        UIManager.instance.UpdateScoreDifficulty(selectedDifficulty);
        //Debug.Log(difficulties[difficultyChoice].getScore() + "I was called by the select");
    }

    public void GetScoreDifference()
    {
        neededPoints = UIManager.instance.SM.ScoreDifference();
    }

    public void OnNextLevel()
    {
        UIManager.instance.ClearAfterTrickHUD(); // reset
        score = neededPoints = 0; // reset
        AudioManager.instance.StartLevelAudio();
        //if(hatted)
        //{
        //    GameObject player = GameObject.FindGameObjectWithTag("Player");
        //    player.GetComponent<Player>().GiveHat(hatIndex);
        //}

        onPausableScene = true;
        // reset score
        UIManager.instance.OpenPanel(UIManager.instance.HUDPanel);
        UIManager.instance.UpdateScoreDifficulty(selectedDifficulty);
        UIManager.instance.timer.GetComponent<Timer>().SetAndStartTimer(selectedDifficulty.getTime());
    }

    public void OnFinishedLevel()
    {
        if (sanic)
        {
            sanic = false;
            Time.timeScale = 1;
        }

        AudioManager.instance.EndLevelAudio();

        onPausableScene = false;
        gameIsPaused = false; // this may be redundant
        GetScoreDifference();

        completedLastLevel = UIManager.instance.SM.CheckScoreWin(); // Did the player win?

        UIManager.instance.ClosePanel(UM.HUDPanel, false);
        //UIManager.instance.timer.GetComponent<Timer>().timerIsRunning = false;

        if(completedLastLevel)
        {
            SceneLoader.instance.LoadNextSceneInBuild(); // Progress linearly
        }
        else
        {
            SceneLoader.instance.LoadScene("defeat"); // Load the defeat scene instead
        }
    }

    public void PauseGame(bool openPauseMenu)
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
        gameIsPaused = true;
        playerInputs.FindActionMap("Player").Disable(); // Pause the player's MOVEMENT Inputs

        Time.timeScale = 0;
        AudioListener.pause = true;

        if(openPauseMenu)
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

        playerInputs.FindActionMap("Player").Enable();
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
        //SetDifficulty(0); // Default to easiest difficult

        FindAndSetInputs(); // This just enables cheats right now
        
        onPausableScene = false; // starts on title

        //UICanvas = GameObject.Find("UI_Main"); // Might be more efficient to search for object on UI layer
        UM = UIManager.instance;
        UICanvas = UM.gameObject;

        AM = AudioManager.instance;

        //if(UICanvas)
        //{
        //    debugPanel = UICanvas.transform.GetChild(0).gameObject; // Gets the debug panel
        //    scoreText = debugPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(); // this is terrible please don't replicate this
        //}
    }
    
    // Update is called once per frame
    void Update()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!
            if(sanic)
            {
                Time.timeScale = 100;
            }
        }

        if(testBool)
        {
            Debug.Log("Testbool is active, calling function");
        }

    }
}
