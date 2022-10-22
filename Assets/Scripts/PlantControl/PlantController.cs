using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;

public class PlantController : MonoBehaviour, IDropHandler
{
    public GameObject infoPrefab;
    public string placeholdertext = "hover over a plant to learn more...";
    public string plantDescription = "";
    public GameObject plantPrefab;
    public GameObject seedPrefab;
    public static GameObject parent1;
    public Plant plant;
    public Image petals;
    public Image stem;
    public Image center;
    //public GameObject background;
    public Phenotype[] phenotypes;
    public AudioSource audioSourcePop;
    private void Start()
    {
        //background.SetActive(false);
        if (plant == null)
        {
            plant = GeneratePlants.GenerateRandomNewPlant();
        }
        else {
            //audioSourcePop = gameObject.GetComponent<AudioSource>();
            //audioSourcePop.pitch = (UnityEngine.Random.Range(0.6f, .9f));
            //audioSourcePop.Play();
        }
        
        SetPlantInfo();
        SetCurrentPhenotype();
    }
    public void OnDrop(PointerEventData eventData) {
        if (DragHandler.itemBeingDragged.name.ToLower().Contains("plant") && transform.parent.tag == "sellSlot" && GeneratePlants.CheckIfTwoPlantsLookTheSame(this.plant, DragHandler.itemBeingDragged.GetComponent<PlantController>().plant))
        {

            DragHandler.itemBeingDragged.transform.SetParent(transform.parent);
            DragHandler.itemBeingDragged.transform.localPosition = Vector3.zero;
            transform.parent.parent.gameObject.GetComponent<SlotQuantity>().UpdateQuantityText();
        }
    }


    public void SetCurrentPhenotype() {  
        stem.sprite = GetPhenotypeSprite(GeneratePlants.CheckHeight(plant.genotypes["height"]));
        petals.color = SetColour(GeneratePlants.CheckColour(plant.genotypes["colour"]));
        petals.sprite = GetPhenotypeSprite(GeneratePlants.CheckPetals(plant.genotypes["petals"]));
    }
    
    public Color32 SetColour(string colour) {
        Color32 color;
        switch (colour)
        {
            case "white":
                color = Color.white;
                return color;
            case "pink":
                color = new Color32(255, 200, 230, 255);
                return color;
            case "red":
                return Color.red;
            default:
                break;
        }
        return Color.white;
    }
    public Sprite GetPhenotypeSprite(string type) {
        foreach(Phenotype p in phenotypes) {
            if (p.name == type) {
                return p.sprite;
            }

        }
        return null;
    }

    void OnMouseOver()
    {
        //Debug.Log("HOVER OVER FLWOER");
        if (Mouse.current.leftButton.wasPressedThisFrame)//Input.GetMouseButtonDown(0)
        {
           // Debug.Log("Left click");
            parent1 = gameObject;
            return;
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)//Input.GetMouseButtonDown(1)
        {
            //Debug.Log("Right click");
            if (parent1 != null && parent1 != gameObject)
            {
                //GenerateChildPlant(parent1, gameObject);
                GenerateChildSeed(parent1, gameObject);
            }
        }
    }

    public void GenerateChildPlant(GameObject parent1, GameObject parent2)
    {
        
        GameObject panel = GameObject.Find("Delivery");
        GameObject childPlantGO = Instantiate(plantPrefab, new Vector3(0, 0, 0), Quaternion.identity, panel.transform);
        childPlantGO.name = "plant";
        childPlantGO.GetComponent<PlantController>().plant = new Plant();
        GeneratePlants.CombineGametes(parent1.GetComponent<PlantController>().plant, parent2.GetComponent<PlantController>().plant, childPlantGO.GetComponent<PlantController>().plant);
        
    }
    public void GenerateChildSeed(GameObject parent1, GameObject parent2)
    {
        
        GameObject inventory = GameObject.Find("seedContainer").gameObject;
        //GameObject inventory = GameObject.Find("Inventory").transform.GetChild(0).GetChild(0).gameObject;
        int noOfChildren = UnityEngine.Random.Range(4, 8);
        for (int i=0; i<noOfChildren;i++) {
            
            GameObject childSeedGO = Instantiate(seedPrefab, new Vector3(0,0,0), Quaternion.identity, inventory.transform);
            childSeedGO.name = "seed";
            childSeedGO.GetComponent<SeedController>().seed = new Seed();
            GeneratePlants.CombineGametes(parent1.GetComponent<PlantController>().plant, parent2.GetComponent<PlantController>().plant, childSeedGO.GetComponent<SeedController>().seed);
        }
        RemoveOnePlantFromStack(parent1);
        RemoveOnePlantFromStack(parent2);
        if (GameVars.story.variablesState["tutorialpt1"].ToString() == "incomplete")
        {
            GameVars.story.ChoosePathString("tutorial_pt2");
        }

    }

    public void RemoveOnePlantFromStack(GameObject plant) {
        GameObject itemSlot = plant.transform.parent.parent.gameObject;
        DestroyImmediate(plant);
        try {
            itemSlot.GetComponent<SlotQuantity>().UpdateQuantityText();
        }
        catch (Exception e) {
            Debug.Log(e.ToString());
        }
        
        
    }
    public void SetPlantInfo() {
        WriteToTextObject(plant.genotypes);
    }
    
    public void DisplayPlantInfo() {
        GameManager.Instance.DisplayItemInfo(transform.position, "plant", plant.description);
    }
    public void RemovePlantInfo()
    {
        GameManager.Instance.RemoveItemInfo();
    }
    public void WriteToTextObject(Dictionary<string, string[]> genotype) {
        //plant.description += $"{name} : {geneticInfo[0]} {geneticInfo[1]}\n";
        foreach (var item in genotype) {
            plant.description += $"{item.Key} : {item.Value[0]} {item.Value[1]}\n";
        }
    }


}
