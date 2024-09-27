//using System;
//using UnityEngine;

//public class InteractController : MonoBehaviour
//{
//    [SerializeField] private InputReaderSO input;
//    [SerializeField] private Transform raycastStartPoint;
//    [SerializeField] private float interactDistance = 2f;
//    [SerializeField] private LayerMask interactLayerMask;

//    private IInteract selectedInteract;
//    private IInteract lastSelectedInteract;

//    private void Start()
//    {
//        input.InteractEvent += InteractHandle;
//    }
//    private void InteractHandle()
//    {
//        if (selectedInteract != null)
//        {
//            selectedInteract.Interact();     
//            lastSelectedInteract = null;
//        }
//    }
//    private void Update()
//    {
//        Interactions();
//    }
//    private void Interactions()
//    {
//        if (Physics.Raycast(raycastStartPoint.position, raycastStartPoint.forward, out RaycastHit raycastHit, interactDistance, interactLayerMask))
//        {
//            if (raycastHit.transform.TryGetComponent(out IInteract interact))
//            {
//                if (interact != selectedInteract)
//                {
//                    SetSelectedItem(interact);
//                    lastSelectedInteract = selectedInteract;
//                    SelectedVisual();
//                }
//            }
//            else
//            {
//                SetSelectedItem(null);
//                UnselectedVisual();
//            }
//        }
//        else
//        {
//            SetSelectedItem(null);
//            UnselectedVisual();
//        }
//    }
//    private void SetSelectedItem(IInteract interact)
//    {
//        selectedInteract = interact;
//    }
//    private void SelectedVisual()
//    {
//        if (lastSelectedInteract != null)
//        {
//            lastSelectedInteract.OnSelected();
//        }
//    }
//    private void UnselectedVisual()
//    {
//        if (lastSelectedInteract != null)
//        {
//            lastSelectedInteract.OnUnselected();
//        }
//    }
//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawRay(raycastStartPoint.position, raycastStartPoint.forward * interactDistance);
//    }
//}
