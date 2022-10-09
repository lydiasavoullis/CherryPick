using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DeleteSave : MonoBehaviour
{
    public GameObject saveSlotButton;
    //make delete trigger more user friendly
    public void DeleteSelf() {     
        Destroy(saveSlotButton);
        string filename = saveSlotButton.GetComponentInChildren<TextMeshProUGUI>().text;
        string dataFile = Application.persistentDataPath + "/" + "saves" + "/" + filename + ".data";
        string image = Application.persistentDataPath + "/" + "saves" + "/" + filename + ".png";
        if (File.Exists(dataFile)) {
            File.Delete(dataFile);
        }
        if (File.Exists(image))
        {
            File.Delete(image);
        }
    }
}
