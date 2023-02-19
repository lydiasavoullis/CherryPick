using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NonInventoryDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    string startParentTag;
    RectTransform rectTransform;
    DropContainer dropContainer;
    Canvas canvas;
    GameObject renderOnTop;
    public void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        renderOnTop = GameObject.FindGameObjectWithTag("dragging");

    }
    public void Start()
    {
        Vector3 pos = this.GetComponent<RectTransform>().position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        transform.SetParent(startParent);
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        itemBeingDragged = null;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform = GetComponent<RectTransform>();

        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        startParentTag = startParent.tag;//if the start parent no longer exists we still have a reference to name
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(startParent);
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        itemBeingDragged = null;

    }


}
