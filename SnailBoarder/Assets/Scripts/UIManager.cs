using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject 
        HUDPanel,
        pausePanel,
        helpPanel,
        settingsPanel,
        confirmPanel;

    #region UI methods


    #region Video Attributes

    public TextMeshProUGUI resText;
    private const string RESOLUTION_PREF_KEY = "Resolution";
    private const string WINDOWED_PREF_KEY = "Windowed";
    private List<Resolution> resolutions;
    private int currentResIndex = 0;
    private bool isWindowed = false;

    #endregion

    #region Panel Functions

    public void OpenPanel(GameObject thePanel)
    {
        // Disable all other panels first
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
        settingsPanel.SetActive(false);
        confirmPanel.SetActive(false);

        // Enable our panel we want to activate
        thePanel.SetActive(true);
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

    #region Save/Load (Not setup)
    #endregion
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
        SceneLoader.instance.LoadNextScene("title");
    }

    public void CancelQuit()
    {
        CancelConfirm();
    }

    private void QuitGame()
    {
        Application.Quit();
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


    #endregion

    #region Button Methods
    public void OnStart()
    {
        SceneLoader.instance.LoadNextScene("game");
    }

    public void OnHelp()
    {
        OpenPanel(helpPanel);
    }

    public void OnSettings()
    {
        OpenPanel(settingsPanel);
    }

    public void OnQuit()
    {
        QuitGame();
    }
    #endregion


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
        
    }
}
