using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReaderSO input;
    public SettingsUI SettingsUI;
    public PauseUI PauseUI;
    public EndGameUI EndGameUI;
    private bool canAction;

    private void OnEnable()
    {
        input.PauseEvent += Pause;
        input.ResumeEvent += Resume;
        EndGame.OnEndGame += EnableEndGame;
    }
    private void OnDisable()
    {
        input.PauseEvent -= Pause;
        input.ResumeEvent -= Resume;
        EndGame.OnEndGame -= EnableEndGame;
    }
    private void Start()
    {
        canAction = true;
    }

    //Pause and Resume
    private void Pause()
    {
        if (!canAction || PauseUI == null) return;
        StartCoroutine("ResetAction");
        PauseUI.ShowPauseUI();
        input.SetUIInput();
        //Time.timeScale = 0f;
    }
    public void Resume()
    {
        if (!canAction || PauseUI == null) return;
        StartCoroutine("ResetAction");
        PauseUI.HidePauseUI();
        input.SetGamePlayInput();
        //Time.timeScale = 1f;
    }
    private IEnumerator ResetAction()
    {
        canAction = false;
        yield return new WaitForSeconds(0.5f);
        canAction = true;
    }
    //EndGame
    public void EnableEndGame()
    {
        if (EndGameUI == null) return;
        EndGameUI.ShowEndGameUI();
        input.DisableAllInput();
        //Time.timeScale = 0f;
    }
    //Menu
    public void TryAgain()
    {
        SceneManager.LoadScene("MainGame");
        input.SetGamePlayInput();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        input.DisableAllInput();
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
