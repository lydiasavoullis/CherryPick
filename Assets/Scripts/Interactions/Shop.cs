using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    public void CloseShop() {
        GameVars.story.variablesState["shop_state"] = "closed";
    }
    
    private void OnEnable()
    {
        //GenerateShopItems();
    }
}
