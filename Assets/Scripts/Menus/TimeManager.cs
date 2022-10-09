using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dayText;
    // Start is called before the first frame update
    void Start()
    {
        dayText.text = GameManager.Instance.day.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartNewDay() {
        if (GameVars.story.variablesState["end_of_day"].ToString()=="true") {
            dayText.text = GameManager.Instance.NewDay().ToString();
            GameVars.story.ChoosePathString($"day_{dayText.text}");
            GameVars.story.variablesState["end_of_day"] = "false";
            GameManager.Instance.ChangeBackground();
            this.gameObject.SetActive(false);
            
        } 
        
    }
}
