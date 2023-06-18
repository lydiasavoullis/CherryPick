using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToKnot : MonoBehaviour
{
    [SerializeField]
    TextAsset story;
    public void GoToKnotName(string knotname)
    {
        GameVars.story = null;
        GameVars.story = new Story(story.text);
        GameVars.story.ChoosePathString(knotname);
    }
}
