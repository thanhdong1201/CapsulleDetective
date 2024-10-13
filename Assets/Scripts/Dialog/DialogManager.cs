using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public enum DialogType
{
    NormalDialog,
    StartQuestDialog,
    IncorrectQuestDialog,
    CorrectQuestDialog
}
public class DialogManager : MonoBehaviour
{
    [Header("Linstening to channels")]
    [SerializeField] private DialogEventChannelSO getDialogEvent;
    [SerializeField] private VoidEventChannelSO normalDialogEvent;
    [SerializeField] private VoidEventChannelSO startDialogEvent;
    [SerializeField] private VoidEventChannelSO correctDialogEvent;
    [SerializeField] private VoidEventChannelSO incorrectDialogueEvent;

    [Header("Broadcasting on channels")]
    [SerializeField] private VoidEventChannelSO completeQuestEvent;
    [SerializeField] private VoidEventChannelSO openInventoryEvent;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private InputReaderSO input;

    private DialogSO dialogSO;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private int index;
    private DialogType dialogType;
    private float fadeTime = 1.0f;

    private void OnEnable()
    {
        normalDialogEvent.OnEventRaised += NormalDialog;
        startDialogEvent.OnEventRaised += StartQuestDialog;
        correctDialogEvent.OnEventRaised += CorrectQuestDialog;
        incorrectDialogueEvent.OnEventRaised += IncorrectQuestDialog;

        getDialogEvent.OnEventRaised += GetDialog;
        input.NextEvent += PlayDialog;
    }
    private void OnDisable()
    {
        normalDialogEvent.OnEventRaised -= NormalDialog;
        startDialogEvent.OnEventRaised -= StartQuestDialog;
        correctDialogEvent.OnEventRaised -= CorrectQuestDialog;
        incorrectDialogueEvent.OnEventRaised -= IncorrectQuestDialog;

        getDialogEvent.OnEventRaised -= GetDialog;
        input.NextEvent -= PlayDialog;
    }
    private void NormalDialog()
    {
        dialogType = DialogType.NormalDialog;
        index = -1;
        ShowDialog();
        PlayDialog();
    }
    private void StartQuestDialog()
    {
        dialogType = DialogType.StartQuestDialog;
        index = -1;
        ShowDialog();
        PlayDialog();
    }
    private void CorrectQuestDialog()
    {
        dialogType = DialogType.CorrectQuestDialog;
        index = -1;
        ShowDialog();
        PlayDialog();
    }
    private void IncorrectQuestDialog()
    {
        dialogType = DialogType.IncorrectQuestDialog;
        index = -1;
        ShowDialog();
        PlayDialog();
    }
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void GetDialog(DialogSO dialog)
    {
        dialogSO = dialog;
        index = -1;
        ShowDialog();
        PlayDialog();
    }
    private void ShowDialog()
    {
        dialogUI.SetActive(true);
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
            case DialogType.NormalDialog:
                input.SetGamePlayInput();
                break;
            case DialogType.StartQuestDialog:
                openInventoryEvent.RaiseEvent();
                break;
            case DialogType.CorrectQuestDialog:
                completeQuestEvent.RaiseEvent();
                input.SetGamePlayInput();
                break;
            case DialogType.IncorrectQuestDialog:
                //
                input.SetGamePlayInput();
                break;
        }
        dialogUI.SetActive(false);
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
            case DialogType.NormalDialog:
                if (index >= dialogSO.GetDialogs().Length) HideDialog();
                else dialogText.text = dialogSO.GetDialogs()[index];
                break;
            case DialogType.StartQuestDialog:
                if (index >= dialogSO.GetDialogs().Length) { HideDialog(); index = -1; }
                else dialogText.text = dialogSO.GetDialogs()[index];
                break;
            case DialogType.CorrectQuestDialog:
                if (index >= 1) { HideDialog(); index = -1; }
                else dialogText.text = dialogSO.GetDialogs()[index];
                StartCoroutine("WaitASec");
                break;
            case DialogType.IncorrectQuestDialog:
                if (index >= 1) { HideDialog(); index = -1; }
                else dialogText.text = "Its not correct!";
                break;
        }
    }
}
