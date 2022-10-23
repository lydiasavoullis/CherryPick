using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject newSlotPrefab;
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject plantPrefab;
    [SerializeField]
    GameObject taskPrefab;
    [SerializeField]
    GameObject taskList;
    [SerializeField]
    GameObject fundsGO;
    [SerializeField]
    GameObject reputationGO;
    [SerializeField]
    GameObject moneyPopup;
    [SerializeField]
    GameObject audioManager;
    [SerializeField]
    GameObject background;
    [SerializeField]
    TextMeshProUGUI nextCustomerButton;
    public static GameManager Instance;
    public GameObject canvas;
    public GameObject infoBoxPrefab;
    public GameObject taskBoard;
    public GameObject taskContent;
    public TextMeshProUGUI reputationText;   
    GameObject infoBoxGO;
    public float posX=1.5f;
    public float posY =2f;
    public int day = 0;
    public int funds = 500;
    public int reputation = 0;
    public int tasksGivenToday = 0;
    public CustomerController taskController;
    public string timeOfDay = "day";
    private void Awake()
    {
        Instance = this;
    }
    public void ChangeBackground() {
        
        switch (GameVars.story.variablesState["end_of_day"])
        {
            case "false":
                background.GetComponent<ChangeBackground>().ChangeToImage("day");
                break;
            case "true":
                background.GetComponent<ChangeBackground>().ChangeToImage("night");
                break;
        }
    }
    public void GiftPlant(string plantInfo)
    {

        string[] infoParse = plantInfo.Split(',');
        int quantity = int.Parse(infoParse[0]);
        Plant plant = new Plant();
        Dictionary<string, string[]> plantGenotypes = new Dictionary<string, string[]>();
        for (int i = 1; i < infoParse.Length; i++)
        {
            string[] geneInfo = infoParse[i].Split(':');
            string[] genoPairs = geneInfo[1].Select(c => c.ToString()).ToArray();
            plantGenotypes.Add(geneInfo[0], genoPairs);

        }
        for (int i = 0; i < quantity;i++) {
            Plant newPlant = new Plant();
            newPlant.genotypes = plantGenotypes;
            newPlant.phenotypes = GeneratePlants.GetPlantPhenotype(newPlant);
            GameObject slot = Instantiate(newSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity, inventory.transform);
            slot.name = "slot";
            GameObject plantGO = Instantiate(plantPrefab, new Vector3(0, 0, 0), Quaternion.identity, slot.transform.GetChild(0));
            plantGO.GetComponent<PlantController>().plant = newPlant;
            plantGO.name = "plant";
        }
        
    }
    public int NewDay()
    {
        
        //timeOfDay = "night";
        //ChangeBackground();
        GameObject greenhousePlanter = GameObject.FindGameObjectWithTag("greenhouseContainer");
        for (int i=0; i < greenhousePlanter.transform.childCount; i++) {
            //get number of pots inside greenhouse and then get soil inside pot and then see if there is anything in the soil
            Transform soil = greenhousePlanter.transform.GetChild(i).GetChild(0);
            if (soil.childCount>0) {
                if(soil.GetChild(0).gameObject.TryGetComponent(out SeedController seedController) && soil.gameObject.GetComponent<Soil>().plantPotState.hydrationValue>0.2)
                {
                    seedController.GrowForOneDay();
                }
                soil.gameObject.GetComponent<Soil>().ChangeHydration(-0.25f);
                //greenhousePlanter.transform.GetChild(i).GetChild(0).gameObject.GetComponent<SeedController>().GrowForOneDay();
            }
            
        }
        CountDownDayForAllTasks();
        tasksGivenToday = 0;
        taskController.GiveNewTask();
        day++;
        return day;
    }
    public void CountDownDayForAllTasks() {
        taskBoard.SetActive(true);
        int taskNo = taskContent.transform.childCount;

        for (int i = 0; i<taskNo;i++) {
            if(taskContent.transform.GetChild(i).gameObject!=null) {
                taskContent.transform.GetChild(i).gameObject.GetComponent<TaskController>().DecrementDaysLeft();
            }
            taskNo = taskContent.transform.childCount;
        }
        reputationText.text = reputation.ToString();
    }
   
    public void DisplayItemInfo(Vector2 itemPos, string name, string description)
    {
        Vector2 pos = itemPos;
        pos.x += 0.5f;
        pos.y += 1.25f;
        if (infoBoxGO != null)
        {
            Destroy(infoBoxGO.gameObject);
        }

        infoBoxGO = Instantiate(infoBoxPrefab, pos, Quaternion.identity, canvas.transform);
        infoBoxGO.GetComponent<ItemInfo>().SetUp(name, description);

    }
    public void RemoveItemInfo()
    {
        if (infoBoxGO.gameObject != null)
        {
            Destroy(infoBoxGO.gameObject);
        }
    }
    /* SetTaskInfo generates a new task prefab and sets the text values and also assigns a task object to the TaskController class with data about the task*/
    public void SetTaskInfo(int quantity, int orderDeadline, string customerName, string phenotypeDescription, List<string> phenotypes)
    {
        GameObject newTaskPrefab = Instantiate(taskPrefab, new Vector3(0, 0, 0), Quaternion.identity, taskList.transform);
        newTaskPrefab.transform.SetAsFirstSibling();
        GameObject requirementText = newTaskPrefab.transform.GetChild(0).gameObject;
        TextMeshProUGUI nameText = requirementText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI phenotypeText = requirementText.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI amountText = requirementText.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI deadlineText = requirementText.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        GameObject slot = newTaskPrefab.transform.GetChild(1).GetChild(0).gameObject;//get item inside slot inside slot container
        Button sellBtn = newTaskPrefab.transform.GetChild(2).gameObject.GetComponent<Button>();

        amountText.text = $"quantity: {quantity}";
        nameText.text = $"name: {customerName}";
        phenotypeText.text = $"phenotype: {phenotypeDescription}";
        deadlineText.text = $"deadline: {orderDeadline} days";
       // Debug.Log($"{amountText.text} | {nameText.text} | {phenotypeText.text} | {deadlineText.text}");
        sellBtn.onClick.AddListener(delegate
        {


            if (slot.transform.childCount > 0)
            {
                List<string> desiredPheno = phenotypes;
                List<string> droppedPlantPheno = slot.transform.GetChild(0).gameObject.GetComponent<PlantController>().plant.phenotypes;
                //Debug.Log(droppedPlantPheno[0] + " " + desiredPheno[0] + " " + droppedPlantPheno[1] + " " + desiredPheno[1] + " ");
                //Debug.Log(droppedPlantPheno.Except(desiredPheno).ToList().Count == 0);

                if ((droppedPlantPheno.Except(desiredPheno).ToList().Count == 0) && slot.transform.childCount == quantity)
                {
                    int moneyCounter = 0;
                    int repCounter = 0;
                    for (int i = 0; i < slot.transform.childCount; i++)
                    {
                        moneyCounter += 50;
                        repCounter++;
                    }
                    GameManager.Instance.funds += moneyCounter;
                    GameManager.Instance.reputation += repCounter;

                    fundsGO.GetComponent<TextInteractionMethods>().UpdateText("funds");
                    reputationGO.GetComponent<TextInteractionMethods>().UpdateText("reputation");

                    GameObject popupPrefab = Instantiate(moneyPopup, newTaskPrefab.transform.position, Quaternion.identity, taskList.transform);
                    popupPrefab.transform.SetSiblingIndex(newTaskPrefab.transform.GetSiblingIndex());
                    popupPrefab.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"You earned ${moneyCounter}";
                    if (GameVars.story.variablesState["tutorialpt3"].ToString() == "incomplete")
                    {
                        GameVars.story.ChoosePathString("tutorial_pt4");
                    }
                    Destroy(newTaskPrefab);
                }
                

            }
        });
        Task task = newTaskPrefab.GetComponent<TaskController>().task;
        //task.description = requirementText.text;
        task.daysLeft = orderDeadline;
        task.customer = customerName;
        task.phenotypes = phenotypes;
        task.quantity = quantity;
        task.description = phenotypeDescription;
    }
    public Tuple<int, int, string, string, List<string>> GetTaskInfo(GameObject taskPrefab) {
        Task task = taskPrefab.GetComponent<TaskController>().task;
        string nameText = task.customer;
        string phenotypeText = task.description;
        //remove any text so string can be parsed into ints
        int amountText = task.quantity;
        int deadlineText = task.daysLeft;
        List<string> phenotypes = taskPrefab.GetComponent<TaskController>().task.phenotypes;
        Tuple<int, int, string, string, List<string>> taskInfo = new Tuple<int, int, string, string, List<string>>(amountText, deadlineText, nameText, phenotypeText, phenotypes);

        return taskInfo;

    }




}

public enum GameState
{

}
