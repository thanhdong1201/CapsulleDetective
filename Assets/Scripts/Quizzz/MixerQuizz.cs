using UnityEngine;

public class MixerQuizz : MonoBehaviour
{
    [SerializeField] private GameObject interactZone;
    [SerializeField] private Animator animator;
    private bool isActive;

    public void Active()
    {
        animator.SetTrigger("Active");
        isActive = true;
        interactZone.SetActive(true);
    }
    public void InActive()
    {
        isActive = false;
        interactZone.SetActive(false);
    }
}
