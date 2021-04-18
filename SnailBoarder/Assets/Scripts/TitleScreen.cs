using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    UIManager UM;

    #region Button Functions
    public void OnStart()
    {
        SceneLoader.instance.LoadNextSceneInBuild();
    }

    public void OnHelp()
    {
        UM.OpenPanel(UM.helpPanel);
    }

    public void OnSettings()
    {
        UM.OpenPanel(UM.settingsPanel);
    }

    public void OnQuit()
    {
        UM.QuitGame();
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        UM = GameObject.Find("UI_Main").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
