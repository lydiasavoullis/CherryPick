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
    GameObject frontScreen;
    [SerializeField]
    TextMeshProUGUI dayNum;
    [SerializeField]
    GameObject deadPlantPrefab;
    [SerializeField]
    GameObject shopItemPrefab;
    [SerializeField]
    GameObject shopPlantPrefab;
    [SerializeField]
    public GameObject shopContent;
    [SerializeField]
    GameObject shop;
    [SerializeField]
    GameObject newSlotPrefab;
    [SerializeField]
    public GameObject heaterPrefab;
    [SerializeField]
    public GameObject heaterContainer;
    [SerializeField]
    public GameObject inventory;
    [SerializeField]
    GameObject plantPrefab;
    [SerializeField]
    GameObject taskPrefab;
    [SerializeField]
    GameObject taskList;
    [SerializeField]
    GameObject tempGO;
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
    [SerializeField]
    GameObject greenHouseCanvas;
    [SerializeField]
    GameObject UICanvas;
    [SerializeField]
    public GameObject crossbreedVisualBox;
    [SerializeField]
    public GameObject customerProfilesContainer;
    [SerializeField]
    public GameObject customerProfileGO;
    [SerializeField]
    GameObject genesContainer;
    [SerializeField]
    GameObject infoContainer;
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
    public int nightTemp = 0;
    public CustomerController taskController;
    public string timeOfDay = "day";
    public int slotSize = 200;
    private void Awake()
    {
        Instance = this;
    }
    //make sure heater heat only counts after shop has closed
    public IEnumerator ChangeDay() {
        bool greenHouseActive = greenHouseCanvas.activeInHierarchy;
        if (!greenHouseActive) {
            greenHouseCanvas.SetActive(true);
        }
        
        GameObject greenhousePlanter = GameObject.FindGameObjectWithTag("greenhouseContainer");
        frontScreen.SetActive(true);
        for (int i = 0; i < greenhousePlanter.transform.childCount; i++)
        {
            //get number of pots inside greenhouse and then get soil inside pot and then see if there is anything in the soil
            Transform soil = greenhousePlanter.transform.GetChild(i).GetChild(0);
            try {
                soil.GetComponent<Soil>().plantPotState.isHeated = false;
            }
            catch (Exception e) { 
            }
            

        }
        frontScreen.GetComponent<ChangeBackground>().ChangeFrontScreen("night");
        dayNum.text = day.ToString();
        yield return new WaitForSeconds(1f);
        frontScreen.GetComponent<ChangeBackground>().ChangeFrontScreen("day");
        dayNum.text = (day).ToString();
        yield return new WaitForSeconds(1f);
        frontScreen.SetActive(false);

        yield return new WaitUntil(()=>!frontScreen.activeSelf);
        for (int i = 0; i < greenhousePlanter.transform.childCount; i++)
        {
            //get number of pots inside greenhouse and then get soil inside pot and then see if there is anything in the soil
            Transform soil = greenhousePlanter.transform.GetChild(i).GetChild(0);
            if (soil.childCount > 0)
            {
                if (!KillPlantIfTooCold(soil) && soil.GetChild(0).gameObject.TryGetComponent(out SeedController seedController) && soil.gameObject.GetComponent<Soil>().plantPotState.hydrationValue > 0.2)
                {
                    seedController.GrowForOneDay();
                }
                soil.gameObject.GetComponent<Soil>().ChangeHydration(-0.25f);
                //greenhousePlanter.transform.GetChild(i).GetChild(0).gameObject.GetComponent<SeedController>().GrowForOneDay();
            }

        }
        tempGO.SetActive(false);
        TurnHeatersOnOff(false);
        CountDownDayForAllTasks();
        tasksGivenToday = 0;
        taskController.GiveNewTask();

        if (!greenHouseActive) {
            greenHouseCanvas.SetActive(false);
        }
    }
    public void SetNewNightTemp()
    {
        nightTemp = UnityEngine.Random.Range(-6, 0);
        tempGO.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = nightTemp.ToString();
        tempGO.SetActive(true);
        if (nightTemp < 0)
        {
            TurnHeatersOnOff(true);
        }
    }
    public void SetNightTemp()
    {
        tempGO.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = nightTemp.ToString();
        tempGO.SetActive(true);
        if (nightTemp < 0)
        {
            TurnHeatersOnOff(true);
        }
    }
    public void GenerateShopItems()
    {
        for (int i = 0; i < shopContent.transform.childCount; i++)
        {
            Destroy(shopContent.transform.GetChild(i).gameObject);
        }
        //GameObject item = Instantiate(shopItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        //item.transform.GetComponent<Buy>().SetItemDetails("pot");
        //item = Instantiate(shopItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        //item.transform.GetComponent<Buy>().SetItemDetails("plant");
        //item = Instantiate(shopItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        //item.transform.GetComponent<Buy>().SetItemDetails("plant");
        //item = Instantiate(shopItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        //item.transform.GetComponent<Buy>().SetItemDetails("plant");
        //item = Instantiate(shopItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        //item.transform.GetComponent<Buy>().SetItemDetails("heater");
        AddShopItem("plant");
        AddShopItem("plant");
        AddShopItem("plant");
        AddShopItem("heater");
        AddShopItem("pot");

    }
    void AddShopItem(string itemName) {
        GameObject item = Instantiate(shopItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, shopContent.transform);
        item.transform.GetComponent<Buy>().SetItemDetails(itemName);
        item.transform.localPosition = Vector3.zero;


    }

    public void CloseShop() {
        GameVars.story.variablesState["shop_state"] = "closed";
        shop.SetActive(false);
    }
    public void IsShopOpen()
    {

        switch (GameVars.story.variablesState["shop_state"])
        {
            case "open":
                GenerateShopItems();
                shop.SetActive(true);
                break;
            case "closed":
                shop.SetActive(false);
                break;
        }
    }
    public void ChangeFunds(int money) {
        funds += money;
        fundsGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = funds.ToString();
    }
    /**
     * true = on false = off
     */
    public void TurnHeatersOnOff(bool heaterState) {
        for (int i =0; i<heaterContainer.transform.childCount;i++) {
            heaterContainer.transform.GetChild(i).GetChild(0).gameObject.SetActive(heaterState);
        }
    }
    
    public void ChangeBackground() {
        
        switch (GameVars.story.variablesState["end_of_day"])
        {
            case "false":
                background.GetComponent<ChangeBackground>().ChangeToImage("day");
                tempGO.SetActive(false);
                //TurnHeatersOnOff(false);
                break;
            case "true":
                background.GetComponent<ChangeBackground>().ChangeToImage("night");
                tempGO.SetActive(true);
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
    //if night temp <0 and no heater kill plant
    public bool KillPlantIfTooCold(Transform soil) {

        if (nightTemp<0 && !soil.gameObject.GetComponent<Soil>().plantPotState.isHeated) {
            Destroy(soil.GetChild(0).gameObject);
            GameObject deadPlantGO = Instantiate(deadPlantPrefab, new Vector3(0, 0, 0), Quaternion.identity, soil);
            deadPlantGO.name = "deadPlant";
            return true;
        }
        return false;
    }

    public int NewDay()
    {

        //timeOfDay = "night";
        //ChangeBackground();
        StartCoroutine(ChangeDay());//start animation
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
        pos.y += 1.60f;
        if (infoBoxGO != null)
        {
            Destroy(infoBoxGO.gameObject);
        }

        infoBoxGO = Instantiate(infoBoxPrefab, pos, Quaternion.identity, UICanvas.transform);
        infoBoxGO.GetComponent<ItemInfo>().SetUp(name, description);

    }
    //reposition info box if it is very large
    public Vector2 CalculateInfoPosition(string description, Vector2 pos) {
        string[] genes = description.Split(':');
        int descriptionSize = genes.Length;
        switch (descriptionSize) {
            case 0:
                pos.x += 0.5f;
                pos.y += 1.25f;
                return pos;
            default:
                pos.x += 0.5f;
                pos.y += 1.25f;
                return pos;
        }
    }
    public void RemoveItemInfo()
    {
        if (infoBoxGO.gameObject != null)
        {
            Destroy(infoBoxGO.gameObject);
        }
    }
    public void AddCharacterProfile(string name, Sprite sprite) {
        Transform profileContainer = customerProfilesContainer.transform;
        for (int i = 0; i< profileContainer.childCount;i++) {
            if (profileContainer.GetChild(i).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text == name) {
                return;
            }
        }
        //create new profile
        GameObject profilePrefab = Instantiate(customerProfileGO, new Vector3(0, 0, 0), Quaternion.identity, profileContainer);
        profilePrefab.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = sprite;
        profilePrefab.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = name;
        profilePrefab.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "A great customer";
        profilePrefab.transform.localPosition = Vector3.zero;


    }
    /* SetTaskInfo generates a new task prefab and sets the text values and also assigns a task object to the TaskController class with data about the task*/
    public void SetTaskInfo(int quantity, int orderDeadline, string customerName, string phenotypeDescription, List<string> phenotypes)
    {
        taskBoard.SetActive(true);
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
        sellBtn.onClick.AddListener((UnityEngine.Events.UnityAction)delegate
        {


            if (slot.transform.childCount == quantity)
            {
                Transform plantSlot = slot.transform.GetChild(0);
                List<string> desiredPheno = phenotypes;
                //List<string> droppedPlantPheno = plantSlot.gameObject.GetComponent<PlantController>().plant.phenotypes;
                for (int j = 0; j> plantSlot.childCount; j++) {
                    if (!GeneratePlants.CheckIfDroppedPlantContainsAllDesiredPhenotypes(desiredPheno, (Plant)plantSlot.gameObject.GetComponent<PlantController>().plant)) {
                        return;
                    }
                }
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
