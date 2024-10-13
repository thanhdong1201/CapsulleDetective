//using UnityEngine;

//public class QuestReceiver : MonoBehaviour
//{
//    public QuestGiver QuestGiver {  get; private set; }
//    [SerializeField] private QuestGiverEventChannelSO getQuestGiverEvent;
//    [SerializeField] private QuestEventChannelSO getQuestEvent;
//    [SerializeField] private ItemEventChannelSO addItemEvent;

//    [SerializeField] private VoidEventChannelSO onEnterDialogEvent;
//    [SerializeField] private VoidEventChannelSO onCompleteDialogEvent;
//    [SerializeField] private VoidEventChannelSO onGiveItemEvent;

//    private void OnEnable()
//    {
//        getQuestEvent.OnEventRaised += SetQuestGiver;
//        onCompleteDialogEvent.OnEventRaised += HandleGetReward;
//    }
//    private void OnDisable()
//    {
//        getQuestEvent.OnEventRaised -= SetQuestGiver;
//        onCompleteDialogEvent.OnEventRaised -= HandleGetReward;
//    }
//    private void SetQuestGiver(QuestGiver questGiver)
//    {
//        QuestGiver = questGiver;
//        getQuestGiverEvent.RaiseEvent(QuestGiver);
//    }
//    private void HandleGetReward()
//    {
//        if (QuestGiver.QuestSO.GetItemReward() == null) return;
//        addItemEvent.RaiseEvent(QuestGiver.QuestSO.GetItemReward());
//        QuestGiver.gameObject.SetActive(false);
//    }
//}
