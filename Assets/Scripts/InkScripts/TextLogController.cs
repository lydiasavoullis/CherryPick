using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLogController 
{

    //public GameObject textLogBox;
    //public GameObject textLogList;
    public TextLogController()
    {//GameObject textLogList, GameObject textLogBox
        //this.textLogBox = textLogBox;
        //this.textLogList = textLogList;
    }
    public void AddToTextLog(string text, GameObject textLogBox, GameObject textLogList)
    {
        GameVars.loadedTextLog.Add(text);//add current string to save list
        GameObject txt = GameObject.Instantiate(textLogBox) as GameObject;
        txt.transform.SetParent(textLogList.transform, false);
        txt.GetComponent<TextMeshProUGUI>().text = text;
    }
    //so we don't add this new text to the text log when it is loaded
    public void AddToTextLogOnLoad(string text, GameObject textLogList, GameObject textLogBox)
    {
        //GameVars.loadedTextLog.Add(text);//add current string to save list
        GameObject txt = GameObject.Instantiate(textLogBox) as GameObject;
        txt.transform.SetParent(textLogList.transform, false);
        txt.GetComponent<TextMeshProUGUI>().text = text;
    }
    public void LoadTextLogContent(GameObject textLogList, GameObject textLogBox)
    {
        foreach (Transform child in textLogList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (string s in GameVars.loadedTextLog)
        {

            AddToTextLogOnLoad(s, textLogList, textLogBox);
        }
    }
}