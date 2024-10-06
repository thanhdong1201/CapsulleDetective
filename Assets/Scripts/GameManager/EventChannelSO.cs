using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "Data/EventSO")]
public class EventChannelSO : ScriptableObject
{
    public UnityEvent<ItemSO> onItemPickedUp;
    public UnityEvent<ItemSO> onItemRemove;
    public UnityEvent<QuestGiver> onQuestGiver;
    public UnityEvent onActiveEvent;

    public void RaiseEvent(ItemSO itemSO)
    {
        onItemPickedUp?.Invoke(itemSO);
    }
    public void RemoveItemEvent(ItemSO itemSO)
    {
        onItemRemove?.Invoke(itemSO);
    }
    public void RaiseEvent(QuestGiver questGiver)
    {
        onQuestGiver?.Invoke(questGiver);
    }
    public void RaiseEvent()
    {
        onActiveEvent?.Invoke();
    }
}
