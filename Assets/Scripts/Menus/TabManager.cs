using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] tabs = { };
    [SerializeField]
    GameObject[] files = { };
    [SerializeField]
    Sprite closedTab;
    [SerializeField]
    Sprite openTab;
    Dictionary<GameObject, GameObject> filetabOrganiser = new Dictionary<GameObject, GameObject>();
    private void Start()
    {
        for (int i = 0; i<tabs.Length;i++) {
            filetabOrganiser.Add(tabs[i], files[i]);
        }
    }
    public void SelectTab(GameObject tab) {
        foreach(GameObject key in filetabOrganiser.Keys) {
            //key.GetComponent<Image>().sprite = closedTab;
            key.GetComponent<Canvas>().sortingOrder = -1;
            key.GetComponent<Button>().interactable = true;
            filetabOrganiser[key].SetActive(false);
        }
        //tab.GetComponent<Image>().sprite = openTab;
        tab.GetComponent<Canvas>().sortingOrder = 1;
        tab.GetComponent<Button>().interactable = false;
        filetabOrganiser[tab].SetActive(true);
    }
}
