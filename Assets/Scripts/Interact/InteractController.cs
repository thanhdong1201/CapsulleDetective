using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] private InputReaderSO input;

    public IInteract selectedIIteract;

    private void Start()
    {
        input.InteractEvent += InteractHandle;
    }
    private void InteractHandle()
    {
        if (selectedIIteract != null)
        {
            selectedIIteract.Interact();
            selectedIIteract = null;
        }
    }
    public void SetSelectedItemObject(IInteract interact)
    {
        selectedIIteract = interact;
    }
}
