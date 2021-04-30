using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    GameObject sadHat;
    public GameObject happySnailEmotePrefab;
    public GameObject sadSnailEmotePrefab;

    public void GiveHat(int hatIndex)
    {
        CosmeticMenu cm = GetComponent<CosmeticMenu>();
        GameObject newHat = Instantiate(cm.hats[hatIndex], cm.hatPoint.transform.position, Quaternion.identity, cm.hatPoint.transform);
        sadHat = newHat;

        //newHat.transform.position = cm.hatPoint.transform.position;
        //newHat.transform.parent = cm.hatPoint.transform.parent.parent.parent;

        //Debug.Log(newHat);
        //Debug.Log(newHat.transform.parent);
    }

    // Start is called before the first frame update
    void Start()
    {
        GiveHat(GameManager.instance.hatIndex);
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!
            //Debug.Log("Words that are identifiable: " + sadHat);
        }
    }

    public void OnOllie()
    {
        Debug.Log("Input Ollie Test");
    }

    public void OnHappyEmote()
    {
        Instantiate(happySnailEmotePrefab, gameObject.transform);
        Debug.Log("Happy!");
    }

    public void OnSadEmote()
    {
        Instantiate(sadSnailEmotePrefab, gameObject.transform);
        Debug.Log("Sad:(");
    }
}
