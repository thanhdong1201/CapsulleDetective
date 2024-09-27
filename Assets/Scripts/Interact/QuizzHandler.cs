using UnityEngine;

public class QuizzHandler : InteractBase
{
    [SerializeField] private InputReaderSO input;
    [SerializeField] private GameObject quizzUIObject;

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
    }
    public override void Interact()
    {
        base.Interact();
        quizzUIObject.SetActive(true);
        GameManager.Instance.UIManager.InventoryUI.HandleShowInventory();
        SoundManager.PlaySound(SoundManager.SoundFX.GetQuest);
    }
    public void CloseTab()
    {
        quizzUIObject.SetActive(false);
        GameManager.Instance.UIManager.InventoryUI.HandleHideInventory();
        input.SetGamePlayInput();
    }
}
