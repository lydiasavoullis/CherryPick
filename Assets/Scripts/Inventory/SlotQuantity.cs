using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SlotQuantity : MonoBehaviour
{
    //public int quantity = 1;
    [SerializeField]
    TextMeshProUGUI quantityText;
    [SerializeField]
    GameObject slot;
    // Start is called before the first frame update
    void Start()
    {
        //if (slot.transform.childCount == 0) {
        //    Destroy(gameObject);
        //}
        //if(gameObject.transform.GetChild(0).childCount==0){
        //    Destroy(gameObject);
        //}
       // UpdateQuantityText();
    }
    //public void AddItemToSlot() {
    //    quantity++;
    //    UpdateQuantityText();
    //}
    //public void RemoveItemFromSlot()
    //{
    //    quantity--;
    //    UpdateQuantityText();
    //}
    public void UpdateQuantityText() {
        if (slot.transform.childCount==0 && gameObject.tag == "inventorySlot") {
            Destroy(gameObject);
            return;
        }
        if (slot.transform.childCount == 1)
        {
            quantityText.text = "";
            return;
        }
        if (slot.transform.childCount > 1)
        {
            quantityText.text = slot.transform.childCount.ToString();
            return;
        }
        else {
            if (gameObject.name == "slot")
            {
                Destroy(gameObject);
            }
            else {
                quantityText.text = "";
            }
        }
            
    }
}
