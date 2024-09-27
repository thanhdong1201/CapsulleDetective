using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<ItemSO> OnAddItem;
    [SerializeField] private List<InventorySlot> InventorySlots;
    [SerializeField] private List<ItemSO> Items;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private InventoryUI inventoryUI;
    private QuestReceiver questReceiver;
    private InventorySlot currentInventorySlot;

    private void Start()
    {
        questReceiver = GetComponent<QuestReceiver>();
    }
    public void SetItemData(InventorySlot inventorySlot)
    {
        currentInventorySlot = inventorySlot;

        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i] == inventorySlot)
            {
                InventorySlots[i].Select(true);
            }
            else
            {
                InventorySlots[i].Select(false);
            }
        }

        if (inventorySlot.InventoryItem != null)
        {
            inventoryUI.SetItemData(inventorySlot.InventoryItem.Item);
        }
        else
        {
            inventoryUI.SetItemData(null);
        }
    }
    public bool AddItem(ItemSO item)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                Items.Add(item);
                OnAddItem?.Invoke(item);
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }
    private void SpawnNewItem(ItemSO item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }
    public void UseSelectedItem()
    {
        InventoryItem inventoryItem = currentInventorySlot.InventoryItem;

        if (inventoryItem != null)
        {
            if (questReceiver.QuestGiver == null) return;

            QuestGiver questGiver = questReceiver.QuestGiver;
            GameManager.Instance.UIManager.InventoryUI.HandleHideInventory();

            if (inventoryItem.Item == questGiver.QuestSO.GetItem())
            {              
                Items.Remove(inventoryItem.Item);
                Destroy(inventoryItem.gameObject);
                GameManager.Instance.QuestUI.SetDialogType(DialogType.CorrectQuest);
                SoundManager.PlaySound(SoundManager.SoundFX.CorrectItem);

                EventSetup eventSetup = questGiver.EventSetup;
                if (eventSetup != null) eventSetup.ActiveEvent();
                questGiver.SetQuestFinish();
            }
            else
            {
                GameManager.Instance.QuestUI.SetDialogType(DialogType.WrongQuest);
                SoundManager.PlaySound(SoundManager.SoundFX.WrongItem);
            }
        }
    }
}
