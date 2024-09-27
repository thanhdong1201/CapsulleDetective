using UnityEngine;

public class InteractBase : MonoBehaviour, IInteract
{
    protected InteractController interactController;
    protected InteractVisual interactVisual;

    public virtual void Interact()
    {
        interactVisual.Hide();
        interactController = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InteractController interactController))
        {
            this.interactController = interactController;
            this.interactController.SetSelectedItemObject(this);
            interactVisual.Show();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (interactController != null)
        {
            interactController.SetSelectedItemObject(null);
            interactVisual.Hide();
        }
    }
}
