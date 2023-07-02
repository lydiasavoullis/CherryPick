using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashCan : MonoBehaviour, IDropHandler
{
    [SerializeField]
    Sprite closedSprite;
    [SerializeField]
    Sprite openSprite;
    public void OnDrop(PointerEventData eventData)
    {
        if (DragHandler.itemBeingDragged != null) {
            DestroyImmediate(DragHandler.itemBeingDragged);
            gameObject.GetComponent<Image>().sprite = closedSprite;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DragHandler.itemBeingDragged == collision.gameObject) {
            gameObject.GetComponent<Image>().sprite = openSprite;
            gameObject.GetComponent<AudioSource>().Play();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<Image>().sprite = closedSprite;
    }
}
