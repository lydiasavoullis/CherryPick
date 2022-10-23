using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed
{
    //public string[] colour;
    //public string[] height;
    public Dictionary<string, string[]> genotypes = new Dictionary<string, string[]>();
    public int growthDuration = 1;
    public int timeGrowing = 0;
    public int maxGenotypes;
}
