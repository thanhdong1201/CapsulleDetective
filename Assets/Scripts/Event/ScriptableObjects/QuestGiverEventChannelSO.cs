using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Quest Giver Event Channel")]
public class QuestGiverEventChannelSO : DescriptionBaseSO
{
    public UnityAction<QuestGiver> OnEventRaised;

    public void RaiseEvent(QuestGiver questGiver)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(questGiver);
    }
}
