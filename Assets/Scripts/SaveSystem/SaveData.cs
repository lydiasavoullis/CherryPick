using System;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SaveData
{
    //https://www.youtube.com/watch?v=XOjd_qU2Ido
    //public string saveState;
    //public string currentScene;
    public List<Tuple<string, Dictionary<string, object>, int>> inventoryPlants = new List<Tuple<string, Dictionary<string, object>, int>>(); //a list of key values, the key is the name of the item. The value dictionary of the item's genetic information
    public List<Tuple<string, Dictionary<string, object>>> greenhousePlants = new List<Tuple<string, Dictionary<string, object>>>();
    //number of potsin greenhouse and their hydration level
    public List<float> plantPots = new List<float>();
    //task list
    public List<Tuple<int, int, string, string, List<string>>> taskBoardList = new List<Tuple<int, int, string, string, List<string>>>();
    public int currentDay = 0;
    public int funds = 0;
    public int reputation = 0;
    //public List<string> inventoryObjects; //list of objects by name
    //public List<string> storyLog;
    public SaveData(SaveInventoryItems saveItems)//InkController script
    {
        this.inventoryPlants = saveItems.inventoryPlants;
        this.currentDay = saveItems.day;
        this.funds = saveItems.funds;
        this.reputation = saveItems.reputation;
        this.greenhousePlants = saveItems.greenhousePlants;
        this.plantPots = saveItems.plantPots;
        this.taskBoardList = saveItems.taskBoardList;
        //in another class loop through all items in inventory
        //if they are a plant save them to inventory plants

    }

}
