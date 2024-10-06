using UnityEngine;

public class MixerQuizz : MonoBehaviour
{
    [SerializeField] private GameObject interactZone;
    [SerializeField] private Animator animator;

    public void Active()
    {
        animator.SetTrigger("Active");
        interactZone.SetActive(true);
    }
    public void InActive()
    {
        interactZone.SetActive(false);
    }
}
