using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using System.Collections;

public enum DialogType
{
    Dialog,
    EnterQuest,
    WrongQuest,
    CorrectQuest
}
public class QuestUI : MonoBehaviour
{
    public static Action OnFinishDialog;
    public static Action OnEnterQuestDialog;
    public static Action OnFinishQuestDialog;

    [SerializeField] private TextMeshProUGUI conversationText;
    [SerializeField] private GameObject questUIObject;
    [SerializeField] private QuestReceiver questReceiver;
    [SerializeField] private InputReaderSO input;
    [SerializeField] private EventChannelSO eventChannelSO;

    private float fadeTime = 1.0f;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private QuestSO questSO;
    private DialogSO dialogSO;
    private int index;
    private DialogType dialogType;

    private void OnEnable()
    {
        eventChannelSO.onQuestGiver.AddListener(SetQuestGiver);
        input.NextEvent += PlayDialog;
        Inventory.OnDialogType += SetDialogType;
    }
    private void OnDisable()
    {
        eventChannelSO.onQuestGiver.RemoveListener(SetQuestGiver);
        input.NextEvent -= PlayDialog;
        Inventory.OnDialogType -= SetDialogType;
    }
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void SetQuestGiver(QuestGiver questGiver)
    {
        QuestSO questSO = questGiver.QuestSO;
        if (questSO.IsQuestFinished()) return;
        if (questSO.GetItem() != null)
        {
            this.questSO = questSO;
            SetDialogType(DialogType.EnterQuest);
        }
        if (questSO.GetItem() == null)
        {
            this.dialogSO = questSO.GetDialog();
            SetDialogType(DialogType.Dialog);
        }       
    }
    private void SetDialogType(DialogType dialog)
    {
        dialogType = dialog;
        index = -1;
        ShowDialog();
        PlayDialog();
    }
    private void ShowDialog()
    {
        questUIObject.SetActive(true);
        canvasGroup.alpha = 0.0f;
        canvasGroup.DOFade(1, fadeTime);
        canvasGroup.transform.localScale = Vector3.zero;
        canvasGroup.transform.DOScale(1f, 0.75f).SetEase(Ease.OutBounce);
        rectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutExpo);
        input.SetDialogueInput();
    }
    private void HideDialog()
    {
        switch (dialogType)
        {
            case DialogType.Dialog:
                OnFinishQuestDialog.Invoke();
                input.SetGamePlayInput();
                break;
            case DialogType.EnterQuest:
                if (!questSO.IsQuestFinished()) { OnEnterQuestDialog?.Invoke(); }
                else { input.SetGamePlayInput(); }
                break;
            case DialogType.CorrectQuest:
                OnFinishQuestDialog?.Invoke();
                input.SetGamePlayInput();
                break;
            case DialogType.WrongQuest:
                input.SetGamePlayInput();
                break;
        }
        questUIObject.SetActive(false);
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2.5f);
        HideDialog();
    }
    private void PlayDialog()
    {
        index++;
        switch (dialogType)
        {
            case DialogType.Dialog:
                if(dialogSO == null) return;
                if (index >= dialogSO.GetDialogs().Length) HideDialog();
                else conversationText.text = dialogSO.GetDialogs()[index];
                break;
            case DialogType.EnterQuest:
                if (index >= 1) { HideDialog(); index = -1; }
                else { conversationText.text = questSO.GetAcceptQuestConversationText(); }
                break;
            case DialogType.CorrectQuest:
                if (index >= 1) { HideDialog(); index = -1; }
                else conversationText.text = questSO.GetFinishQuestConversationText();
                StartCoroutine("WaitASec");
                break;
            case DialogType.WrongQuest:
                if (index >= 1) { HideDialog(); index = -1; }
                else conversationText.text = "Its not correct!";
                break;
        }
    }
}
