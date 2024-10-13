using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Data/QuestSO")]
public class QuestSO : ScriptableObject
{
    public string QuestName;
    public ItemSO ItemSO;
    public DialogSO DialogSO;
    public ItemSO ItemReward;
    private bool isQuestFinished;
    public ItemSO GetItem() { return ItemSO; }
    public ItemSO GetItemReward() { return ItemReward; }
    public DialogSO GetDialog() { return DialogSO; }
    public bool IsQuestFinished() { return isQuestFinished; }
    public void SetQuestFinished()  {   isQuestFinished = true; }

    private void OnEnable()
    {
        isQuestFinished = false;
    }
}
