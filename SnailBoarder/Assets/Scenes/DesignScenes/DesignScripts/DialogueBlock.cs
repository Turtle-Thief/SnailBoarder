using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueBlock", menuName = "ScriptableObjects/DialogueBlock", order = 1)]

public class DialogueBlock : ScriptableObject
{

    public string snailName;

    public string[] dialogueLines;

    public string[] japaneseDialogueLines;

    public float textSpeed;

    public Animation emotion;

    public int cameraSlot;
    


}
