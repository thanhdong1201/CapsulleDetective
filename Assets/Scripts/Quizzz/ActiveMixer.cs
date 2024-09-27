using UnityEngine;

public class ActiveMixer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MixerQuizz mixerQuizz;
    private EventSetup eventSetup;

    private void Start()
    {
        eventSetup = GetComponent<EventSetup>();
        eventSetup.OnPrepareEvent += PrepareEvent;
        eventSetup.OnActiveEvent += ActiveEvent;
    }
    private void PrepareEvent()
    {
        animator.SetTrigger("Active");
    }
    private void ActiveEvent()
    {
        mixerQuizz.Active();
    }
}
