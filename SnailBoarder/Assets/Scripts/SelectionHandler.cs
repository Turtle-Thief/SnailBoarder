using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SelectionHandler : MonoBehaviour
    //, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    public GameObject previousMenuSelection;
    public GameObject nextMenuSelection;

    public void OnOpenMenu()
    {
        nextMenuSelection.GetComponent<Selectable>().Select();
        nextMenuSelection.GetComponent<SelectionHandler>().previousMenuSelection = this.gameObject;
    }

    public void OnCloseMenu()
    {
        previousMenuSelection.GetComponent<Selectable>().Select();
    }

    //public void OnDeselect(BaseEventData eventData)
    //{
    //    //previousMenuSelection.GetComponent<Selectable>().Select();
    //}

    //public void OnSelect(BaseEventData eventData)
    //{
    //    //previousMenuSelection.GetComponent<Selectable>().Select();
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
