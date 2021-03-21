using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // ***** Still debating whether to make a singleton or not

    public GameObject 
        HUDPanel,
        pausePanel,
        helpPanel,
        settingsPanel,
        confirmPanel,
        currentPanel;

    public bool previousExists = false;
    private GameObject previousPanel; // Reference object

    #region Video Attributes

    public TextMeshProUGUI resText;
    private const string RESOLUTION_PREF_KEY = "Resolution";
    private const string WINDOWED_PREF_KEY = "Windowed";
    private List<Resolution> resolutions;
    private int currentResIndex = 0;
    private bool isWindowed = false;

    #endregion

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
    }

    public void CloseAllPanels()
    {
        // Disable all panels
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        confirmPanel.SetActive(false);
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
        resText.text = resolution.width + "x" + resolution.height;
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
        // Open our help panel and reference our pause panel
        OpenPanel(helpPanel, pausePanel);
    }

    // Settings;
    public void PauseToSettings()
    {
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
        if(GameManager.instance.UM)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject); // Don't destroy our main UI Menu

        #region Video Initializations

        resText.text = "Auto";
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