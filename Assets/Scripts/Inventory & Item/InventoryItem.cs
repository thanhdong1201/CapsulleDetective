using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private CanvasGroup canvasGroup;

    public ItemSO Item { get; private set; }
    public Transform ParentAfterDrag { get; set; }
    public Transform LastParent { get; set; }

    private void Start()
    {
        itemImage = GetComponent<Image>();
    }
    public void InitializeItem(ItemSO newItem)
    {
        if (newItem.Sprite != null)
        {
            itemImage.sprite = newItem.Sprite;
        }
        Item = newItem;
    }
  
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;
        canvasGroup.alpha = 0.45f;
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = true;
        canvasGroup.alpha = 1f;
        transform.SetParent(ParentAfterDrag);
    }
}
