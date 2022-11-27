using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GeneratePlants
{
    //always lowercase
    static string heightGene = "t";
    static string colourGene = "r";
    static string petalGene = "p";
    static string clustersGene = "c";
    static string colourSplitGene = "s";
    public static string[] genotypesRange = { "colour", "height", "petals","clusters", "split" };
    static string[] geneRange = { colourGene, heightGene, petalGene, clustersGene, colourSplitGene };
    //static Dictionary<string, string> genotypesAndGenes = new Dictionary<string, string>() { {"colour", colourGene }, { "height", heightGene }, { "petals", petalGene} };
    public static Plant GenerateRandomNewPlant() {
        Plant plant = new Plant();
        plant.maxGenotypes = MaxGenotypes();
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
    //change number of genotypes available
    public static int MaxGenotypes() {
        return 5;// UnityEngine.Random.Range(2, 6);
    }
    public static List<string> GetPlantPhenotype(Plant plant) {
        List<string> phenotypes = new List<string>();
        try {
            phenotypes.Add(CheckColour(plant.genotypes["colour"]));
            phenotypes.Add(CheckHeight(plant.genotypes["height"]));
            phenotypes.Add(CheckPetals(plant.genotypes["petals"]));
            phenotypes.Add(CheckClusters(plant.genotypes["clusters"]));
            phenotypes.Add(CheckColourSplit(plant.genotypes["split"]));
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
            return "four";
        }
        else
        {
            return "five";
        }
    }
    public static string CheckColour(string[] genotype) {
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
    public static string CheckColourSplit(string[] genotype)
    {
        string geno = string.Join("", genotype);
        if (geno.Contains(colourSplitGene.ToLower()))
        {
            if (geno.Contains(colourSplitGene.ToUpper()))
            {
                return "two";
            }
            else
            {
                return "three";
            }
        }
        else
        {
            return "one";
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
