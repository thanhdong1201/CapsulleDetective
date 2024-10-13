using System.Collections.Generic;
using UnityEngine;

public class QuizzManager : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private InventorySlot combineSlot;
    [SerializeField] private Transform combineTransform;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject combineButton;
    [SerializeField] private GameObject takeButton;
    [SerializeField] private RecipeSO RecipeSO;
    [SerializeField] private ItemEventChannelSO onAddItem;
    [SerializeField] private ItemEventChannelSO onRemoveItem;
    InventoryItem inventoryItem1;
    InventoryItem inventoryItem2;
    private InventoryItem combineItem;
    private bool canCombine;
    private bool CanCombine()
    {
        inventoryItem1 = inventorySlots[0].GetComponentInChildren<InventoryItem>();
        inventoryItem2 = inventorySlots[1].GetComponentInChildren<InventoryItem>();
        if (inventoryItem1 == null || inventoryItem2 == null)
        {
            canCombine = false;
        }
        if (inventoryItem1 != null || inventoryItem2 != null)
        {
            if (inventoryItem1.Item == RecipeSO.FirstRequireItem && inventoryItem2.Item == RecipeSO.SecondRequireItem)
            {
                canCombine = true;
            }
        }
        return canCombine;
    }
    public void CombineItem()
    {
        if (CanCombine())
        {
            takeButton.SetActive(true);
            combineButton.SetActive(false);

            GameObject newItemGO = Instantiate(inventoryItemPrefab, combineTransform);
            InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
            inventoryItem.InitializeItem(RecipeSO.FinalItem);
            combineItem = inventoryItem;
            Destroy(inventoryItem1.gameObject);
            Destroy(inventoryItem2.gameObject);
            foreach (InventorySlot slot in inventorySlots)
            {
                onRemoveItem.RaiseEvent(slot.GetComponentInChildren<InventoryItem>().Item);
            }
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Not Correct!");
        }
    }
    public void TakeItem()
    {
        ItemSO item = combineSlot.GetComponentInChildren<InventoryItem>().Item;
        if (item != null)
        {
            onAddItem.RaiseEvent(item);
            Destroy(combineItem.gameObject);
            takeButton.SetActive(false);
            combineButton.SetActive(true);
        }
    }
}
