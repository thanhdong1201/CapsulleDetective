using UnityEngine;

public class InteractVisual : MonoBehaviour
{
    [SerializeField] private GameObject interactVisualGameObject;

    public void Show()
    {
        interactVisualGameObject.SetActive(true);
    }
    public void Hide()
    {
        interactVisualGameObject.SetActive(false);
    }
}
