using UnityEngine;

public class QuestReceiver : MonoBehaviour
{
    public QuestGiver QuestGiver {  get; private set; }
    private QuestUI questUI;

    private void Start()
    {
        questUI = GameManager.Instance.QuestUI;
        GameManager.Instance.QuestUI.OnFinishQuestDialog += HandleGetReward;
    }
    public void GetQuest(QuestGiver questGiver)
    {
        QuestGiver = questGiver;
        if (questGiver.QuestSO.IsQuestFinished()) return;
        if (questGiver.QuestSO.GetItem() != null) 
        {         
            questUI.SetQuest(questGiver);
        }
        if (questGiver.QuestSO.GetItem() == null)
        {
            questUI.SetDialog(QuestGiver.QuestSO.GetDialog());
        }
    }
    private void HandleGetReward()
    {
        if (QuestGiver.QuestSO.GetItemReward() == null) return;
        GameManager.Instance.Inventory.AddItem(QuestGiver.QuestSO.GetItemReward());
        QuestGiver.SetQuestFinish();
    }
}
