using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField]
    Sprite night;
    [SerializeField]
    Sprite day;
    [SerializeField]
    Image background;
    [SerializeField]
    GameObject rainParticleSystem;
    [SerializeField]
    GameObject customerContainer;
    public void ChangeToImage(string image) {
        
        switch (image) {
            case "night":
                customerContainer.GetComponent<CustomerController>().ClearCustomers();
                background.sprite = night;
                rainParticleSystem.SetActive(false);
                break;
            case "day":
                background.sprite = day;
                if (Random.Range(0, 2)==1) rainParticleSystem.SetActive(true);
                break;
            default:
                break;
        }
    }
}
