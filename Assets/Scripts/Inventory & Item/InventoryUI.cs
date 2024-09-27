using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private InputReaderSO input;
    [SerializeField] private GameObject inventoryUIObject;
    private Inventory inventory;
    private QuestUI questUI;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private float fadeTime = 0.25f;
    private bool canAction;

    private void Start()
    {
        inventory = GameManager.Instance.Inventory; 
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        questUI = GameManager.Instance.QuestUI;
        questUI.OnEnterQuestDialog += HandleShowInventory;

        input.OpenInventoryEvent += HandleShowInventory;
        input.CLoseInventoryEvent += HandleHideInventory;

        canAction = true;
    }

    public void UseSelectedItemButton()
    {
        inventory.UseSelectedItem();
        ResetItemData();
    }
    private void ResetItemData()
    {
        nameText.SetText("");
        descriptionText.SetText("");
    }
    public void SetItemData(ItemSO item)
    {
        if (item != null)
        {
            nameText.SetText(item.ItemName);
            descriptionText.SetText(item.Description);
        }
        else
        {
            nameText.SetText("");
            descriptionText.SetText("");
        }
    }
    private IEnumerator ResetAction()
    {
        canAction = false;
        yield return new WaitForSeconds(fadeTime + 0.1f);
        canAction = true;
    }
    public void HandleShowInventory()
    {
        if (!canAction || inventoryUIObject == null) return;
        StartCoroutine("ResetAction");
        SoundManager.PlaySound(SoundManager.SoundFX.OpenInventory);
        inventoryUIObject.SetActive(true);
        rectTransform.transform.localPosition = new Vector3(-1000f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutCirc);
        input.SetUIInput();
    }
    public void HandleHideInventory()
    {
        if (!canAction || inventoryUIObject == null) return;
        StartCoroutine("ResetAction");
        SoundManager.PlaySound(SoundManager.SoundFX.CloseInventory);
        StartCoroutine("WaitASec");
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(-1000f, 0f), fadeTime, false).SetEase(Ease.OutCirc);
        input.SetGamePlayInput();
    }
    private IEnumerator WaitASec()
    {
        canAction = false;
        yield return new WaitForSeconds(fadeTime + 0.1f);
        inventoryUIObject.SetActive(false);
    }
}
