using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CustomerController : MonoBehaviour
{
    int tasksPerDay = 3;
    //bool interactionFinished = true;
    [SerializeField]
    GameObject customerPrefab;
    [SerializeField]
    GameObject speechPrefab;
    [SerializeField]
    GameObject speechContainer;
    [SerializeField]
    GameObject taskObject;
    [SerializeField]
    GameObject endOfDayNotice;
    //int orderMaxQuantity = 3;
    bool canServeNewCustomer = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GiveNewTask());
    }
    public void GiveNewTaskButton() {
        if (canServeNewCustomer)
        {
            ClearCustomers();
            StartCoroutine(GiveNewTask());
        }
        
    }
    public IEnumerator GiveNewTask() {
       
        ClearCustomers();
        if (GameManager.Instance.tasksGivenToday < tasksPerDay)
        {
            //cannot serve while customer is in action
            canServeNewCustomer = false;
            //start new customer interaction
            NewCustomer();
            yield return new WaitForSeconds(0.8f);
            //interactionFinished = false;
            ShowTask();
            canServeNewCustomer = true;
            //yield return new WaitUntil(() => interactionFinished);
        }
        else {
            Instantiate(endOfDayNotice, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        }
        yield return null;
    }

    public void NewCustomer() {
        if (GameManager.Instance.timeOfDay=="night") {
            GameManager.Instance.timeOfDay = "day";
            GameManager.Instance.ChangeBackground();
        }
        GameManager.Instance.tasksGivenToday++;
        Vector3 rot = new Vector3(0, 0, -90);
        GameObject newCustomer = Instantiate(customerPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        newCustomer.transform.SetAsFirstSibling();
        newCustomer.transform.position = Vector3.zero;
        GameObject speechBubble = Instantiate(speechPrefab, new Vector3(0, 0, 0), speechPrefab.transform.rotation, speechContainer.transform);
        //speechBubble.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = taskObject.transform.gameObject.GetComponent<CreateTask>().GenerateTask();
    }
    public void ClearCustomers() {
        for (int i = 0; i<gameObject.transform.childCount; i++) {
            Destroy(gameObject.transform.GetChild(i).gameObject);
            Destroy(speechContainer.transform.GetChild(i).gameObject);
        }
    } 
    public void ShowTask() {
        taskObject.SetActive(true);
    }
   

}
