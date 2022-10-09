using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveController
{
    //Button saveSlot;
    //GameObject scrollList;
    //MonoBehaviour monobehaviour;
    //GameObject optionsMenu;
    //public TextAsset inkJSON;
    public SaveController()//Button saveSlot, GameObject scrollList, GameObject optionsMenu, TextAsset inkJSON
    {
        //this.saveSlot = saveSlot;
        //this.scrollList = scrollList;
        //this.optionsMenu = optionsMenu;
        //this.inkJSON = inkJSON;
    }
    /// <summary>
    /// Populate a list with save slot buttons
    /// *loads existing files*
    /// </summary>
    //public void PopulateScrollList(Button saveSlot, GameObject scrollList, TextAsset inkJSON, GameObject optionsMenu)
    //{
    //    string folderpath = Application.persistentDataPath + "/" + "saves";
    //    if (!Directory.Exists(folderpath))
    //    {
    //        Directory.CreateDirectory(folderpath);
    //    }
    //    var sortedFiles = new DirectoryInfo(folderpath).GetFiles().OrderBy(f => f.LastWriteTime).ToList();

    //    foreach (FileInfo file in sortedFiles)
    //    {
    //        if (file.Extension.Contains(".story"))
    //        {
    //            string filename = file.Name.Replace(folderpath, "").Replace(@"\", "").Replace(".story", "");//just get filename
    //            CreateSaveSlot(filename, saveSlot, scrollList, inkJSON, optionsMenu);
    //        }

    //    }
    //}
   

    /// <summary>
    /// Spawn visible save slot in scroll list
    /// *loads existing files*
    /// </summary>
    //public void CreateSaveSlot(string filename, Button saveSlot, GameObject scrollList, TextAsset inkJSON, GameObject optionsMenu)
    //{
    //    Button newButton = GameObject.Instantiate(saveSlot) as Button;
    //    newButton.transform.SetParent(scrollList.transform, false);
    //    newButton.GetComponentInChildren<TextMeshProUGUI>().text = filename;
    //    newButton.transform.SetAsFirstSibling();//ensure button is first
    //    newButton.onClick.AddListener(delegate {
    //        LoadStory(filename, inkJSON, optionsMenu);

    //    });
    //}

    //public void LoadStory(string filename, TextAsset inkJSON, GameObject optionsMenu)
    //{
    //    GameVars.ResetStaticVariables(inkJSON);
        
    //    SaveData data = SaveSystem.LoadData(filename);
    //    GameVars.loadedTextLog = data.storyLog;
    //    GameVars.loadedState = data.saveState;//load save state
    //    GameVars.story.state.LoadJson(data.saveState);
    //    //this log of previous dialogue is not saved in ink story file. Everything else can be loaded from the save state
    //    GameVars.justLoaded = true;
    //    SceneManager.LoadScene(data.currentScene);//load scene 
    //    optionsMenu.SetActive(false);
    //    Debug.Log(GameVars.story.variablesState["characters"].ToString());

    //}
    public string GenerateFileName()
    {
        string format = "yyyy-MM-dd HH,mm,ss";
        string filename = GameVars.story.variablesState["chapter"].ToString() + " - " + System.DateTime.Now.ToString(format);
        if (GameVars.lastFilename != null)
        {
            while (GameVars.lastFilename == filename)//while they are equal
            {
                filename = System.DateTime.Now.ToString(format);//reset filename
            }
        }
        GameVars.lastFilename = filename;
        return filename;
    }

}
