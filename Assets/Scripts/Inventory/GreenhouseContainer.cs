using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GreenhouseContainer : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Has Dropped");
        if (DragHandler.itemBeingDragged.gameObject.tag == "pot") {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
        }
        

    }

}
