using System;
using System.Collections;
using UnityEngine;

public class EventSetup : MonoBehaviour
{
    public event Action OnActiveEvent;
    public event Action OnPrepareEvent;

    [SerializeField] private GameObject disableInteract;
    [SerializeField] private Transform target;
    [SerializeField] private float prepareWaitTime = 0.25f;
    private ChangeEventCamera eventCamera;
    private void Start()
    {
        eventCamera = FindObjectOfType<ChangeEventCamera>();
    }
    public void ActiveEvent()
    {
        OnPrepareEvent?.Invoke();  
        eventCamera.SetEventCamera(target);
        StartCoroutine("WaitASecBeforeActive");
    }

    private IEnumerator WaitASecBeforeActive()
    {
        yield return new WaitForSeconds(prepareWaitTime);
        eventCamera.EnableCamera();
        StartCoroutine("WaitASec");
    }
    private IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(2f);
        OnActiveEvent?.Invoke();
        yield return new WaitForSeconds(1f);
        eventCamera.DisableCamera();
        Destroy(disableInteract);
    }
}
