using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStoryNull : MonoBehaviour
{
    public void ClearStory()
    {
        GameVars.story.ResetState();
        GameVars.ResetStaticVariables();
    }
}
