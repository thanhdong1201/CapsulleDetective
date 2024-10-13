using System;
using UnityEngine;

public class QuizzHandler : InteractBase
{
    [SerializeField] private InputReaderSO input;
    [SerializeField] private GameObject quizzUIObject;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private VoidEventChannelSO openInventoryEvent;
    [SerializeField] private VoidEventChannelSO closeInventoryEvent;
    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
    }
    public override void Interact()
    {
        base.Interact();
        quizzUIObject.SetActive(true);
        //uiManager.InventoryUI.HandleShowInventory();
        openInventoryEvent.RaiseEvent();
        SoundManager.PlaySound(SoundManager.SoundFX.GetQuest);
    }
    public void CloseTab()
    {
        quizzUIObject.SetActive(false);
        //uiManager.InventoryUI.HandleHideInventory();
        closeInventoryEvent.RaiseEvent();
        input.SetGamePlayInput();
    }
}
