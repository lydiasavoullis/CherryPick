using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
    void OnMouseOver()
    {
        Debug.Log("mouse over");
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(gameObject);
        }
    }

}
