using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject fireExtinguisher;

    private EventSetup eventSetup;

    private void Start()
    {
        eventSetup = GetComponent<EventSetup>();
        eventSetup.OnPrepareEvent += PrepareEvent;
        eventSetup.OnActiveEvent += ActiveEvent;

        fireExtinguisher.SetActive(false);
    }
    private void PrepareEvent()
    {
        fireExtinguisher.SetActive(true);
        animator.SetTrigger("Active");
    }
    private void ActiveEvent()
    {
        //fireVfx.SetActive(false);
    }
}
