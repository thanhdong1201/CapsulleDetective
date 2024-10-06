using System;
using UnityEngine;

public class TurnOnGenerator : MonoBehaviour
{
    [SerializeField] private GameObject activeLed;
    [SerializeField] private GameObject canvas;
    private EventSetup eventSetup;
    private void Start()
    {
        eventSetup = GetComponent<EventSetup>();
        eventSetup.OnPrepareEvent += PrepareEvent;
        eventSetup.OnActiveEvent += ActiveEvent;
        activeLed.SetActive(false);
    }
    private void PrepareEvent()
    {
        activeLed.SetActive(true);
        canvas.SetActive(false);
    }
    private void ActiveEvent()
    {
        canvas.SetActive(true);
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            light.enabled = true;
        }

    }
}
