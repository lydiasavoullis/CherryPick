using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Heater : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private void Start()
    {
        if (gameObject.transform.parent.tag == "heaterContainer" && (GameVars.story != null && GameVars.story.variablesState["end_of_day"].ToString() == "true"))
        {
            //Debug.Log("turn on heaters");
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("turn off heaters");
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if ( gameObject.transform.parent.tag == "heaterContainer" && GameManager.Instance.timeOfDay == "night")
        {
            //Debug.Log("turn on heaters");
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

