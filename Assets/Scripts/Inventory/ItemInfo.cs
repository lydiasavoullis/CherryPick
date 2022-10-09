using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemInfo : MonoBehaviour
{

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemInfo;

    public void SetUp(string name, string info) {
        itemName.text = name;
        itemInfo.text = info;
    }
}
