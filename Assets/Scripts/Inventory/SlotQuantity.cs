using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SlotQuantity : MonoBehaviour
{
    //public int quantity = 1;
    public TextMeshProUGUI quantityText;
    // Start is called before the first frame update
    void Start()
    {
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
        if (gameObject.transform.GetChild(0).childCount == 1)
        {
            quantityText.text = "";
            return;
        }
        if (gameObject.transform.GetChild(0).childCount > 1)
        {
            quantityText.text = gameObject.transform.GetChild(0).childCount.ToString();
            return;
        }
        else {
            if (gameObject.name == "itemSlot")
            {
                Destroy(gameObject);
            }
            else {
                quantityText.text = "";
            }
        }
            
    }
}
