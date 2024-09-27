using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputReaderSO input;
    [SerializeField] private SettingsUI SettingsUI;

    private void Awake()
    {
        input.DisableAllInput();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        input.DisableAllInput();
        Time.timeScale = 1f;
    }
    public void Play()
    {
        SceneManager.LoadScene("Intro");
        input.SetGamePlayInput();
        Time.timeScale = 1f;
    }
    public void Setting()
    {
        SettingsUI.ShowSettingUI();
    }
    public void Exit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
