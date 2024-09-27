using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomViewPort : MonoBehaviour
{
    [SerializeField] private GameObject roomViewPort;
    [SerializeField] private GameObject blackViewDoor;

    private bool alreadyUnlockView;

    private void Start()
    {
        alreadyUnlockView = false;
    }
    public void UnlockView()
    {
        if (!alreadyUnlockView && roomViewPort != null)
        {
            alreadyUnlockView = true;
            roomViewPort.SetActive(false);
            blackViewDoor.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnlockView();
        }
    }
}
