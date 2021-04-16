using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleZoneEnterTrigger : MonoBehaviour
{
    public GameManager.ZoneStyle styleName = GameManager.ZoneStyle.None;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);


        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(styleName.ToString());
            GameManager.instance.currentStyle = styleName;
        }
    }
}
