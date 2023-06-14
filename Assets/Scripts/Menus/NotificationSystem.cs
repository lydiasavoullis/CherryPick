using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    [SerializeField]
    GameObject tasksNotification;
    [SerializeField]
    GameObject journalNotification;
    [SerializeField]
    GameObject menuNotification;
    private void Start()
    {
        SetActive();
    }
    public void SetActive() {
        if (tasksNotification.activeSelf || journalNotification.activeSelf)
        {
            menuNotification.SetActive(true);
        }
        else {
            menuNotification.SetActive(false);
        }
    }
}
