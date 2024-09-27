using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject endGameUIObject;

    public void ShowEndGameUI()
    {
        endGameUIObject.SetActive(true);
        //SoundManager.PlaySound(SoundManager.SoundFX.PauseMenu);
    }
}
