using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Space]
    public static UIManager instance = null;
    // Panel attributes
    public GameObject
        timer,
        HUDPanel,
        pausePanel,
        helpPanel,
        settingsPanel,
        confirmPanel,
        currentPanel;

    public List<TextMeshProUGUI> styles;

    // HUD attributes
    public TextMeshProUGUI 
        trickNameText,
        scoreText;

    #region HII Public Variables
    [HideInInspector]
    public ScoreManager SM;

    [HideInInspector]
    public bool previousExists = false;

    [HideInInspector]
    public GameObject lastSelected, firstSelected;

    #endregion
    #region Private Variables
    private GameObject previousPanel; // Reference object

    private bool 
        trickFinished = false, 
        fading = false;
    #endregion
    #region Video Attributes

    private const string RESOLUTION_PREF_KEY = "Resolution";
    private const string WINDOWED_PREF_KEY = "Windowed";
    private List<Resolution> resolutions;
    private int currentResIndex = 0;
    private bool isWindowed = false;

    #endregion

    // We can separate these out to other scripts if need be,
    //  a "HUD" script might be especially useful, but for now
    //  I hope separating everything into regions is clean enough
    #region General UI Methods

    #region Panel Logic Functions

    /// <summary>
    /// Opens the UI Panel 'nextPanel'
    /// </summary>
    /// <param name="nextPanel"></param>
    public void OpenPanel(GameObject nextPanel)
    {
        // Disable all other panels first
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        confirmPanel.SetActive(false);

        // Enable our panel we want to activate
        nextPanel.SetActive(true);
        currentPanel = nextPanel;
        previousExists = false;
    }

    /// <summary>
    /// Opens givenPanel in the GUI and sets previousPanel as the panel to return to
    /// </summary>
    /// <param name="nextPanel"></param>
    /// <param name="previousPanel"></param>
    public void OpenPanel(GameObject givenPanel, GameObject previousPanel)
    {
        // Disable all other panels first
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        confirmPanel.SetActive(false);

        // Enable our panel we want to activate
        givenPanel.SetActive(true);
        currentPanel = givenPanel;
        // Record our previous panel
        this.previousPanel = previousPanel;
        previousExists = true;

        firstSelected = currentPanel.GetComponentInChildren<Selectable>().gameObject;
        firstSelected.GetComponent<Selectable>().Select();
    }

    /// <summary>
    /// Closes the current panel with the option of opening the previous panel
    /// </summary>
    /// <param name="nextPanel"></param>
    /// <param name="returnToPreviousPanel"></param>
    public void ClosePanel(GameObject givenPanel, bool returnToPreviousPanel)
    {
        givenPanel.SetActive(false);

        // If we should return to the previous panel, AND we have a previous panel defined, then return to it
        //  *note: this works now, but if we end up needing multiple nested menus, this should be turned into an array/list
        if (returnToPreviousPanel && previousPanel)
            previousPanel.SetActive(true);

        // Reset our reference panel
        previousPanel = null;
        previousExists = false;
        
        if(lastSelected && givenPanel != HUDPanel)
            lastSelected.GetComponent<Selectable>().Select();
    }

    public void CloseAllPanels()
    {
        // Disable all panels
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        confirmPanel.SetActive(false);

        //lastSelected.GetComponent<Selectable>().Select();
    }

    private void ConfirmChoice()
    {
        confirmPanel.SetActive(true);
    }

    private void CancelConfirm()
    {
        confirmPanel.SetActive(false);
    }

    #endregion
    // I will map these to settings menu later
    #region Video Settings Functions

    public void SetNextResolution()
    {
        currentResIndex = GetNextWrappedIndex(resolutions, currentResIndex);
        SetResolutionText(resolutions[currentResIndex]);
    }

    public void SetPreviousResolution()
    {
        currentResIndex = GetPreviousWrappedIndex(resolutions, currentResIndex);
        SetResolutionText(resolutions[currentResIndex]);
    }

    void AdjustScreenResolution(int newResIndex)
    {
        currentResIndex = newResIndex;
        ApplyResolution(resolutions[currentResIndex]);
    }

    void ApplyResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, true);
        PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResIndex);
    }

    // Changes the text to the given resolution
    void SetResolutionText(Resolution resolution)
    {
        //resText.text = resolution.width + "x" + resolution.height;
    }

    // Apply resolution changes
    public void ApplyChanges()
    {
        AdjustScreenResolution(currentResIndex);
    }

    // *********** May want to make a reference to this in ApplyChanges() instead of button
    public void SetWindowedMode()
    {
        Screen.fullScreen = isWindowed = !isWindowed;
        PlayerPrefs.SetInt(WINDOWED_PREF_KEY, (isWindowed ? 1 : 0));
    }

    #endregion 
    #region Sound Settings Functions (not setup)
    #endregion
    #region Quit Functions

    public void QuitToTitle()
    {
        SceneLoader.instance.LoadScene("title");
    }

    public void CancelQuit()
    {
        CancelConfirm();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
    // I made this for fun, I'm sorry
    #region Button Access

    // This won't work if it's not a button... be careful with this
    public void ClickButton(GameObject button)
    {
        button.GetComponent<Button>().onClick.Invoke();
    }

    public void ClickButton(Button button)
    {
        button.onClick.Invoke();
    }

    public void ClickCurrentSelectedButton()
    {
        // I have broken the game
        // This is 1000000000% cheating
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }

    #endregion

    #endregion
    #region Menu UI Methods

    #region Pause Menu

    // Pause menu buttons and logic listed in order

    // Resume;
    //  Resume the game - should only be called while in a game scene
    public void PauseToResume()
    {
        // Resume the game
        GameManager.instance.ResumeGame(); // May need to edit this to make CloseAllPanels private
    }

    // Controls;
    public void PauseToControls()
    {
        lastSelected = EventSystem.current.currentSelectedGameObject;
        // Open our help panel and reference our pause panel
        OpenPanel(helpPanel, pausePanel);
    }

    // Settings;
    public void PauseToSettings()
    {
        lastSelected = EventSystem.current.currentSelectedGameObject;
        // Open our settings panel and reference our pause panel
        OpenPanel(settingsPanel, pausePanel);
    }

    // Quit to Title;
    public void PauseToTitle()
    {
        // Close our panels
        CloseAllPanels();
        // Load our title scene
        SceneLoader.instance.LoadScene("title");
    }

    #endregion

    #endregion
    #region HUD Methods

    public void UpdateScoreDifficulty(GameManager.Difficulty diff)
    {
        SM.parkScore = diff.getScore();
        Debug.Log("parkscore = " + SM.parkScore);
    }

    // Given a trick and score, we change the HUD text

    // It doesn't make a lot of sense that we have to input the scoreValue like this,
    //   we should use a ScriptableObject, a Dictionary, or an Enum to be able to pass
    //   in a Trick reference and obtain name and score all from the same object
    public void ChangeTrickAndScoreHUD(string trickName, int scoreValue) 
    {
        trickNameText.text = trickName;
        trickNameText.CrossFadeAlpha(1, 0, false); // reset our alpha value to normal
        scoreText.text = scoreValue.ToString();
    }
    
    public void ChangeTrick(string trickName)
    {
        trickNameText.text = trickName;
        trickNameText.CrossFadeAlpha(1, 0, false);
    }

    IEnumerator FadeOutTrickHUD()
    {
        yield return new WaitForSeconds(2f); // wait 2 seconds
        //if(!comboSuccess)
        {
            fading = true;

            // fade out trick text alpha over 2 seconds
            trickNameText.CrossFadeAlpha(0, 2, false);
            
            // fade out buttons image alpha over 2 seconds; this might not work
            //foreach(Image button in buttonInputImages)
            //{
            //    button.CrossFadeAlpha(0, 2, false);
            //}
        }
    }

    public void AddButtonHUD(string input)
    {
        // If our last input had us finish a trick
        if(trickFinished)
        {
            // clean up
            ClearAfterTrickHUD();
        }

        // if we're currently fading our GUI, speed it up
        if (fading)
        {

        }

        // if list has space, add image at current button index
        // else if list is out of bounds / full, reset button list, then add button image to beginning

            // else if combo success bool, reset list
    }

    public void ResetButtonListHUD()
    {
        // for each button, remove image and set alpha to 0
    } //empty function

    public void TrickStartedHUD()
    {
        // combo success bool to true


        //if(success)
        {
        }
        //else
        {
            // change trick func
        }
        // start coroutine for fade
    } //empty function

    public void TrickFinishedHUD(TricksController.Trick trick, bool shouldMultiply)
    {
        if (shouldMultiply)
        {
            SM.AddToScore(trick.mPoints, 2);
            int totalScore;
            totalScore = SM.SendDisplayScore();
            GameManager.instance.score = totalScore;

            scoreText.text = "Total Score:\n" + totalScore.ToString() + " / " + SM.parkScore;
            trickNameText.text = trick.mName + "\n" + trick.mPoints.ToString() + " * 2";
        }
        else
        {
            SM.AddToScore(trick.mPoints);
            int totalScore;
            totalScore = SM.SendDisplayScore();
            GameManager.instance.score = totalScore;

            scoreText.text = "Total Score:\n" + totalScore.ToString() + " / " + SM.parkScore;
            trickNameText.text = trick.mName + "\n" + trick.mPoints.ToString(); // This is bad formatting lol
        }
    }

    // Clear our button images and our score image and text
    public void ClearAfterTrickHUD()
    {
        scoreText.text = trickNameText.text = "";
        SM.ResetScore();
        // clear buttons
        // clear score
        // immediate-fade trick name
    }

    public void DisplayNewStyles()
    {
        GameManager.instance.RandomizePreferences();

        for(int i = 0; i < styles.Count; i++)
        {
            styles[i].text = GameManager.instance.judgesPreferences[i].ToString();
        }
    }


    #endregion
    #region Tools

    // Using modulos to "wrap" the array
    int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1)
            return 0;
        return (currentIndex + 1) % collection.Count;
    }

    // Using modulos to "wrap" the array
    int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1)
            return 0;
        if ((currentIndex - 1) < 0)
            return collection.Count - 1;
        return (currentIndex - 1) % collection.Count;
    }

    #endregion

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
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayNewStyles();

        DontDestroyOnLoad(this.gameObject); // Don't destroy our main UI Menu
        SM = HUDPanel.GetComponent<ScoreManager>();
        #region Video Initializations

        resolutions = new List<Resolution>();
        resolutions.AddRange(Screen.resolutions);
        resolutions = resolutions.Distinct().ToList();
        currentResIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, 0);
        isWindowed = PlayerPrefs.GetInt(WINDOWED_PREF_KEY) != 0;
        

        #endregion

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