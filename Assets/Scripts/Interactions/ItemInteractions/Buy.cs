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
    public GameObject heaterPrefab;
    [SerializeField]
    public GameObject potPrefab;
    [SerializeField]
    public GameObject itemSlotPrefab;
    [SerializeField]
    public GameObject plantPrefab;
    [SerializeField]
    public GameObject buySlot;
    [SerializeField]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescText;
    public string itemName;
    public void SetItemDetails(string itemName)
    {
        this.itemName = itemName;
        itemNameText.text = itemName;
        GameObject newItem = null;
        switch (itemName) {
            case "pot":
                
                newItem = Instantiate(potPrefab, new Vector3(0, 0, 0), Quaternion.identity, buySlot.transform);
                itemDescText.text = "A vessel for growing plants";
                break;
            case "heater":
                newItem = Instantiate(heaterPrefab, new Vector3(0, 0, 0), Quaternion.identity, buySlot.transform);
                itemDescText.text = "To keep plants from freezing";
                break;
            case "plant":
                newItem = Instantiate(plantPrefab, new Vector3(0, 0, 0), Quaternion.identity, buySlot.transform);
                itemDescText.text = "A random plant";
                break;
            default:
                break;

        }
        newItem.name = itemName;
        newItem.transform.GetComponent<DragHandler>().enabled = false;
        newItem.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        newItem.transform.localPosition = Vector3.zero;
    }
    public void BuyItem() {
        int price = Int32.Parse(priceLabel.text.Replace("$", ""));//
        if (GameManager.Instance.funds >= price)
        {
            GameManager.Instance.ChangeFunds(-price);
            Transform item = buySlot.transform.GetChild(0);

            GameObject slotGO = Instantiate(itemSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameManager.Instance.inventory.transform);
            slotGO.name = "slot";
            switch (itemName) {
            case "pot":
                    GameObject newItemGO = Instantiate(potPrefab, new Vector3(0, 0, 0), Quaternion.identity, slotGO.transform.GetChild(0));
                    newItemGO.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    newItemGO.transform.localPosition = Vector3.zero;
                    newItemGO.name = itemName;
                    break;
            case "heater":
                    GameObject newHeaterGO = Instantiate(heaterPrefab, new Vector3(0, 0, 0), Quaternion.identity, slotGO.transform.GetChild(0));
                    newHeaterGO.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                    newHeaterGO.transform.localPosition = Vector3.zero;
                    newHeaterGO.name = itemName;
                    break;
            case "plant":
                    GameObject newPlantGO = Instantiate(plantPrefab, new Vector3(0, 0, 0), Quaternion.identity, slotGO.transform.GetChild(0));
                    newPlantGO.name = "plant";
                    newPlantGO.GetComponent<PlantController>().plant = item.GetComponent<PlantController>().plant;
                    newPlantGO.name = itemName;
                    break;
            default:
                break;
        }

            
            Destroy(this.gameObject);
        }
        
    }

}
