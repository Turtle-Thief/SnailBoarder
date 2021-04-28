using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class DialogueStructure : MonoBehaviour
{

    public int shownCharacters = 0;

    public DialogueBlock[] block;

    public int blockNum;

    public int lineNum;

    public string parsedDialogue;

    
    public int characterLen;


    private bool triggerDialogue = true;

    private float timeFrame;
    

    public GameObject nextBlock;


    public TMP_Text mainBox;
    public TMP_Text nameBox;

    public GameObject[] cam;


    // Start is called before the first frame update
    void Start()
    {
        SetName();
    }

    // Update is called once per frame
    void Update()
    {

        ParseText();
        IncurText();

    }

    private void SetName()
    {

        nameBox.text = block[blockNum].snailName;
        IdentifySpeaker().SetActive(true);
        mainBox.maxVisibleCharacters = 0;
    }



    private void OnSkipAll()
    {

        if (SceneManager.GetActiveScene().buildIndex == 4) // dialog scene; this is bad
            SceneLoader.instance.DetermineResultsScreen();
        else
            SceneLoader.instance.LoadScene("nextLevel");

    }

    private void OnNextDialogue()
    {

        if(lineNum + 1 == block[blockNum].dialogueLines.Length)
        {

            if(blockNum + 1 == block.Length)
            {
                if (SceneManager.GetActiveScene().buildIndex == 4) // dialog scene; this is bad
                    SceneLoader.instance.DetermineResultsScreen();
                else
                    SceneLoader.instance.LoadScene("nextLevel");
            }

            IdentifySpeaker().SetActive(false);
            blockNum++;
            lineNum = 0;
            shownCharacters = 0;
            SetName();
            triggerDialogue = true;



        }
        else
        {
            lineNum++;
            shownCharacters = 0;
            triggerDialogue = true;
        }



    }



    public GameObject IdentifySpeaker()
    {
        GameObject temp = null;
        for (int i = 0; i < cam.Length; i++)
        {
            if(block[blockNum].cameraSlot == i)
            {
                temp = cam[i];
            }
        }
        return temp;
    }


    



    private void ParseText()
    {
        if(Time.time > timeFrame + block[blockNum].textSpeed && triggerDialogue)
        {

            

            if((block[blockNum].dialogueLines[lineNum][shownCharacters]).ToString() == " ")
            {

                shownCharacters++;
            }

           
            
            if(shownCharacters+1 == block[blockNum].dialogueLines[lineNum].Length)
            {
                triggerDialogue = false;
            }
            shownCharacters++;
            mainBox.maxVisibleCharacters = shownCharacters;
            timeFrame = Time.time;
        }






    }


    private void IncurText()
    {

        mainBox.text = block[blockNum].dialogueLines[lineNum];
    }


    private void ClearBox()
    {

    }

    public void ExitBlock()
    {
        ClearBox();
        nextBlock.SetActive(true);
    }



}
