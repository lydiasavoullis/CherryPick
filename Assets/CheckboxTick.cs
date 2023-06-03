using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckboxTick : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI label;
    [SerializeField]
    TMP_FontAsset bold_font;
    [SerializeField]
    TMP_FontAsset thin_font;
    [SerializeField]
    Image checkbox;
    public void CheckboxChanged() {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            label.font = bold_font;
            checkbox.color = new Color32(38, 162, 134, 255);

        }
        else {
            label.font = thin_font;
            checkbox.color = Color.white;
        }
    }
}
