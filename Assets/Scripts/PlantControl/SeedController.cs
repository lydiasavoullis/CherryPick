using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SeedController : MonoBehaviour
{
    public Plant plant;
    public Seed seed;
    public GameObject plantPrefab;
    //public int daysGrowing;//current day
    private void Start()
    {
        Seed seed = new Seed();
    }
    public void SpawnPlant() {
        Plant newPlant = new Plant();
        //newPlant.height = seed.height;
        //newPlant.colour = seed.colour;
        //newPlant.genotypes.Add("colour", newPlant.colour);
        //newPlant.genotypes.Add("height", newPlant.height);
        newPlant.genotypes = seed.genotypes;
        newPlant.phenotypes = GeneratePlants.GetPlantPhenotype(newPlant);
        GameObject plantGO = Instantiate(plantPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform.parent);
        plantGO.GetComponent<PlantController>().plant = newPlant;
        plantGO.name = "plant";
        Destroy(gameObject);
        //set all genotype seed info to new plant object info
        //spawn a plant prefab in this object's coordinates
        //destroy this object
    }
    public void GrowForOneDay() {
        seed.timeGrowing++;
        if(seed.growthDuration<= seed.timeGrowing)
        {
            SpawnPlant();
        }
    }
    public void DisplaySeedInfo()
    {
        GameManager.Instance.DisplayItemInfo(transform.position, "seed", "days growing: " + seed.timeGrowing + "\ndays left: "+ (seed.growthDuration- seed.timeGrowing));
    }
    public void RemoveSeedInfo()
    {
        GameManager.Instance.RemoveItemInfo();
    }

}
