using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Data/QuestSO")]
public class QuestSO : ScriptableObject
{
    [SerializeField] private ItemSO item;
    [SerializeField] private DialogSO dialog;
    [field: TextArea]
    [SerializeField] private string[] acceptQuestConversations;
    [field: TextArea]
    [SerializeField] private string finishQuestConversation;
    [SerializeField] private bool isQuestFinished;
    [SerializeField] private ItemSO itemReward;
    public ItemSO GetItem() { return item; }
    public ItemSO GetItemReward() { return itemReward; }
    public DialogSO GetDialog() { return dialog; }
    public string GetAcceptQuestConversationText() { return acceptQuestConversations[Random.Range(0, acceptQuestConversations.Length)]; }
    public string GetFinishQuestConversationText() { return finishQuestConversation; }
    public bool IsQuestFinished() { return isQuestFinished; }
    public void SetQuestFinished()  {   isQuestFinished = true; }
    public void ResetQuest()    {   isQuestFinished = false;    }
}
