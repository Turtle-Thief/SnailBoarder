using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance = null;

    // public int values to change scenes
    public int title, nextLevel, judge, dialog;

    private int currentScene = 0;

    public void LoadNextScene(string sceneName)
    {
        if (sceneName == "nextLevel")
        {
            SceneManager.LoadScene(nextLevel);
            currentScene = nextLevel;
        }
        else if (sceneName == "judge")
        {
            SceneManager.LoadScene(judge);
            currentScene = judge;
        }
        else if (sceneName == "dialog")
        {
            SceneManager.LoadScene(dialog);
            currentScene = dialog;
        }
        else if (sceneName == "title")
        {
            SceneManager.LoadScene(title);
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

}
