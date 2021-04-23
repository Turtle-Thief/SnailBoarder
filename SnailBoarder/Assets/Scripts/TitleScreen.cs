﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreen : MonoBehaviour
{
    UIManager UM;

    #region Button Functions
    public void OnStart()
    {
        UIManager.instance.lastSelected = EventSystem.current.currentSelectedGameObject;
        SceneLoader.instance.LoadNextSceneInBuild();
    }

    public void OnHelp()
    {
        UIManager.instance.lastSelected = EventSystem.current.currentSelectedGameObject;
        UM.OpenPanel(UM.helpPanel);
    }

    public void OnSettings()
    {
        UIManager.instance.lastSelected = EventSystem.current.currentSelectedGameObject;
        UM.OpenPanel(UM.settingsPanel);
    }

    public void OnQuit()
    {
        UIManager.instance.lastSelected = EventSystem.current.currentSelectedGameObject;
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
