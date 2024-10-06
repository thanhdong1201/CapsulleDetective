using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private QuestGiver questGiver;
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
