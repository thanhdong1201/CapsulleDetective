using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseUIObject;

    public void ShowPauseUI()
    {
        pauseUIObject.SetActive(true);
        SoundManager.PlaySound(SoundManager.SoundFX.PauseMenu);
    }
    public void HidePauseUI()
    {
        pauseUIObject.SetActive(false);
        SoundManager.PlaySound(SoundManager.SoundFX.PauseMenu);
    }
}
