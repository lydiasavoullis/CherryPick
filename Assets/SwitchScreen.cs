using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScreen : MonoBehaviour
{
    public void ChangeCameraPositionToGreenhouse() {
       Vector3 oldPos = this.gameObject.GetComponent<Transform>().position;
        this.gameObject.GetComponent<Transform>().position = new Vector3(17.8f, oldPos.y, oldPos.z);
    }
    public void ChangeCameraPositionToShopFront()
    {
        Vector3 oldPos = this.gameObject.GetComponent<Transform>().position;
        this.gameObject.GetComponent<Transform>().position = new Vector3(0, oldPos.y, oldPos.z);
    }
}
