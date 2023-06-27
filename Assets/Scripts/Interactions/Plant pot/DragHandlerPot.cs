using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandlerPot : MonoBehaviour
{
    [SerializeField]
    GameObject soil;
    [SerializeField]
    GameObject waterBar;
    //[SerializeField]
    //DragHandler dragHandler;
    private void Start()
    {
        IfChildOfInventorySlotHideWaterBar();
    }
    public void EnableDisableDrag() {
        if (soil.transform.childCount != 0)
        {
            //Debug.Log("disable drag");
            this.gameObject.GetComponent<DragHandler>().enabled = false;
        }
        else
        {
            //Debug.Log("enable drag");
            this.gameObject.GetComponent<DragHandler>().enabled = true;
        }
    }
    public void IfChildOfInventorySlotHideWaterBar() {
        if (this.gameObject.transform.parent.tag == "dragging" || this.gameObject.transform.parent.tag == "inventorySlot")
        {
            //Debug.Log("hide bar");
            waterBar.SetActive(false);

        }
        else {
            waterBar.SetActive(true);
        }
    }
}
