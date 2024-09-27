using UnityEngine;

public class ContainerHandler : InteractBase
{
    [SerializeField] private ItemSO item;

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
    }
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.Inventory.AddItem(item);
        SoundManager.PlaySound(SoundManager.SoundFX.GetQuest);       
    }
}
