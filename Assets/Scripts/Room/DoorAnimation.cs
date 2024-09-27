using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private QuestGiver questGiver;
    private Animator animator;
    private RoomViewPort roomViewPort;

    private void Start()
    {
        animator = GetComponent<Animator>();
        roomViewPort = GetComponent<RoomViewPort>();

        questGiver.OnPlayAnimation += OpenDoor;
    }

    private void OpenDoor()
    {
        animator.SetTrigger("Open");
        roomViewPort.UnlockView();
    }     
}
