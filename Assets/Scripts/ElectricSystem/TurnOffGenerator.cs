using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffGenerator : MonoBehaviour
{
    private EventSetup eventSetup;
    private void Start()
    {
        eventSetup = GetComponent<EventSetup>();
        eventSetup.OnActiveEvent += ActiveEvent;
    }
    private void ActiveEvent()
    {
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            light.enabled = false;
        }

        Robot[] robots = FindObjectsOfType<Robot>();
        foreach (Robot robot in robots)
        {
            robot.enabled = false;
        }
    }
}
