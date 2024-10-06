using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<ItemSO> OnInventory;
    public static event Action<DialogType> OnDialogType;

    [SerializeField] private Dictionary<ItemSO, InventorySlot> itemDictionary;

    [SerializeField] private List<InventorySlot> InventorySlots;
    [SerializeField] private List<ItemSO> Items;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private InventoryUI inventoryUI;
    private QuestReceiver questReceiver;
    private InventorySlot currentInventorySlot;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EventChannelSO eventChannelSO;

    private void AddItemToInventory(ItemSO item)
    {
        AddItem(item);
    }
    private void OnEnable()
    {
        foreach (InventorySlot slot in InventorySlots) 
        {
            slot.OnInventorySlot += OnSetItemData;
        }

        eventChannelSO.onItemPickedUp.AddListener(AddItemToInventory);
        eventChannelSO.onItemRemove.AddListener(RemoveItem);
    }
    private void OnDisable()
    {
        foreach (InventorySlot slot in InventorySlots)
        {
            slot.OnInventorySlot -= OnSetItemData;
        }

        eventChannelSO.onItemPickedUp.RemoveListener(AddItemToInventory);
        eventChannelSO.onItemRemove.RemoveListener(RemoveItem);
    }
    private void Start()
    {
        questReceiver = GetComponent<QuestReceiver>();
        itemDictionary = new Dictionary<ItemSO, InventorySlot>();
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            Items.Add(null);
        }
    }
    private void Update()
    {
        UpdateInventory();
    }
    private void UpdateInventory()
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventoryItem inventoryItem = InventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null)
            {
                Items[i] = inventoryItem.Item;
            }
            if (inventoryItem == null)
            {
                Items[i] = null;
            }
        }
    }
    private void OnSetItemData(InventorySlot inventorySlot)
    {
        currentInventorySlot = inventorySlot;

        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i] == inventorySlot) InventorySlots[i].Select(true);
            if (InventorySlots[i] != inventorySlot) InventorySlots[i].Select(false);
        }

        InventoryItem inventoryItem = currentInventorySlot.GetComponentInChildren<InventoryItem>();
        if (inventoryItem != null) OnInventory?.Invoke(inventoryItem.Item);
        if (inventoryItem == null) OnInventory?.Invoke(null);

        UpdateInventory();
    }
    private bool AddItem(ItemSO item)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                if (itemDictionary.ContainsKey(item))
                {
                    // Nếu item tồn tại
                }
                else
                {
                    // Nếu item chưa tồn tại, tạo mới và thêm vào dictionary
                    GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
                    InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
                    inventoryItem.InitializeItem(item);
                    itemDictionary.Add(item, slot);
                    Debug.Log("Add " + item + " to inventory.");
                }
                UpdateInventory();
                return true;
            }
        }
        return false;
    }
    private void RemoveItem(ItemSO item)
    {
        if (itemDictionary.ContainsKey(item))
        {
            InventorySlot slot = itemDictionary[item];
            itemDictionary.Remove(item);
            Debug.Log("Xóa " + item + " khỏi inventory.");
        }
        else
        {
            Debug.Log("Không tìm thấy item " + item + " để xóa.");
        }
    }
    public void UseSelectedItem()
    {
        InventoryItem inventoryItem = currentInventorySlot.InventoryItem;

        if (inventoryItem != null)
        {
            if (questReceiver.QuestGiver.QuestSO == null) return;

            QuestSO questSO = questReceiver.QuestGiver.QuestSO;
            uiManager.InventoryUI.HandleHideInventory();

            if (inventoryItem.Item == questSO.GetItem())
            {
                RemoveItem(inventoryItem.Item);
                Destroy(inventoryItem.gameObject);
                OnDialogType?.Invoke(DialogType.CorrectQuest);
                SoundManager.PlaySound(SoundManager.SoundFX.CorrectItem);
                questReceiver.QuestGiver.EventSetup?.ActiveEvent();
                questReceiver.QuestGiver.SetQuestFinish();
            }
            else
            {
                OnDialogType?.Invoke(DialogType.WrongQuest);
                SoundManager.PlaySound(SoundManager.SoundFX.WrongItem);
            }
        }
    }
}
