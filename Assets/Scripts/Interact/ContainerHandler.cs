using System;
using UnityEngine;

public class ContainerHandler : InteractBase
{
    [SerializeField] private ItemSO item;
    [SerializeField] private ItemEventChannelSO onAddItem;

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
    }
    public override void Interact()
    {
        base.Interact();
        onAddItem.RaiseEvent(item);
        SoundManager.PlaySound(SoundManager.SoundFX.GetQuest);       
    }
}
