using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance = null;

    // Serialzied int values to change scenes in title editor
    [SerializeField]
    private int title = 0, hat = 1, instruct = 2, nextLevel = 3, judge = 4, dialog = 5, victory = 6, defeat = 7;

    public int currentScene = 0;

    public void LoadScene(string sceneName)
    {
        switch (sceneName)
        {
            case "title":
                SceneManager.LoadScene(title);
                GameManager.instance.onPausableScene = false;
                GameManager.instance.ResumeGame();
                currentScene = title;
                break;
            case "hat":
                SceneManager.LoadScene(hat);
                currentScene = hat;
                break;
            case "instruct":
                SceneManager.LoadScene(instruct);
                currentScene = instruct;
                break;
            case "nextLevel":
                // Load the next scene
                SceneManager.LoadScene(nextLevel);
                currentScene = nextLevel;

                // Signal that we're starting the next level (may need to add loading test here)
                GameManager.instance.OnNextLevel();
                break;
            case "judge":
                //Load the judge scene
                SceneManager.LoadScene(judge);
                currentScene = judge;
                break;
            case "dialog":
                //Signal that we've finished the previous level (may need to add loading test here)
                GameManager.instance.OnFinishedLevel();
                SceneManager.LoadScene(dialog);
                currentScene = dialog;
                break;
            case "victory":
                SceneManager.LoadScene(victory);
                currentScene = victory;
                break;
            case "defeat":
                SceneManager.LoadScene(defeat);
                currentScene = defeat;
                break;
            default:
                Debug.Log("Error: sceneName does not exist");
                break;
        }

    }
    public void LoadScene(int sceneName)
    {
        switch (sceneName)
        {
            case 0:
                SceneManager.LoadScene(title);
                GameManager.instance.onPausableScene = false;
                GameManager.instance.ResumeGame();
                currentScene = title;
                break;
            case 1:
                SceneManager.LoadScene(hat);
                currentScene = hat;
                break;
            case 2:
                SceneManager.LoadScene(instruct);
                currentScene = instruct;
                break;
            case 3:
                // Load the next scene
                SceneManager.LoadScene(nextLevel);
                currentScene = nextLevel;

                // Signal that we're starting the next level (may need to add loading test here)
                GameManager.instance.OnNextLevel();
                break;
            case 4:
                //Load the judge scene
                SceneManager.LoadScene(judge);
                currentScene = judge;
                break;
            case 5:
                //Signal that we've finished the previous level (may need to add loading test here)
                GameManager.instance.OnFinishedLevel();
                SceneManager.LoadScene(dialog);
                currentScene = dialog;
                break;
            case 6:
                SceneManager.LoadScene(victory);
                currentScene = victory;
                break;
            case 7:
                SceneManager.LoadScene(defeat);
                currentScene = defeat;
                break;
            default:
                Debug.Log("Error: sceneName does not exist");
                break;
        }
    }

    public void LoadNextSceneInBuild()
    {
        currentScene++;
        SceneManager.LoadScene(currentScene);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void DetermineResultsScreen()
    {
        if(GameManager.instance.completedLastLevel)
        {
            LoadScene(victory);
        }
        else
        {
            LoadScene(defeat);
        }
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
