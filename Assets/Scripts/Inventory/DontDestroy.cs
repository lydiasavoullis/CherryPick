using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [HideInInspector]
    public string objectId;
    private void Awake()
    {
        objectId = name + transform.position.ToString();
    }
    void Start()
    {
        for (int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++) {
            if(FindObjectsOfType<DontDestroy>()[i] !=this){
                if (FindObjectsOfType<DontDestroy>()[i].objectId == objectId)
                {
                    Destroy(gameObject);
                }
            }
        }
        //DontDestroyOnLoad(gameObject);
        //this.transform.SetParent(GameObject.FindGameObjectWithTag("background").transform);
    }
}
