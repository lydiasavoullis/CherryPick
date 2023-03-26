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
    public GameObject center2;
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
        else
        {
            //audioSourcePop = gameObject.GetComponent<AudioSource>();
            //audioSourcePop.pitch = (UnityEngine.Random.Range(0.6f, .9f));
            //audioSourcePop.Play();
        }

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
        if (DragHandler.itemBeingDragged != null && DragHandler.itemBeingDragged.name.ToLower().Contains("plant") && transform.parent.tag == "sellSlot" && GeneratePlants.CheckIfDroppedPlantContainsAllDesiredPhenotypes(phenotypes, DragHandler.itemBeingDragged.GetComponent<PlantController>().plant))
        {

            DragHandler.itemBeingDragged.transform.SetParent(transform.parent);
            DragHandler.itemBeingDragged.transform.localPosition = Vector3.zero;
            transform.parent.parent.gameObject.GetComponent<SlotQuantity>().UpdateQuantityText();
        }
    }


    public void SetCurrentPhenotype()
    {
        
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        //GeneratePlants.genotypesRange;
        //plant.phenotypes
        SetPetals(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite($"petal_{plant.phenotypes["petalShape"]}"), center, SetColour(plant.phenotypes["colour"]));//GeneratePlants.CheckColour(plant.genotypes["colour"])

        try
        {
            stem.sprite = GetPhenotypeSprite(plant.phenotypes["height"]);//GeneratePlants.CheckHeight(plant.genotypes["height"])
            if (plant.phenotypes["height"] == "tall")
=======
        SetPetals(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite("petal"), center, SetColour(GeneratePlants.CheckColour(plant.genotypes["colour"])));//

        try
        {
            stem.sprite = GetPhenotypeSprite(GeneratePlants.CheckHeight(plant.genotypes["height"]));
            
            foreach (Image p in petals)
>>>>>>> parent of 4876e25 (Adding more plant genetics)
=======
        SetPetals(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite("petal"), center, SetColour(GeneratePlants.CheckColour(plant.genotypes["colour"])));//

        try
        {
            stem.sprite = GetPhenotypeSprite(GeneratePlants.CheckHeight(plant.genotypes["height"]));
            
            foreach (Image p in petals)
>>>>>>> parent of 4876e25 (Adding more plant genetics)
=======
        SetPetals(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite("petal"), center, SetColour(GeneratePlants.CheckColour(plant.genotypes["colour"])));//

        try
        {
            stem.sprite = GetPhenotypeSprite(GeneratePlants.CheckHeight(plant.genotypes["height"]));
            
            foreach (Image p in petals)
>>>>>>> parent of 4876e25 (Adding more plant genetics)
            {
                //p.sprite = GetPhenotypeSprite(GeneratePlants.CheckPetals(plant.genotypes["petals"]));
                
                //p.color = SetColour(GeneratePlants.CheckColour(plant.genotypes["colour"]));
            }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]), $"petal_{plant.phenotypes["petalShape"]}");
=======
            SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]));
            //SetColourSplit(GeneratePlants.CheckColourSplit(plant.genotypes["split"]));
>>>>>>> parent of 4876e25 (Adding more plant genetics)
=======
            SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]));
            //SetColourSplit(GeneratePlants.CheckColourSplit(plant.genotypes["split"]));
>>>>>>> parent of 4876e25 (Adding more plant genetics)
=======
            SetClustersActive(GeneratePlants.CheckClusters(plant.genotypes["clusters"]));
            //SetColourSplit(GeneratePlants.CheckColourSplit(plant.genotypes["split"]));
>>>>>>> parent of 4876e25 (Adding more plant genetics)

        }
        catch (Exception e)
        {
        }

    }
    //for new dynamic petal creation system
    public void SetPetals(int petalNumber, Sprite petalSprite, GameObject center, Color32 color) {
        //for (int i = 0; i < center.transform.childCount;i++) {
        //    Destroy(center.transform.GetChild(i));
        //}
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;
        Gradient gradient;
        gradient = new Gradient();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.magenta;
        colorKey[1].time = 1.0f;
        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        for (int i = 0; i < petalNumber; i++)
        {
            GameObject petalGO = Instantiate(petalPrefab, new Vector3(0, 0, 0), Quaternion.identity, center.transform);
            petalGO.name = "petal";
            petalGO.GetComponent<Image>().sprite = petalSprite;
            petalGO.GetComponent<Image>().color = color;
            petalGO.GetComponent<Image>().material.color = gradient.Evaluate(1);

            
        }
    }

    public Color32 SetColour(string colour)
    {
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
    public void SetClustersActive(string noClusters)
    {
        if (noClusters == "two")
        {
            cluster1.SetActive(true);
            SetPetals(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite("petal"), cluster1, SetColour(GeneratePlants.CheckColour(plant.genotypes["colour"])));
        }
        if (noClusters == "three")
        {
            cluster1.SetActive(true);
            SetPetals(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite("petal"), cluster1, SetColour(GeneratePlants.CheckColour(plant.genotypes["colour"])));
            cluster2.SetActive(true);
            SetPetals(GeneratePlants.CheckPetalsInt(plant.genotypes["petals"]), GetPhenotypeSprite("petal"), cluster2, SetColour(GeneratePlants.CheckColour(plant.genotypes["colour"])));
        }
    }
    public void SetColourSplit(string noSplit)
    {
        string geno = string.Join("", plant.genotypes["colour"]);
        if (geno.Any(char.IsUpper) && geno.Any(char.IsLower))
        {
            string[] colour1 = { geno.Substring(0, 1), geno.Substring(0, 1) };

            string[] colour2 = { geno.Substring(1, 1), geno.Substring(1, 1) };

            if (noSplit == "two")
            {
                petals[1].color = SetColour(GeneratePlants.CheckColour(colour1));
                petals[2].color = SetColour(GeneratePlants.CheckColour(colour1));
            }
            if (noSplit == "three")
            {
                petals[1].color = SetColour(GeneratePlants.CheckColour(colour1));
                petals[2].color = SetColour(GeneratePlants.CheckColour(colour2));
            }
        }
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
                if (parent1.GetComponent<PlantController>().plant.maxGenotypes == this.plant.maxGenotypes)
                {
                    GenerateChildSeed(parent1, gameObject);
                }
                //GenerateChildPlant(parent1, gameObject);

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
