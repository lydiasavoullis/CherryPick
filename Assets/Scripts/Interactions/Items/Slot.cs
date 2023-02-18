using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    GameObject itemSlotPrefab;
    GameObject panel;
    private void Start()
    {
    }
    public GameObject item {
        get
        {
            if (transform.childCount>0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //if (!item)
        //{
        //    DragHandler.itemBeingDragged.transform.SetParent(transform);
        //}
    }

}
