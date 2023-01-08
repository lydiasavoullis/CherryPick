using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HeaterContainer : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Has Dropped");
        if (DragHandler.itemBeingDragged.gameObject.tag == "heater")
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            GameManager.Instance.CheckHeater(DragHandler.itemBeingDragged.gameObject);
        }


    }

}
