using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Heater : MonoBehaviour, IBeginDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject heater = DragHandler.itemBeingDragged;
        if (heater.tag == "heater")
        {
            //Debug.Log("turn off heaters");
            heater.transform.GetChild(0).gameObject.SetActive(false);
        }
        

    }

}

