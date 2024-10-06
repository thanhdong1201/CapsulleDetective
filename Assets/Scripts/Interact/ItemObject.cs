using UnityEngine;

public class ItemObject : InteractBase
{
    [SerializeField] private EventChannelSO eventChannelSO;
    [SerializeField] private ItemSO item;
    [SerializeField] private GameObject itemObject;

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
    }

    public override void Interact()
    {
        base.Interact();
        eventChannelSO.RaiseEvent(item);
        SoundManager.PlaySound(SoundManager.SoundFX.PickUp);
        Destroy(itemObject);
    }
}
