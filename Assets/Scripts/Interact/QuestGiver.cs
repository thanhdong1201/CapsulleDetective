using System;
using UnityEngine;

public class QuestGiver : InteractBase
{
    public event Action OnPlayAnimation;
    public QuestSO QuestSO;
    public EventSetup EventSetup { get; private set; }
    [SerializeField] private QuestGiverEventChannelSO getQuestGiverEvent;

    private void Start()
    {
        interactVisual = GetComponent<InteractVisual>();
        EventSetup = GetComponent<EventSetup>();
    }
    public override void Interact()
    {
        base.Interact();
        getQuestGiverEvent.RaiseEvent(this);
        SoundManager.PlaySound(SoundManager.SoundFX.GetQuest);
    }
    public void SetQuestFinish()
    {
        OnPlayAnimation?.Invoke();
        QuestSO.SetQuestFinished();
        gameObject.SetActive(EventSetup != null);
    }
}
