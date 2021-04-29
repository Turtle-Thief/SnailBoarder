using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance = null;

    // Serialzied int values to change scenes in title editor
    [SerializeField]
    private int title = 0, hat = 1, instruct = 2, nextLevel = 3, dialog = 4, victory = 5, defeat = 6, tutorial = 7;

    public int currentScene = 0;

    public void LoadScene(string sceneName)
    {
        switch (sceneName)
        {
            case "title":
                GameManager.instance.onPausableScene = false;
                currentScene = title;
                UIManager.instance.DisplayNewStyles();
                SceneManager.LoadScene(title);
                GameManager.instance.ResumeGame();
                break;
            case "hat":
                UIManager.instance.CloseAllPanels();
                currentScene = hat;
                SceneManager.LoadScene(hat);
                break;
            case "instruct":
                currentScene = instruct;
                SceneManager.LoadScene(instruct);
                break;
            case "nextLevel":
                // Load the next scene
                currentScene = nextLevel;
                SceneManager.LoadScene(nextLevel);

                // Signal that we're starting the next level (may need to add loading test here)
                GameManager.instance.OnNextLevel();
                break;
            case "dialog":
                //Signal that we've finished the previous level (may need to add loading test here)
                GameManager.instance.OnFinishedLevel();
                currentScene = dialog;
                SceneManager.LoadScene(dialog);
                break;
            case "victory":
                currentScene = victory;
                SceneManager.LoadScene(victory);
                break;
            case "defeat":
                currentScene = defeat;
                SceneManager.LoadScene(defeat);
                break;
            case "tutorial":
                currentScene = tutorial;
                GameManager.instance.onPausableScene = true;
                SceneManager.LoadScene(tutorial);
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
                GameManager.instance.onPausableScene = false;
                currentScene = title;
                UIManager.instance.DisplayNewStyles();
                SceneManager.LoadScene(title);
                GameManager.instance.ResumeGame();
                break;
            case 1:
                UIManager.instance.CloseAllPanels();
                currentScene = hat;
                SceneManager.LoadScene(hat);
                break;
            case 2:
                currentScene = instruct;
                SceneManager.LoadScene(instruct);
                break;
            case 3:
                // Load the next scene
                currentScene = nextLevel;
                SceneManager.LoadScene(nextLevel);

                // Signal that we're starting the next level (may need to add loading test here)
                GameManager.instance.OnNextLevel();
                break;
            case 4:
                //Signal that we've finished the previous level (may need to add loading test here)
                GameManager.instance.OnFinishedLevel();
                currentScene = dialog;
                SceneManager.LoadScene(dialog);
                break;
            case 5:
                currentScene = victory;
                SceneManager.LoadScene(victory);
                break;
            case 6:
                currentScene = defeat;
                SceneManager.LoadScene(defeat);
                break;
            case 7:
                currentScene = tutorial;
                GameManager.instance.onPausableScene = true;
                SceneManager.LoadScene(tutorial);
                break;
            default:
                Debug.Log("Error: sceneName does not exist");
                break;
        }
    }

    // Should be used SPARINGLY
    public void LoadNextSceneInBuild()
    {
        currentScene++;
        Debug.Log("I loaded this scene: " + currentScene);
        LoadScene(currentScene);
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
