using System;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SaveData
{
    //https://www.youtube.com/watch?v=XOjd_qU2Ido
    //public string saveState;
    //public string currentScene;
    public string saveState;
    public List<string> storyLog;
    public List<string> loadedChars;
    public string currentSpeaker;
    public List<Tuple<string, Dictionary<string, object>, int>> inventoryPlants = new List<Tuple<string, Dictionary<string, object>, int>>(); //a list of key values, the key is the name of the item. The value dictionary of the item's genetic information
    //number of pots in greenhouse and if they contain plant /seed and their hydration level 
    public List<Tuple<string, Dictionary<string, object>, float>> potsInGreenhouse = new List<Tuple<string, Dictionary<string, object>, float>>();

    //task list
    public List<Tuple<int, int, string, string, List<string>>> taskBoardList = new List<Tuple<int, int, string, string, List<string>>>();
    public int currentDay = 0;
    public int funds = 0;
    public int reputation = 0;
    public Tuple<float, float, float> speechPos = new Tuple<float, float, float>(0f,0f,0f);
    //public List<string> inventoryObjects; //list of objects by name
    //public List<string> storyLog;
    public SaveData(SaveInventoryItems saveItems)//InkController script
    {
        this.inventoryPlants = saveItems.inventoryPlants;
        this.currentDay = saveItems.day;
        this.funds = saveItems.funds;
        this.reputation = saveItems.reputation;
        //this.greenhousePlants = saveItems.greenhousePlants;
        //this.plantPots = saveItems.plantPots;
        this.potsInGreenhouse = saveItems.potsInGreenhouse;
        this.taskBoardList = saveItems.taskBoardList;
        this.saveState = GameVars.story.state.ToJson();
        this.storyLog = GameVars.loadedTextLog;
        this.loadedChars = GameVars.loadedChars;
        this.currentSpeaker = GameVars.story.variablesState["currentSpeaker"].ToString();
        this.speechPos = new Tuple<float, float, float>(GameVars.loadedSpeechPos.x, GameVars.loadedSpeechPos.y, GameVars.loadedSpeechPos.z);
        //in another class loop through all items in inventory
        //if they are a plant save them to inventory plants

    }

}
