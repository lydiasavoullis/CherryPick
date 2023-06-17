using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class GeneManagement : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI title;
    [SerializeField]
    public GeneText[] geneSquare;
    
    private void Start()
    {
        //change to tuple
        List<KeyValuePair<string, string>> geneDictionary = new List<KeyValuePair<string, string>>();//phenotype, gene combo
        geneDictionary.Add(new KeyValuePair<string, string>("Tall", "TT"));
        geneDictionary.Add(new KeyValuePair<string, string>("Tall", "Tt"));
        geneDictionary.Add(new KeyValuePair<string, string>("Tall", "Tt"));
        geneDictionary.Add(new KeyValuePair<string, string>("Short", "tt"));
        PunnetSquare("Height", geneDictionary);
    }
    public void PunnetSquare(string name, List<KeyValuePair<string, string>> geneDictionary) {
        title.text = name;
        for (int i = 0; i<geneDictionary.Count; i++) {
            geneSquare[i].phenotype.GetComponent<TextMeshProUGUI>().text = geneDictionary.ElementAt(i).Key;
            geneSquare[i].geneCombo.GetComponent<TextMeshProUGUI>().text = geneDictionary.ElementAt(i).Value;
        }
    }
}
