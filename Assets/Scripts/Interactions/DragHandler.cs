using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    string startParentTag;
    RectTransform rectTransform;
    DropContainer dropContainer;
    Canvas canvas;
    GameObject renderOnTop;
    float intervalTime=1f;
    int timesClicked = 0;
    float lastClickedTime = 0f;
    public void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        renderOnTop = GameObject.FindGameObjectWithTag("dragging");
       
    }
    public void Start()
    {
        Vector3 pos = this.GetComponent<RectTransform>().position;
    }
    private void OnMouseDown()
    {
        if (this.transform.parent.tag == "inventorySlot")
        {
            return;
        }
        if (timesClicked > 2 || Time.time - lastClickedTime > intervalTime)
        {
            timesClicked = 0;
        }
        
        timesClicked++;
        //Debug.Log("Clicked: " + timesClicked);
        if (timesClicked == 1)
        {
            lastClickedTime = Time.time;
            return;
        }
        if (timesClicked > 1 && Time.time - lastClickedTime < intervalTime)
        {
            timesClicked = 0;
            lastClickedTime = 0;
            MoveToInventory();
        }
        

    }
    public void OnDrop(PointerEventData eventData)
    {
        if (gameObject.transform.parent.parent.parent.tag.Contains("inventory")) {
            GameObject inventory = GameObject.FindGameObjectWithTag("inventory");
            inventory.transform.parent.gameObject.GetComponent<DropContainer>().SortItem(DragHandler.itemBeingDragged);

        }
        
    }
    public void MoveToInventory() {
        //if already in inventory
        try
        {
            GameObject objectContainer = gameObject.transform.parent.gameObject;
            GameObject inventory = GameObject.FindGameObjectWithTag("inventory");
            inventory.transform.parent.gameObject.GetComponent<DropContainer>().SortItem(gameObject);
            //transform.SetParent(inventory.transform);
            if (objectContainer.tag.ToLower().Contains("slot"))
            {
                objectContainer.transform.parent.GetComponent<SlotQuantity>().UpdateQuantityText();
            }
            
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform = GetComponent<RectTransform>();

        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        startParentTag = startParent.tag;//if the start parent no longer exists we still have a reference to name
        if (startParentTag.ToLower().Contains("slot")) {
            dropContainer = startParent.parent.parent.parent.GetComponent<DropContainer>();
        }
        gameObject.transform.SetParent(renderOnTop.transform);
        if (startParent.tag.ToLower().Contains("slot"))
        {
            startParent.parent.GetComponent<SlotQuantity>().UpdateQuantityText();
        }
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if our start parent is an inventory slot and hasn't been droppped anywhere else
        if (startParentTag == "inventorySlot" && transform.parent.gameObject == renderOnTop)
        {
            dropContainer.SetItemAsChildOfNewSlot(gameObject);
            //gameObject.transform.parent.parent.GetComponent<SlotQuantity>().UpdateQuantityText();
        }
        else {
            if (transform.parent.gameObject == renderOnTop)
            {

                transform.SetParent(startParent);
                transform.position = startPosition;
            }
            if (transform.parent == startParent)
            {
                transform.position = startPosition;

            }
        }
        //if plant has been dropped in a slot update the quantity text
        if (gameObject.transform.parent.tag.ToLower().Contains("slot"))
        {
            gameObject.transform.parent.parent.GetComponent<SlotQuantity>().UpdateQuantityText();
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        itemBeingDragged = null;
    }

   
}
