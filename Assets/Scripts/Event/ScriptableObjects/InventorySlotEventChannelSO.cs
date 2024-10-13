using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Inventory Slot Event Channel")]
public class InventorySlotEventChannelSO : DescriptionBaseSO
{
    public UnityAction<InventorySlot> OnEventRaised;

    public void RaiseEvent(InventorySlot inventorySlot)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(inventorySlot);
    }
}
