using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PickUpUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private EventChannelSO eventChannelSO;

    private float fadeTime = 0.5f;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void OnEnable()
    {
        eventChannelSO.onItemPickedUp.AddListener(AddItem);
        //DOTween.Init(true, true, LogBehaviour.ErrorsOnly);
    }
    private void OnDisable()
    {
        eventChannelSO.onItemPickedUp.RemoveListener(AddItem);
    }
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void AddItem(ItemSO itemSO)
    {
        itemImage.sprite = itemSO.Sprite;
        ShowPickUI();
    }
    private void ShowPickUI()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.DOFade(1, fadeTime);
        canvasGroup.transform.localScale = Vector3.zero;
        canvasGroup.transform.DOScale(1f, fadeTime).SetEase(Ease.InOutBounce);
        rectTransform.transform.localPosition = new Vector3(1000f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutQuad);
        StartCoroutine("WaitASec");
    }
    private void HidePickUpUI()
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.DOFade(0, fadeTime);
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(1000f, 0f), fadeTime, false).SetEase(Ease.InQuad);
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(1.5f);
        HidePickUpUI();
    }
}
