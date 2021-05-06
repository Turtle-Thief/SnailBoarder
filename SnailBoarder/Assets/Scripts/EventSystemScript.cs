using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemScript : MonoBehaviour
{

    private void Awake()
    {
        if (!EventSystem.current)
        {
            EventSystem.current = this.gameObject.GetComponent<EventSystem>();
        }
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
