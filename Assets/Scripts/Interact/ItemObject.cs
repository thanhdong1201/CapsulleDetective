using System;
using UnityEngine;

public class ItemObject : InteractBase
{
    [SerializeField] private ItemSO item;
    [SerializeField] private GameObject itemObject;

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
    }
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.Inventory.AddItem(item);
        SoundManager.PlaySound(SoundManager.SoundFX.PickUp);
        Destroy(itemObject);
    }
}
