using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;
using System.Linq;

public class PlantController : MonoBehaviour, IDropHandler
{
    public GameObject infoPrefab;
    public string placeholdertext = "hover over a plant to learn more...";
    public string plantDescription = "";
    public GameObject plantPrefab;
    public GameObject seedPrefab;
    public static GameObject parent1;
    public Plant plant;
    public Image[] petals;
    public Image stem;
    //public Image center;
    public GameObject cluster1;
    public GameObject cluster2;
    public GameObject petalPrefab;
    public GameObject center;
    //public GameObject center2;
    public GameObject leaves_right;
    public GameObject leaves_left;
    //public GameObject background;
    public Phenotype[] phenotypes;
    public AudioSource audioSourcePop;
    Vector3 oldSizeL = new Vector3(18f, 18f);
    Vector3 oldSizeR = new Vector3(18f, 18f);
    private void Start()
    {
        //oldSizeL = leaves_left.GetComponent<RectTransform>().rect.size;
        //oldSizeR = leaves_left.GetComponent<RectTransform>().rect.size;
        //background.SetActive(false);
        if (plant == null)
        {
            //plant = GeneratePlants.GenerateRandomNewPlant(1);
            CheckLevelAndChangeRandomPlantCategory();
        }
        else
        {
            plant.phenotypes = GeneratePlants.GetPlantPhenotype(plant);
            //audioSourcePop = gameObject.GetComponent<AudioSource>();
            //audioSourcePop.pitch = (UnityEngine.Random.Range(0.6f, .9f));
            //audioSourcePop.Play();
        }
        ResetPlantCharacteristics();
    }
    public void CheckLevelAndChangeRandomPlantCategory() {
        int reputation = GameManager.Instance.reputation;
        if (reputation == 0) {
            plant = GeneratePlants.GenerateRandomNewPlant(1);
            return;
        } 
        else if(reputation > 0 && reputation <=2)
        {
            plant = GeneratePlants.GenerateRandomNewPlant(2);
            return;
        }
        else {
            plant = GeneratePlants.GenerateRandomNewPlant(2);
        }
    }
    public void ResetPlantCharacteristics() {
        leaves_left.GetComponent<RectTransform>().sizeDelta = new Vector3(oldSizeL.x, oldSizeL.y, oldSizeL.z);
        leaves_right.GetComponent<RectTransform>().sizeDelta = new Vector3(oldSizeR.x , oldSizeL.y, oldSizeR.z);
        ClearAllPetals(center, cluster1, cluster2);
        cluster2.SetActive(false);
        cluster1.SetActive(false);
        SetPlantInfo();
        SetCurrentPhenotype();
    }
    public void OnDrop(PointerEventData eventData)
    {
        //GeneratePlants.CheckIfTwoPlantsLookTheSame(this.plant, DragHandler.itemBeingDragged.GetComponent<PlantController>().plant)
        List<string> phenotypes = new List<string>();
        if (transform.parent.tag == "sellSlot")
        {
            //get object where Task script is attached
            phenotypes = this.transform.parent.parent.parent.GetComponent<TaskController>().task.phenotypes;
        }
        if (DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.tag.ToLower().Contains("plant") && transform.parent.tag == "sellSlot" && GeneratePlants.CheckIfDroppedPlantContainsAllDesiredPhenotypes(phenotypes, DragHandler.itemBeingDragged.GetComponent<PlantController>().plant))
        {

            DragHandler.itemBeingDragged.transform.SetParent(transform.parent);
            DragHandler.itemBeingDragged.transform.localPosition = Vector3.zero;
            transform.parent.parent.gameObject.GetComponent<SlotQuantity>().UpdateQuantityText();
        }
    }
    private void OnMouseEnter()
    {
        if(gameObject.transform.parent.tag == "soil"){
            gameObject.GetComponent<Animation>().Play();
        }
        
    }
    
    //set how the plant LOOKS
    //try getting from phenotype list now
    public void SetCurrentPhenotype()
    {

        //GeneratePlants.genotypesRange;
        //plant.phenotypes

        stem.sprite = GetPhenotypeSprite(plant.phenotypes[1]);//GeneratePlants.CheckHeight(plant.genotypes["height"])

        
        if (plant.phenotypes[1] == "tall")
        {
            float multiplier = 2f;
            Vector3 oldSizeL = leaves_left.GetComponent<RectTransform>().sizeDelta;
            Vector3 oldSizeR = leaves_left.GetComponent<RectTransform>().sizeDelta;
            leaves_left.GetComponent<RectTransform>().sizeDelta = new Vector3(oldSizeL.x * multiplier, oldSizeL.y* multiplier, oldSizeL.z);
            leaves_right.GetComponent<RectTransform>().sizeDelta = new Vector3(oldSizeR.x * multiplier, oldSizeL.y * multiplier, oldSizeR.z);
        }
        Color32 redSpectrum = SetColourR(plant.phenotypes[0]);
        if (plant.category == 1) {
            SetPetalsSprite(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite($"petal_round"), center);
            SetPetalsColour(redSpectrum, center);
            return;
        }

        Color32 blueSpectrum = new Color32();
        Color32 greenSpectrum = new Color32();
        if (plant.category > 1)
        {
            SetPetalsSprite(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite($"petal_{plant.phenotypes[4]}"), center);//GeneratePlants.CheckColour(plant.genotypes["colour"])
            SetPetalsColour(redSpectrum, center);
            //Debug.Log(plant.phenotypes.Count);
            leaves_left.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
            leaves_right.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
            leaves_right.transform.localRotation = Quaternion.Euler(0, 180, 0);
            SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]), $"petal_{plant.phenotypes[4]}", redSpectrum);
            //add blue spectrum
            blueSpectrum = SetColourB(redSpectrum, plant.phenotypes[6]);
            switch (plant.phenotypes[3])
            {
                case "one":
                    ModifyPetalsColour(blueSpectrum, center);
                    break;
                case "two":
                    ModifyPetalsColour(blueSpectrum, center);
                    ModifyPetalsColour(blueSpectrum, cluster1);
                    break;
                case "three":
                    ModifyPetalsColour(blueSpectrum, center);
                    ModifyPetalsColour(blueSpectrum, cluster1);
                    ModifyPetalsColour(blueSpectrum, cluster2);
                    break;
                default:
                    ModifyPetalsColour(blueSpectrum, center);
                    break;
            }
        }
        if (plant.category > 2)
        {
            greenSpectrum = SetColourG(blueSpectrum, plant.phenotypes[7]);
            switch (plant.phenotypes[3])
            {
                case "one":
                    ModifyPetalsColour(greenSpectrum, center);
                    break;
                case "two":
                    ModifyPetalsColour(greenSpectrum, center);
                    ModifyPetalsColour(greenSpectrum, cluster1);
                    break;
                case "three":
                    ModifyPetalsColour(greenSpectrum, center);
                    ModifyPetalsColour(greenSpectrum, cluster1);
                    ModifyPetalsColour(greenSpectrum, cluster2);
                    break;
                default:
                    ModifyPetalsColour(blueSpectrum, center);
                    break;
            }
        }
       

    }
    public void ClearAllPetals(GameObject center1, GameObject center2, GameObject center3) {
        for (int i = 0; i < center1.transform.childCount; i++)
        {
            Destroy(center1.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < center2.transform.childCount; i++)
        {
            Destroy(center2.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < center3.transform.childCount; i++)
        {
            Destroy(center3.transform.GetChild(i).gameObject);
        }
    }
    //for new dynamic petal creation system
    public void SetPetalsSprite(int petalNumber, Sprite petalSprite, GameObject center) {
        //for (int i = 0; i < center.transform.childCount;i++) {
        //    Destroy(center.transform.GetChild(i));
        //}
        for (int i = 0; i < petalNumber; i++)
        {
            GameObject petalGO = Instantiate(petalPrefab, new Vector3(0, 0, 0), Quaternion.identity, center.transform);
            petalGO.name = "petal";
            petalGO.GetComponent<Image>().sprite = petalSprite;
        }
    }
    public void SetLeavesSprite(string leafType)
    {
        //for (int i = 0; i < center.transform.childCount;i++) {
        //    Destroy(center.transform.GetChild(i));
        //}
        switch (leafType) {
            case "round":
                leaves_left.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
                leaves_right.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
                break;
            case "pointed":
                leaves_left.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
                leaves_right.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
                break;
            case "jagged":
                leaves_left.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
                leaves_right.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
                break;
            default:
                break;
        }
        
    }
    public void SetPetalsColour(Color32 color, GameObject center)
    {
        //for (int i = 0; i < center.transform.childCount;i++) {
        //    Destroy(center.transform.GetChild(i));
        //}
        for (int i = 0; i < center.transform.childCount; i++)
        {
            GameObject petalGO = center.transform.GetChild(i).gameObject;
            petalGO.GetComponent<Image>().color = color;
        }
    }
    public void ModifyPetalsColour(Color32 color, GameObject center)
    {
        //for (int i = 0; i < center.transform.childCount;i++) {
        //    Destroy(center.transform.GetChild(i));
        //}
        for (int i = 0; i < center.transform.childCount; i++)
        {
            GameObject petalGO = center.transform.GetChild(i).gameObject;
            petalGO.GetComponent<Image>().color = color;
        }
    }

    public Color32 SetColourR(string colour)
    {
        Color32 color;
        //Debug.Log(colour);
        switch (colour)
        {
            case "white":
                color = new Color32(255, 255, 255, 255);
                return color;
            case "pink":
                return color = new Color32(250, 170, 230, 255);
            case "red":
                return color = new Color32(240, 9, 0, 255);
            default:
                break;
        }
        return Color.white;
    }
    public Color32 SetColourB(Color32 current, string colour)
    {
        
        Color32 color;
        switch (colour)
        {
            case "white":
                color = new Color32(current.r, current.g, current.b, 255);
                return color;
            case "light_blue":
                color = new Color32(SubtractByte(current.r, 50), SubtractByte(current.g, 50), 255, 255);
                return color;
            case "blue":
                return color = new Color32(SubtractByte(current.r, 120), SubtractByte(current.g, 120), 255, 255);
            default:
                break;
        }
        return Color.white;
    }
    public Byte SubtractByte(Byte colourbyte, int valueChange) {
        int value = Convert.ToInt32(colourbyte) - valueChange;
        if (value<0) {
            value = 0;
        }
        return Convert.ToByte(value);
    }
    public Color32 SetColourG(Color32 current, string colour)
    {
        Color32 color;
        switch (colour)
        {
            case "white":
                color = new Color32(current.r, current.g, current.b, 255);
                return color;
            case "light_green":
                color = new Color32(current.r, 150, SubtractByte(current.b, 20), 255);
                return color;
            case "green":
                return color = new Color32(255, 255, SubtractByte(current.b, 100), 255);
            default:
                break;
        }
        return Color.white;
    }
    public Sprite GetPhenotypeSprite(string type)
    {
        foreach (Phenotype p in phenotypes)
        {
            if (p.name == type)
            {
                return p.sprite;
            }

        }
        return null;
    }
    public void SetClustersActive(string noClusters, string petalType, Color32 colour)
    {
        if (noClusters == "two")
        {
            cluster1.SetActive(true);
            SetPetalsSprite(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite(petalType), cluster1);
            SetPetalsColour(colour, cluster1);
        }
        if (noClusters == "three")
        {
            cluster1.SetActive(true);
            SetPetalsSprite(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite(petalType), cluster1);
            SetPetalsColour(colour, cluster1);
            cluster2.SetActive(true);
            SetPetalsSprite(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite(petalType), cluster2);
            SetPetalsColour(colour, cluster2);
        }
    }

    void OnMouseOver()
    {
        //Debug.Log("HOVER OVER FLWOER");
        if (Mouse.current.leftButton.wasPressedThisFrame)//Input.GetMouseButtonDown(0)
        {
            try {
                ClearBreedingVisual();
            }
            catch (Exception e) {
                Debug.Log("No plants found");
            }
            // Debug.Log("Left click");
            parent1 = gameObject;
            // visual representation of breeding plants
            GameObject crossBreedGO1 = GameManager.Instance.crossbreedVisualBox.transform.GetChild(0).GetChild(0).gameObject;
            crossBreedGO1.GetComponent<PlantController>().plant = this.plant;
            crossBreedGO1.GetComponent<PlantController>().ResetPlantCharacteristics();
            crossBreedGO1.SetActive(true);
            return;
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)//Input.GetMouseButtonDown(1)
        {
            //Debug.Log("Right click");
            if (parent1 != null && parent1 != gameObject)
            {
                if (parent1.GetComponent<PlantController>().plant.maxGenotypes == this.plant.maxGenotypes)
                {
                    GameObject crossBreedGO2 = GameManager.Instance.crossbreedVisualBox.transform.GetChild(1).GetChild(0).gameObject;
                    crossBreedGO2.GetComponent<PlantController>().plant = this.plant;
                    crossBreedGO2.GetComponent<PlantController>().ResetPlantCharacteristics();
                    crossBreedGO2.SetActive(true);
                    
                    GenerateChildSeed(parent1, gameObject);
                    
                }
                //GenerateChildPlant(parent1, gameObject);

            }
        }
    }
    public void ClearBreedingVisual() {
        GameObject crossBreedGO1 = GameManager.Instance.crossbreedVisualBox.transform.GetChild(0).GetChild(0).gameObject;
        GameObject crossBreedGO2 = GameManager.Instance.crossbreedVisualBox.transform.GetChild(1).GetChild(0).gameObject;
        crossBreedGO1.SetActive(false);
        crossBreedGO2.SetActive(false);
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
        GameObject inventory;
        try {
            inventory = GameObject.Find("seedContainer").gameObject;
        }
        catch (Exception e) {
            Debug.Log("Seed container is inactive. Make sure you're inside the greenhouse");
            return;  
        }
        //GameObject inventory = GameObject.Find("Inventory").transform.GetChild(0).GetChild(0).gameObject;
        int noOfChildren = UnityEngine.Random.Range(4, 8);
        for (int i = 0; i < noOfChildren; i++)
        {

            GameObject childSeedGO = Instantiate(seedPrefab, new Vector3(0, 0, 0), Quaternion.identity, inventory.transform);
            childSeedGO.name = "seed";
            
            childSeedGO.GetComponent<SeedController>().seed = new Seed();
            childSeedGO.GetComponent<SeedController>().seed.category = parent1.GetComponent<PlantController>().plant.category;
            GeneratePlants.CombineGametes(parent1.GetComponent<PlantController>().plant, parent2.GetComponent<PlantController>().plant, childSeedGO.GetComponent<SeedController>().seed);
        }
        RemoveOnePlantFromStack(parent1);
        RemoveOnePlantFromStack(parent2);
        if (GameVars.story.variablesState["tutorialpt1"].ToString() == "incomplete")
        {
            GameVars.story.ChoosePathString("tutorial_pt2");
        }


    }

    public void RemoveOnePlantFromStack(GameObject plant)
    {
        GameObject itemSlot = plant.transform.parent.parent.gameObject;
        DestroyImmediate(plant);
        try
        {
            itemSlot.GetComponent<SlotQuantity>().UpdateQuantityText();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }


    }
    public void SetPlantInfo()
    {
        WriteToTextObject(plant.genotypes);
    }

    public void DisplayPlantInfo()
    {
        GameManager.Instance.DisplayItemInfo(transform.position, "plant", plant.description);
    }
    public void RemovePlantInfo()
    {
        GameManager.Instance.RemoveItemInfo();
    }
    public void WriteToTextObject(Dictionary<string, string[]> genotype)
    {
        //plant.description += $"{name} : {geneticInfo[0]} {geneticInfo[1]}\n";
        plant.description = "";
        plant.description += $"Genotypes : {genotype.Count}\n\n";
        foreach (var item in genotype)
        {
            plant.description += $"{item.Key} : {item.Value[0]} {item.Value[1]}\n";
        }
    }


}
