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
    public GameObject leafPrefab;
    public GameObject center;
    //public GameObject center2;
    public GameObject leaves_right;
    public GameObject leaves_left;
    //public GameObject background;
    public Phenotype[] phenotypes;
    public AudioSource audioSourcePop;
    Vector3 oldSizeL = new Vector3(30f, 30f);
    Vector3 oldSizeR = new Vector3(30f, 30f);
    private void Start()
    {
        if (plant == null)
        {
            int plantLevel = UnityEngine.Random.Range(1, GameManager.Instance.level+1);
            plant = GeneratePlants.GenerateRandomNewPlant(plantLevel);//GameManager.Instance.level
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
  
    public void ResetPlantCharacteristics() {
        //leaves_left.GetComponent<RectTransform>().sizeDelta = new Vector3(oldSizeL.x, oldSizeL.y, oldSizeL.z);
        //leaves_right.GetComponent<RectTransform>().sizeDelta = new Vector3(oldSizeR.x , oldSizeL.y, oldSizeR.z);
        ResetLeaves();
        GeneratePlants.ResizeLeaves(leaves_left, oldSizeL);
        GeneratePlants.ResizeLeaves(leaves_right, oldSizeR);
        ClearAllPetals(center, cluster1, cluster2);
        cluster2.SetActive(false);
        cluster1.SetActive(false);
        CenterColourChange(new Color32(224, 219, 94, 255));
        SetPlantInfo();
        SetCurrentPhenotype();
    }
    //get to prefab state
    public void ResetLeaves()
    {
        for (int i = 0; i < leaves_left.transform.childCount; i++)
        {
            Destroy(leaves_left.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < leaves_right.transform.childCount; i++)
        {
            Destroy(leaves_right.transform.GetChild(i).gameObject);
        }
    }
    void CenterColourChange(Color32 colour) {
        center.GetComponent<Image>().color = colour;
        cluster2.GetComponent<Image>().color = colour;
        cluster1.GetComponent<Image>().color = colour;
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
    public void ResizeLeaves() {
        float multiplier = 1.5f;
        Vector3 oldSizeL = leaves_left.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta;
        Vector3 oldSizeR = leaves_right.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta;
        GeneratePlants.ResizeLeaves(leaves_left, new Vector3(oldSizeL.x * multiplier, oldSizeL.y * multiplier, oldSizeL.z));
        GeneratePlants.ResizeLeaves(leaves_right, new Vector3(oldSizeR.x * multiplier, oldSizeR.y * multiplier, oldSizeR.z));
    }
    //set how the plant LOOKS
    //try getting from phenotype list now
    public void SetCurrentPhenotype()
    {
        //{ "colourR", "height", "petals","clusters", "petalShape", "leafShapeGene", "colourB", "colourG", "leafQuantityGene", "centerColourGene", "centerShapeGene"};

        //GeneratePlants.genotypesRange;
        //plant.phenotypes
        //center shape
        //for a low level plant
        stem.sprite = GetPhenotypeSprite(plant.phenotypes[1]);//all types will be tall or short sprite
        Color32 redSpectrum = SetColourR(plant.phenotypes[0]);//all types have red spectrum
        Color32 blueSpectrum;
        Color32 greenSpectrum;
        if (plant.phenotypes.Count <9)
        {
            SetLeavesQuantity(leaves_left, 1, leafPrefab, 0);
            SetLeavesQuantity(leaves_right, 1, leafPrefab, 180);
            if (plant.phenotypes[1] == "tall")
            {
                ResizeLeaves();
            }
            if (plant.phenotypes.Count == 2) {
                SetPetalsSprite(5, GetPhenotypeSprite($"petal_round"), center, redSpectrum);
                SetPetalsColour(redSpectrum, center);
                return;
            }
            if (plant.phenotypes.Count == 4)
            {
                SetPetalsSprite(Int32.Parse(plant.phenotypes[2]), GetPhenotypeSprite($"petal_round"), center, redSpectrum);
                SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]), $"petal_round", redSpectrum);
                //SetPetalColourForAllClusters(plant.phenotypes[3], redSpectrum);
                return;
            }
            if (plant.phenotypes.Count > 4)
            {
                GeneratePlants.ChangeLeavesSprite(leaves_left, GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}"));
                GeneratePlants.ChangeLeavesSprite(leaves_right, GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}"));
                if (plant.phenotypes.Count == 6) {
                    SetPetalsSprite(Int32.Parse(plant.phenotypes[2]), GetPhenotypeSprite($"petal_{plant.phenotypes[4]}"), center, redSpectrum);
                    SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]), $"petal_{plant.phenotypes[4]}", redSpectrum);
                    return;
                }
                if (plant.phenotypes.Count == 8)
                {
                    blueSpectrum = SetColourB(redSpectrum, plant.phenotypes[6]);
                    greenSpectrum = SetColourG(blueSpectrum, plant.phenotypes[7]);
                    SetPetalsSprite(Int32.Parse(plant.phenotypes[2]), GetPhenotypeSprite($"petal_{plant.phenotypes[4]}"), center, greenSpectrum);
                    SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]), $"petal_{plant.phenotypes[4]}", greenSpectrum);
                    SetPetalColourForAllClusters(plant.phenotypes[3], greenSpectrum);
                }

            }
            
            return;
        }
        if (plant.phenotypes.Count > 8)
        {
            blueSpectrum = SetColourB(redSpectrum, plant.phenotypes[6]);
            greenSpectrum = SetColourG(blueSpectrum, plant.phenotypes[7]);
            SetPetalsSprite(Int32.Parse(plant.phenotypes[2]), GetPhenotypeSprite($"petal_{plant.phenotypes[4]}"), center, greenSpectrum);
            SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]), $"petal_{plant.phenotypes[4]}", greenSpectrum);
            SetPetalColourForAllClusters(plant.phenotypes[3], greenSpectrum);
            if (plant.phenotypes[8] == "4")
            {
                SetLeavesQuantity(leaves_left, 2, leafPrefab, 0);
                SetLeavesQuantity(leaves_right, 2, leafPrefab, 180);

            }
            else
            {

                SetLeavesQuantity(leaves_left, 1, leafPrefab, 0);
                SetLeavesQuantity(leaves_right, 1, leafPrefab, 180);
            }
        }
        else
        {
            SetLeavesQuantity(leaves_left, 1, leafPrefab, 0);
            SetLeavesQuantity(leaves_right, 1, leafPrefab, 180);
        }
        if (plant.phenotypes[1] == "tall")
        {
            ResizeLeaves();
        }
        if (plant.phenotypes.Count == 10)
        {
            CenterColourChange(GetCenterColour(plant.phenotypes[9]));
            //SetCenterSprite(GetPhenotypeSprite($"center_{plant.phenotypes[10]}"));
        }
        //stem.sprite = GetPhenotypeSprite(plant.phenotypes[1]);//GeneratePlants.CheckHeight(plant.genotypes["height"])

        return;
        
    }
    public void SetPetalColourForAllClusters(string clustersNumber, Color32 colour) {
        switch (clustersNumber)
        {
            case "one":
                ModifyPetalsColour(colour, center);
                break;
            case "two":
                ModifyPetalsColour(colour, center);
                ModifyPetalsColour(colour, cluster1);
                break;
            case "three":
                ModifyPetalsColour(colour, center);
                ModifyPetalsColour(colour, cluster1);
                ModifyPetalsColour(colour, cluster2);
                break;
            default:
                ModifyPetalsColour(colour, center);
                break;
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
    public void SetPetalsSprite(int petalNumber, Sprite petalSprite, GameObject center, Color32 colour) {
        //for (int i = 0; i < center.transform.childCount;i++) {
        //    Destroy(center.transform.GetChild(i));
        //}
        for (int i = 0; i < petalNumber; i++)
        {
            GameObject petalGO = Instantiate(petalPrefab, new Vector3(0, 0, 0), Quaternion.identity, center.transform);
            petalGO.name = "petal";
            petalGO.GetComponent<Image>().sprite = petalSprite;
        }
        SetPetalsColour(colour, center);
    }
    public void SetCenterSprite(Sprite centerSprite)
    {
        center.GetComponent<Image>().sprite = centerSprite;
        cluster1.GetComponent<Image>().sprite = centerSprite;
        cluster2.GetComponent<Image>().sprite = centerSprite;
    }
    public static void SetLeavesQuantity(GameObject leaves_container, int quantity, GameObject leafPrefab, float rot)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject leafGO = Instantiate(leafPrefab, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0,rot,0)), leaves_container.transform);
            leafGO.name = "leaf";
            leafGO.transform.localPosition = Vector3.zero;
        }
    }
    //public void SetLeavesSprite(string leafType)
    //{
    //    //for (int i = 0; i < center.transform.childCount;i++) {
    //    //    Destroy(center.transform.GetChild(i));
    //    //}
    //    switch (leafType) {
    //        case "round":
    //            leaves_left.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
    //            leaves_right.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
    //            break;
    //        case "pointed":
    //            leaves_left.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
    //            leaves_right.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
    //            break;
    //        case "jagged":
    //            leaves_left.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
    //            leaves_right.GetComponent<Image>().sprite = GetPhenotypeSprite($"leaf_{plant.phenotypes[5]}");
    //            break;
    //        default:
    //            break;
    //    }

    //}
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
    public Color32 GetCenterColour(string colour) {
        Color32 color;
        //Debug.Log(colour);
        switch (colour)
        {
            case "brown":
                color = new Color32(63, 39, 26, 255);
                return color;
            case "green":
                return color = new Color32(46, 111, 62, 255);
            case "yellow":
                return color = new Color32(224, 219, 94, 255);
            default:
                break;
        }
        return Color.yellow;
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
            SetPetalsSprite(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite(petalType), cluster1, colour);
            //SetPetalsColour(colour, center);
        }
        if (noClusters == "three")
        {
            cluster1.SetActive(true);
            SetPetalsSprite(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite(petalType), cluster1, colour);
            //SetPetalsColour(colour, cluster1);
            cluster2.SetActive(true);
            SetPetalsSprite(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite(petalType), cluster2, colour);
            //SetPetalsColour(colour, cluster2);
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
            crossBreedGO1.transform.localPosition = Vector3.zero;
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
                    crossBreedGO2.transform.localPosition = Vector3.zero;
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
        childPlantGO.transform.localPosition = Vector3.zero;

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
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            Debug.Log("plant bred");
            GameObject.FindGameObjectWithTag("audioManager").GetComponent<AudioManager>().Play("pop");
            //gameObject.GetComponent<AudioSource>().Play();
        }
        
        for (int i = 0; i < noOfChildren; i++)
        {

            GameObject childSeedGO = Instantiate(seedPrefab, new Vector3(0, 0, 0), Quaternion.identity, inventory.transform);
            childSeedGO.name = "seed";
            
            childSeedGO.GetComponent<SeedController>().seed = new Seed();
            childSeedGO.GetComponent<SeedController>().seed.category = parent1.GetComponent<PlantController>().plant.category;
            childSeedGO.GetComponent<SeedController>().seed.growthDuration = childSeedGO.GetComponent<SeedController>().seed.category;
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
