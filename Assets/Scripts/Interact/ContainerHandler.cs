using System;
using UnityEngine;

public class ContainerHandler : InteractBase
{
    [SerializeField] private ItemSO item;
    [SerializeField] private EventChannelSO eventChannelSO;

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
    }
    public override void Interact()
    {
        base.Interact();
        eventChannelSO.RaiseEvent(item);
        SoundManager.PlaySound(SoundManager.SoundFX.GetQuest);       
    }
}
