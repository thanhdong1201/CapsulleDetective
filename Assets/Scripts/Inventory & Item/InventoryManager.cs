using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Dictionary<ItemSO, InventorySlot> itemDictionary;
    [SerializeField] private List<InventorySlot> inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject uiInventoryGameObject;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Listening to channels")]
    [SerializeField] private InventorySlotEventChannelSO chooseInventorySlotEvent;
    [SerializeField] private VoidEventChannelSO openInventoryEvent;

    [Header("Broadcasting on channels")]
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private QuestGiverEventChannelSO getQuestGiverEvent;
    [SerializeField] private ItemEventChannelSO addItemEvent;
    [SerializeField] private ItemEventChannelSO removeItemEvent;
    [SerializeField] private VoidEventChannelSO correctDialogEvent;
    [SerializeField] private VoidEventChannelSO incorrectDialogueEvent;

    private InventorySlot currentInventorySlot;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private QuestGiver questGiver;
    private float fadeTime = 0.25f;
    private bool canAction;

    private void OnEnable()
    {
        inputReader.TabEvent += OnOpenInventory;
        inputReader.CLoseTabEvent += OnCloseInventory;

        chooseInventorySlotEvent.OnEventRaised += OnSetItem;
        openInventoryEvent.OnEventRaised += OnOpenInventory;

        getQuestGiverEvent.OnEventRaised += GetQuestGiver;
        addItemEvent.OnEventRaised += AddItem;
        removeItemEvent.OnEventRaised += RemoveItem;
    }
    private void OnDisable()
    {
        inputReader.TabEvent -= OnOpenInventory;
        inputReader.CLoseTabEvent -= OnCloseInventory;

        chooseInventorySlotEvent.OnEventRaised -= OnSetItem;
        openInventoryEvent.OnEventRaised -= OnOpenInventory;

        getQuestGiverEvent.OnEventRaised -= GetQuestGiver;
        addItemEvent.OnEventRaised -= AddItem;
        removeItemEvent.OnEventRaised -= RemoveItem;
    }
    private void UpdateInventory()
    {

    }
    private void Start()
    {
        itemDictionary = new Dictionary<ItemSO, InventorySlot>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canAction = true;
    }
    private void Update()
    {
        UpdateInventory();
    }
    private void GetQuestGiver(QuestGiver questGiver)
    {
        this.questGiver = questGiver;
    }
    //UI setup
    private void OnOpenInventory()
    {
        if (!canAction) return;
        StartCoroutine("ResetAction");
        SoundManager.PlaySound(SoundManager.SoundFX.OpenInventory);
        uiInventoryGameObject.SetActive(true);
        rectTransform.transform.localPosition = new Vector3(-1000f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutCirc);
        inputReader.SetUIInput();
    }
    public void OnCloseInventory()
    {
        if (!canAction) return;
        StartCoroutine("ResetAction");
        SoundManager.PlaySound(SoundManager.SoundFX.CloseInventory);
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(-1000f, 0f), fadeTime, false).SetEase(Ease.OutCirc);
        uiInventoryGameObject.SetActive(false);
        inputReader.SetGamePlayInput();
    }
    private IEnumerator ResetAction()
    {
        canAction = false;
        yield return new WaitForSeconds(fadeTime);
        canAction = true;
    }
    private void SetDescription(ItemSO itemSO)
    {
        if (itemSO != null)
        {
            nameText.SetText(itemSO.ItemName);
            descriptionText.SetText(itemSO.Description);
        }
        else
        {
            ResetDescription();
        }
    }
    private void ResetDescription()
    {
        nameText.SetText("");
        descriptionText.SetText("");
    }

    //Logic
    private void OnSetItem(InventorySlot inventorySlot)
    {
        currentInventorySlot = inventorySlot;

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i] == inventorySlot) inventorySlots[i].Select(true);
            if (inventorySlots[i] != inventorySlot) inventorySlots[i].Select(false);
        }

        InventoryItem inventoryItem = currentInventorySlot.GetComponentInChildren<InventoryItem>();
        if (inventoryItem != null) SetDescription(inventoryItem.Item);
        if (inventoryItem == null) SetDescription(null);
    }
    private void AddItem(ItemSO item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                if (!itemDictionary.ContainsKey(item))
                {
                    GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
                    InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
                    inventoryItem.InitializeItem(item);
                    itemDictionary.Add(item, slot);
                }
                return;
            }
        }
    }
    private void RemoveItem(ItemSO item)
    {
        if (itemDictionary.ContainsKey(item))
        {
            InventorySlot slot = itemDictionary[item];
            itemDictionary.Remove(item);
        }
    }
    public void UseSelectedItem()
    {
        InventoryItem inventoryItem = currentInventorySlot.InventoryItem;
        QuestSO questSO = questGiver.QuestSO;
        if (inventoryItem == null || questSO == null) return;
        OnCloseInventory();

        if (inventoryItem.Item == questSO.GetItem())
        {
            RemoveItem(inventoryItem.Item);
            Destroy(inventoryItem.gameObject);
            correctDialogEvent.RaiseEvent();
            SoundManager.PlaySound(SoundManager.SoundFX.CorrectItem);
            questGiver.EventSetup?.ActiveEvent();
            questGiver.SetQuestFinish();
        }
        else
        {
            incorrectDialogueEvent.RaiseEvent();
            SoundManager.PlaySound(SoundManager.SoundFX.WrongItem);
        }

        ResetDescription();
    }
}
