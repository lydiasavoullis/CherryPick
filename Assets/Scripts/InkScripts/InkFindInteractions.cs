using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink;
public class InkFindInteractions
{
    List<string> interactionsForToday = new List<string>();
    string[] names = { "beatrice", "charlie" };
    //repeatable main
    //e.g. 1-name-repeatable-5

    public void FindInteraction(string interactionType) {
        int lvl = 1;
        string name = "";
        name = names[Random.Range(0,names.Length)];
        string interactionLevel = Random.Range(1, lvl+1).ToString();//get a task in a random interaction level
        string knotName = $"{name}_{interactionType}_{lvl}";
        GameVars.story.ChoosePathString(knotName);
        //if (GameVars.story.currentChoices.Count>0) {
        //    int choiceIndexRandom = Random.Range(0, GameVars.story.currentChoices.Count + 1);//get a task in a random interaction level
        //    GameVars.story.ChooseChoiceIndex(choiceIndexRandom);
        //}
        //interactionsForToday.Add(GameVars.story.path.ToString());
        
    }
}
