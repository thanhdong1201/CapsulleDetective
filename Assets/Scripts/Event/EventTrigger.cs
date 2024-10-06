//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EventTrigger : MonoBehaviour
//{
//    private EventSetup eventSetup;
//    private bool alreadyActive;

//    private void Start()
//    {
//        eventSetup = GetComponent<EventSetup>();
//        alreadyActive = false;
//    }
//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player") && !alreadyActive)
//        {
//            alreadyActive = true;
//            //eventSetup.ActiveEvent();
//        }
//    }
//}
