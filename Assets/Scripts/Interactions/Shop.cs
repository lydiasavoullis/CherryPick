using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject shopItemPrefab;
    [SerializeField]
    GameObject shopPlantPrefab;
    [SerializeField]
    GameObject shopContent;
    public void CloseShop() {
        GameVars.story.variablesState["shop_state"] = "closed";
    }
    public void GenerateShopItems() {
        for(int i = 0; i<shopContent.transform.childCount;i++) {
            Destroy(shopContent.transform.GetChild(i).gameObject);
        }
        Instantiate(shopPlantPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        Instantiate(shopPlantPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        Instantiate(shopPlantPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        Instantiate(shopItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);

    }
    private void OnEnable()
    {
        GenerateShopItems();
    }
}
