using UnityEngine;

public class ItemObject : InteractBase
{
    [SerializeField] private ItemEventChannelSO onAddItem;
    [SerializeField] private ItemSO item;
    [SerializeField] private GameObject itemObject;

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
    }

    public override void Interact()
    {
        base.Interact();
        onAddItem?.RaiseEvent(item);
        SoundManager.PlaySound(SoundManager.SoundFX.PickUp);
        Destroy(itemObject);
    }
}
