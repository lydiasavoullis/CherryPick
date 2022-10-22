using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class SaveInventoryItems : MonoBehaviour
{
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject greenhouse;
    string lastFilename;
    [SerializeField]
    Button saveSlot;
    [SerializeField]
    GameObject savesList;
    [SerializeField]
    GameObject plantPrefab;
    [SerializeField]
    GameObject seedPrefab;
    [SerializeField]
    GameObject potPrefab;
    [SerializeField]
    GameObject taskPrefab;
    [SerializeField]
    TextMeshProUGUI dayTextObject;
    [SerializeField]
    TextMeshProUGUI fundsTextObject;
    [SerializeField]
    TextMeshProUGUI reputationTextObject;
    [SerializeField]
    GameObject itemSlotPrefab;
    [SerializeField]
    GameObject taskBoard;
    [SerializeField]
    GameObject customerContainer;
    [SerializeField]
    GameObject speechContainer;
    [SerializeField]
    GameObject dialogueController;
    public List<Tuple<string, Dictionary<string, object>, int>> inventoryPlants = new List<Tuple<string, Dictionary<string, object>, int>>();
    //public List<Tuple<string, Dictionary<string, object>>> greenhousePlants = new List<Tuple<string, Dictionary<string, object>>>();
    //int quantity, int orderDeadline, string customerName, string phenotypeDescription, List<string> phenotypes
    public List<Tuple<int, int, string, string, List<string>>> taskBoardList = new List<Tuple<int, int, string, string, List<string>>>();
    //public List<float> plantPots = new List<float>();
    public List<Tuple<string, Dictionary<string, object>, float>> potsInGreenhouse = new List<Tuple<string, Dictionary<string, object>, float>>();
    public int day = 0;
    public int funds = 0;
    public int reputation = 0;

    private void Start()
    {
        PopulateScrollList();
    }
    
    public void SaveGame() {
        day = GameManager.Instance.day;
        funds = GameManager.Instance.funds;
        reputation = GameManager.Instance.reputation;
        SaveInventoryContents();
        SaveGreenhouseContents();
        SaveTasks();
        SaveSystem.SaveData(GenerateFileName(), this);
        CreateSaveSlot(lastFilename);
        //reset all this stored data
        inventoryPlants.Clear();
        potsInGreenhouse.Clear();
        taskBoardList.Clear();
    }
    public void SaveInventoryContents() {
        int counter = 0;
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            counter++;
            int quantity = inventory.transform.GetChild(i).GetChild(0).childCount;
            if (inventory.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.TryGetComponent(out SeedController seedController))//get slot, get inner slot and then
            {
                inventoryPlants.Add(new Tuple<string, Dictionary<string, object>, int>("seed", AddItemInfoToList(seedController.seed), quantity));
            }
            else if (inventory.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.TryGetComponent(out PlantController plantController))
            {
                inventoryPlants.Add(new Tuple<string, Dictionary<string, object>, int>("plant", AddItemInfoToList(plantController.plant), quantity));
            }

        }

        //Debug.Log("Numer of stored plants added: " + counter);
    }
    public void SaveGreenhouseContents()
    {
        //need to load individual pots and hydration value
        int counter = 0;
        for (int i = 0; i < greenhouse.transform.childCount; i++)
        {
            //Transform soilTransform = greenhouse.transform.GetChild(i).GetChild(0);
            Transform hydrationSliderTransform = greenhouse.transform.GetChild(i).GetChild(1);
            float hydrationValue = hydrationSliderTransform.gameObject.GetComponent<Slider>().value;
            Tuple<string, Dictionary<string, object>, float> itemInPot = new Tuple<string, Dictionary<string, object>, float>("empty", null, hydrationValue);
            if (greenhouse.transform.GetChild(i).GetChild(0).childCount!=0)
            {
                counter++;
                if (greenhouse.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.TryGetComponent(out SeedController seedController))
                {
                    itemInPot = new Tuple<string, Dictionary<string, object>, float>("seed", AddItemInfoToList(seedController.seed), hydrationValue);
                    
                }
                else if (greenhouse.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.TryGetComponent(out PlantController plantController))
                {
                    itemInPot = new Tuple<string, Dictionary<string, object>, float>("plant", AddItemInfoToList(plantController.plant), hydrationValue);
                }
            }
            potsInGreenhouse.Add(itemInPot);
        }
        //Debug.Log("Numer of growing plants added: " + counter);
    }
    public void LoadItems(string filename)
    {
        Debug.Log("Load items");
        //clear inventory and greenhouse
        ClearObjectChildren(inventory);
        ClearObjectChildren(greenhouse);
        ClearObjectChildren(taskBoard);
        ClearObjectChildren(customerContainer);
        ClearObjectChildren(speechContainer);
        SaveData data = SaveSystem.LoadData(filename);
        
        //progress variables
        GameManager.Instance.day = data.currentDay;
        GameManager.Instance.funds = data.funds;
        GameManager.Instance.reputation = data.reputation;
        dayTextObject.text = data.currentDay.ToString();
        fundsTextObject.text = data.funds.ToString();
        reputationTextObject.text = data.reputation.ToString();
        //INK
        GameVars.story.state.LoadJson(data.saveState);
        GameManager.Instance.ChangeBackground();
        GameVars.loadedCurrentSpeaker = data.currentSpeaker;
        GameVars.loadedSpeechPos = new Vector3(data.speechPos.Item1, data.speechPos.Item2, data.speechPos.Item3);
        //Debug.Log(this.name +" "+ (GameVars.loadedTextLog.Count -1));
        GameVars.loadedTextLog = data.storyLog;
        dialogueController.GetComponent<DialogueController>().LoadTextLog();
        GameVars.loadedChars = data.loadedChars;
        dialogueController.GetComponent<DialogueController>().LoadCharacters(data.loadedChars);
        GameVars.finishedTyping = true;
        if ((GameVars.loadedTextLog.Count)>0) {
            dialogueController.GetComponent<DialogueController>().LoadSpeech(GameVars.loadedTextLog[GameVars.loadedTextLog.Count - 1]);
        }
        
        //load taskboard
        LoadTasks(data);
        //Debug.Log("Load plants "+ data.inventoryPlants.Count());
        //load greenhouse
        LoadGreenhouse(data);
        //load inventory
        LoadInventory(data);
        
    }
    public void LoadInventory(SaveData data) {
        for (int i = 0; i < data.inventoryPlants.Count(); i++)
        {

            if (data.inventoryPlants[i].Item1 == "seed")
            {
                GameObject slotGO = Instantiate(itemSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity, inventory.transform);
                slotGO.name = "slot";
                GameObject seedGO = Instantiate(seedPrefab, new Vector3(0, 0, 0), Quaternion.identity, slotGO.transform.GetChild(0));
                seedGO.GetComponent<SeedController>().seed = LoadSeedFromList(data.inventoryPlants[i].Item2);
                seedGO.name = "seed";
                seedGO.transform.localPosition = Vector3.zero;
            }
            else if (data.inventoryPlants[i].Item1 == "plant")
            {
                GameObject slotGO = Instantiate(itemSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity, inventory.transform);
                slotGO.name = "slot";
                for (int x = 0; x < data.inventoryPlants[i].Item3; x++)
                {
                    //foreach item in slot
                    GameObject plantGO = Instantiate(plantPrefab, new Vector3(0, 0, 0), Quaternion.identity, slotGO.transform.GetChild(0));
                    plantGO.GetComponent<PlantController>().plant = LoadPlantFromList(data.inventoryPlants[i].Item2);
                    plantGO.name = "plant";
                    plantGO.transform.localPosition = Vector3.zero;
                }

                slotGO.GetComponent<SlotQuantity>().UpdateQuantityText();
            }

        }
    }
    public void LoadGreenhouse(SaveData data) {
        int counter = 0;
        for (int i = 0; i < data.potsInGreenhouse.Count; i++)//greenhouse.transform.childCount
        {
            GameObject plantPot = Instantiate(potPrefab, new Vector3(0, 0, 0), Quaternion.identity, greenhouse.transform);
            plantPot.name = "pot";
            plantPot.transform.GetChild(1).gameObject.GetComponent<Slider>().value = data.potsInGreenhouse[i].Item3;


            if (data.potsInGreenhouse[i].Item1 == "seed")
            {
                
                counter++;
                GameObject seedGO = Instantiate(seedPrefab, new Vector3(0, 0, 0), Quaternion.identity, plantPot.transform.GetChild(0));
                seedGO.GetComponent<SeedController>().seed = LoadSeedFromList(data.potsInGreenhouse[i].Item2);
                seedGO.name = "seed";
                seedGO.transform.localPosition = Vector3.zero;
            }
            else if (data.potsInGreenhouse[i].Item1 == "plant")
            {
                counter++;
                GameObject plantGO = Instantiate(plantPrefab, new Vector3(0, 0, 0), Quaternion.identity, plantPot.transform.GetChild(0));
                plantGO.GetComponent<PlantController>().plant = LoadPlantFromList(data.potsInGreenhouse[i].Item2);
                plantGO.name = "plant";
                plantGO.transform.localPosition = Vector3.zero;
            }
            else if (data.potsInGreenhouse[i].Item1 == "empty") {
                continue;

            }
        }
        Debug.Log("Pots: " + data.potsInGreenhouse.Count + " Planted: " + counter);
    }
    public void SaveTasks() {
        for (int i = 0; i < taskBoard.transform.childCount; i++)
        {
            taskBoardList.Add(GameManager.Instance.GetTaskInfo(taskBoard.transform.GetChild(i).gameObject));
        }
    }
    public void LoadTasks(SaveData data) {
        foreach (Tuple<int, int, string, string, List<string>> taskInfo in data.taskBoardList) {
            //GameObject taskGO = Instantiate(taskPrefab, new Vector3(0, 0, 0), Quaternion.identity, taskBoard.transform);
            //taskGO.transform.SetAsFirstSibling();
            //taskGO.name = "task";
            GameManager.Instance.SetTaskInfo(taskInfo.Item1, taskInfo.Item2, taskInfo.Item3, taskInfo.Item4, taskInfo.Item5);
            //taskBoardPrefab
        }
    }
    //delete all children of a game object container
    public void ClearObjectChildren(GameObject container) {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
    }

    #region test greenhouse methods
    public Seed LoadSeedFromList(Dictionary<string, object> geneticInfo)
    {
        Seed seed = new Seed();
        //seed.height = (string[])geneticInfo["height"];
        //seed.colour = (string[])geneticInfo["colour"];
        seed.genotypes = (Dictionary<string, string[]>)geneticInfo["genotypes"];
        seed.timeGrowing = (int)geneticInfo["timeGrowing"];
        seed.growthDuration = (int)geneticInfo["growthDuration"];
        return seed;
    }
    public Plant LoadPlantFromList(Dictionary<string, object> geneticInfo)
    {
        Plant plant = new Plant();
        //plant.height = (string[])geneticInfo["height"];
        //plant.colour = (string[])geneticInfo["colour"];
        //plant.genotypes = geneticInfo;
        //plant.genotypes.Add("colour", plant.height);
        //plant.genotypes.Add("height", plant.colour);
        plant.genotypes = (Dictionary<string, string[]>)geneticInfo["genotypes"];
        return plant;
    }
    public Dictionary<string, object> AddItemInfoToList(Seed seed)
    {
        Dictionary<string, object> geneticInfo = new Dictionary<string, object>();
        //geneticInfo.Add("height", seed.height);
        //geneticInfo.Add("colour", seed.colour);
        geneticInfo.Add("genotypes", seed.genotypes);
        geneticInfo.Add("timeGrowing", seed.timeGrowing);
        geneticInfo.Add("growthDuration", seed.growthDuration);
        return geneticInfo;
    }
    public Dictionary<string, object> AddItemInfoToList(Plant plant)
    {
        Dictionary<string, object> geneticInfo = new Dictionary<string, object>();
        geneticInfo.Add("genotypes", plant.genotypes);
        //geneticInfo.Add("height", plant.height);
        //geneticInfo.Add("colour", plant.colour);
        return geneticInfo;
    }
    #endregion

    public string GenerateFileName()
    {
        string format = "yyyy-MM-dd HH,mm,ss";
        string filename = "day " + GameManager.Instance.day.ToString() + " - " + System.DateTime.Now.ToString(format);
        if (lastFilename != null)
        {
            while (lastFilename == filename)//while they are equal
            {
                filename = System.DateTime.Now.ToString(format);//reset filename
            }
        }
        lastFilename = filename;
        return filename;
    }
    /// <summary>
    /// Populate a list with save slot buttons
    /// *loads existing files*
    /// </summary>
    public void PopulateScrollList()
    {
        string folderpath = Application.persistentDataPath + "/saves";
        if (!Directory.Exists(folderpath))
        {
            Directory.CreateDirectory(folderpath);
        }
        var sortedFiles = new DirectoryInfo(folderpath).GetFiles().OrderBy(f => f.LastWriteTime).ToList();

        foreach (FileInfo file in sortedFiles)
        {
            if (file.Extension.Contains(".data"))
            {
                string filename = file.Name.Replace(folderpath, "").Replace(@"\", "").Replace(".data", "");//just get filename
                CreateSaveSlot(filename);
            }

        }
    }
    public void DeleteSave(string filename, GameObject saveSlot) { 
        string filePath = Application.persistentDataPath + $"/saves/{filename}.data";
        File.Delete(filePath);
        Destroy(saveSlot);
    }
    /// <summary>
    /// Spawn visible save slot in scroll list
    /// *loads existing files*
    /// </summary>
    public void CreateSaveSlot(string filename)
    {
        Button newButton = GameObject.Instantiate(saveSlot) as Button;
        newButton.transform.SetParent(savesList.transform, false);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = filename;
        newButton.transform.SetAsFirstSibling();//ensure button is first
        Button deleteButton = newButton.transform.GetChild(1).GetComponent<Button>();
        newButton.onClick.AddListener(delegate {
            LoadItems(filename);

        });
        deleteButton.onClick.AddListener(delegate{
            DeleteSave(filename, newButton.gameObject);
        });
    }

}
