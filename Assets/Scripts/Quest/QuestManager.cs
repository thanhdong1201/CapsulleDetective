using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Linstening to channels")]
    [SerializeField] private QuestEventChannelSO getQuestEvent;

    [Header("Broadcasting on channels")]
    [SerializeField] private VoidEventChannelSO startDialogEvent;
    [SerializeField] private VoidEventChannelSO completeQuestEvent;
    [SerializeField] private ItemEventChannelSO addItemEvent;
    [SerializeField] private QuestGiverEventChannelSO getQuestGiverEvent;
    [SerializeField] private DialogEventChannelSO getDialogEvent;
    [SerializeField] private InputReaderSO input;

    private QuestGiver questGiver;
    private QuestSO questSO;

    private void OnEnable()
    {
        getQuestGiverEvent.OnEventRaised += GetQuestGiver;
        completeQuestEvent.OnEventRaised += GetReward;
    }
    private void OnDisable()
    {
        getQuestGiverEvent.OnEventRaised -= GetQuestGiver;
        completeQuestEvent.OnEventRaised -= GetReward;
    }

    private void GetReward()
    {
        if (!questSO.IsQuestFinished()) addItemEvent.RaiseEvent(questSO.ItemReward);
        else input.SetGamePlayInput();
    }
    private void GetQuestGiver(QuestGiver questGiver)
    {
        this.questGiver = questGiver;
        questSO = questGiver.QuestSO;
        getDialogEvent.RaiseEvent(questGiver.QuestSO.GetDialog());
        startDialogEvent.RaiseEvent();
    }
}

