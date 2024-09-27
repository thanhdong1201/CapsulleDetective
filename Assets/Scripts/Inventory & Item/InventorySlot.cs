using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public Image Image;
    public InventoryItem InventoryItem {  get; private set; }

    private CanvasGroup canvasGroup;
    private void OnDisable()
    {
        Image.enabled = false;
    }
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
        GameManager.Instance.Inventory.SetItemData(this);
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
        GameManager.Instance.Inventory.SetItemData(this);
    }
}
