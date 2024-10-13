using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// This class is used for Item interaction events.
/// Example: Pick up an item passed as paramater
/// </summary>

[CreateAssetMenu(menuName = "Events/Quest Event Channel")]
public class QuestEventChannelSO : DescriptionBaseSO
{
    public UnityAction<QuestSO> OnEventRaised;

    public void RaiseEvent(QuestSO questSO)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(questSO);
    }
}


