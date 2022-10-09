using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadHighscore : MonoBehaviour
{
    public TextMeshProUGUI highscore;

    private void Start()
    {
        highscore.text = "highscore: " + PlayerPrefs.GetInt("highscore").ToString();
    }
}
