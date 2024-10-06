using UnityEngine;

public class QuestReceiver : MonoBehaviour
{
    public QuestGiver QuestGiver {  get; private set; } 
    [SerializeField] private EventChannelSO eventChannelSO;
    private void OnEnable()
    {
        eventChannelSO.onQuestGiver.AddListener(SetQuestGiver);
        QuestUI.OnFinishQuestDialog += HandleGetReward;
    }
    private void OnDisable()
    {
        eventChannelSO.onQuestGiver.RemoveListener(SetQuestGiver);
        QuestUI.OnFinishQuestDialog -= HandleGetReward;
    }
    private void SetQuestGiver(QuestGiver questGiver)
    {
        QuestGiver = questGiver;
    }
    private void HandleGetReward()
    {
        if (QuestGiver.QuestSO.GetItemReward() == null) return;
        eventChannelSO.RaiseEvent(QuestGiver.QuestSO.GetItemReward());
        QuestGiver.gameObject.SetActive(false);
    }
}
