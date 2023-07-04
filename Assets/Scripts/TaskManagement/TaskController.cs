using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    
    public Task task;// = new Task();
    //[SerializeField]
    //TextMeshProUGUI reputationText;
    public List<string> requiredPhenotypes;
    
    void Awake()
    {
        if (task==null) {
            task = new Task();
        }
        
    }
  

    public void DecrementDaysLeft() {
        if (task.daysLeft!=-0) {
            task.daysLeft--;
        }
        if (task.daysLeft != -0 && task.daysLeft<0) {
            GameManager.Instance.reputation--;
            if (GameManager.Instance.reputation<0) {
                GameManager.Instance.reputation = 0;
            }
            Destroy(gameObject);
        }
        if (task.daysLeft != -0) {
            GameObject requirementText = gameObject.transform.GetChild(0).gameObject;
            //TextMeshProUGUI amountText = requirementText.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI deadlineText = requirementText.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
            deadlineText.text = $"deadline: {task.daysLeft} days";
        }
        
    }

}
