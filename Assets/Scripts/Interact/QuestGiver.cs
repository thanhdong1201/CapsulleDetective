using System;

public class QuestGiver : InteractBase
{
    public event Action OnPlayAnimation;
    public QuestSO QuestSO;

    private QuestReceiver questReceiver;
    public EventSetup EventSetup { get; private set; }

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
        EventSetup = GetComponent<EventSetup>();
        QuestSO.ResetQuest();
    }
    public override void Interact()
    {
        base.Interact();
        questReceiver = GameManager.Instance.QuestReceiver;
        questReceiver.GetQuest(this);
        SoundManager.PlaySound(SoundManager.SoundFX.GetQuest);
    }
    public void SetQuestFinish()
    {
        OnPlayAnimation?.Invoke();
        QuestSO.SetQuestFinished();
        if (EventSetup == null)
        {
            gameObject.SetActive(false);
        }
    }

}
