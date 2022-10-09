using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    
    public Task task;// = new Task();
    [SerializeField]
    TextMeshProUGUI reputationText;
    void Awake()
    {
        if (task==null) {
            task = new Task();
        }
        
    }
  

    public void DecrementDaysLeft() {
        task.daysLeft--;
        if (task.daysLeft<0) {
            GameManager.Instance.reputation--;
            Destroy(gameObject);
        }
        GameObject requirementText = gameObject.transform.GetChild(0).gameObject;
        //TextMeshProUGUI amountText = requirementText.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI deadlineText = requirementText.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        deadlineText.text = $"deadline: {task.daysLeft} days";
    }

}
