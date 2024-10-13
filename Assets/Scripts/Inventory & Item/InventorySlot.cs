using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] private InventorySlotEventChannelSO chooseInventorySlotEvent;
    public Image Image;
    public InventoryItem InventoryItem {  get; private set; }

    private void OnDisable()
    {
        Image.enabled = false;
    }
    public void Select(bool state)
    {
        Image.enabled = state;
    }
    public void OnDrop(PointerEventData pointerEventData)
    {
        if (transform.childCount == 1)
        {
            InventoryItem = pointerEventData.pointerDrag.GetComponent<InventoryItem>();
            InventoryItem.ParentAfterDrag = transform;        
        }
        chooseInventorySlotEvent.RaiseEvent(this);
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (transform.childCount == 2)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                InventoryItem = pointerEventData.pointerDrag.GetComponent<InventoryItem>();
            }
        }
        chooseInventorySlotEvent.RaiseEvent(this);
    }
}
