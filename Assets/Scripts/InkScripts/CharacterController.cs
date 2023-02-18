using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController
{
    //public GameObject stage;
    //public GameObject characterBox;
    public CharacterController() {
        //this.stage = stage;
        //this.characterBox = characterBox;

    }
    //public void ChangeSprite(string characterName, InkList characterSprite)
    //{
    //    foreach (InkListItem item in characterSprite.Keys)
    //    {
    //        string spriteName = item.ToString().Replace(characterName + ".", "");
    //        spriteName = char.ToUpper(spriteName[0]) + spriteName.Substring(1);//make string start with uppercase
    //        GameObject.Find(characterName).GetComponent<Image>().sprite = Resources.Load<Sprite>($"characters/{spriteName}/{spriteName}");//load default character image from resources
    //    }
    //}
    //update characters in scene
    public void UpdateCharactersInScene(string characters, GameObject stage, GameObject characterBox)
    {
        List<string> newCharacters = characters.Split(',').ToList();
        //Debug.Log("CHARACTER UPDATE: " + characters.ToString());
        List<string> updatedList = new List<string>();
        foreach (string item in newCharacters)
        {
            if (item=="") {
                break;
            }
            string temp = item.Replace(" ","");//.ToString().Replace("characters.", "");
            temp = char.ToUpper(temp[0]) + temp.Substring(1);
            updatedList.Add(temp);//add character
            //Debug.Log("CHARACTER NAME: " + temp);
        }

        foreach (string removedChar in GameVars.loadedChars)
        {
            if (!updatedList.Contains(removedChar))//if a character is in old list that is NOT in new
            {
                RemoveCharacter(removedChar, stage);
            }
        }
        foreach (string addedChar in updatedList)
        {
            if (!GameVars.loadedChars.Contains(addedChar))//if a character is in new list that is NOT in old
            {
                try {
                    LoadCharacterSpriteType(addedChar, stage, characterBox);
                }
                catch (Exception e) {
                    Debug.Log($"Could not load character: {addedChar}");
                }
                
            }
        }
        GameVars.loadedChars = updatedList;
    }

    /// <summary>
    /// Destroy a character after use
    /// </summary>
    /// <param name="character"></param>
    public void RemoveCharacter(string character, GameObject stage)
    {
        var charPlaceholder = stage.GetComponent<Transform>().Find(character);
        //try {
        UnityEngine.Object.Destroy(charPlaceholder.gameObject);
        //InkList currentCharacterList = (InkList)GameVars.story.variablesState["characters"];
        ////var exclude = (InkList)GameVars.story.variablesState["characters"];
        ////exclude.Clear();
        //string temp = character;
        //temp = char.ToLower(temp[0]) + temp.Substring(1);
        ////exclude.AddItem(temp);
        //InkListItem key;
        //foreach (InkListItem ch in currentCharacterList.Keys)
        //{
        //    if (ch.itemName==temp)
        //    {
        //        key = ch;
        //        GameVars.story.variablesState["characters"] = currentCharacterList.Remove(key);
        //        break;
        //    }
        //}
        //currentCharacterList = currentCharacterList.Without(exclude);
        
        //PrintCharacters();
        //}
        //catch (Exception e) {
        //    Debug.Log(e);
        //}
        
    }

    /// <summary>
    /// Remove all the characters from the stage
    /// </summary>
    public void ClearStage(GameObject stage)
    {
        for (int i = 0; i < stage.transform.childCount; i++)
        {
            UnityEngine.Object.Destroy(stage.transform.GetChild(i).gameObject);
        }
    }
    //public void GoToGameScene(string gameScene, string currentScene)
    //{
    //    GameVars.currentScene = currentScene;
    //    SceneManager.LoadScene(gameScene);
    //}




    /// <summary>
    /// Load new scene
    /// </summary>
    /// <param name="image"></param>
    public void ChangeScene(string sceneName, string currentScene)
    {
        //
        //check if scene has changed
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(sceneName);
            // UpdateCharactersInScene((InkList)story.variablesState["characters"]);//so we update the character list before we load in
            currentScene = sceneName;

        }

    }
    /// <summary>
    /// Instantiate character sprite in game
    /// </summary>
    /// <param name="character"></param>
    public void LoadCharacterSpriteType(string character, GameObject stage, GameObject characterBox)
    {
        //Debug.Log($"characters/{character}/" + GameVars.story.variablesState[character].ToString());
        //Debug.Log(stage.name + " characterController");
        GameObject charPlaceholder = UnityEngine.Object.Instantiate(characterBox) as GameObject;
        charPlaceholder.transform.SetParent(stage.transform, false);
        charPlaceholder.name = character;
        

        charPlaceholder.GetComponent<Image>().sprite = Resources.Load<Sprite>($"characters/{character}/" + GameVars.story.variablesState[character].ToString());//load image from resources: specifically the type of sprite for that character

    }
    /// <summary>
    /// Add all current characters to the stage
    /// Used when loading characters
    /// </summary>
    //public void RefreshCharacters(List<string> currentCharacters)
    //{
    //    ClearStage();
    //    for (int i = 0; i < currentCharacters.Count; i++)
    //    {
    //        LoadCharacterSpriteType(currentCharacters[i]);
    //    }
    //}
    public void RefreshCharacters(InkList characters, GameObject stage, GameObject characterBox)
    {
        ClearStage(stage);
        GameVars.loadedChars.Clear();
        foreach (InkListItem ch in characters.Keys)
        {
            string temp = ch.itemName.ToString();//.Replace("characters.", "");
            temp = char.ToUpper(temp[0]) + temp.Substring(1);
            GameVars.loadedChars.Add(temp);
            LoadCharacterSpriteType(temp.ToString(), stage, characterBox);
        }
    }

    public void LoadCharacterSprite(string character, string spriteName, GameObject stage, GameObject characterBox) //e.g. Maki, maki_sad
    {
        
        GameObject charPlaceholder = UnityEngine.Object.Instantiate(characterBox) as GameObject;
        charPlaceholder.transform.SetParent(stage.transform, false);
        charPlaceholder.name = character;
        //GameVars.story.variablesState[character] = spriteName;
        charPlaceholder.GetComponent<Image>().sprite = Resources.Load<Sprite>($"characters/{character}/" + spriteName);
        //update current sprite in ink for character
        var currentSprite = new InkList(character, GameVars.story);
        currentSprite.AddItem(spriteName);
        GameVars.story.variablesState[character] = currentSprite;
        //update character list
        // var currentCharacterList = new InkList("characters", GameVars.story);
        var currentCharacterList = (InkList)GameVars.story.variablesState["characters"];
        string temp = character;
        temp = char.ToLower(temp[0]) + temp.Substring(1);
        currentCharacterList.AddItem(temp);
        GameVars.story.variablesState["characters"] = currentCharacterList;
        //position code unused
        //string[] coord = xy.Split(',');
        //charPlaceholder.GetComponent<RectTransform>().sizeDelta = new Vector2(stage.GetComponent<RectTransform>().sizeDelta.x,stage.GetComponent<RectTransform>().sizeDelta.y);
        //charPlaceholder.transform.localPosition = new Vector3(float.Parse(coord[0]), float.Parse(coord[1]), charPlaceholder.transform.localPosition.z);
        //GameObject charPlaceholder = UnityEngine.Object.Instantiate(characterBox, new Vector3(float.Parse(coord[0]), float.Parse(coord[1]), 0), Quaternion.identity);
    }
    public void ChangeCharacterSprite(string character, string spriteName, GameObject stage) //e.g. Maki, maki_sad
    {
        var charPlaceholder = stage.GetComponent<Transform>().Find(character);
        charPlaceholder.GetComponent<Image>().sprite = Resources.Load<Sprite>($"characters/{character}/" + spriteName);
        //update current sprite in ink for character
        var currentSprite = new InkList(character, GameVars.story);
        currentSprite.AddItem(spriteName);
        GameVars.story.variablesState[character] = currentSprite;
    }
    public void PrintCharacters()
    {
        string str = "";
        InkList characterList = (InkList)(GameVars.story.variablesState["characters"]);
        foreach (InkListItem ch in characterList.Keys)
        {
            str += "InkItem: " + ch.itemName.ToString() + "\n";
        }
        Debug.Log(str);
    }

}
