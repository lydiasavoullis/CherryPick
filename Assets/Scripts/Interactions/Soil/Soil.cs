using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class Soil : MonoBehaviour, IDropHandler
{
    [SerializeField]
    Slider slider;
    public PlantPotState plantPotState;
    bool watering = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audioManager").GetComponent<AudioManager>();
        //wateringCan = gameObject.GetComponent<RectTransform>();
        
    }
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragHandler.itemBeingDragged == null) {
            return;
        }
        if (!item && DragHandler.itemBeingDragged.name.ToLower()=="seed")
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
        }
        if (int.Parse(GameVars.story.variablesState["tutorialCounter"].ToString()) < 1 && DragHandler.itemBeingDragged.name.ToLower() == "seed")
        {
            GameVars.story.variablesState["tutorialCounter"] = (int.Parse(GameVars.story.variablesState["tutorialCounter"].ToString()) + 1).ToString();
        }
    }
    private void OnParticleSystemStopped()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        switch(other.name){
            case "water":
                if (slider.value <= slider.maxValue)
                {
                    
                    slider.value += slider.maxValue / 50;
                    plantPotState.hydrationValue = slider.value;
                    StartCoroutine(PlayWaterEffect());
                }
                //Debug.Log(int.Parse(GameVars.story.variablesState["tutorialCounter"].ToString()));
                if (int.Parse(GameVars.story.variablesState["tutorialCounter"].ToString()) == 1)
                {
                    GameVars.story.variablesState["tutorialCounter"] = (int.Parse(GameVars.story.variablesState["tutorialCounter"].ToString()) + 1).ToString();
                    GameVars.story.ChoosePathString("tutorial_pt3");
                }
                break;
            case "heat":
                try {
                    plantPotState.isHeated = true;
                }
                catch (Exception e) {
                    
                }
                
                break;
            default: 
                break;
        }
        
        //plantPotState.hydrationValue = slider.value;
    }
    IEnumerator PlayWaterEffect()
    {
        audioManager.PlayAtRandomTime("watering pot");
        yield return new WaitForSeconds(0.2f);
        audioManager.StopImmediate("watering pot");
    }

    IEnumerator WaterPlant() {
        while ((slider.value <= slider.maxValue) && watering)
        {
            yield return new WaitForSeconds(0.5f);
            slider.value += slider.maxValue / 20;
        }
        plantPotState.hydrationValue = slider.value;
    }

    private void Start()
    {
        if (plantPotState == null)
        {
            plantPotState = new PlantPotState();
        }

    }
    public bool IsEmpty()
    {
        if (gameObject.transform.childCount < 1)
        {
            plantPotState.isEmpty = true;
            return true;
        }
        else
        {
            plantPotState.isEmpty = false;
            return false;
        }
    }
    public void ChangeHydration(float changeVal) {
        if ((slider.value += changeVal) < 0)
        {
            slider.value = 0;
        }
        else {
            slider.value += changeVal;
        }
        
    }

}
