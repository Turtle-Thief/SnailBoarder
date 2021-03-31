using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueBlock", menuName = "ScriptableObjects/DialogueBlock", order = 1)]

public class DialogueBlock : ScriptableObject
{

    public string snailName;

    public string[] dialogueLines;

    public string[] japaneseDialogueLines;

    [Tooltip("Amount of time inbetween each letter being rendered (in seconds.)")]
    [Range(0,1)]
    public float textSpeed = 0.1f;

    public Animation emotion;

    [Tooltip("The Dialogue master has camera positions childed to it. This int represents the child that the camera will share the position of.")]
    public int cameraSlot;
    


}
