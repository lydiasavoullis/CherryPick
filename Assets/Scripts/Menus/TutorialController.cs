using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class TutorialController : MonoBehaviour
{
    public bool hasBredFlowers;
    public TextMeshProUGUI tutorial;
    public GameObject tutorialBox;
    public TextAsset tutorialContent;
    public List<string> tutorialLines;
    public int tutorialIndex=0;
    private void Start()
    {
        tutorialLines = new List<string>();
        ReadDialogueToList();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {

            GetTutorialLine();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tutorialBox.activeInHierarchy)
            {
                tutorialBox.SetActive(false);
            }
            else {
                tutorialBox.SetActive(true);
            }
                
        }
    }
    public void GetTutorialLine() {
        tutorialBox.SetActive(true);
        if (tutorialLines.Count <= tutorialIndex)
        {
            tutorialIndex = 0;

        }   
        tutorial.text = tutorialLines[tutorialIndex];
        tutorialIndex++;
    }
    public void ReadDialogueToList(){
        string tutorialText = tutorialContent.text;
        string[] texts = tutorialText.Split('.');
        foreach (string s in texts) {
            tutorialLines.Add(s);
        }

    }

}

