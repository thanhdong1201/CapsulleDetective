using UnityEngine;

public class SwitchPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject laserFences;

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
        laserFences.SetActive(false);
    }
}
