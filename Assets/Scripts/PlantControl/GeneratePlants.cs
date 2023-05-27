using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class GeneratePlants
{
    //this class contains methods that alter plants in certain ways
    //always lowercase
    static string heightGene = "t";
    static string colourGene = "r";
    static string petalGene = "p";
    static string clustersGene = "c";
    static string petalShapeGene = "s";
    static string centerShapeGene = "h";
    static string centerColourGene = "e";
    static string petalSizeGene = "x";
    static string petalVariagatedGene = "u";
    static string leafQuantityGene = "q";
    static string colourB = "b";
    static string colourG = "g";
    static string leafSize = "l";
    static string leafShapeGene = "m";
    public static string[] genotypesRange = { "colourR", "height", "petals","clusters", "petalShape", "leafShapeGene", "colourB", "colourG", "leafQuantityGene", "centerColourGene"};
    static string[] geneRange =             { colourGene, heightGene, petalGene, clustersGene, petalShapeGene, leafShapeGene, colourB, colourG, leafQuantityGene, centerColourGene};
    //static Dictionary<string, string> genotypesAndGenes = new Dictionary<string, string>() { {"colour", colourGene }, { "height", heightGene }, { "petals", petalGene} };
    public static Plant GenerateRandomNewPlant(int category = 2) {
        Plant plant = new Plant();
        plant.category = category;
        plant.maxGenotypes = MaxGenotypes(plant);
        for (int i = 0;i< plant.maxGenotypes; i++) {
            plant.genotypes.Add(genotypesRange[i], NewGeno(geneRange[i]));
        }
        try {
            plant.phenotypes = GetPlantPhenotype(plant);
        }
        catch (Exception e) { 
        }
        
        return plant;
    }
    public static void ResizeLeaves(GameObject leaves_container, Vector3 oldSize)
    {
        for (int i = 0; i < leaves_container.transform.childCount; i++)
        {
            leaves_container.transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(oldSize.x, oldSize.y, oldSize.z);
        }
    }
    
    public static void ChangeLeavesSprite(GameObject leaves_container, Sprite sprite)
    {
        for (int i = 0; i < leaves_container.transform.childCount; i++)
        {
            leaves_container.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = sprite;
            //leaves_container.transform.GetChild(i).localRotation = Quaternion.Euler(rotation);
        }
    }
    
    
    //change number of genotypes available
    public static int MaxGenotypes(Plant plant) {
        switch (plant.category) {
            case 1:
                return 4;
            case 2:
                return 10;
            //case 3:
            //    return 12;
            //case 4:
            //    return 16;
            //case 5:
            //    return 20;
            default:
                return 4;
        }
    }
    public static int GenotypeCategory(Plant plant)
    {
        switch (plant.maxGenotypes)
        {
            case 4:
                return 1;
            case 9:
                return 2;
            //case 3:
            //    return 12;
            //case 4:
            //    return 16;
            //case 5:
            //    return 20;
            default:
                return 6;
        }
    }
    public static List<string> GetPlantPhenotype(Plant plant) {
        List<string> phenotypes = new List<string>();
        try {
            
            if (plant.category==1) {
                phenotypes.Add(CheckColourR(plant.genotypes["colourR"]));
                phenotypes.Add(CheckHeight(plant.genotypes["height"]));
                phenotypes.Add(CheckPetals(plant.genotypes["petals"]));
                phenotypes.Add(CheckClusters(plant.genotypes["clusters"]));
                return phenotypes;
            }
            //check 4 more phenotypes
            if (plant.category == 2)
            {
                phenotypes.Add(CheckColourR(plant.genotypes["colourR"]));
                phenotypes.Add(CheckHeight(plant.genotypes["height"]));
                phenotypes.Add(CheckPetals(plant.genotypes["petals"]));
                phenotypes.Add(CheckClusters(plant.genotypes["clusters"]));
                phenotypes.Add(CheckPetalShape(plant.genotypes["petalShape"]));
                phenotypes.Add(CheckLeafShape(plant.genotypes["leafShapeGene"]));
                phenotypes.Add(CheckColourB(plant.genotypes["colourB"]));
                phenotypes.Add(CheckColourG(plant.genotypes["colourG"]));
                phenotypes.Add(CheckLeafQuantity(plant.genotypes["leafQuantityGene"]));
                phenotypes.Add(CheckCenterColour(plant.genotypes["centerColourGene"]));
                return phenotypes;
            }
            //check 4 more phenotypes
            if (plant.category == 3)
            {
                return phenotypes;
            }
            //check 4 more phenotypes
            if (plant.category == 4)
            {
                return phenotypes;
            }
            //check 4 more phenotypes
            if (plant.category == 5)
            {
                return phenotypes;
            }
        }
        catch (Exception e) { 
        }
        
        return phenotypes;
    }
    //public static Plant GenerateHeterozygousPlant()
    //{
    //    Plant plant = new Plant();
    //    plant.phenotypes.Add(CheckColour(plant.genotypes["colour"]));
    //    plant.phenotypes.Add(CheckHeight(plant.genotypes["height"]));
    //    plant.phenotypes.Add(CheckPetals(plant.genotypes["petals"]));
    //    plant.phenotypes.Add(CheckClusters(plant.genotypes["clusters"]));
    //    return plant;
    //}
    public static void CombineGametes(Plant plant1, Plant plant2, Plant childPlant)
    {
        foreach (string s in genotypesRange)
        {
            childPlant.genotypes.Add(s, CombineGeno(plant1.genotypes[s], plant2.genotypes[s]));
        }
        childPlant.phenotypes = GetPlantPhenotype(childPlant);
    }
    public static void CombineGametes(Plant plant1, Plant plant2, Seed childPlant)
    {
        foreach (string s in plant1.genotypes.Keys) {
            childPlant.genotypes.Add(s, CombineGeno(plant1.genotypes[s], plant2.genotypes[s]));
        }
        childPlant.maxGenotypes = childPlant.genotypes.Count;
    }
    public static string[] CombineGeno(string[] gen1, string[] gen2) {
        string[] childType = { gen1[UnityEngine.Random.Range(0, 2)], gen2[UnityEngine.Random.Range(0, 2)] };
        return childType;
    }
    public static string[] NewHetGeno(string type)
    {
        return new string[2] { type.ToUpper(), type };
    }

    public static string[] NewGeno(string type) {
        int scenario = UnityEngine.Random.Range(0, 4);
        string[] newGenotype;
        switch (scenario) {
            case 0:
                newGenotype = new string[2]{type, type };
                return newGenotype;
            case 1:
                newGenotype = new string[2] { type.ToUpper(), type };
                return newGenotype;
            case 2:
                newGenotype = new string[2] { type.ToUpper(), type };
                return newGenotype;
            case 3:
                newGenotype = new string[2] { type.ToUpper(), type.ToUpper() };
                return newGenotype;

            default:
                break;
        }
        return new string[2]{type, type};
    }
    
    public static string CheckHeight(string[] genotype) {
        string geno = string.Join("", genotype);
        if (geno.Contains(heightGene + heightGene))
        {
            return "short";
        }
        else {
            return "tall";
        }
    }
    public static string CheckPetals(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(petalGene))
        {
            return "5";
        }
        else
        {
            return "6";
        }
    }
    public static int CheckPetalsInt(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(petalGene))
        {
            return 5;
        }
        else
        {
            return 6;
        }
    }
    public static string CheckColourR(string[] genotype) {
        string geno = string.Join("", genotype);
        if (geno.Contains(colourGene.ToLower()))
        {
            if (geno.Contains(colourGene.ToUpper()))
            {
                return "pink";
            }
            else
            {
                return "white";
            }
        }
        else {
            return "red";
        } 
    }
    public static string CheckCenterColour(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(centerColourGene.ToLower()))
        {
            if (geno.Contains(centerColourGene.ToUpper()))
            {
                return "green";
            }
            else
            {
                return "brown";
            }
        }
        else
        {
            return "yellow";
        }
    }
    public static string CheckLeafQuantity(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(leafQuantityGene.ToLower()))
        {
            if (geno.Contains(leafQuantityGene.ToUpper()))
            {
                return "2";
            }
            else
            {
                return "4";
            }
        }
        else
        {
            return "2";
        }
    }
    public static string CheckColourB(string[] genotype)
    {
        
        string geno = string.Join("", genotype);
        if (geno.Contains(colourB.ToLower()))
        {
            if (geno.Contains(colourB.ToUpper()))
            {
                return "light_blue";
            }
            else
            {
                return "white";
            }
        }
        else
        {
            return "blue";
        }
    }
    public static string CheckColourG(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(colourG.ToLower()))
        {
            if (geno.Contains(colourG.ToUpper()))
            {
                return "light_green";
            }
            else
            {
                return "white";
            }
        }
        else
        {
            return "green";
        }
    }
    public static string CheckLeafShape(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(leafShapeGene.ToLower()))
        {
            if (geno.Contains(leafShapeGene.ToUpper()))
            {
                return "jagged";
            }
            else
            {
                return "round";
            }
        }
        else
        {
            return "pointed";
        }
    }
    public static string CheckPetalShape(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(petalShapeGene.ToLower()))
        {
            if (geno.Contains(clustersGene.ToUpper()))
            {
                return "oval";
            }
            else
            {
                return "round";
            }
        }
        else
        {
            return "pointed";
        }
    }
    public static string CheckClusters(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(clustersGene.ToLower()))
        {
            if (geno.Contains(clustersGene.ToUpper()))
            {
                return "two";
            }
            else
            {
                return "one";
            }
        }
        else
        {
            return "three";
        }
    }
    public static bool CheckIfPlantHasPhenotype(Plant plant, string phenotype)
    {
        if (plant.phenotypes.Contains(phenotype))
        {
            return true;
        }
        else {
            return false;
        }
    }
    //dropped plant must contain all characteristics of desired plant, but dropped plant may have some that desired plant does not
    public static bool CheckIfDroppedPlantContainsAllDesiredPhenotypes(List<string> phenotypes, Plant droppedPlant) {
        foreach (string s in phenotypes) {
            if (!droppedPlant.phenotypes.Contains(s)) {
                return false;
            }
        }
        return true;
    }
    public static bool CheckIfTwoPlantsAreTheSame(Plant plant1, Plant plant2)
    {
        if (plant1.maxGenotypes!=plant2.maxGenotypes) {
            return false;
        }
        foreach (string s in plant1.genotypes.Keys) {
            if (!Enumerable.SequenceEqual(plant1.genotypes[s].OrderBy(e => e), plant2.genotypes[s].OrderBy(e => e)))
            {
                return false;
            }
        }
        return true;
    }
    public static bool CheckIfTwoPlantsLookTheSame(Plant plant1, Plant plant2)
    {
        if (plant1.maxGenotypes != plant2.maxGenotypes)
        {
            return false;
        }
        foreach (string s in plant1.genotypes.Keys)
        {
            if (!Enumerable.SequenceEqual(plant1.phenotypes.OrderBy(e => e), plant2.phenotypes.OrderBy(e => e)))
            {
                return false;
            }
        }
        return true;
    }
}
