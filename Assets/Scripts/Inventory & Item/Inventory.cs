//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class Inventory : MonoBehaviour
//{
//    public static event Action<ItemSO> OnInventory;
//    public static event Action<DialogType> OnDialogType;

//    [SerializeField] private Dictionary<ItemSO, InventorySlot> itemDictionary;
//    [SerializeField] private List<InventorySlot> InventorySlots;
//    [SerializeField] private List<ItemSO> Items;
//    [SerializeField] private GameObject inventoryItemPrefab;
//    [SerializeField] private InventoryUI inventoryUI;
//    [SerializeField] private UIManager uiManager;
//    [SerializeField] private ItemEventChannelSO onAddItem;
//    [SerializeField] private ItemEventChannelSO onRemoveItem;
//    //private QuestReceiver questReceiver;
//    private InventorySlot currentInventorySlot;

//    private void OnEnable()
//    {
//        //foreach (InventorySlot slot in InventorySlots) 
//        //{
//        //    slot.OnInventorySlot += OnSetItemData;
//        //}
//        onAddItem.OnEventRaised += AddItemToInventory;
//        onRemoveItem.OnEventRaised += RemoveItem;
//    }
//    private void OnDisable()
//    {
//        //foreach (InventorySlot slot in InventorySlots)
//        //{
//        //    slot.OnInventorySlot -= OnSetItemData;
//        //}
//        onAddItem.OnEventRaised -= AddItemToInventory;
//        onRemoveItem.OnEventRaised -= RemoveItem;
//    }
//    private void Start()
//    {
//        //questReceiver = GetComponent<QuestReceiver>();
//        itemDictionary = new Dictionary<ItemSO, InventorySlot>();
//        for (int i = 0; i < InventorySlots.Count; i++)
//        {
//            Items.Add(null);
//        }
//    }
//    private void Update()
//    {
//        //UpdateInventory();
//    }
//    private void UpdateInventory()
//    {
//        for (int i = 0; i < InventorySlots.Count; i++)
//        {
//            InventoryItem inventoryItem = InventorySlots[i].GetComponentInChildren<InventoryItem>();
//            if (inventoryItem != null)
//            {
//                Items[i] = inventoryItem.Item;
//            }
//            if (inventoryItem == null)
//            {
//                Items[i] = null;
//            }
//        }
//    }
//    private void OnSetItemData(InventorySlot inventorySlot)
//    {
//        currentInventorySlot = inventorySlot;

//        for (int i = 0; i < InventorySlots.Count; i++)
//        {
//            if (InventorySlots[i] == inventorySlot) InventorySlots[i].Select(true);
//            if (InventorySlots[i] != inventorySlot) InventorySlots[i].Select(false);
//        }

//        InventoryItem inventoryItem = currentInventorySlot.GetComponentInChildren<InventoryItem>();
//        if (inventoryItem != null) OnInventory?.Invoke(inventoryItem.Item);
//        if (inventoryItem == null) OnInventory?.Invoke(null);
//    }
//    private void AddItemToInventory(ItemSO item)
//    {
//        AddItem(item);
//    }
//    private bool AddItem(ItemSO item)
//    {
//        for (int i = 0; i < InventorySlots.Count; i++)
//        {
//            InventorySlot slot = InventorySlots[i];
//            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
//            if (itemInSlot == null)
//            {
//                if (!itemDictionary.ContainsKey(item))
//                {
//                    GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
//                    InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
//                    inventoryItem.InitializeItem(item);
//                    itemDictionary.Add(item, slot);
//                }
//                return true;
//            }
//        }
//        return false;
//    }
//    private void RemoveItem(ItemSO item)
//    {
//        if (itemDictionary.ContainsKey(item))
//        {
//            InventorySlot slot = itemDictionary[item];
//            itemDictionary.Remove(item);
//        }
//    }
//    public void UseSelectedItem()
//    {
//        InventoryItem inventoryItem = currentInventorySlot.InventoryItem;

//        //if (inventoryItem == null || questReceiver.QuestGiver.QuestSO == null) return;

//        //QuestSO questSO = questReceiver.QuestGiver.QuestSO;
//        //uiManager.InventoryUI.HandleHideInventory();

//        //if (inventoryItem.Item == questSO.GetItem())
//        //{
//        //    RemoveItem(inventoryItem.Item);
//        //    Destroy(inventoryItem.gameObject);
//        //    OnDialogType?.Invoke(DialogType.CorrectQuest);
//        //    SoundManager.PlaySound(SoundManager.SoundFX.CorrectItem);
//        //    questReceiver.QuestGiver.EventSetup?.ActiveEvent();
//        //    questReceiver.QuestGiver.SetQuestFinish();
//        //}
//        //else
//        //{
//        //    OnDialogType?.Invoke(DialogType.WrongQuest);
//        //    SoundManager.PlaySound(SoundManager.SoundFX.WrongItem);
//        //}
//    }
//}
