using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem water;
    //RectTransform wateringCan;
    Vector3 origPos;
    ParticleSystem.EmissionModule waterEmission;
    AudioManager audioManager;
    private void Awake()
    {
        waterEmission = water.emission;
        waterEmission.enabled = false;
        audioManager = GameObject.FindGameObjectWithTag("audioManager").GetComponent<AudioManager>();
        //wateringCan = gameObject.GetComponent<RectTransform>();
    }
    private void OnMouseDown()
    {

        waterEmission.enabled = true;
        origPos = gameObject.transform.eulerAngles;
        Vector3 vec3 = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 45);
        gameObject.transform.eulerAngles = vec3;
        audioManager.Play("watering can");
        
        
    }
    private void OnMouseUp()
    {
        audioManager.StopImmediate("watering can");
        waterEmission.enabled = false;
        gameObject.transform.eulerAngles = origPos;
    }

}
