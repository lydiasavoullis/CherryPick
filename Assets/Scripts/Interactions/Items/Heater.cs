using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Heater : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("turn off heaters");
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if ( gameObject.transform.parent.tag == "heaterContainer")
        {
            //Debug.Log("turn on heaters");
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

