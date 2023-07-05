using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public string text;
    public void PlaySound() {
        AudioManager audioManager = GameObject.FindGameObjectWithTag("audioManager").GetComponent<AudioManager>();
        audioManager.Play(text);
    }
}
