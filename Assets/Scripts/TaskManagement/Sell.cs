using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class Sell : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    List<string> phenotypes = new List<string>();
    [SerializeField]
    GameObject taskController;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    private void Start()
    {
        //Find parent object containing TaskController script
        phenotypes = taskController.GetComponent<TaskController>().task.phenotypes;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //&& GeneratePlants.CheckIfDroppedPlantContainsAllDesiredPhenotypes(phenotypes, DragHandler.itemBeingDragged.GetComponent<PlantController>().plant)
        // !item
        if (DragHandler.itemBeingDragged !=null && DragHandler.itemBeingDragged.tag.Equals("plant") && !item && GeneratePlants.CheckIfDroppedPlantContainsAllDesiredPhenotypes(phenotypes, DragHandler.itemBeingDragged.GetComponent<PlantController>().plant))
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            DragHandler.itemBeingDragged.transform.localPosition = Vector3.zero;
    }
}
}

