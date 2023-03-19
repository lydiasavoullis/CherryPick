using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{
    public void ChangeOrderInLayer(int i) {
        gameObject.GetComponent<Canvas>().sortingOrder = i;
    }
}
