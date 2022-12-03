using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Buy : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI priceLabel;
    [SerializeField]
    public GameObject itemPrefab;
    [SerializeField]
    public GameObject itemSlotPrefab;
    [SerializeField]
    public GameObject buySlot;
    public void BuyItem() {
        int price = Int32.Parse(priceLabel.text.Replace("$", ""));//
        if (GameManager.Instance.funds>=price) {
            GameManager.Instance.ChangeFunds(-price);
            Transform item = buySlot.transform.GetChild(0);
            
            GameObject slotGO = Instantiate(itemSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameManager.Instance.inventory.transform);
            slotGO.name = "slot";
            GameObject newItemGO = Instantiate(itemPrefab, new Vector3(0, 0, 0), Quaternion.identity, slotGO.transform.GetChild(0));
            newItemGO.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            newItemGO.transform.localPosition = Vector3.zero;
            if (item.name == "plant") {
                newItemGO.name = "plant";
                newItemGO.GetComponent<PlantController>().plant = item.GetComponent<PlantController>().plant;
                
            }
            Destroy(this.gameObject);
        }
        
    }

}
