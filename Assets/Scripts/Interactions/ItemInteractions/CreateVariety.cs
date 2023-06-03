using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class CreateVariety : MonoBehaviour, IDropHandler
{
    [SerializeField]
    GameObject phenotypePrefrab;
    Plant plant;
    [SerializeField]
    GameObject plantProfile;
    [SerializeField]
    GameObject plantJournal;
    [SerializeField]
    TMP_InputField plantName;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("dropped somthing");
        try
        {
            DragHandler.itemBeingDragged.GetComponent<PlantController>();
        }
        catch(Exception e) {
            return;
        }
        plant = DragHandler.itemBeingDragged.GetComponent<PlantController>().plant;
        Clear();
        for (int i = 0; i<plant.phenotypes.Count; i++) {
            GameObject newPhenotypeGO= Instantiate(phenotypePrefrab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
            newPhenotypeGO.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = plant.phenotypes[i];

        }

    }
    public void SavePlant()
    {
        if (plantName.text.Trim() == "" || plantName.text.Length<3) {
            return;
        }
        GameObject newPlantProfileGO = Instantiate(plantProfile, new Vector3(0, 0, 0), Quaternion.identity, plantJournal.transform);
        newPlantProfileGO.transform.localPosition = Vector3.zero;
        newPlantProfileGO.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = plantName.text;
        for (int i = 0; i < plant.phenotypes.Count; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.GetComponent<Toggle>().isOn) {
                newPlantProfileGO.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text += $"{plant.phenotypes[i]} |";
            }
            //plant.phenotypes[i];

        }
        Clear();
    }
    public void Clear() {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }

}
