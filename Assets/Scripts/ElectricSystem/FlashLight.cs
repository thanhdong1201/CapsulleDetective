using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private GameObject lightSource;

    private void Start()
    {
        //TurnOff();
    }
    public void TurnOn()
    {
        lightSource.SetActive(true);
    }
    public void TurnOff()
    {
        lightSource.SetActive(false);
    }
}
