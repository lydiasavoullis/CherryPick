using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class Sell : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragHandler.itemBeingDragged.name.ToLower().Contains("plant") && !item)
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            DragHandler.itemBeingDragged.transform.localPosition = Vector3.zero;
    }
}
}

