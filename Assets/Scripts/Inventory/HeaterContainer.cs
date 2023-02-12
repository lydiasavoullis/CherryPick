using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HeaterContainer : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject heater = DragHandler.itemBeingDragged;
        //Debug.Log("Has Dropped");
        if (heater.tag == "heater")
        {
            heater.transform.SetParent(transform);
            CheckHeater(heater, transform);
        }
        //if (heater.transform.parent.tag != "heaterContainer") {
        //    heater.transform.GetChild(0).gameObject.SetActive(false);
        //}


    }

    public void CheckHeater(GameObject heater, Transform parent)
    {
        if (GameManager.Instance.nightTemp < 0 && GameVars.story.variablesState["end_of_day"].ToString() == "true" && parent.tag == "heaterContainer")
        {
            Debug.Log("turn on heaters");
            heater.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
}
