using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperiorArrays;
public class DialogueBrancher : MonoBehaviour
{

    public GameObject dialogueMaster;

    public DialogueBlock[] preText;

    public DialogueBlock[] branchOne;

    public DialogueBlock[] branchTwo;

    public DialogueBlock[] postText;
  //  public enum styles;



    // Start is called before the first frame update
    void Start()
    {
        AssignBranches();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AssignBranches()
    {
        DialogueBlock[] tempArray = null;
      

        for (int j = 0; j < preText.Length; j++)
        {
            //Add per line
            tempArray = new DialogueBlock[dialogueMaster.GetComponent<DialogueStructure>().block.Length + 1];
            for (int i = 0; i < dialogueMaster.GetComponent<DialogueStructure>().block.Length; i++)
            {
                tempArray[i] = dialogueMaster.GetComponent<DialogueStructure>().block[i];
            }
            tempArray[tempArray.Length - 1] = preText[j];

            dialogueMaster.GetComponent<DialogueStructure>().block = tempArray;

        }

        

        //Branch One

        tempArray = new DialogueBlock[dialogueMaster.GetComponent<DialogueStructure>().block.Length + 1];
        for (int i = 0; i < dialogueMaster.GetComponent<DialogueStructure>().block.Length; i++)
        {
            tempArray[i] = dialogueMaster.GetComponent<DialogueStructure>().block[i];
        }
        tempArray[tempArray.Length - 1] = GrabStyles(0);

        dialogueMaster.GetComponent<DialogueStructure>().block = tempArray;

        //Branch Two

        tempArray = new DialogueBlock[dialogueMaster.GetComponent<DialogueStructure>().block.Length + 1];
        for (int i = 0; i < dialogueMaster.GetComponent<DialogueStructure>().block.Length; i++)
        {
            tempArray[i] = dialogueMaster.GetComponent<DialogueStructure>().block[i];
        }
        tempArray[tempArray.Length - 1] = GrabStyles(1);

        dialogueMaster.GetComponent<DialogueStructure>().block = tempArray;


        for (int j = 0; j < postText.Length; j++)
        {
            //Add per line
            tempArray = new DialogueBlock[dialogueMaster.GetComponent<DialogueStructure>().block.Length + 1];
            for (int i = 0; i < dialogueMaster.GetComponent<DialogueStructure>().block.Length; i++)
            {
                tempArray[i] = dialogueMaster.GetComponent<DialogueStructure>().block[i];
            }
            tempArray[tempArray.Length - 1] = postText[j];

            dialogueMaster.GetComponent<DialogueStructure>().block = tempArray;

        }

        dialogueMaster.GetComponent<DialogueStructure>().block = tempArray;




        dialogueMaster.GetComponent<DialogueStructure>().enabled = true;

    }



    public DialogueBlock GrabStyles(int branchNum)
    {

        DialogueBlock newBlock = null;

        int style = (int)GameManager.instance.judgesPreferences[0];

        switch (branchNum)
        {

            case 0:
                newBlock = branchOne[style];
                break;

            case 1:
                newBlock = branchTwo[style];
                break;
        }
        


        return newBlock;

    }



}
