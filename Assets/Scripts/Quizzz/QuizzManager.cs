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

    private InventoryItem combineItem;
    private bool canCombine;
    private bool CanCombine()
    {
        if (inventorySlots[0].InventoryItem == null || inventorySlots[1].InventoryItem == null)
        {
            canCombine = false;
        }
        if (inventorySlots[0].InventoryItem != null && inventorySlots[1].InventoryItem != null)
        {
            if (inventorySlots[0].InventoryItem.Item == RecipeSO.FirstRequireItem && inventorySlots[1].InventoryItem.Item == RecipeSO.SecondRequireItem)
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
            foreach (InventorySlot slot in inventorySlots)
            {
                Destroy(slot.InventoryItem.gameObject);
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
            GameManager.Instance.Inventory.AddItem(item);
            Destroy(combineItem.gameObject);
            takeButton.SetActive(false);
            combineButton.SetActive(true);
        }
    }
}
