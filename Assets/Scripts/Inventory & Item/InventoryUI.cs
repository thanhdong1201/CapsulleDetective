//using DG.Tweening;
//using System;
//using System.Collections;
//using TMPro;
//using UnityEngine;

//public class InventoryUI : MonoBehaviour
//{
//    [SerializeField] private TextMeshProUGUI nameText;
//    [SerializeField] private TextMeshProUGUI descriptionText;
//    [SerializeField] private InputReaderSO input;
//    [SerializeField] private GameObject inventoryUIObject;
//    [SerializeField] private Inventory inventory;

//    [SerializeField] private VoidEventChannelSO onEnterDialogEvent;
//    [SerializeField] private VoidEventChannelSO onCompleteDialogEvent;

//    private CanvasGroup canvasGroup;
//    private RectTransform rectTransform;
//    private float fadeTime = 0.25f;
//    private bool canAction;

//    private void OnEnable()
//    {
//        Inventory.OnInventory += OnSetItem;

//        onEnterDialogEvent.OnEventRaised += HandleShowInventory;

//        input.TabEvent += HandleShowInventory;
//        input.CLoseTabEvent += HandleHideInventory;
//    }
//    private void OnDisable()
//    {
//        Inventory.OnInventory -= OnSetItem;

//        onEnterDialogEvent.OnEventRaised -= HandleShowInventory;

//        input.TabEvent -= HandleShowInventory;
//        input.CLoseTabEvent -= HandleHideInventory;
//    }
//    private void Start()
//    {
//        canvasGroup = GetComponent<CanvasGroup>();
//        rectTransform = GetComponent<RectTransform>();
//        canAction = true;
//    }
//    public void UseSelectedItemButton()
//    {
//        inventory.UseSelectedItem();
//        ResetItemData();
//    }
//    private void ResetItemData()
//    {
//        nameText.SetText("");
//        descriptionText.SetText("");
//    }
//    private void OnSetItem(ItemSO itemSO)
//    {
//        if (itemSO != null)
//        {
//            nameText.SetText(itemSO.ItemName);
//            descriptionText.SetText(itemSO.Description);
//        }
//        else
//        {
//            nameText.SetText("");
//            descriptionText.SetText("");
//        }
//    }
//    public void HandleShowInventory()
//    {
//        if (!canAction) return;
//        StartCoroutine("ResetAction");
//        SoundManager.PlaySound(SoundManager.SoundFX.OpenInventory);
//        inventoryUIObject.SetActive(true);
//        rectTransform.transform.localPosition = new Vector3(-1000f, 0f, 0f);
//        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutCirc);
//        input.SetUIInput();
//    }
//    public void HandleHideInventory()
//    {
//        if (!canAction) return;
//        StartCoroutine("ResetAction");
//        SoundManager.PlaySound(SoundManager.SoundFX.CloseInventory);
//        StartCoroutine("WaitASec");
//        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
//        rectTransform.DOAnchorPos(new Vector2(-1000f, 0f), fadeTime, false).SetEase(Ease.OutCirc);
//        input.SetGamePlayInput();
//        inventoryUIObject.SetActive(false);
//    }
//    private IEnumerator ResetAction()
//    {
//        canAction = false;
//        yield return new WaitForSeconds(fadeTime + 0.1f);
//        canAction = true;
//    }
//    private IEnumerator WaitASec()
//    {
//        yield return new WaitForSeconds(fadeTime + 0.1f);
//        inventoryUIObject.SetActive(false);
//    }
//}
