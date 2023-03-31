using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CreateTask : MonoBehaviour
{
    //[SerializeField]
    //GameObject taskPrefab;
    //[SerializeField]
    //GameObject taskList;
    //[SerializeField]
    //GameObject funds;
    //[SerializeField]
    //GameObject reputation;
    //[SerializeField]
    //GameObject moneyPopup;
    //public static string[] names = { "Beatrice", "Alex", "Charlie"};
    public static Plant plant;
   
    public void GenerateTask(string info)
    {
        
        string[] infoParse = info.Split(',');
        string name = infoParse[0];
        int quantity = int.Parse(infoParse[1]);
        int days = int.Parse(infoParse[2]);
        Plant plant = new Plant();
        //int quantity = Random.Range(1, orderMaxQuantity + 1);
        //int orderDeadline = quantity * 2;
        //int nameIndex = Random.Range(0, names.Length);
        //string customerName = names[nameIndex];
        string phenotypeDescription = "";
        //Dictionary<string, string> phenotypes = new Dictionary<string, string>();
        for (int i= 3; i<infoParse.Length;i++) {
            string[] phenoPairs = infoParse[i].Split(':');
            plant.phenotypes.Add(phenoPairs[1]);
            
        }
        int count = 0;
        phenotypeDescription += "\n";
        foreach (string s in plant.phenotypes)
        {
            
            string label = GeneratePlants.genotypesRange[count];
            Debug.Log(s);
            phenotypeDescription += $"{label}: {s}\n";
            count++;
        }
        GameManager.Instance.SetTaskInfo(quantity, days, name, phenotypeDescription, plant.phenotypes);
        //return $"Hi, I'm {name}. I would like {info[1]} {phenotypeDescription} flower(s) please, and I need them in {days} days";
    }



}
