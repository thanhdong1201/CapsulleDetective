using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject settingUIObject;
    public void ShowSettingUI()
    {
        settingUIObject.SetActive(true);
        //SoundManager.PlaySound(SoundManager.SoundFX.PauseMenu);
    }
    public void HideSettingUI()
    {
        settingUIObject.SetActive(false);
        //SoundManager.PlaySound(SoundManager.SoundFX.PauseMenu);
    }
}
