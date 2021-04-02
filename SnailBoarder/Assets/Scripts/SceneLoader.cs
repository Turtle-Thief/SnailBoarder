using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance = null;

    // public int values to change scenes
    public int title, prelevel, nextLevel, judge, dialog;

    private int currentScene = 0;

    public void LoadScene(string sceneName)
    {
        if(sceneName == "prelevel")
        {
            SceneManager.LoadScene(prelevel);
            currentScene = prelevel;
        }
        else if (sceneName == "nextLevel")
        {
            // Load the next scene
            SceneManager.LoadScene(nextLevel);
            currentScene = nextLevel;
            
            // Signal that we're starting the next level (may need to add loading test here)
            GameManager.instance.OnNextLevel();
        }
        else if (sceneName == "judge")
        {
            // Load the judge scene
            SceneManager.LoadScene(judge);
            currentScene = judge;

            // Signal that we've finished the previous level (may need to add loading test here)
            GameManager.instance.OnFinishedLevel();
        }
        else if (sceneName == "dialog")
        {
            SceneManager.LoadScene(dialog);
            currentScene = dialog;
        }
        else if (sceneName == "title")
        {
            SceneManager.LoadScene(title);
            GameManager.instance.onPausableScene = false;
            GameManager.instance.ResumeGame();
            currentScene = title;
        }
        else
        {
            Debug.Log("Error: sceneName does not exist");
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
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
    private void Update()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!

        }
    }
}
