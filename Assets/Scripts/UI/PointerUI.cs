using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private float newScale = 1.1f;
    private float fadeTime = 0.1f;
    private CanvasGroup canvasGroup;

    private void OnDisable()
    {
        canvasGroup.transform.localScale = Vector3.one;
    }
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        canvasGroup.transform.DOScale(newScale, fadeTime).SetEase(Ease.Flash);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.transform.DOScale(1f, fadeTime).SetEase(Ease.Flash);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.PlaySound(SoundManager.SoundFX.ClickButton);
    }
}
