using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform rectTransform;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        rectTransform.localScale = new Vector3(1.2f,1.2f,1.0f);
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        rectTransform.localScale = new Vector3(1.0f,1.0f,1.0f);
    }
    /*
    public string WriteMessage()
    {
        stats = transform.parent.GetComponent<InventorySlot>().GetComponent<Stats>();
    }*/
}