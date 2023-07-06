using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortingOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Canvas>().overrideSorting = true;
        gameObject.GetComponent<Canvas>().sortingLayerName = "UI";
    }

    
}
