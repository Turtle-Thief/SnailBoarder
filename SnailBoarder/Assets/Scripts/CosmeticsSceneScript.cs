using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CosmeticsSceneScript : MonoBehaviour, ISelectHandler
{

    public void OnSelect(BaseEventData eventData)
    {
        int index = 0;

        foreach(Transform child in transform.parent)
        {
            //Debug.Log("Parent: " + transform.parent);
            
            if(child.gameObject == this.gameObject)
            {
                break;
            }
            index++;
        }

        GameManager.instance.SetDifficulty(index);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
