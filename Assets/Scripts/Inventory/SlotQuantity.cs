using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SlotQuantity : MonoBehaviour
{
    //public int quantity = 1;
    public TextMeshProUGUI quantityText;
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
