using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using Ink.UnityIntegration;
using System.IO;
using System.Threading;
using UnityEngine.InputSystem;
using System;

public class DialogueController : MonoBehaviour
{

    #region prefabs
    [SerializeField]
    Button buttonPrefab;
    [SerializeField]
    Button saveSlot;
    #endregion
    //objects(objects this script interacts with)
    #region Interactable Objects
    [SerializeField]
    GameObject notificationPrefab;
    [SerializeField]
    GameObject endOfDayBtn;
    [SerializeField]
    GameObject taskBoard;
    [SerializeField]
    GameObject speechPrefab;
    [SerializeField]
    GameObject speechReversedPrefab;
    [SerializeField]
    GameObject speechContainer;
    [SerializeField]
    GameObject customerContainer;
    [SerializeField]
    TextMeshProUGUI storyText;
    [SerializeField]
    GameObject nameTag;
    [SerializeField]
    GameObject quitDialogue;
    [SerializeField]
    TextMeshProUGUI logTextBox;
    [SerializeField]
    Scrollbar scrollBar;
    //[SerializeField]
    //GameObject textLogBox;
    //[SerializeField]
    //GameObject textLogList;
    [SerializeField]
    GameObject backgroundDialogueBox;
    [SerializeField]
    GameObject characterBox;
    [SerializeField]
    GameObject menuSystem;
    [SerializeField]
    GameObject audioManager;
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject loadingScreen;
    [SerializeField]
    GameObject scrollList;
    [SerializeField]
    GameObject saveMenu;//if active we don't want to advance the story
    [SerializeField]
    GameObject optionsMenu;//if active we don't want to advance the story
    [SerializeField]
    GameObject greenHouse;//if active we don't want to advance the story
    [SerializeField]
    GameObject greenhouseArrow;
    #endregion
    #region controllers
    //public bool effectJustPlayed = false;
    TextLogController textLogControl = new TextLogController();
    CharacterController characterControl = new CharacterController();
    //AudioController audioControl = new AudioController();//audioManager
    SaveController saveControl = new SaveController();
    UIController uIControl = new UIController();
    InkFindInteractions inkFindInteractions = new InkFindInteractions();
    #endregion
    #region Ink libraries
    public TextAsset inkJSON;
    public Vector3 speechPosition;
    string yourname = "You";
    //public InkFile storyVariables;

    #endregion
    void Start()
    {
        if (GameVars.story==null) {
            GameVars.story =  new Story(inkJSON.text);
        }
        GameVars.finishedTyping = true;
        try
         {
            GameVars.story.UnbindExternalFunction("AddCharacter");
            GameVars.story.UnbindExternalFunction("ChangeSprite");
            GameVars.story.UnbindExternalFunction("RemoveCharacter");
            GameVars.story.UnbindExternalFunction("AddToUpcomingEvents");
            GameVars.story.UnbindExternalFunction("RemoveFromUpcomingEvents");
        }
        catch (Exception e)
        {
            GameVars.story.BindExternalFunction("AddCharacter", (string charName, string charType) => characterControl.LoadCharacterSprite(charName, charType, this.customerContainer, characterBox));
            GameVars.story.BindExternalFunction("ChangeSprite", (string charName, string charType) => characterControl.ChangeCharacterSprite(charName, charType, this.customerContainer));
            GameVars.story.BindExternalFunction("RemoveCharacter", (string charName) => characterControl.RemoveCharacter(charName, this.customerContainer));
            GameVars.story.BindExternalFunction("AddToUpcomingEvents", (string knotName) => inkFindInteractions.AddToUpcomingEvents(knotName));
            GameVars.story.BindExternalFunction("RemoveFromUpcomingEvents", (string knotName) => inkFindInteractions.RemoveFromUpcomingEvents(knotName));
            //Debug.Log("tried to bind function twice " + e);

        }
        //if (storyVariables != null) {
        //    GameVars.SetAllStoryVariables(storyVariables.filePath);
        //}
        //saveControl.PopulateScrollList(saveSlot, scrollList, inkJSON, optionsMenu);
    }


    private void Update()
    {
        //scrollBar.value = 0;
        //if (Keyboard.current.fKey.wasPressedThisFrame)
        //{

        //    if (!GameVars.autoMode && !GameVars.dontAdvanceStory)
        //    {
        //        StartCoroutine(FastForward());
        //    }
        //    else
        //    {
        //        GameVars.autoMode = false;
        //    }
        //}
        if (Keyboard.current.spaceKey.wasPressedThisFrame && GameVars.finishedTyping && !GameVars.dontAdvanceStory && !greenHouse.activeInHierarchy)// && SceneManager.GetActiveScene().name != "MainMenu" 
        {
            ClearSpeech();
            //Debug.Log("space pressed");
            KeepLoadingStory();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("don't advance Story: " + GameVars.dontAdvanceStory);
        }

    }
    
    private void OnLevelWasLoaded(int level)
    {
        if (GameVars.story == null)
        {
            GameVars.story = new Story(inkJSON.text);
        }
        //try
        //{
        //    GameVars.story.UnbindExternalFunction("AddCharacter");
        //    GameVars.story.UnbindExternalFunction("ChangeSprite");
        //    GameVars.story.UnbindExternalFunction("RemoveCharacter");
        //}
        //catch (Exception e) {
        //    Debug.Log(e);
        //    GameVars.story.BindExternalFunction("AddCharacter", (string charName, string charType) => characterControl.LoadCharacterSprite(charName, charType, this.customerContainer, characterBox));
        //    GameVars.story.BindExternalFunction("ChangeSprite", (string charName, string charType) => characterControl.ChangeCharacterSprite(charName, charType, this.customerContainer));
        //    GameVars.story.BindExternalFunction("RemoveCharacter", (string charName) => characterControl.RemoveCharacter(charName, this.customerContainer));
        //}

        //characterControl.RefreshCharacters((InkList)GameVars.story.variablesState["characters"], stage, characterBox);
        //uIControl.SetDialogueBoxActive((string)GameVars.story.variablesState["textBoxIsActive"], backgroundDialogueBox);
        //uIControl.SetNameTag((string)GameVars.story.variablesState["currentSpeaker"], nameTag);
        //textLogControl.LoadTextLogContent(textLogList, textLogBox);//load text log

    }
    
    public void ClearSpeech() {
        for (int i = 0; i < speechContainer.transform.childCount; i++) {
            Destroy(speechContainer.transform.GetChild(i).gameObject);
        }
    }
    public Vector3 GetCharacterPos(string name) {
        
        for (int i =0; i< customerContainer.transform.childCount;i++) {
            if (customerContainer.transform.GetChild(i).name == name) {
                //Debug.Log(customerContainer.transform.GetChild(i).GetComponent<RectTransform>().position);
                Vector3 pos = customerContainer.transform.GetChild(i).GetComponent<RectTransform>().position;
                return new Vector3(pos.x, pos.y-2f, pos.z);
            }
        }
        return new Vector3(0, 0, 0);
    }
    public void KeepLoadingStory()
    {
        GameVars.story.variablesState.variableChangedEvent += ObserveAnyVar;
        if (GameVars.story.canContinue)
        {
            string text = GameVars.story.Continue();//get text from ink
            if (text.Contains("¬"))
            {
                //scrollBar.value = 0;
                return;
            }
            ShowSpeech(text);
            //textLogControl.AddToTextLog(text, textLogBox, textLogList);//log all text
            string speaker = GameVars.story.variablesState["currentSpeaker"].ToString();
            if (speaker == "you")
            {
                StartCoroutine(textLogControl.AddToTextLogBox($"\n<color=#14A7A8>You:</color> {text.TrimStart('\n')}", logTextBox, scrollBar));
            }
            else
            {
                string hexCol = ColorUtility.ToHtmlStringRGB(uIControl.SetNameColour(speaker));
                StartCoroutine(textLogControl.AddToTextLogBox($"\n<color=#{hexCol}>{speaker}:</color> {text.TrimStart('\n')}", logTextBox, scrollBar));

            }

        }
        else { 
            //if the story is not able to continue we will try and get the next event in GameVars.upcomingEventsToday
        }

        GameVars.story.variablesState.variableChangedEvent -= ObserveAnyVar;
    }
    public void ShowSpeech(string text) {
        GameObject speechBubble;
        string speaker;
        
        if (GameVars.story.variablesState["currentSpeaker"].ToString()=="you")
        {
            Vector3 characterSpeechPos = new Vector3(customerContainer.transform.position.x+3, customerContainer.transform.position.y - 3.5f, customerContainer.transform.position.z);
            speechBubble = Instantiate(speechReversedPrefab, characterSpeechPos, speechReversedPrefab.transform.rotation, speechContainer.transform);
            TextMeshProUGUI yourName = speechBubble.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            yourName.text = yourname;
            //yourName.color = uIControl.SetNameColour(you);
            TextMeshProUGUI yourTextBox = speechBubble.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            StartCoroutine(uIControl.WriteText(text, yourTextBox, speechBubble, characterSpeechPos));//typewriter effect

        }
        else {
            speaker = GameVars.story.variablesState["currentSpeaker"].ToString();
            GameVars.loadedSpeechPos = GetCharacterPos(speaker);
            speechBubble = Instantiate(speechPrefab, GameVars.loadedSpeechPos, speechPrefab.transform.rotation, speechContainer.transform);
            TextMeshProUGUI tag = speechBubble.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            tag.text = speaker;
            tag.color = uIControl.SetNameColour(speaker);
            TextMeshProUGUI textBox = speechBubble.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            StartCoroutine(uIControl.WriteText(text, textBox));//typewriter effect
        }
    }
    public void LoadCharacters(List<string> characters) {
        foreach (string s in characters)
        {
            s.ToLower();
            string character = char.ToUpper(s[0]) + s.Substring(1);
            characterControl.LoadCharacterSpriteType(character, customerContainer, characterBox);
        }
        
    }
    public void LoadTextLog() {
        //textLogControl.LoadTextLogContent(textLogList, textLogBox);//load text log
        textLogControl.LoadTextLogText(logTextBox);
        
    }
    public void LoadSpeech(string text) {
        GameObject speechBubble;
        string speaker;
        if (GameVars.loadedCurrentSpeaker == "you")//GameVars.story.variablesState["currentSpeaker"].ToString()
        {
            Vector3 characterSpeechPos = new Vector3(customerContainer.transform.position.x, customerContainer.transform.position.y - 4f, customerContainer.transform.position.z);
            speechBubble = Instantiate(speechReversedPrefab, characterSpeechPos, speechReversedPrefab.transform.rotation, speechContainer.transform);
            TextMeshProUGUI yourName = speechBubble.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            yourName.text = "You";
            //yourName.color = uIControl.SetNameColour(you);
            TextMeshProUGUI yourTextBox = speechBubble.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            StartCoroutine(uIControl.WriteTextNoTyping(text, yourTextBox, speechBubble, characterSpeechPos));
        }
        else
        {
            speaker = GameVars.loadedCurrentSpeaker;//GameVars.story.variablesState["currentSpeaker"].ToString();
            //GameVars.loadedSpeechPos = GetCharacterPos(speaker);
            speechBubble = Instantiate(speechPrefab, GameVars.loadedSpeechPos, speechPrefab.transform.rotation, speechContainer.transform);
            TextMeshProUGUI tag = speechBubble.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            tag.text = speaker;
            tag.color = uIControl.SetNameColour(speaker);
            TextMeshProUGUI textBox = speechBubble.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            StartCoroutine(uIControl.WriteTextNoTyping(text, textBox));
        }
    }

    //add Play music to audio controller
    public void ObserveAnyVar(string varName, object newValue)
    {
        switch (varName)
        {
            //case "textBoxIsActive":
            //    uIControl.SetDialogueBoxActive(newValue.ToString());
            //    break;
            case "characters":
                characterControl.UpdateCharactersInScene(newValue.ToString(), customerContainer, characterBox);
                break;
            case "end_of_day":
                if (newValue.ToString()=="true") {
                    endOfDayBtn.SetActive(true);
                    GameManager.Instance.ChangeBackground();
                    GameManager.Instance.SetNewNightTemp();
                }
                else{
                    endOfDayBtn.SetActive(false);
                }
                break;
            case "shop_state":
                GameManager.Instance.IsShopOpen();
                break;
            case "currentSpeaker":
                GameVars.loadedCurrentSpeaker = GameVars.story.variablesState["currentSpeaker"].ToString();
                //get current speaker pos
                break;
            case "gift":
                if (GameVars.story.variablesState["gift"].ToString() != "")
                {
                    GameManager.Instance.GiftPlant(newValue.ToString());
                    GameVars.story.variablesState["gift"] = "";
                }
                break;
            case "task":
                if (GameVars.story.variablesState["task"].ToString()!="") {
                    string taskInfo = GameVars.story.variablesState["task"].ToString();
                    GameVars.story.variablesState["task"] = "";
                    taskBoard.GetComponent<CreateTask>().GenerateTask(taskInfo);
                }
                break;
            case "show_notification":
                if (GameVars.story.variablesState["show_notification"].ToString() == "greenhouse") {
                    if (greenhouseArrow.transform.childCount<2) {
                        GameObject arrow = Instantiate(notificationPrefab, new Vector3(0, 0, 0), Quaternion.identity, greenhouseArrow.transform);
                        arrow.transform.localPosition = Vector3.zero;
                        arrow.transform.SetSiblingIndex(0);
                    }
                    
                }
                break;
            case "remove_notification":
                if (GameVars.story.variablesState["remove_notification"].ToString() == "greenhouse")
                {
                    if (greenhouseArrow.transform.childCount == 2 && greenhouseArrow.transform.GetChild(0).tag == "uiNotification")
                    {
                        Destroy(greenhouseArrow.transform.GetChild(0).gameObject);
                    }

                }
                break;
            //case "music":
            //    audioControl.PlayMusic(newValue.ToString(), audioManager);
            //    break;
            //case "sfx":
            //    audioControl.PlaySound(newValue.ToString(), audioManager);
            //    break;
            ////case "gameScene":
            ////    characterControl.GoToGameScene(newValue.ToString(), GameVars.story.variablesState["scene"].ToString());
            ////    break;
            default:
                break;
        }

    }


    #region Fast Forward
    /// <summary>
    /// puts the story on AUTO mode where it automatically loads
    /// </summary>

    public IEnumerator FastForward()
    {
        GameVars.autoMode = true;
        while (GameVars.autoMode)
        {
            yield return new WaitUntil(() => GameVars.finishedTyping && !GameVars.dontAdvanceStory);
            KeepLoadingStory();
            yield return new WaitForSeconds(GameVars.autoSpeed);
        }
    }
    #endregion


    #region Camera effects
    /// <summary>
    /// Play camera effect from variable
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="backgroundDialogueBox"></param>
    //public void PlayEffect(string effectName, GameObject backgroundDialogueBox)
    //{
    //    uIControl.SetDialogueBoxActive("false", backgroundDialogueBox);
    //    GameVars.effectJustPlayed = true;
    //    switch (effectName)
    //    {
    //        case "shake":
    //            backgroundDialogueBox.SetActive(false);
    //            //uIControl.ShowOrHideDialogueBox("false", backgroundDialogueBox);
    //            //StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>().TriggerCameraEffect(effectName));
    //            StartCoroutine(mainCamera.GetComponent<ShakeCamera>().TriggerCameraEffect(effectName, backgroundDialogueBox));
    //            break;
    //        case "blur_in":
    //            backgroundDialogueBox.SetActive(false);
    //            //StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>().BlurIn());
    //            //uIControl.ShowOrHideDialogueBox("false", backgroundDialogueBox);
    //            StartCoroutine(mainCamera.GetComponent<ShakeCamera>().BlurIn(backgroundDialogueBox));
    //            break;
    //        default:
                
    //            break;
    //    }
    //}
    /// <summary>
    /// Continue loading story until a choice is available
    /// </summary>

    public void HasEffectJustPlayed()
    {
        if (GameVars.effectJustPlayed)
        {
            GameVars.story.variablesState["effectName"] = "";
            GameVars.effectJustPlayed = false;
            //backgroundDialogueBox.SetActive(true);
            uIControl.SetDialogueBoxActive("true", backgroundDialogueBox);
        }
    }

    #endregion 

    #region UI methods
    /// <summary>
    /// Add choice buttons to the choices GameObject when they appear in the story
    /// </summary>
    public void LoadButtons()
    {
        if (GameVars.story.canContinue == true)
        {
            return;
        }
        foreach (Choice choice in GameVars.story.currentChoices)
        {

            Button choiceButton = Instantiate(buttonPrefab) as Button;
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;
            choiceButton.transform.SetParent(this.transform, false);
            choiceButton.onClick.AddListener(delegate {
                ChooseStoryChoice(choice);

            });

        }
        GameVars.hasLoadedButtons = true;
    }

    //mini game scene, gift scene/shop scene
    public void GoToGameScene(string gameScene, string currentScene)
    {
        menuSystem.SetActive(false);
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = "Load "+ gameScene;
            choiceButton.transform.SetParent(this.transform, false);
            choiceButton.onClick.AddListener(delegate {
                GameVars.currentScene = currentScene;
                SceneManager.LoadScene(gameScene);

            });

            Button choiceButton2 = Instantiate(buttonPrefab) as Button;
            TextMeshProUGUI choiceText2 = choiceButton2.GetComponentInChildren<TextMeshProUGUI>();
            choiceText2.text = "Skip " + gameScene;
            choiceButton2.transform.SetParent(this.transform, false);
            choiceButton2.onClick.AddListener(delegate {
                RefreshUI();
                menuSystem.SetActive(true);
            });

            GameVars.hasLoadedButtons = true;
    }

    /// <summary>
    /// Select the corresponding choice in ink as the button pressed
    /// </summary>
    /// <param name="choice"></param>
    void ChooseStoryChoice(Choice choice)
    {
        GameVars.story.ChooseChoiceIndex(choice.index);
        //Debug.Log("CURRENT CHARACTERS" + GameVars.story.variablesState["characters"].ToString()); 
        RefreshUI();
    }
    public void RefreshUI()
    {
        GameVars.hasLoadedButtons = false;
        GameVars.dontAdvanceStory = false;
        uIControl.EraseUI(this.gameObject, storyText);
        //characterControl.RefreshCharacters(GameVars.loadedChars);
        characterControl.RefreshCharacters((InkList)GameVars.story.variablesState["characters"], customerContainer, characterBox);
        LoadButtons();
    }
    #endregion

    #region Saving/loading methods


    public void AfterLoadedFromSave()
    {
        
        GameVars.story.state.LoadJson(GameVars.loadedState);
        HasEffectJustPlayed();
        //PlayEffect(GameVars.story.variablesState["effectName"].ToString(), backgroundDialogueBox);
        //UI control
        RefreshUI();
        uIControl.ShowOrHideDialogueBox(GameVars.story.variablesState["textBoxIsActive"].ToString(), backgroundDialogueBox);
        uIControl.SetNameTag(GameVars.story.variablesState["currentSpeaker"].ToString(), nameTag);
        //Dialogue control
        uIControl.GetLastLineOfDialogue(storyText);
        //textLogControl.LoadTextLogContent(textLogList, textLogBox);//load text log
        //sound control
        //audioControl.PlayMusic(GameVars.story.variablesState["music"].ToString(), audioManager);
        //audioControl.PlaySound(GameVars.story.variablesState["sfx"].ToString(), audioManager);
    }

    /// <summary>
    /// Save to binary file
    /// </summary>
    public void SaveStory()
    {
        GameVars.loadedScene = SceneManager.GetActiveScene().name;
        string filename = saveControl.GenerateFileName();
        StartCoroutine(ScreenshotHandler.TakeScreenshot_Static(saveMenu, filename));//save menu
        GameVars.loadedState = GameVars.story.state.ToJson();//save story state
        GameVars.loadedScene = SceneManager.GetActiveScene().name;
        //SaveSystem.SaveData(this, filename);//save data to file on system
        //saveControl.CreateSaveSlot(filename, saveSlot, scrollList, inkJSON, optionsMenu);//create save slot to load file
    }


    #endregion


}
