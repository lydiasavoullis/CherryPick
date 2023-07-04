using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink;
using Ink.Runtime;
using System;
using System.Linq;

public class InkFindInteractions
{
    List<string> interactionsForToday = new List<string>();
    string[] names = { "yulia", "mei", "oliver", "fearn" };
    static int lvl = 1;
    int maxEvents = 3;
    //repeatable main
    //e.g. 1-name-repeatable-5

    public string FindRandomNameString(List<string> characters) {
        string name = "";
        name = characters[UnityEngine.Random.Range(0, characters.Count)];
       // string interactionLevel = UnityEngine.Random.Range(1, lvl + 1).ToString();//get a task in a random interaction level
        
        return name;
    }
    //KEEP could be useful
    //public string GetNextUnvisitedMainEvent() {
    //    string name = names[UnityEngine.Random.Range(0, names.Length)];
    //    string knotName = "";
    //    for (int i = 1; i<=3;i++) {
    //        string temp = $"{name}_main_{i}";
    //        //try {
    //            int visitCount = GameVars.story.state.VisitCountAtPathString(temp);
    //            if (visitCount<1) {
    //                return temp;
    //            }
    //        //}
    //        //catch (Exception e) {
    //        //    Debug.Log(e + " not valid ink path");
    //        //}
            
    //    }
    //    return FindRandomInteractionString("repeatable");
    //}
    public void RemoveFromUpcomingEvents(string eventKnotName) {
        GameVars.upcomingEvents.Remove(eventKnotName);
        
    }
    public void AddToUpcomingEvents(string eventKnotName)
    {
        GameVars.upcomingEvents.Add(eventKnotName);

    }
    public void ChooseEventsForCurrentDay()
    {
        List<string> characters = names.ToList();
        //check if upcoming events has 4. If less than 4, choose from other random knots
        //if customer has already been in, don't bring them in again
        for (int i = 0; i < maxEvents; i++)
        {
            if (GameVars.upcomingEventsToday.Count < maxEvents && GameVars.upcomingEvents.Count>0)
            {
                GameVars.upcomingEventsToday.Add(GameVars.upcomingEvents.First());
                GameVars.upcomingEvents.RemoveAt(0);
            }
            else
            {
                break;
            }
        }
        //remove any characters from list that will appear in main event for the day
        for (int i = 0; i < GameVars.upcomingEventsToday.Count;i++) {
            foreach (string characterName in characters)
            {
                if (GameVars.upcomingEventsToday[i].Contains(characterName)) {
                    characters.Remove(characterName);
                    break;
                }
            }
        }
        if (GameVars.upcomingEventsToday.Count < maxEvents)
        {
            for (int i = GameVars.upcomingEventsToday.Count; GameVars.upcomingEventsToday.Count < maxEvents; i++)
            {
                string chosenChar = FindRandomNameString(characters);
                string knotName = $"{chosenChar}_repeatable_{lvl}";
                GameVars.upcomingEventsToday.Add(knotName);
                characters.Remove(chosenChar);
            }
            //add some more events
        }

        GameVars.upcomingEventsToday.Insert(0, "vera_morning");//("vera_morning");
        GameVars.upcomingEventsToday.Add("vera_evening");
    }

}
