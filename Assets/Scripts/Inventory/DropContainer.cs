using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropContainer : MonoBehaviour, IDropHandler
{
    [SerializeField]
    GameObject panel;
    [SerializeField]
    GameObject itemSlotPrefab;
    public void OnDrop(PointerEventData eventData)
    {
       // Debug.Log("Has Dropped");
        SortItem(DragHandler.itemBeingDragged);

    }

    public void SortItem(GameObject newItem) {
        //seeds need new slot no matter what
        if (newItem==null) {
            return;
        }
        if (newItem.name=="seed") {
            SetItemAsChildOfNewSlot(newItem);
            return;
        }
        for (int i = 0; i < panel.transform.childCount; i++) {
            GameObject currentSlotContainer = panel.transform.GetChild(i).gameObject;//gets slot container
            GameObject currentSlot = currentSlotContainer.transform.GetChild(0).gameObject;//gets slot container
            GameObject currentItem = gameObject;
            try {
                currentItem = currentSlot.transform.GetChild(0).gameObject;//gets item inside inner slot
            }
            catch (Exception e) {
                Debug.Log(e.ToString());
            }
            
           
            if (currentItem.name.ToLower() == newItem.name.ToLower()) {
                if (newItem.name.ToLower().Equals("plant"))
                {
                    bool matchingItem = GeneratePlants.CheckIfTwoPlantsAreTheSame(newItem.GetComponent<PlantController>().plant, currentItem.GetComponent<PlantController>().plant);
                    if (matchingItem)
                    {
                        newItem.transform.SetParent(currentSlot.transform);
                        newItem.transform.position = currentItem.transform.position;
                        currentSlotContainer.GetComponent<SlotQuantity>().UpdateQuantityText();
                        return;
                    }
                }
                else {//if not a plant
                    newItem.transform.SetParent(currentSlot.transform);
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    currentSlotContainer.GetComponent<SlotQuantity>().UpdateQuantityText();
                    return;
                }
               
            }
        }
        SetItemAsChildOfNewSlot(newItem);
    }

    public void SetItemAsChildOfNewSlot(GameObject newItem) {
        GameObject newSlotPrefab = Instantiate(itemSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity, panel.transform);
        newSlotPrefab.name = "slot";
        newItem.transform.SetParent(newSlotPrefab.transform.GetChild(0));
        newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        newItem.transform.position = Vector3.zero;
    }


}
