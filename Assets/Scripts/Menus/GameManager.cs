using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject saveGameItems;
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
    Slider reputationSlider;
    [SerializeField]
    TextMeshProUGUI levelGO;
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
    GameObject punnetPrefab;
    [SerializeField]
    GameObject infoContainer;
    [SerializeField]
    GameObject[] notifications;
    [SerializeField]
    TextAsset storyJson;
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
    public static string loadedFileName = "";
    public int level = 1;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (loadedFileName != "") {
            GameVars.ResetStaticVariables(storyJson);
            saveGameItems.GetComponent<SaveInventoryItems>().LoadItems(loadedFileName);
            loadedFileName = "";
        }
    }
    public GameObject GetChildWithTag(string tag, Transform parent) {
        for (int i=0; i<parent.childCount;i++) {
            if (parent.GetChild(i).tag == tag) {
                return parent.GetChild(i).gameObject;
            }
        }
        Debug.Log($"GAME OBJECT WITH TAG {tag} for parent object {parent.name} was not found");
        return null;
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
            Transform soil = greenhousePlanter.transform.GetChild(i).GetChild(1);
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
            Transform soil = greenhousePlanter.transform.GetChild(i).GetChild(1);
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
                if (shop.activeInHierarchy == false) {
                    GenerateShopItems();
                    shop.SetActive(true);
                }
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
            newPlant.category = GeneratePlants.GenotypeCategory(newPlant);
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
                try {
                    taskContent.transform.GetChild(i).gameObject.GetComponent<TaskController>().DecrementDaysLeft();
                }
                catch (Exception e) {
                    Debug.Log(e);
                }
                
            }
            taskNo = taskContent.transform.childCount;
        }
        reputationText.text = reputation.ToString();
        CheckLevel();
    }

    public void CheckLevel() {
        if ($"LVL: {level}" != levelGO.text) {
            levelGO.text = $"LVL: {level}";
        }

        if (reputation >= 5)
        {
            level++;
            reputation = 0;
            reputationSlider.value = 0;
            levelGO.text = $"LVL: {level}";
            AddAvailablePunnetSquares();
        }
        else {
            reputationSlider.value = reputation;
        }
    }
    //take in character e.g. 'g'
    //all combinations Gg Gg GG gg
    //convert genotype => phenotype
    //instantiate punnet square using GeneManagement 
    public void AddAvailablePunnetSquares() {
        int phenotypes;
        if (level > 5)
        {
            return;
        }
        else
        {
            phenotypes = level * 2;
        }
        AddPunnetSquare(phenotypes - 1);
        AddPunnetSquare(phenotypes - 2);
    }
    public void ClearGenesAndAddAllAvailable(int level) {
        int phenotypes;
        if (level > 5)
        {
            return;
        }
        else
        {
            phenotypes = level * 2;
        }

        for (int i =0; i< genesContainer.transform.childCount; i++) {
            Destroy(genesContainer.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < phenotypes; i++)
        {
            AddPunnetSquare(i);
        }
    }
    public void AddPunnetSquare(int geneNo) {
        //get level and check genes available
        
        //level x 2 phenotype number get last two phenotypes in list
        string geneLetter1 = GeneratePlants.geneRange[geneNo];
        string genotype1 = GeneratePlants.genotypesRange[geneNo];
        //create punnet combinations from gene letter
        List<string[]> genePunnet1 = GetAllPunnetCombos(geneLetter1);
        //put gene letter in switch case and get phenotype for each gene combo
        //{ "colourR", "height", "petals","clusters", "petalShape", "leafShapeGene", "colourB", "colourG", "leafQuantityGene", "centerColourGene", "centerShapeGene"}
        string[] correspondingPhenotypes1 = GetAllGenotypePhenotypeCombos(genotype1, genePunnet1);

        GameObject punnetPrefab1 = Instantiate(punnetPrefab, new Vector3(0,0,0), Quaternion.identity, genesContainer.transform);
        punnetPrefab1.transform.localPosition = Vector3.zero;
        punnetPrefab1.GetComponent<GeneManagement>().title.text = char.ToUpper(genotype1[0]) + genotype1.Substring(1);// genotype1;
        punnetPrefab1.GetComponent<GeneManagement>().geneSquare[3].geneCombo.text = string.Join("",genePunnet1[0]);
        punnetPrefab1.GetComponent<GeneManagement>().geneSquare[3].phenotype.text = correspondingPhenotypes1[0];
        punnetPrefab1.GetComponent<GeneManagement>().geneSquare[2].geneCombo.text = string.Join("", genePunnet1[1]);
        punnetPrefab1.GetComponent<GeneManagement>().geneSquare[2].phenotype.text = correspondingPhenotypes1[1];
        punnetPrefab1.GetComponent<GeneManagement>().geneSquare[1].geneCombo.text = string.Join("", genePunnet1[2]);
        punnetPrefab1.GetComponent<GeneManagement>().geneSquare[1].phenotype.text = correspondingPhenotypes1[2];
        punnetPrefab1.GetComponent<GeneManagement>().geneSquare[0].geneCombo.text = string.Join("", genePunnet1[3]);
        punnetPrefab1.GetComponent<GeneManagement>().geneSquare[0].phenotype.text = correspondingPhenotypes1[3];
    }
    public string[] GetAllGenotypePhenotypeCombos(string genotype, List<string[]> genePunnet) {
        string combo1 = "";
        string combo2 = "";
        string combo3 = "";
        string combo4 = "";
        switch (genotype)
        {
            case "colourR":
                combo1 = GeneratePlants.CheckColourR(genePunnet[0]);
                combo2 = GeneratePlants.CheckColourR(genePunnet[1]);
                combo3 = GeneratePlants.CheckColourR(genePunnet[2]);
                combo4 = GeneratePlants.CheckColourR(genePunnet[3]);
                break;
            case "height":
                combo1 = GeneratePlants.CheckHeight(genePunnet[0]);
                combo2 = GeneratePlants.CheckHeight(genePunnet[1]);
                combo3 = GeneratePlants.CheckHeight(genePunnet[2]);
                combo4 = GeneratePlants.CheckHeight(genePunnet[3]);
                break;
            case "petals":
                combo1 = GeneratePlants.CheckPetals(genePunnet[0]);
                combo2 = GeneratePlants.CheckPetals(genePunnet[1]);
                combo3 = GeneratePlants.CheckPetals(genePunnet[2]);
                combo4 = GeneratePlants.CheckPetals(genePunnet[3]);
                break;
            case "clusters":
                combo1 = GeneratePlants.CheckClusters(genePunnet[0]);
                combo2 = GeneratePlants.CheckClusters(genePunnet[1]);
                combo3 = GeneratePlants.CheckClusters(genePunnet[2]);
                combo4 = GeneratePlants.CheckClusters(genePunnet[3]);
                break;
            case "petalShape":
                combo1 = GeneratePlants.CheckPetalShape(genePunnet[0]);
                combo2 = GeneratePlants.CheckPetalShape(genePunnet[1]);
                combo3 = GeneratePlants.CheckPetalShape(genePunnet[2]);
                combo4 = GeneratePlants.CheckPetalShape(genePunnet[3]);
                break;
            case "leafShapeGene":
                combo1 = GeneratePlants.CheckLeafShape(genePunnet[0]);
                combo2 = GeneratePlants.CheckLeafShape(genePunnet[1]);
                combo3 = GeneratePlants.CheckLeafShape(genePunnet[2]);
                combo4 = GeneratePlants.CheckLeafShape(genePunnet[3]);
                break;
            case "colourB":
                combo1 = GeneratePlants.CheckColourB(genePunnet[0]);
                combo2 = GeneratePlants.CheckColourB(genePunnet[1]);
                combo3 = GeneratePlants.CheckColourB(genePunnet[2]);
                combo4 = GeneratePlants.CheckColourB(genePunnet[3]);
                break;
            case "colourG":
                combo1 = GeneratePlants.CheckColourG(genePunnet[0]);
                combo2 = GeneratePlants.CheckColourG(genePunnet[1]);
                combo3 = GeneratePlants.CheckColourG(genePunnet[2]);
                combo4 = GeneratePlants.CheckColourG(genePunnet[3]);
                break;
            case "leafQuantityGene":
                combo1 = GeneratePlants.CheckLeafQuantity(genePunnet[0]);
                combo2 = GeneratePlants.CheckLeafQuantity(genePunnet[1]);
                combo3 = GeneratePlants.CheckLeafQuantity(genePunnet[2]);
                combo4 = GeneratePlants.CheckLeafQuantity(genePunnet[3]);
                break;
            case "centerColourGene":
                combo1 = GeneratePlants.CheckCenterColour(genePunnet[0]);
                combo2 = GeneratePlants.CheckCenterColour(genePunnet[1]);
                combo3 = GeneratePlants.CheckCenterColour(genePunnet[2]);
                combo4 = GeneratePlants.CheckCenterColour(genePunnet[3]);
                break;
            default:
                break;
        }
        string[] phenotypes = {combo1, combo2, combo3, combo4};
        return phenotypes;
    }
    public List<string[]> GetAllPunnetCombos(string geneLetter1) {
        List<string[]> genePunnet1 = new List<string[]>();
        genePunnet1.Add(new string[] { geneLetter1, geneLetter1 });
        genePunnet1.Add(new string[] { geneLetter1.ToUpper(), geneLetter1 });
        genePunnet1.Add(new string[] { geneLetter1.ToUpper(), geneLetter1 });
        genePunnet1.Add(new string[] { geneLetter1.ToUpper(), geneLetter1.ToUpper() });
        return genePunnet1;
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
        ActivateNotification("journal");


    }
    /* SetTaskInfo generates a new task prefab and sets the text values and also assigns a task object to the TaskController class with data about the task*/
    public void SetTaskInfo(int quantity, int orderDeadline, string customerName, string phenotypeDescription, List<string> phenotypes, string nextEvent)
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
        if (orderDeadline == -0) {
            deadlineText.text = $"deadline: none";
        }
        else {
            deadlineText.text = $"deadline: {orderDeadline} days";
        }
        
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
                        repCounter+=5;
                    }
                GameManager.Instance.funds += moneyCounter;
                GameManager.Instance.reputation += repCounter;
                CheckLevel();
                fundsGO.GetComponent<TextInteractionMethods>().UpdateText("funds");
                reputationGO.GetComponent<TextInteractionMethods>().UpdateText("reputation");
                GameVars.upcomingEvents.Add(nextEvent);
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
        task.nextEvent = nextEvent;
        taskBoard.SetActive(false);
    }
    public Tuple<int, int, string, string, List<string>, string> GetTaskInfo(GameObject taskPrefab) {
        Task task = taskPrefab.GetComponent<TaskController>().task;
        string nameText = task.customer;
        string phenotypeText = task.description;
        //remove any text so string can be parsed into ints
        int amountText = task.quantity;
        int deadlineText = task.daysLeft;
        List<string> phenotypes = taskPrefab.GetComponent<TaskController>().task.phenotypes;
        string taskNextEvent = task.nextEvent;
        Tuple<int, int, string, string, List<string>, string> taskInfo = new Tuple<int, int, string, string, List<string>, string>(amountText, deadlineText, nameText, phenotypeText, phenotypes, taskNextEvent);

        return taskInfo;

    }
    public void ActivateNotification(string type) {
        for (int i =0; i<notifications.Length;i++) {
            if (notifications[i].tag == $"{type}Notification") {
                notifications[i].SetActive(true);
            }
            if (notifications[i].tag == $"menuNotification")
            {
                notifications[i].SetActive(true);
            }
        }
        
    }



}

public enum GameState
{

}
