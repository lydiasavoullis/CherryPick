using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink;
using Ink.Runtime;
using System;

public class InkFindInteractions
{
    List<string> interactionsForToday = new List<string>();
    string[] names = { "yulia", "mei" };
    static int lvl = 1;
    int maxEvents = 4;
    //repeatable main
    //e.g. 1-name-repeatable-5

    public void FindRandomInteraction(string interactionType) {
        
        string name = "";
        name = names[UnityEngine.Random.Range(0,names.Length)];
        string interactionLevel = UnityEngine.Random.Range(1, lvl+1).ToString();//get a task in a random interaction level
        string knotName = $"{name}_{interactionType}_{lvl}";
        GameVars.story.ChoosePathString(knotName);
        //if (GameVars.story.currentChoices.Count>0) {
        //    int choiceIndexRandom = Random.Range(0, GameVars.story.currentChoices.Count + 1);//get a task in a random interaction level
        //    GameVars.story.ChooseChoiceIndex(choiceIndexRandom);
        //}
        //interactionsForToday.Add(GameVars.story.path.ToString());
        
    }
    public string FindRandomInteractionString(string interactionType) {
        string name = "";
        name = names[UnityEngine.Random.Range(0, names.Length)];
        string interactionLevel = UnityEngine.Random.Range(1, lvl + 1).ToString();//get a task in a random interaction level
        string knotName = $"{name}_{interactionType}_{lvl}";
        return knotName;
    }
    public string GetNextUnvisitedMainEvent() {
        string name = names[UnityEngine.Random.Range(0, names.Length)];
        string knotName = "";
        for (int i = 1; i<=3;i++) {
            string temp = $"{name}_main_{i}";
            //try {
                int visitCount = GameVars.story.state.VisitCountAtPathString(temp);
                if (visitCount<1) {
                    return temp;
                }
            //}
            //catch (Exception e) {
            //    Debug.Log(e + " not valid ink path");
            //}
            
        }
        return FindRandomInteractionString("repeatable");
    }
    public void RemoveFromUpcomingEvents(string eventKnotName) {
        GameVars.upcomingEvents.Remove(eventKnotName);
        
    }
    public void AddToUpcomingEvents(string eventKnotName)
    {
        GameVars.upcomingEvents.Add(eventKnotName);

    }
    public void ChooseEventsForCurrentDay()
    {
        //check if upcoming events has 4. If less than 4, choose from other random knots
        //if customer has already been in, don't bring them in again
        for (int i = 0; i <= maxEvents; i++)
        {
            if (GameVars.upcomingEvents.Count < i)
            {
                GameVars.upcomingEventsToday.Add(GameVars.upcomingEvents[i]);
                GameVars.upcomingEvents.RemoveAt(i);
            }
            else
            {
                break;
            }
        }
        if (GameVars.upcomingEventsToday.Count < maxEvents)
        {
            for (int i = GameVars.upcomingEventsToday.Count; GameVars.upcomingEventsToday.Count < maxEvents; i++)
            {
                GameVars.upcomingEventsToday.Add(FindRandomInteractionString("repeatable"));
            }

            //add some more events
        }
    }

}
