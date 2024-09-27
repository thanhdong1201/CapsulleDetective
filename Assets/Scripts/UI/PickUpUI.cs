using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PickUpUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    private float fadeTime = 0.5f;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        Inventory.OnAddItem += OnPlayerPickUpItem;
    }

    public void OnPlayerPickUpItem(ItemSO item)
    {
        itemImage.sprite = item.Sprite;
        ShowPickUI();
    }

    private void ShowPickUI()
    {
        canvasGroup.alpha = 0.0f;
        rectTransform.transform.localPosition = new Vector3(1000f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.OutQuad);
        canvasGroup.DOFade(1, fadeTime);
        canvasGroup.transform.localScale = Vector3.zero;
        canvasGroup.transform.DOScale(1f, fadeTime).SetEase(Ease.InOutBounce);
        StartCoroutine("WaitASec");
    }
    private void HidePickUpUI()
    {
        canvasGroup.alpha = 1.0f;
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(new Vector2(1000f, 0f), fadeTime, false).SetEase(Ease.InQuad);
        canvasGroup.DOFade(0, fadeTime);
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2.0f);
        HidePickUpUI();
    }
}
