using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GeneratePlants
{
    //always lowercase
    static string heightGene = "t";
    static string colourGene = "r";
    static string[] genotypesRange = { "colour", "height"};
    public static Plant GenerateRandomNewPlant() {
        Plant plant = new Plant();
        plant.genotypes.Add("colour", NewGeno(colourGene));
        plant.genotypes.Add("height", NewGeno(heightGene));

        plant.phenotypes.Add(CheckColour(plant.genotypes["colour"]));
        plant.phenotypes.Add(CheckHeight(plant.genotypes["height"]));
        return plant;
    }
    public static List<string> GetPlantPhenotype(Plant plant) {
        List<string> phenotypes = new List<string>();
        phenotypes.Add(CheckColour(plant.genotypes["colour"]));
        phenotypes.Add(CheckHeight(plant.genotypes["height"]));
        return phenotypes;
    }
    public static Plant GenerateHeterozygousPlant()
    {
        Plant plant = new Plant();
        plant.phenotypes.Add(CheckColour(plant.genotypes["colour"]));
        plant.phenotypes.Add(CheckHeight(plant.genotypes["height"]));
        return plant;
    }
    public static void CombineGametes(Plant plant1, Plant plant2, Plant childPlant)
    {
        foreach (string s in genotypesRange)
        {
            childPlant.genotypes.Add(s, CombineGeno(plant1.genotypes[s], plant2.genotypes[s]));
        }

        childPlant.phenotypes.Add(CheckColour(childPlant.genotypes["colour"]));
        childPlant.phenotypes.Add(CheckHeight(childPlant.genotypes["height"]));
    }
    public static void CombineGametes(Plant plant1, Plant plant2, Seed childPlant)
    {
        foreach (string s in genotypesRange) {
            childPlant.genotypes.Add(s, CombineGeno(plant1.genotypes[s], plant2.genotypes[s]));
        }
    }
    public static string[] CombineGeno(string[] gen1, string[] gen2) {
        string[] childType = { gen1[Random.Range(0, 2)], gen2[Random.Range(0, 2)] };
        return childType;
    }
    public static string[] NewHetGeno(string type)
    {
        return new string[2] { type.ToUpper(), type };
    }

    public static string[] NewGeno(string type) {
        int scenario = Random.Range(0, 4);
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
    public static bool CheckIfTwoPlantsAreTheSame(Plant plant1, Plant plant2)
    {
        foreach (string s in genotypesRange) {
            if (!Enumerable.SequenceEqual(plant1.genotypes[s].OrderBy(e => e), plant2.genotypes[s].OrderBy(e => e)))
            {
                return false;
            }
        }
        return true;
    }
    public static bool CheckIfTwoPlantsLookTheSame(Plant plant1, Plant plant2)
    {
        foreach (string s in genotypesRange)
        {
            if (!Enumerable.SequenceEqual(plant1.phenotypes.OrderBy(e => e), plant2.phenotypes.OrderBy(e => e)))
            {
                return false;
            }
        }
        return true;
    }
}
