using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnActive : MonoBehaviour
{
    private void OnEnable()
    {
        this.GetComponent<Animation>().Play();
    }
}
